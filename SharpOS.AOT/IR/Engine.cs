// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using SharpOS.AOT.IR;
using SharpOS.AOT.IR.Instructions;
using SharpOS.AOT.IR.Operands;
using SharpOS.AOT.IR.Operators;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;

namespace SharpOS.AOT.IR {
	/// <summary>
	/// An instance of this class must be passed to the 
	/// <see cref="SharpOS.AOT.IR.Engine" /> constructor.
	/// The contained values defined the options used during
	/// AOT compilation.
	/// </summary>
	public class EngineOptions {
		public string[] Assemblies = null;
		public string OutputFilename = "SharpOS.Kernel.bin";
		public string CPU = "X86";
		public string DumpFile = null;
		public bool TextDump = false;
		public int DumpVerbosity = 1;
		public int Verbosity = 0;
		
		public bool Dump {
			get {
				return DumpFile != null;
			}
		}
	}
	
	/// <summary>
	/// Defines an exception thrown by <see cref="SharpOS.AOT.IR.Engine" />
	/// and related classes when an unrecoverable error occurs while performing
	/// the AOT operation. This exception should be caught by the program
	/// embedding the AOT engine and displayed to the user. This exception is
	/// not thrown when an internal AOT error occurs.
	/// </summary>
	public class EngineException : Exception {
		public EngineException(string message):
			base(message)
		{
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
		/// Represents the version of the AOT compiler engine.
		/// </summary>
		public const string EngineVersion = "svn";
		
		private EngineOptions options = null;
		private IAssembly asm = null;
		private DumpProcessor dump = null;
		
		private List<ADCLayer> adcLayers = new List<ADCLayer>();
		private ADCLayer adcLayer = null;
		
		private List<AssemblyDefinition> assemblies = new List<AssemblyDefinition>();
		private List<Class> classes = new List<Class> ();
		
		/// <summary>
		/// Gets the assembly.
		/// </summary>
		/// <value>The assembly.</value>
		public IAssembly Assembly {
			get {
				return asm;
			}
		}
		
		/// <summary>
		/// Provides access to the <see cref="EngineOptions" />
		/// object used to initialize this Engine instance.
		/// </summary>
		public EngineOptions Options {
			get {
				return options;
			}
		}
		
		/// <summary>
		/// Provides access to the dump processing object, which
		/// is used for debugging.
		/// </summary>
		public DumpProcessor Dump {
			get {
				return dump;
			}
		}

		/// <summary>
		/// Provides access to the ADC layer selected for use
		/// for this compiler invocation.
		/// </summary>		
		public ADCLayer ADC {
			get {
				return adcLayer;
			}
		}
		
		/// <summary>
		/// Prints a console message if <paramref name="lvl" /> is less
		/// than or equal to the Verbosity option.
		/// </summary>
		public void Message(int lvl, string msg, params object[] prms)
		{
			if (options.Verbosity >= lvl)
				Console.WriteLine(msg, prms);
		}
		
		/// <summary>
		/// Modifies the method reference <paramref name="call" /> to
		/// refer to the equivalent ADC layer method.
		/// </summary>
		public void FixupADCMethod(MethodReference call)
		{
			// TODO: do real confirmation of the existence of a compatible method!
			
			TypeReference ntype = new TypeReference(call.DeclaringType.Name, adcLayer.Namespace, 
								call.DeclaringType.Scope, 
								call.DeclaringType.IsValueType);
			
			Console.WriteLine("Replacing ADC method `{0}': scope is `{1}'", call.ToString(), 
						call.DeclaringType.Scope);
			
			call.DeclaringType = ntype;
		}
		
		/// <summary>
		/// Finds the MethodDefinition that matches the method reference
		/// <paramref name="call" />. This method searches through the list
		/// of assemblies provided by the 'Assemblies' option in 
		/// <see cref="EngineOptions" />.
		/// </summary>
		public MethodDefinition GetCILDefinition(MethodReference call)
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
									
									callPrm = call.Parameters[x];
									defPrm = def.Parameters[x];
									
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
			
			if (options.TextDump)
				dumpType = DumpType.Text;
			
			IAssembly asm = null;
			
			switch (options.CPU) {
			case "X86":
				asm = new SharpOS.AOT.X86.Assembly();
			break;
			default:
				throw new EngineException(string.Format(
					"Error: processor type `{0}' not supported", 
					options.CPU));
			break;
			}
			
			Message(1, "AOT compiling for processor `{0}'", options.CPU);
			Run(asm);
		}
		
		/// <summary>
		/// Runs the AOT compiler engine.
		/// </summary>
		/// <param name="asm">
		/// The IAssembly implementation used to translate the
		/// compiler's intermediate representation into 
		/// architecture-native code and write that code to file.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="asm" /> is null.
		/// </exception>
		public void Run (IAssembly asm)
		{
			if (asm == null)
				throw new ArgumentNullException("asm");
			
			DumpType dumpType = DumpType.XML;
			
			if (options.TextDump)
				dumpType = DumpType.Text;
			
			dump = new DumpProcessor(dumpType);
			
			dump.Section(DumpSection.Root);
			
			this.asm = asm;

			foreach (string assemblyFile in options.Assemblies) {
				bool skip = false;
				
				Message(1, "Loading assembly `{0}'", assemblyFile);
				
				AssemblyDefinition library = AssemblyFactory.GetAssembly (assemblyFile);
				
				// Check for ADCLayerAttribute
				
				foreach (CustomAttribute ca in library.CustomAttributes) {
					if (ca.Constructor.DeclaringType.FullName == 
					    typeof(SharpOS.AOT.Attributes.ADCLayerAttribute).FullName) {
					    	if (ca.ConstructorParameters.Count != 2)
					    		throw new EngineException(string.Format(
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
						
						ADCLayer newLayer = new ADCLayer(adcCPU, adcNamespace);
						
						if (options.CPU == adcCPU)
							adcLayer = newLayer;
						
						Message (2, "Assembly `{0}' implements ADC for CPU `{1}' in namespace `{2}'",
							 library.Name, 
							 adcCPU,
							 adcNamespace);
						
						adcLayers.Add(newLayer);
					}
				}
				
				assemblies.Add(library);
				
				Dump.Element(library, assemblyFile);
				Message(1, "Generating IR for assembly types...");
		
				// We first add the data (Classes and Methods)
				foreach (TypeDefinition type in library.MainModule.Types) {
					bool ignore = false;
					string ignoreReason = null;
					
					if (type.Name.Equals ("<Module>"))
						continue;
					
					foreach (ADCLayer layer in this.adcLayers) {
						if (layer == this.adcLayer)
							continue;
						
						if (type.Namespace.StartsWith(layer.Namespace)) {
							Message (2, "Ignoring unused ADC type `{0}' in layer `{1}'",
								 type.FullName, layer.CPU);
							
							ignore = true;
							ignoreReason = "Unused ADC implementation";
							break;
						}
					}
					
					if (skip) {
						Dump.IgnoreMember(type.Name, ignoreReason);
						
						continue;
					}
					
					Dump.Element(type);
	
					Class _class = new Class (this, type);
	
					this.classes.Add (_class);
	
					foreach (MethodDefinition entry in type.Constructors) {
						if (!entry.Name.Equals (".cctor"))
							continue;
	
						Method method = new Method (this, entry);
	
						_class.Add (method);
	
						break;
					}
	
					foreach (MethodDefinition entry in type.Methods) {
						if (entry.ImplAttributes != MethodImplAttributes.Managed) {
							Dump.IgnoreMember(entry.Name, 
									"Method is unmanaged");
	
							continue;
						}
	
						Method method = new Method (this, entry);
	
						_class.Add (method);
					}
					
					Dump.FinishElement();
				}
				
				Dump.FinishElement();
			}
			
			if (adcLayer != null)
				Message(1, "Selected ADC layer `{0}' for compilation", 
					adcLayer.Namespace);
			else
				Message(1, "No available ADC layer matches CPU type.");
			
			Message(1, "Processing IR methods...");
			
			foreach (Class _class in this.classes)
				foreach (Method _method in _class) 
					_method.Process ();

			Message(1, "Encoding output for `{0}' to `{1}'...", options.CPU, 
					options.OutputFilename);
			
			asm.Encode (this, options.OutputFilename);

			Dump.FinishElement();
			
			if (options.DumpFile != null) {
				if (options.DumpFile == "-")
					Console.WriteLine(Dump.RenderDump(true));
				else {
					Message(1, "Creating dump file `{0}'", options.DumpFile);
					using (StreamWriter sw = new StreamWriter(options.DumpFile))
						sw.Write(Dump.RenderDump(true));
				}
			}
			
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
			return this.GetTypeSize (type, 2);
		}

		/// <summary>
		/// Gets the size of the object.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public int GetObjectSize (string type)
		{
			return this.GetTypeSize (type, 4);
		}

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
								foreach (FieldReference field in _class.ClassDefinition.Fields)
									result += this.GetFieldSize (field.FieldType.FullName);

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

			//throw new Exception ("'" + type + "' not supported.");
			return Operand.InternalSizeType.NotSet;
		}
	}
}

