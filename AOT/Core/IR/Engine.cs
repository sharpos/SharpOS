//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	William Lahti <xfurious@gmail.com>
//	Bruce Markham <illuminus86@gmail.com>
//	Stanislaw Pitucha <viraptor@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Collections;
using System.Collections.Generic;
using Reflect = System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using SharpOS.AOT.IR;
using SharpOS.AOT.IR.Instructions;
using SharpOS.AOT.IR.Operands;
using AOTAttr = SharpOS.AOT.Attributes;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;

namespace SharpOS.AOT.IR {

	/// <summary>
	/// The core class of the AOT compiler. To embed the AOT compiler,
	/// an instance of this class should be constructed with an
	/// <see cref="SharpOS.AOT.IR.EngineOptions" /> instance.
	/// </summary>
	public partial class Engine : IEnumerable<Class> {
		/// <summary>
		/// Initializes a new instance of the <see cref="Engine"/> class.
		/// </summary>
		/// <param name="opts">
		/// Specifies the set of options used to perform the AOT
		/// operation.
		/// </param>
		public Engine (EngineOptions opts)
		{
			options = opts;
		}

		/// <summary>
		/// Represents the current stage of the compilation process.
		/// </summary>
		public enum Status : int {
			/// <summary>
			/// The stage is unknown.
			/// </summary>
			None = 0,

			/// <summary>
			/// Loads assemblies using Mono.Cecil and collects all types
			/// in a list.
			/// </summary>
			AssemblyLoading,

			/// <summary>
			/// During this stage, the AOT chooses which architecture-dependent
			/// implementation will be compiled into the kernel binary.
			/// </summary>
			ADCLayerSelection,

			/// <summary>
			/// Generates the intermediate representation of IL, so that
			/// we can translate  it into Static-Single-Assign format,
			/// perform register allocation, and (optionally) optimizations.
			/// Register allocation is used to increase the speed of the
			/// code, as IL uses a 'stack' for almost all it's operations.
			/// Instead of using the native (X86) stack exclusively, we use
			/// register allocation to improve efficiency. On X86, the
			/// ebx, esi, and edi registers are used for register allocation,
			/// and thus cannot be used directly.
			/// </summary>
			IRGeneration,

			/// <summary>
			/// During this stage, the IR is actually converted to SSA form,
			/// register allocation is done, and any optimizations.
			/// </summary>
			IRProcessing,

			/// <summary>
			/// In this phase the intermediate representation of the code is
			/// converted to native assembly and encoded into the kernel binary.
			/// Multiboot information is inserted here, as well as the PE header
			/// and COFF table.
			/// </summary>
			Encoding,

			/// <summary>
			/// A successful operation.
			/// </summary>
			Success,

			/// <summary>
			/// A failed operation.
			/// </summary>
			Failure
		}

		/// <summary>
		/// Represents the version of the AOT compiler engine.
		/// </summary>
		public const string EngineVersion = "svn";

		/// <summary>
		/// A constant string used by the AOT to identify it's own attributes.
		/// </summary>
		public const string SHARPOS_ATTRIBUTES = "SharpOS.AOT.Attributes.";
		public const string SYSTEM_ARRAY = "System.Array";

		EngineOptions options = null;
		IAssembly asm = null;
		DumpProcessor dump = null;

		List<ADCLayer> adcLayers = new List<ADCLayer> ();
		List<string> adcInterfaces = new List<string> ();
		ADCLayer adcLayer = null;

		List<AssemblyDefinition> assemblies = new List<AssemblyDefinition> ();
		List<Class> classes = new List<Class> ();
		Dictionary<string, Class> classesDictionary = new Dictionary<string, Class> ();
		Dictionary<string, byte []> resources = null;

		Status status;
		string currentAssemblyFile;
		AssemblyDefinition currentAssembly;
		ModuleDefinition currentModule;
		TypeReference currentType;
		MethodReference currentMethod;

		/// <summary>
		/// The current engine status.
		/// </summary>
		public Status CurrentStatus
		{
			get
			{
				return this.status;
			}
		}

		public AssemblyDefinition [] Sources {
			get {
				return this.assemblies.ToArray ();
			}
		}

		/// <summary>
		/// A dictionary of resource filenames and their
		/// data. Holds the embedded resources of the
		/// assemblies being compiled.
		/// </summary>
		public Dictionary<string, byte []> Resources
		{
			get
			{
				return this.resources;
			}
		}

		/// <summary>
		/// Provides storage for information about the architecture-dependent
		/// code layers found during initial processing of the assemblies to
		/// be AOTed.
		/// </summary>
		public class ADCLayer {
			public ADCLayer (string cpu, string ns)
			{
				CPU = cpu;
				Namespace = ns;
			}

			public string CPU, Namespace;
		}

		/// <summary>
		/// Gets the architecture-dependent IAssembly backend which encodes
		/// the compiler's intermediate representation into architecture-native
		/// binary code.
		/// </summary>
		public IAssembly Assembly
		{
			get
			{
				return this.asm;
			}
		}

		/// <summary>
		/// Provides access to the <see cref="EngineOptions" />
		/// object used to configure this compiler engine instance.
		/// </summary>
		public EngineOptions Options
		{
			get
			{
				return this.options;
			}
		}

		/// <summary>
		/// Provides access to the dump processing object, which
		/// is used for advanced debugging output.
		/// </summary>
		public DumpProcessor Dump
		{
			get
			{
				return this.dump;
			}
		}

		/// <summary>
		/// Provides access to the ADC layer selected for use
		/// for this compiler invocation.
		/// </summary>
		public ADCLayer ADC
		{
			get
			{
				return this.adcLayer;
			}
		}

		/// <summary>
		/// Provides the filename of the assembly file currently
		/// being processed.
		/// </summary>
		public string ProcessingAssemblyFile
		{
			get
			{
				return this.currentAssemblyFile;
			}
		}

		Class methodBoundaryClass = null;

		/// <summary>
		/// Gets the method boundary class.
		/// </summary>
		/// <value>The method boundary class.</value>
		public Class MethodBoundaryClass
		{
			get
			{
				return this.methodBoundaryClass;
			}
		}

		Class exceptionHandlingClauseClass = null;

		/// <summary>
		/// Gets the exception handling clause.
		/// </summary>
		/// <value>The exception handling clause.</value>
		public Class ExceptionHandlingClauseClass
		{
			get
			{
				return this.exceptionHandlingClauseClass;
			}
		}

		Class vtableClass = null;

		/// <summary>
		/// Gets the Class object representing the kernel type
		/// used to store vtable information. In the SharpOS
		/// kernel this type is SharpOS.Korlib.Runtime.VTable.
		/// </summary>
		/// <value>The VTable class in the kernel.</value>
		public Class VTableClass
		{
			get
			{
				return vtableClass;
			}
		}

		Class itableClass = null;

		/// <summary>
		/// Gets the Class object representing the kernel type
		/// used to store itable information. In the SharpOS
		/// kernel this type is SharpOS.Korlib.Runtime.ITable.
		/// </summary>
		/// <value>The ITable class in the kernel.</value>
		public Class ITableClass
		{
			get
			{
				return itableClass;
			}
		}

		Class typeInfoClass = null;

		/// <summary>
		/// Gets the type info class.
		/// </summary>
		/// <value>The type info class.</value>
		public Class TypeInfoClass
		{
			get
			{
				return this.typeInfoClass;
			}
		}

		/// <summary>
		/// Gets the array class.
		/// </summary>
		/// <value>The array class.</value>
		public Class ArrayClass
		{
			get
			{
				if (!this.classesDictionary.ContainsKey (SYSTEM_ARRAY))
					throw new EngineException ("'System.Array' not found.");

				return this.classesDictionary [SYSTEM_ARRAY];
			}
		}

		Method allocObject = null;

		/// <summary>
		/// Gets the alloc object method.
		/// </summary>
		/// <value>The alloc object.</value>
		public Method AllocObject
		{
			get
			{
				return this.allocObject;
			}
		}

		Method allocArray = null;

		/// <summary>
		/// Gets the alloc SZArray method.
		/// </summary>
		/// <value>The alloc SZArray method.</value>
		public Method AllocArray
		{
			get
			{
				return this.allocArray;
			}
		}

		/// <summary>
		/// Changes the Status property of the Engine.
		/// </summary>
		internal void SetStatus (Status status)
		{
			this.status = status;
		}

		/// <summary>
		/// Gets the status information.
		/// </summary>
		/// <param name="assembly">The assembly.</param>
		/// <param name="module">The module.</param>
		/// <param name="type">The type.</param>
		/// <param name="method">The method.</param>
		public void GetStatusInformation (out AssemblyDefinition assembly, out ModuleDefinition module, out
						  TypeReference type, out MethodReference method)
		{
			assembly = this.currentAssembly;
			module = this.currentModule;
			type = this.currentType;
			method = this.currentMethod;
		}

		/// <summary>
		/// Clears the status information.
		/// </summary>
		public void ClearStatusInformation ()
		{
			this.currentAssembly = null;
			this.currentModule = null;
			this.currentType = null;
			this.currentMethod = null;
		}

		/// <summary>
		/// Sets the status information.
		/// </summary>
		/// <param name="assembly">The assembly.</param>
		/// <param name="module">The module.</param>
		/// <param name="type">The type.</param>
		/// <param name="method">The method.</param>
		public void SetStatusInformation (AssemblyDefinition assembly, ModuleDefinition module,
						  TypeReference type, MethodReference method)
		{
			this.currentAssembly = assembly;
			this.currentModule = module;
			this.currentType = type;
			this.currentMethod = method;
		}

		/// <summary>
		/// Retrieve a type definition for the specified type.
		/// </summary>
		public TypeDefinition GetTypeDefinition (string ns, string name)
		{
			if (ns == null)
				throw new ArgumentNullException ("ns");

			if (name == null)
				throw new ArgumentNullException ("name");

			foreach (AssemblyDefinition def in assemblies) {
				foreach (ModuleDefinition mod in def.Modules) {
					foreach (TypeDefinition type in mod.Types) {
						if (type.Namespace == ns && type.Name == name)
							return type;
					}
				}
			}

			return null;
		}

		/// <summary>
		/// Gets the size of System.Object.
		/// </summary>
		/// <value>The size of the get object.</value>
		public int ObjectSize
		{
			get
			{
				if (!this.classesDictionary.ContainsKey (Mono.Cecil.Constants.Object))
					throw new EngineException ("System.Object not found.");

				return this.classesDictionary [Mono.Cecil.Constants.Object].Size;
			}
		}

		/// <summary>
		/// Gets the size of the VTable.
		/// </summary>
		/// <value>The size of the get VTable.</value>
		public int VTableSize
		{
			get
			{
				return this.vtableClass.Size;
			}
		}

		/// <summary>
		/// Prints a console message if <paramref name="lvl" /> is less
		/// than or equal to the Verbosity option.
		/// </summary>
		public void Message (int lvl, string msg, params object [] prms)
		{
			if (options.Verbosity >= lvl)
				Console.WriteLine (msg, prms);
		}

		/// <summary>
		/// Modifies the method reference <paramref name="call" /> to
		/// refer to the equivalent ADC layer method.
		/// </summary>
		public Mono.Cecil.MethodReference FixupADCMethod (Mono.Cecil.MethodReference call)
		{
			// TODO: do real confirmation of the existence of a compatible method!
			string rootns = null;
			string nsseg = null;
			TypeDefinition adcStubType;
			MethodDefinition adcStub;
			TypeReference ntype;
			bool matched = false;

			foreach (string iface in adcInterfaces) {
				if (call.DeclaringType.Namespace.StartsWith (iface + ".")) {
					rootns = iface;
					break;
				}
			}

			if (rootns != null)
				nsseg = "." + call.DeclaringType.Namespace.Substring (rootns.Length + 1);
			else
				nsseg = "";

			ntype = new TypeReference (call.DeclaringType.Name,
						   adcLayer.Namespace + nsseg,
						   call.DeclaringType.Scope,
						   call.DeclaringType.IsValueType);
			adcStubType = GetTypeDefinition (ntype.Namespace, ntype.Name);

			if (adcStubType == null)
				throw new EngineException (string.Format (
					"No match found for the ADC stub class `{0}'",
					ntype.Name));

			// Find the equivalent ADC layer method
			foreach (MethodDefinition def in adcStubType.Methods) {
				if (def.ReturnType.ReturnType.FullName == call.ReturnType.ReturnType.FullName &&
				    def.Parameters.Count == call.Parameters.Count) {
					bool badParams = false;
					for (int x = 0; x < call.Parameters.Count; ++x) {
						if (call.Parameters [x].ParameterType.FullName !=
						    def.Parameters [x].ParameterType.FullName) {
							badParams = true;
							break;
						}
					}

					if (!badParams) {
						matched = true;
						adcStub = def;
						break;
					}
				}
			}

			if (!matched)
				throw new EngineException (string.Format (
					"ADC stub method `{0}' does not match any ADC methods from type `{1}'",
					call, adcStubType));

			Message (3, "Replacing ADC method: `{0}'",
				call.ToString ());
			Message (4, " -- scope: `{0}', ns-segment = `{1}', class = '{2}'",
				call.DeclaringType.Scope, nsseg, call.DeclaringType.Name);


			Mono.Cecil.MethodReference nn = new Mono.Cecil.MethodReference (
				call.Name, ntype, call.ReturnType.ReturnType, call.HasThis,
				call.ExplicitThis, call.CallingConvention);

			foreach (ParameterDefinition def in call.Parameters)
				nn.Parameters.Add (def);

			return nn;
		}

		/// <summary>
		/// Finds the MethodDefinition that matches the method reference
		/// <paramref name="call" />. This method searches through the list
		/// of assemblies provided by the 'Assemblies' option in
		/// <see cref="EngineOptions" />.
		/// </summary>
		public MethodDefinition GetCILDefinition (Mono.Cecil.MethodReference call)
		{
			// TODO: work on performance

			foreach (AssemblyDefinition assem in assemblies) {
				foreach (ModuleDefinition mod in assem.Modules) {
					foreach (TypeDefinition type in mod.Types) {
						if (type.FullName == call.DeclaringType.FullName) {
							foreach (MethodDefinition def in type.Methods) {
								bool badParams = false;

								if (def.Name != call.Name)
									continue;

								if (def.ReturnType.ReturnType != call.ReturnType.ReturnType)
									continue;

								if (def.Parameters.Count != call.Parameters.Count)
									continue;

								for (int x = 0; x < def.Parameters.Count; ++x) {
									ParameterDefinition callPrm, defPrm;

									callPrm = call.Parameters [x];
									defPrm = def.Parameters [x];

									if (callPrm.ParameterType.FullName !=
										defPrm.ParameterType.FullName) {
										badParams = true;
										break;
									}

									if (callPrm.Attributes != defPrm.Attributes) {
										badParams = true;
										break;
									}
								}

								if (badParams)
									continue;

								return def;
							}
						}
					}
				}
			}

			return null;
		}

		/// <summary>
		/// Creates the correct IAssembly object corresponding to
		/// the CPU architecture chosen by the 'CPU' option of
		/// <see cref="EngineOptions" />, then runs the AOT compiler
		/// engine using <see cref="Run(IAssembly)" />.
		/// </summary>
		public void Run ()
		{
			IAssembly asm = null;

			switch (options.CPU) {
			case "X86":
				asm = new SharpOS.AOT.X86.Assembly ();
				break;

			default:
				throw new EngineException (string.Format (
					"Error: processor type `{0}' not supported",
					options.CPU));
			}

			if (!options.ForceRecompile) {
				bool outOfDate = false;
				string aotCore = System.Reflection.Assembly.GetExecutingAssembly ().Location;

				if (File.Exists (options.OutputFilename)) {
					foreach (string assem in options.Assemblies) {
						if (File.GetLastWriteTime (assem) >= File.GetLastWriteTime (options.OutputFilename)) {
							outOfDate = true;
							break;
						}
					}
				}
				if (File.GetLastWriteTime (aotCore) >= File.GetLastWriteTime (options.OutputFilename))
					outOfDate = true;

				if (!outOfDate) {
					Console.WriteLine ("Kernel is up to date. (To force recompilation, use /force)");
					return;
				}
			}

			Message (1, "AOT compiling for processor `{0}'", options.CPU);
			Run (asm);
		}

		/// <summary>
		/// Loads the resources.
		/// </summary>
		/// <param name="def">The def.</param>
		public void LoadResources (AssemblyDefinition def)
		{
			// TODO: does this cover multi-module assemblies?

			Message (2, "Adding resources from {0}", def.Name.Name);

			foreach (EmbeddedResource res in def.MainModule.Resources) {
				Message (2, "- Added resource {0}/Resources/{1}",
					def.Name.Name, res.Name);

				resources [def.Name.Name + "/Resources/" + res.Name] =
					res.Data;
			}
		}

		/// <summary>
		/// Determines whether [has sharp OS attribute] [the specified call].
		/// </summary>
		/// <param name="call">The call.</param>
		/// <returns>
		/// 	<c>true</c> if [has sharp OS attribute] [the specified call]; otherwise, <c>false</c>.
		/// </returns>
		internal bool HasSharpOSAttribute (SharpOS.AOT.IR.Instructions.Call call)
		{
			if (!(call.Method.MethodDefinition is MethodDefinition)
					|| (call.Method.MethodDefinition as MethodDefinition).CustomAttributes.Count == 0)
				return false;

			foreach (CustomAttribute attribute in (call.Method.MethodDefinition as MethodDefinition).CustomAttributes) {
				if (!attribute.Constructor.DeclaringType.FullName.StartsWith (SHARPOS_ATTRIBUTES))
					continue;

				if (attribute.Constructor.DeclaringType.FullName == typeof (SharpOS.AOT.Attributes.LabelAttribute).ToString ())
					continue;

				// No need for internal propagation
				if (attribute.Constructor.DeclaringType.FullName ==
				      typeof (SharpOS.AOT.Attributes.PointerToObjectAttribute).ToString () ||
				    attribute.Constructor.DeclaringType.FullName ==
				      typeof (SharpOS.AOT.Attributes.ObjectToPointerAttribute).ToString ()) {
					call.IsSpecialCase = true;
					continue;
				}

				return true;
			}

			return false;
		}

		// TODO Refactor
		internal int GetFieldOffset (IR.Operands.FieldOperand field)
		{
			string objectName = Class.GetTypeFullName (field.Field.FieldDefinition.DeclaringType);
			string fieldName = field.Field.Name;

			if (this.classesDictionary.ContainsKey (objectName))
				return this.classesDictionary [objectName].GetFieldOffset (fieldName);

			throw new EngineException ("'" + field.Field.FieldDefinition.ToString () + "' has not been found.");
		}

		/*internal int GetSystemArrayLengthOffset ()
		{
			Class _class = this.ArrayClass;

			Field firstEntry = _class.GetFieldByName ("FirstEntry");

			int result = _class.GetFieldOffset ("FirstEntry");
			result += firstEntry.Type.GetFieldOffset ("Length");

			return result;
		}*/

		/// <summary>
		/// Gets the field.
		/// </summary>
		/// <param name="field">The field.</param>
		/// <returns></returns>
		public Field GetField (FieldReference field)
		{
			string typeFullName = Class.GetTypeFullName (field);

			if (this.classesDictionary.ContainsKey (typeFullName))
				return this.classesDictionary [typeFullName].GetFieldByName (field.Name);

			throw new EngineException (string.Format ("Field '{0}' not found.", field.ToString ()));
		}

		/// <summary>
		/// Gets the class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public Class GetClass (TypeReference type)
		{
			string typeFullName = Class.GetTypeFullName (type);

			if (this.classesDictionary.ContainsKey (typeFullName))
				return this.classesDictionary [typeFullName];

			else if (type is TypeSpecification)
				return AddSpecialType (type);

			throw new EngineException (string.Format ("Class '{0}' not found.", type.ToString ()));
		}


		/// <summary>
		/// Gets the name of the class by.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public Class GetClassByName (string value)
		{
			if (this.classesDictionary.ContainsKey (value))
				return this.classesDictionary [value];

			if (value.EndsWith ("[]")) {
				value = value.Substring (0, value.Length - 2);

				if (this.classesDictionary.ContainsKey (value)) {
					ArrayType arrayType = new ArrayType (this.classesDictionary [value].ClassDefinition);

					return this.AddSpecialType (arrayType);
				}
			}

			throw new EngineException (string.Format ("Class '{0}' not found.", value));
		}

		/// <summary>
		/// Adds the type of the special.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		private Class AddSpecialType (TypeReference type)
		{
			Class _class = new Class (this, type);

			this.classes.Add (_class);
			this.classesDictionary [_class.TypeFullName] = _class;

			_class.Setup ();

			return _class;
		}

		/// <summary>
		/// Gets the method.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <returns></returns>
		public Method GetMethod (MethodReference method)
		{
			Class _class = this.GetClass (method.DeclaringType);

			return _class.GetMethodByName (method);

			/*string typeFullName = Class.GetTypeFullName (method.DeclaringType);

			if (this.classesDictionary.ContainsKey (typeFullName))
				return this.classesDictionary [typeFullName].GetMethodByName (method);

			throw new EngineException (string.Format ("Method '{0}' not found.", method.ToString ()));*/
		}

		/// <summary>
		/// Runs the AOT compiler engine.
		/// </summary>
		/// <param name="asm">The IAssembly implementation used to translate the
		/// compiler's intermediate representation into
		/// architecture-native code and write that code to file.</param>
		/// <exception cref="ArgumentNullException">
		/// 	<paramref name="asm"/> is null.
		/// </exception>
		public void Run (IAssembly asm)
		{
			byte dumpType = 0;

			if (asm == null)
				throw new ArgumentNullException ("asm");

			// Decide the dump type and start the processor

			if (this.options.ConsoleDump)
				dumpType |= (byte) DumpType.Console;

			if (!this.options.TextDump)
				dumpType |= (byte) DumpType.XML;

			if (this.options.Dump)
				dumpType |= (byte) DumpType.File;

			dump = new DumpProcessor ((byte) dumpType, options.DumpFile);
			dump.Section (DumpSection.Root);

			this.asm = asm;
			this.resources = this.options.Resources;

			string aotCorePath = System.Reflection.MethodBase.GetCurrentMethod ().Module.Assembly.Location;
			bool found = false;

			foreach (string assemblyFile in options.Assemblies) {
				if (assemblyFile == aotCorePath) {
					found = true;
					break;
				}
			}

			if (!found) {
				List<string> array = new List<string> (options.Assemblies);
				array.Add (aotCorePath);
				options.Assemblies = array.ToArray ();
			}

			foreach (string assemblyFile in options.Assemblies) {
				bool skip = false;

				Message (1, "Loading assembly `{0}'", assemblyFile);

				SetStatus (Status.AssemblyLoading);
				this.currentAssemblyFile = assemblyFile;

				AssemblyDefinition library = AssemblyFactory.GetAssembly (assemblyFile);

				this.currentAssembly = library;

				LoadResources (library);

				AggregateADCLayers (library);

				assemblies.Add (library);

				GenerateIR (assemblyFile, skip, library);
			}

			PostIRProcessing ();

			if (adcLayer != null)
				Message (1, "Selected ADC layer `{0}' for compilation",
					adcLayer.Namespace);
			else
				Message (1, "No available ADC layer matches CPU type.");


			DumpStatistics ();

			ProcessIRMethods ();

			Message (1, "Encoding output for `{0}' to `{1}'...", options.CPU,
					options.OutputFilename);
			SetStatus (Status.Encoding);

			asm.Encode (this, options.OutputFilename);

			Dump.PopElement ();
			SetStatus (Status.Success);

			return;
		}

		private bool IncludeAOTCoreClass (TypeDefinition typeDefinition)
		{
			foreach (CustomAttribute customAttribute in typeDefinition.CustomAttributes) {
				if (customAttribute.Constructor.DeclaringType.FullName.Equals (typeof (SharpOS.AOT.Attributes.IncludeAttribute).ToString ()))
					return true;
			}

			return false;
		}

		/// <summary>
		/// Generates the IR.
		/// </summary>
		/// <param name="assemblyFile">The assembly file.</param>
		/// <param name="skip">if set to <c>true</c> [skip].</param>
		/// <param name="library">The library.</param>
		private void GenerateIR (string assemblyFile, bool skip, AssemblyDefinition library)
		{
			Dump.Element (library, assemblyFile);
			Message (1, "Generating IR for assembly types...");
			SetStatus (Status.IRGeneration);

			// We first add the data (Classes and Methods)
			foreach (TypeDefinition type in library.MainModule.Types) {
				bool includeAOTCoreClass = false;
				bool isAOTCore = library.MainModule.Name == System.Reflection.MethodBase.GetCurrentMethod ().Module.ToString ();

				if (isAOTCore && this.IncludeAOTCoreClass (type))
					isAOTCore = false;

				if (isAOTCore) {
					if (!this.asm.IsInstruction (type.FullName)
							&& !this.asm.IsRegister (type.FullName)
							&& !this.asm.IsMemoryAddress (type.FullName))
						continue;
				}

				// bool ignore = false;
				string ignoreReason = null;

				if (type.Name.Equals ("<Module>"))
					continue;

				foreach (ADCLayer layer in this.adcLayers) {
					if (layer == this.adcLayer)
						continue;

					if (type.Namespace.StartsWith (layer.Namespace)) {
						Message (2, "Ignoring unused ADC type `{0}' in layer `{1}'",
							 type.FullName, layer.CPU);

						// ignore = true;
						ignoreReason = "Unused ADC implementation";
						break;
					}
				}

				if (skip) {
					Dump.IgnoreMember (type.Name, ignoreReason);

					continue;
				}

				Dump.Element (type);

				Class _class = new Class (this, type);

				if (this.classesDictionary.ContainsKey (_class.TypeFullName))
					throw new NotImplementedEngineException ("Already contains " + _class.TypeFullName);

				this.classes.Add (_class);
				this.classesDictionary [_class.TypeFullName] = _class;

				// We don't need the constructors of the registers
				if (isAOTCore) {
					_class.IsInternal = true;

					if (!this.asm.IsMemoryAddress (type.FullName)
							&& !this.asm.IsInstruction (type.FullName))
						continue;
				}

				foreach (MethodDefinition entry in type.Constructors) {
					Method method = new Method (this, _class, entry);

					if (isAOTCore && this.asm.IsMemoryAddress (type.FullName))
						method.SkipProcessing = true;

					_class.Add (method);
				}

				// We don't need the methods of the registers or memory addresses
				if (isAOTCore && !this.asm.IsInstruction (type.FullName))
					continue;

				foreach (MethodDefinition entry in type.Methods) {
					if (entry.ImplAttributes != MethodImplAttributes.Managed) {
						Dump.IgnoreMember (entry.Name,
								"Method is unmanaged");

						continue;
					}

					Method method = new Method (this, _class, entry);

					_class.Add (method);
				}
			}

			Dump.PopElement ();
		}

		/// <summary>
		/// Posts the IR processing.
		/// </summary>
		private void PostIRProcessing ()
		{
			List<Class> temp = new List<Class> (this.classes);

			// Post processing
			foreach (Class _class in temp) {
				_class.Setup (0);

				foreach (CustomAttribute customAttribute in _class.ClassDefinition.CustomAttributes) {
					if (customAttribute.Constructor.DeclaringType.FullName ==
							typeof (SharpOS.AOT.Attributes.VTableAttribute).FullName) {

						if (this.vtableClass != null)
							throw new EngineException ("More than one class was tagged as VTable Class.");

						this.vtableClass = _class;

					} else if (customAttribute.Constructor.DeclaringType.FullName ==
							typeof (SharpOS.AOT.Attributes.TypeInfoAttribute).FullName) {

						if (this.typeInfoClass != null)
							throw new EngineException ("More than one class was tagged as TypeInfo Class.");

						this.typeInfoClass = _class;

					} else if (customAttribute.Constructor.DeclaringType.FullName ==
							typeof (SharpOS.AOT.Attributes.ITableAttribute).FullName) {

						if (this.itableClass != null)
							throw new EngineException ("More than one class was tagged as ITable Class.");

						this.itableClass = _class;

					} else if (customAttribute.Constructor.DeclaringType.FullName ==
							typeof (SharpOS.AOT.Attributes.MethodBoundaryAttribute).FullName) {

						if (this.methodBoundaryClass != null)
							throw new EngineException ("More than one class was tagged as MethodBoundary Class.");

						this.methodBoundaryClass = _class;

					} else if (customAttribute.Constructor.DeclaringType.FullName ==
							typeof (SharpOS.AOT.Attributes.ExceptionHandlingClauseAttribute).FullName) {

						if (this.exceptionHandlingClauseClass != null)
							throw new EngineException ("More than one class was tagged as ExceptionHandlingClause Class.");

						this.exceptionHandlingClauseClass = _class;
					}
				}

				// assigning interface method uids
				if (_class.IsInterface) {
					foreach (Method _method in _class.VirtualMethods) {
						_method.AssignInterfaceMethodNumber();
					}
				}
			}

			// 2nd Step Post Processing
			foreach (Class _class in temp)
				_class.Setup (1);

			if (this.vtableClass == null)
				throw new EngineException ("No VTable Class defined.");

			if (this.typeInfoClass == null)
				throw new EngineException ("No TypeInfo Class defined.");

			if (this.itableClass == null)
				throw new EngineException ("No ITable Class defined.");

			if (this.methodBoundaryClass == null)
				throw new EngineException ("No MethodBoundary Class defined.");

			if (this.exceptionHandlingClauseClass == null)
				throw new EngineException ("No ExceptionHandlingClause Class defined.");

			// This block of code needs the vtableClass to be set
			foreach (Class _class in this.classes) {
				foreach (Method _method in _class) {
					if (_method.IsAllocObject) {
						if (this.allocObject != null)
							throw new EngineException ("More than one method was tagged as AllocObject Method.");

						this.allocObject = _method;

					} else if (_method.IsAllocArray) {
						if (this.allocArray != null)
							throw new EngineException ("More than one method was tagged as AllocSZArray Method.");

						this.allocArray = _method;
					}
				}
			}

			if (this.allocObject == null)
				throw new EngineException ("No AllocObject Method defined.");

			if (this.allocArray == null)
				throw new EngineException ("No AllocSZArray Method defined.");
		}

		/// <summary>
		/// Aggregates the ADC layers.
		/// </summary>
		/// <param name="library">The library.</param>
		private void AggregateADCLayers (AssemblyDefinition library)
		{
			// In the AOT.Core itself there are no attributes
			if (library.MainModule.Name == System.Reflection.MethodBase.GetCurrentMethod ().Module.ToString ())
				return;

			// Check for ADCLayerAttribute
			Message (2, "Aggregating ADC layers...");
			SetStatus (Status.ADCLayerSelection);

			foreach (CustomAttribute ca in library.CustomAttributes) {

				if (ca.Constructor.DeclaringType.FullName ==
				    typeof (AOTAttr.ADCLayerAttribute).FullName) {
					if (ca.ConstructorParameters.Count != 2)
						throw new EngineException (string.Format (
							"[ADCLayer] in assembly `{0}': must have 2 parameters",
							library.Name));

					string adcCPU = ca.ConstructorParameters [0] as string;
					string adcNamespace = ca.ConstructorParameters [1] as string;

					if (adcCPU == null || adcNamespace == null)
						throw new EngineException (string.Format (
							"[ADCLayer] in assembly `{0}': both parameters must be strings",
							library.Name));

					// check for any conflicts with previously found layers
					foreach (ADCLayer layer in adcLayers) {
						if (layer.CPU == adcCPU)
							throw new EngineException (string.Format (
								"Multiple ADC layers claim processor type `{0}'",
								adcCPU));
					}

					ADCLayer newLayer = new ADCLayer (adcCPU, adcNamespace);

					if (options.CPU == adcCPU)
						adcLayer = newLayer;

					Message (2, "Assembly `{0}' implements ADC for CPU `{1}' in namespace `{2}'",
						 library.Name,
						 adcCPU,
						 adcNamespace);

					adcLayers.Add (newLayer);
				} else if (ca.Constructor.DeclaringType.FullName ==
					   typeof (AOTAttr.ADCInterfaceAttribute).FullName) {

					if (ca.ConstructorParameters.Count != 1)
						throw new EngineException (string.Format (
							"[ADCLayer] in assembly `{0}': must have 1 parameters",
							library.Name));

					string iface = ca.ConstructorParameters [0] as string;
					adcInterfaces.Add (iface);

					Message (2, "Assembly `{0}' contains an ADC interface in namespace `{1}'",
						library.Name,
						iface);
				}
			}
		}

		/// <summary>
		/// Processes the IR methods.
		/// </summary>
		private void ProcessIRMethods ()
		{
			Message (1, "Processing IR methods...");
			SetStatus (Status.IRProcessing);

			Method markedEntryPoint = null;
			Method mainEntryPoint = null;

			List<Class> _classes = new List<Class> (this.classes);

			foreach (Class _class in _classes) {
				if (_class.IsSpecialType)
					continue;

				List<string> defNames = new List<string> ();

				this.currentModule = _class.ClassDefinition.Module;
				this.currentType = _class.ClassDefinition as TypeDefinition;

				foreach (Method _method in _class) {
					if (_method.IsMarkedMain) {
						if (markedEntryPoint != null)
							throw new EngineException ("More than one Marked Entry Point found.");

						markedEntryPoint = _method;
					}

					if (_method.IsSimpleMain) {
						if (mainEntryPoint != null)
							throw new EngineException ("More than one Main Entry Point found.");

						mainEntryPoint = _method;
					}

					if (defNames.Contains (_method.MethodFullName))
						throw new EngineException ("Already compiled this method: " +
							_method.MethodFullName);
					defNames.Add (_method.MethodFullName);
					this.currentMethod = _method.MethodDefinition;

					if (this.options.DumpFilter.Length > 0
							&& _method.ToString ().IndexOf (this.options.DumpFilter) == -1) {

						// If a filter is defined then turn off the verbosity
						Dump.Enabled = false;

						_method.Process ();

						Dump.Enabled = true;
					} else
						_method.Process ();

					this.currentMethod = null;
				}

				this.currentModule = null;
				this.currentType = null;
			}

			if (markedEntryPoint != null)
				markedEntryPoint.EntryPoint = true;

			else if (mainEntryPoint != null)
				mainEntryPoint.EntryPoint = true;

			else
				throw new EngineException ("No entry point defined.");
		}

		/// <summary>
		/// Dumps the statistics.
		/// </summary>
		private void DumpStatistics ()
		{
			// Statistics
			int classes = 0;
			int methods = 0;
			int ilInstructions = 0;
			foreach (Class _class in this.classes) {
				classes++;

				foreach (Method _method in _class) {
					methods++;

					ilInstructions += _method.CILInstructionsCount;
				}
			}

			Message (1, "Classes: `{0}'", classes);
			Message (1, "Methods: `{0}'", methods);
			Message (1, "IL Instructions: `{0}'", ilInstructions);
		}

		/// <summary>
		/// Returns an enumerator that iterates through the <see cref="Class" />
		/// objects that this instance contains.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that
		/// can be used to iterate through the collection.
		/// </returns>
		IEnumerator<Class> IEnumerable<Class>.GetEnumerator ()
		{
			foreach (Class _class in this.classes)
				yield return _class;
		}

		/// <summary>
		/// Returns an enumerator that iterates through the <see cref="Class" />
		/// objects that this instance contains.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator"></see> object that
		/// can be used to iterate through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return ((IEnumerable<Class>) this).GetEnumerator ();
		}

		/// <summary>
		/// Gets the size of the type <paramref name="type" />.
		/// </summary>
		/// <param name="type">
		/// Either the C# name for the type (`int', `short', `bool')
		/// or a fully-qualified type name (`System.Int32',
		/// `System.Int16', `System.Boolean').
		/// </param>
		/// <returns>
		/// The size of the given type in bytes.
		/// </returns>
		public int GetTypeSize (string type)
		{
			return this.GetTypeSize (type, 0);
		}

		/// <summary>
		/// Gets the size of the field type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public int GetFieldSize (string type)
		{
			return this.GetTypeSize (type, 0);
		}

		/// <summary>
		/// Gets the size of the type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="align">if set to 0 there will be no alignment.</param>
		/// <returns></returns>
		public int GetTypeSize (object type, int align)
		{
			int result = -1;
			Operands.InternalType sizeType;

			if (type is InternalType)
				sizeType = (Operands.InternalType) type;

			else if (type is string)
				sizeType = GetInternalType (type as string);

			else
				throw new EngineException (string.Format ("'{0}' is not supported.", type.GetType ().ToString ()));

			switch (sizeType) {
			case InternalType.I1:
			case InternalType.U1:
				result = 1;
				break;

			case InternalType.I2:
			case InternalType.U2:
				result = 2;
				break;

			case InternalType.I4:
			case InternalType.U4:
				result = 4;
				break;

			case InternalType.I:
			case InternalType.U:
			case InternalType.O:
			case InternalType.M:
			case InternalType.SZArray:
			case InternalType.Array:
				result = this.asm.IntSize;
				break;

			case InternalType.I8:
			case InternalType.U8:
				result = 8;
				break;

			case InternalType.R4:
				result = 4;
				break;

			case InternalType.R8:
				result = 8;
				break;

			case InternalType.ValueType:
				if (this.classesDictionary.ContainsKey (type.ToString ()))
					result = this.classesDictionary [type.ToString ()].Size;

				break;
			}

			if (result == -1)
				throw new EngineException ("'" + type + "' not supported.");

			if (align != 0 && result % align != 0)
				result = ((result / align) + 1) * align;

			return result;
		}

		/// <summary>
		/// Gets a <see cref="Operands.InternalType" /> that
		/// represents the type <paramref name="type" />.
		/// </summary>
		/// <param name="type">
		/// Either the C# name for the type (`int', `short', `bool')
		/// or a fully-qualified type name (`System.Int32',
		/// `System.Int16', `System.Boolean').
		/// </param>
		/// <returns></returns>
		public Operands.InternalType GetInternalType (string type)
		{
			if (type.EndsWith ("*"))
				return Operands.InternalType.U;
			else if (type.EndsWith ("&"))
				return Operands.InternalType.U;
			else if (type.EndsWith ("][]"))
				return Operands.InternalType.Array;
			else if (type.EndsWith (",]"))
				return Operands.InternalType.Array;
			else if (type.EndsWith ("[]"))
				return Operands.InternalType.SZArray;

			else if (type.Equals ("System.IntPtr"))
				return Operands.InternalType.I;
			else if (type.Equals ("System.UIntPtr"))
				return Operands.InternalType.I;

			else if (type.Equals ("void"))
				return Operands.InternalType.NotSet;
			else if (type.Equals ("System.Void"))
				return Operands.InternalType.NotSet;

			else if (type.Equals ("System.Boolean"))
				return Operands.InternalType.U1;
			else if (type.Equals ("bool"))
				return Operands.InternalType.U1;

			else if (type.Equals ("System.Byte"))
				return Operands.InternalType.U1;
			else if (type.Equals ("System.SByte"))
				return Operands.InternalType.I1;

			else if (type.Equals ("char"))
				return Operands.InternalType.U2;
			else if (type.Equals ("System.Char"))
				return Operands.InternalType.U2;
			else if (type.Equals ("short"))
				return Operands.InternalType.I2;
			else if (type.Equals ("ushort"))
				return Operands.InternalType.U2;
			else if (type.Equals ("System.UInt16"))
				return Operands.InternalType.U2;
			else if (type.Equals ("System.Int16"))
				return Operands.InternalType.I2;

			else if (type.Equals ("int"))
				return Operands.InternalType.I4;
			else if (type.Equals ("uint"))
				return Operands.InternalType.U4;
			else if (type.Equals ("System.UInt32"))
				return Operands.InternalType.U4;
			else if (type.Equals ("System.Int32"))
				return Operands.InternalType.I4;

			else if (type.Equals ("long"))
				return Operands.InternalType.I8;
			else if (type.Equals ("ulong"))
				return Operands.InternalType.U8;
			else if (type.Equals ("System.UInt64"))
				return Operands.InternalType.U8;
			else if (type.Equals ("System.Int64"))
				return Operands.InternalType.I8;

			else if (type.Equals ("float"))
				return Operands.InternalType.R4;
			else if (type.Equals ("System.Single"))
				return Operands.InternalType.R4;

			else if (type.Equals ("double"))
				return Operands.InternalType.R8;
			else if (type.Equals ("System.Double"))
				return Operands.InternalType.R8;

			else if (type.Equals ("string"))
				return Operands.InternalType.O;
			else if (type.Equals ("System.String"))
				return Operands.InternalType.O;

			else if (type.Equals ("object"))
				return Operands.InternalType.O;
			else if (type.Equals ("System.Object"))
				return Operands.InternalType.O;

			else if (type.Equals ("System.TypedReference"))
				return Operands.InternalType.TypedReference;

			else if (this.Assembly != null && this.Assembly.IsRegister (type))
				return this.Assembly.GetRegisterSizeType (type);

			if (type.IndexOf ("::") != -1) {
				string objectName = type.Substring (0, type.IndexOf ("::"));
				string fieldName = type.Substring (type.IndexOf ("::") + 2);

				if (this.classesDictionary.ContainsKey (objectName))
					return this.classesDictionary [objectName].GetFieldType (fieldName);

			} else if (this.classesDictionary.ContainsKey (type.ToString ()))
				return this.classesDictionary [type.ToString ()].InternalType;

			Console.Error.WriteLine ("WARNING: '" + type + "' not supported.");

			return InternalType.NotSet;
		}
	}
}




