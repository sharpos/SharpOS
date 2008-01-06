// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	William Lahti <xfurious@gmail.com>
//	Bruce Markham <illuminus86@gmail.com>
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

		public enum Status : int {
			None = 0,
			AssemblyLoading,
			ADCLayerSelection,
			IRGeneration,
			IRProcessing,
			Encoding,
			Success,
			Failure
		}

		/// <summary>
		/// Represents the version of the AOT compiler engine.
		/// </summary>
		public const string EngineVersion = "svn";
		public const string SHARPOS_ATTRIBUTES = "SharpOS.AOT.Attributes.";

		EngineOptions options = null;
		IAssembly asm = null;
		DumpProcessor dump = null;

		List<ADCLayer> adcLayers = new List<ADCLayer> ();
		List<string> adcInterfaces = new List<string> ();
		ADCLayer adcLayer = null;

		List<AssemblyDefinition> assemblies = new List<AssemblyDefinition> ();
		Dictionary<string, Class> classes = new Dictionary<string, Class> ();
		Dictionary<string, byte []> resources = null;

		Status status;
		string currentAssemblyFile;
		AssemblyDefinition currentAssembly;
		ModuleDefinition currentModule;
		TypeDefinition currentType;
		MethodDefinition currentMethod;

		public Status CurrentStatus
		{
			get
			{
				return this.status;
			}
		}

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

		public string ProcessingAssemblyFile
		{
			get
			{
				return this.currentAssemblyFile;
			}
		}

		Class vtableClass = null;

		/// <summary>
		/// Gets the VTable class.
		/// </summary>
		/// <value>The V table class.</value>
		public Class VTableClass
		{
			get
			{
				return vtableClass;
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
						  TypeDefinition type, out MethodDefinition method)
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
						  TypeDefinition type, MethodDefinition method)
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
			DumpType dumpType = DumpType.XML;

			IAssembly asm = null;

			switch (options.CPU) {
			case "X86":
				asm = new SharpOS.AOT.X86.Assembly ();
				break;

			default:
				throw new EngineException (string.Format (
					"Error: processor type `{0}' not supported",
					options.CPU));
				break;
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

				return true;
			}

			return false;
		}

		/// <summary>
		/// Gets the size of the base type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		internal int GetBaseTypeSize (TypeDefinition type)
		{
			int result = 0;

			if (type != null) {
				result = this.GetBaseTypeSize (type.BaseType as TypeDefinition);

				foreach (FieldReference field in type.Fields) {
					if ((field as FieldDefinition).IsStatic)
						continue;

					result += this.GetFieldSize (field.FieldType.FullName);
				}
			}

			return result;
		}

		// TODO refactor this one
		internal int GetFieldOffset (IR.Operands.FieldOperand field)
		{
			string objectName = Class.GetTypeFullName (field.Field.Type.DeclaringType);
			string fieldName = field.Field.Type.Name;

			if (this.classes.ContainsKey (objectName))
				return this.classes [objectName].GetFieldOffset (fieldName);

			throw new EngineException ("'" + field.Field.Type.ToString () + "' has not been found.");
		}

		/// <summary>
		/// Gets the field.
		/// </summary>
		/// <param name="field">The field.</param>
		/// <returns></returns>
		public Field GetField (FieldReference field)
		{
			string typeFullName = Class.GetTypeFullName (field);

			if (this.classes.ContainsKey (typeFullName))
				return this.classes [typeFullName].GetFieldByName (field.Name);

			throw new EngineException (string.Format ("Field '{0}' not found.", field.ToString ()));
		}

		/// <summary>
		/// Gets the method.
		/// </summary>
		/// <param name="method">The method.</param>
		/// <returns></returns>
		public Method GetMethod (MethodReference method)
		{
			string typeFullName = Class.GetTypeFullName (method.DeclaringType);
			string methodName = Method.GetLabel (method);

			if (this.classes.ContainsKey (typeFullName))
				return this.classes [typeFullName].GetMethodByName (methodName);

			throw new EngineException (string.Format ("Method '{0}' not found.", method.ToString ()));
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

			bool isAOTCore = library.MainModule.Name == System.Reflection.MethodBase.GetCurrentMethod ().Module.ToString ();

			// We first add the data (Classes and Methods)
			foreach (TypeDefinition type in library.MainModule.Types) {
				if (isAOTCore) {
					if (!this.asm.IsInstruction (type.FullName)
							&& !this.asm.IsRegister(type.FullName)
							&& !this.asm.IsMemoryAddress (type.FullName))
						continue;
				}

				bool ignore = false;
				string ignoreReason = null;

				if (type.Name.Equals ("<Module>"))
					continue;

				foreach (ADCLayer layer in this.adcLayers) {
					if (layer == this.adcLayer)
						continue;

					if (type.Namespace.StartsWith (layer.Namespace)) {
						Message (2, "Ignoring unused ADC type `{0}' in layer `{1}'",
							 type.FullName, layer.CPU);

						ignore = true;
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

				if (this.classes.ContainsKey (_class.TypeFullName))
					throw new NotImplementedEngineException ();
				
				this.classes [_class.TypeFullName] = _class;

				// We don't need the constructors of the registers
				if (isAOTCore && !this.asm.IsMemoryAddress (type.FullName)
						&& !this.asm.IsInstruction (type.FullName))
					continue;

				

				foreach (MethodDefinition entry in type.Constructors) {
					Method method = new Method (this, entry);

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

					Method method = new Method (this, entry);

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
			// Post processing
			foreach (Class _class in this.classes.Values) {
				_class.Setup ();

				foreach (CustomAttribute customAttribute in _class.ClassDefinition.CustomAttributes) {
					if (customAttribute.Constructor.DeclaringType.FullName !=
							typeof (SharpOS.AOT.Attributes.VTableAttribute).FullName)
						continue;

					if (this.vtableClass != null)
						throw new EngineException ("More than one class was tagged as VTable Class.");

					this.vtableClass = _class;
				}

			}

			if (this.vtableClass == null)
				throw new EngineException ("No VTable Class defined.");

			// This block of code needs the vtableClass to be set
			foreach (Class _class in this.classes.Values) {
				foreach (Method _method in _class) {
					if (!_method.IsAllocObject)
						continue;

					if (this.allocObject != null)
						throw new EngineException ("More than one method was tagged as AllocObject Method.");

					this.allocObject = _method;
				}
			}

			if (this.allocObject == null)
				throw new EngineException ("No AllocObject Method defined.");
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

			foreach (Class _class in this.classes.Values) {
				List<string> defNames = new List<string> ();

				this.currentModule = _class.ClassDefinition.Module;
				this.currentType = _class.ClassDefinition;

				foreach (Method _method in _class) {
					foreach (CustomAttribute attribute in _method.MethodDefinition.CustomAttributes) {
						if (!attribute.Constructor.DeclaringType.FullName.Equals (typeof (SharpOS.AOT.Attributes.KernelMainAttribute).ToString ()))
							continue;

						if (markedEntryPoint != null)
							throw new EngineException ("More than one Marked Entry Point found.");

						markedEntryPoint = _method;
					}

					if (_method.MethodDefinition.Name.Equals ("Main")
							&& _method.MethodDefinition.Parameters.Count == 0
							&& (_method.MethodDefinition.ReturnType.ReturnType.FullName.Equals (Mono.Cecil.Constants.Int32)
								|| _method.MethodDefinition.ReturnType.ReturnType.FullName.Equals (Mono.Cecil.Constants.Void))) {

						if (mainEntryPoint != null)
							throw new EngineException ("More than one Main Entry Point found.");

						mainEntryPoint = _method;
					}

					if (defNames.Contains (_method.MethodDefinition.ToString ()))
						throw new EngineException ("Already compiled this method: " +
							_method.MethodDefinition.ToString ());
					defNames.Add (_method.MethodDefinition.ToString ());
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
			foreach (Class _class in this.classes.Values) {
				classes++;

				foreach (Method _method in _class) {
					methods++;

					if (_method.MethodDefinition.Body != null)
						ilInstructions += _method.MethodDefinition.Body.Instructions.Count;
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
			foreach (Class _class in this.classes.Values)
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
			return this.GetTypeSize (type, 0); //2);
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
					if (this.classes.ContainsKey (type.ToString ()))
						result = this.classes [type.ToString ()].Size;

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
			else if (type.EndsWith ("[]"))
				return Operands.InternalType.U;

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
				
				if (this.classes.ContainsKey (objectName))
					return this.classes [objectName].GetFieldType (fieldName);

			} else if (this.classes.ContainsKey (type.ToString ()))
				return this.classes [type.ToString ()].InternalType;

			Console.Error.WriteLine ("WARNING: '" + type + "' not supported.");

			return InternalType.NotSet;
		}
	}
}

