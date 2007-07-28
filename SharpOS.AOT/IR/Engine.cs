// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	William Lahti <xfurious@gmail.com>
//	Bruce Markham <illuminus86@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
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
using SharpOS.AOT.IR.Operators;
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

		EngineOptions options = null;
		IAssembly asm = null;
		DumpProcessor dump = null;
		
		List<ADCLayer> adcLayers = new List<ADCLayer>();
		List<string> adcInterfaces = new List<string>();
		ADCLayer adcLayer = null;
		
		List<AssemblyDefinition> assemblies = new List<AssemblyDefinition>();
		List<Class> classes = new List<Class> ();
		Dictionary<string,byte[]> resources = null;
		
		Status status;
		string currentAssemblyFile;
		AssemblyDefinition currentAssembly;
		ModuleDefinition currentModule;
		TypeDefinition currentType;
		MethodDefinition currentMethod;

		public Status CurrentStatus {
			get {
				return this.status;
			}
		}
		
		public Dictionary<string,byte[]> Resources {
			get {
				return this.resources;
			}
		}
		
		/// <summary>
		/// Provides storage for information about the architecture-dependent 
		/// code layers found during initial processing of the assemblies to
		/// be AOTed.
		/// </summary>
		public class ADCLayer {
			public ADCLayer(string cpu, string ns)
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
		public IAssembly Assembly {
			get {
				return this.asm;
			}
		}
		
		/// <summary>
		/// Provides access to the <see cref="EngineOptions" />
		/// object used to configure this compiler engine instance.
		/// </summary>
		public EngineOptions Options {
			get {
				return this.options;
			}
		}
		
		/// <summary>
		/// Provides access to the dump processing object, which
		/// is used for advanced debugging output.
		/// </summary>
		public DumpProcessor Dump {
			get {
				return this.dump;
			}
		}

		/// <summary>
		/// Provides access to the ADC layer selected for use
		/// for this compiler invocation.
		/// </summary>
		public ADCLayer ADC {
			get {
				return this.adcLayer;
			}
		}

		public string ProcessingAssemblyFile {
			get {
				return this.currentAssemblyFile;
			}
		}
		
		/// <summary>
		/// Changes the Status property of the Engine.
		/// </summary>
		internal void SetStatus (Status status)
		{
			this.status = status;
		}

		public void GetStatusInformation (out AssemblyDefinition assembly, out ModuleDefinition module, out
						  TypeDefinition type, out MethodDefinition method)
		{
			assembly = this.currentAssembly;
			module = this.currentModule;
			type = this.currentType;
			method = this.currentMethod;
		}
		
		public void ClearStatusInformation ()
		{
			this.currentAssembly = null;
			this.currentModule = null;
			this.currentType = null;
			this.currentMethod = null;
		}
		
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
				call.ToString());
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
			
			foreach (string assemblyFile in options.Assemblies) {
				bool skip = false;

				Message (1, "Loading assembly `{0}'", assemblyFile);
				
				SetStatus (Status.AssemblyLoading);
				this.currentAssemblyFile = assemblyFile;

				AssemblyDefinition library = AssemblyFactory.GetAssembly (assemblyFile);

				this.currentAssembly = library;

				LoadResources (library);
				
				// Check for ADCLayerAttribute

				Message (2, "Aggregating ADC layers...");
				SetStatus (Status.ADCLayerSelection);

				foreach (CustomAttribute ca in library.CustomAttributes) {

					if (ca.Constructor.DeclaringType.FullName ==
					    typeof(AOTAttr.ADCLayerAttribute).FullName) {
						if (ca.ConstructorParameters.Count != 2)
					    		throw new EngineException (string.Format (
					    			"[ADCLayer] in assembly `{0}': must have 2 parameters",
					    			library.Name));

						string adcCPU = ca.ConstructorParameters[0] as string;
						string adcNamespace = ca.ConstructorParameters[1] as string;

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
						   typeof(AOTAttr.ADCInterfaceAttribute).FullName) {
					
						if (ca.ConstructorParameters.Count != 1)
					    		throw new EngineException (string.Format (
					    			"[ADCLayer] in assembly `{0}': must have 1 parameters",
					    			library.Name));

					    	string iface = ca.ConstructorParameters[0] as string;
						adcInterfaces.Add(iface);

						Message (2, "Assembly `{0}' contains an ADC interface in namespace `{1}'",
							library.Name,
							iface);
					}
				}

				assemblies.Add (library);

				Dump.Element (library, assemblyFile);
				Message (1, "Generating IR for assembly types...");
				SetStatus (Status.IRGeneration);

				// We first add the data (Classes and Methods)
				foreach (TypeDefinition type in library.MainModule.Types) {
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

					this.classes.Add (_class);

					foreach (MethodDefinition entry in type.Constructors) {
						Method method = new Method (this, entry);

						_class.Add (method);

						break;
					}

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

			if (adcLayer != null)
				Message (1, "Selected ADC layer `{0}' for compilation",
					adcLayer.Namespace);
			else
				Message (1, "No available ADC layer matches CPU type.");

			Message (1, "Processing IR methods...");
			SetStatus (Status.IRProcessing);
			
			foreach (Class _class in this.classes) {
				List <string> defNames = new List <string> ();
				
				this.currentModule = _class.ClassDefinition.Module;
				this.currentType = _class.ClassDefinition;
				
				foreach (Method _method in _class) {
					if (defNames.Contains (_method.MethodDefinition.ToString ()))
						throw new Exception ("Already compiled this method: " + 
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

			Message (1, "Encoding output for `{0}' to `{1}'...", options.CPU,
					options.OutputFilename);
			SetStatus (Status.Encoding);

			asm.Encode (this, options.OutputFilename);

			Dump.PopElement ();
			SetStatus (Status.Success);
			
			return;
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
			return this.GetTypeSize (type, 0); //2);
		}

		/// <summary>
		/// Gets the size of the object.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		/*public int GetObjectSize (string type)
		{
			return this.GetTypeSize (type, 4);
		}*/

		/// <summary>
		/// Gets the size of the type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="align">if set to 0 there will be no alignment.</param>
		/// <returns></returns>
		public int GetTypeSize (string type, int align)
		{
			int result = 0;
			Operands.Operand.InternalSizeType sizeType = GetInternalType (type);

			switch (sizeType) {
				case Operand.InternalSizeType.I1:
				case Operand.InternalSizeType.U1:
					result = 1;
					break;

				case Operand.InternalSizeType.I2:
				case Operand.InternalSizeType.U2:
					result = 2;
					break;

				case Operand.InternalSizeType.I4:
				case Operand.InternalSizeType.U4:
					result = 4;
					break;

				case Operand.InternalSizeType.I:
				case Operand.InternalSizeType.U:
					result = this.asm.IntSize;
					break;

				case Operand.InternalSizeType.I8:
				case Operand.InternalSizeType.U8:
					result = 8;
					break;

				case Operand.InternalSizeType.R4:
					result = 4;
					break;

				case Operand.InternalSizeType.R8:
					result = 8;
					break;

				case Operand.InternalSizeType.ValueType:
					foreach (Class _class in this.classes) {
						if (_class.ClassDefinition.FullName.Equals (type)) {
							if (_class.ClassDefinition.IsEnum) {
								foreach (FieldDefinition field in _class.ClassDefinition.Fields) {
									if ((field.Attributes & FieldAttributes.RTSpecialName) != 0) {
										result = this.GetTypeSize (field.FieldType.FullName);
										break;
									}
								}

							} else if (_class.ClassDefinition.IsValueType) {
								if ((_class.ClassDefinition.Attributes & TypeAttributes.ExplicitLayout) != 0) {
									foreach (FieldDefinition field in _class.ClassDefinition.Fields) {
										if ((field as FieldDefinition).IsStatic)
											continue;

										int value = (int) (field.Offset + this.GetTypeSize (field.FieldType.FullName));

										if (value > result)
											result = value;
									}

								} else {
									foreach (FieldReference field in _class.ClassDefinition.Fields) {
										if ((field as FieldDefinition).IsStatic)
											continue;

										result += this.GetFieldSize (field.FieldType.FullName);
									}
								}

							} else
								break;
						}
					}

					break;
			}

			if (result == 0)
				throw new Exception ("'" + type + "' not supported.");

			if (align != 0 && result % align != 0)
				result = ((result / align) + 1) * align;

			return result;
		}

		/// <summary>
		/// Gets a <see cref="Operands.Operand.InternalSizeType" /> that 
		/// represents the type <paramref name="type" />.
		/// </summary>
		/// <param name="type">
		/// Either the C# name for the type (`int', `short', `bool') 
		/// or a fully-qualified type name (`System.Int32', 
		/// `System.Int16', `System.Boolean').
		/// </param>
		/// <returns></returns>
		public Operands.Operand.InternalSizeType GetInternalType (string type)
		{
			if (type.EndsWith ("*"))
				return Operands.Operand.InternalSizeType.U;
			else if (type.EndsWith ("[]"))
				return Operands.Operand.InternalSizeType.U;

			else if (type.Equals ("void"))
				return Operands.Operand.InternalSizeType.NotSet;
			else if (type.Equals ("System.Void"))
				return Operands.Operand.InternalSizeType.NotSet;
			
			else if (type.Equals ("System.Boolean"))
				return Operands.Operand.InternalSizeType.U1;
			else if (type.Equals ("bool"))
				return Operands.Operand.InternalSizeType.U1;

			else if (type.Equals ("System.Byte"))
				return Operands.Operand.InternalSizeType.U1;
			else if (type.Equals ("System.SByte"))
				return Operands.Operand.InternalSizeType.I1;

			else if (type.Equals ("char"))
				return Operands.Operand.InternalSizeType.U2;
			else if (type.Equals ("short"))
				return Operands.Operand.InternalSizeType.I2;
			else if (type.Equals ("ushort"))
				return Operands.Operand.InternalSizeType.U2;
			else if (type.Equals ("System.UInt16"))
				return Operands.Operand.InternalSizeType.U2;
			else if (type.Equals ("System.Int16"))
				return Operands.Operand.InternalSizeType.I2;

			else if (type.Equals ("int"))
				return Operands.Operand.InternalSizeType.I4;
			else if (type.Equals ("uint"))
				return Operands.Operand.InternalSizeType.U4;
			else if (type.Equals ("System.UInt32"))
				return Operands.Operand.InternalSizeType.U4;
			else if (type.Equals ("System.Int32"))
				return Operands.Operand.InternalSizeType.I4;

			else if (type.Equals ("long"))
				return Operands.Operand.InternalSizeType.I8;
			else if (type.Equals ("ulong"))
				return Operands.Operand.InternalSizeType.U8;
			else if (type.Equals ("System.UInt64"))
				return Operands.Operand.InternalSizeType.U8;
			else if (type.Equals ("System.Int64"))
				return Operands.Operand.InternalSizeType.I8;

			else if (type.Equals ("float"))
				return Operands.Operand.InternalSizeType.R4;
			else if (type.Equals ("System.Single"))
				return Operands.Operand.InternalSizeType.R4;

			else if (type.Equals ("double"))
				return Operands.Operand.InternalSizeType.R8;
			else if (type.Equals ("System.Double"))
				return Operands.Operand.InternalSizeType.R8;

			else if (type.Equals ("string"))
				return Operands.Operand.InternalSizeType.U;
			else if (type.Equals ("System.String"))
				return Operands.Operand.InternalSizeType.U;
			else if (this.Assembly != null && this.Assembly.IsRegister (type))
				return this.Assembly.GetRegisterSizeType (type);

			if (type.IndexOf ("::") != -1) {
				string objectName = type.Substring (0, type.IndexOf ("::"));
				string fieldName = type.Substring (type.IndexOf ("::") + 2);

				foreach (Class _class in this.classes) {
					if (_class.ClassDefinition.FullName.Equals (objectName)) {
						foreach (FieldDefinition field in _class.ClassDefinition.Fields) {
							if (field.Name.Equals (fieldName))
								return this.GetInternalType (field.FieldType.FullName);
						}
					}
				}

			} else {
				foreach (Class _class in this.classes) {
					if (_class.ClassDefinition.FullName.Equals (type)) {
						if (_class.ClassDefinition.IsEnum) {
							foreach (FieldDefinition field in _class.ClassDefinition.Fields) {
								if ((field.Attributes & FieldAttributes.RTSpecialName) != 0)
									return this.GetInternalType (field.FieldType.FullName);
							}

						} else
							return Operands.Operand.InternalSizeType.ValueType;
					}
				}
			}

			Console.Error.WriteLine ("WARNING: '" + type + "' not supported.");
			
			return Operand.InternalSizeType.NotSet;
		}
	}
}

