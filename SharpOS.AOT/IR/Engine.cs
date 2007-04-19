// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
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
	public partial class Engine : IEnumerable<Class> {
		/// <summary>
		/// Initializes a new instance of the <see cref="Engine"/> class.
		/// </summary>
		public Engine ()
		{
		}

		private IAssembly asm = null;

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
		/// Runs the specified asm.
		/// </summary>
		/// <param name="asm">The asm.</param>
		/// <param name="assembly">The assembly.</param>
		/// <param name="target">The target.</param>
		public void Run (IAssembly asm, string assembly, string target)
		{
			this.asm = asm;

			AssemblyDefinition library = AssemblyFactory.GetAssembly (assembly);

			// We first add the data (Classes and Methods)
			foreach (TypeDefinition type in library.MainModule.Types) {
				this.WriteLine (type.Name);

				if (type.Name.Equals ("<Module>"))
					continue;

				this.WriteLine (type.FullName);

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

					if (!entry.IsStatic || entry.ImplAttributes != MethodImplAttributes.Managed) {
						this.WriteLine ("Not processing '" + entry.DeclaringType.FullName + "." + entry.Name + "'");

						continue;
					}

					Method method = new Method (this, entry);

					_class.Add (method);
				}
			}

			foreach (Class _class in this.classes)
				foreach (Method _method in _class) 
					_method.Process ();

			asm.Encode (this, target);

			return;
		}

		private List<Class> classes = new List<Class> ();

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.
		/// </returns>
		IEnumerator<Class> IEnumerable<Class>.GetEnumerator ()
		{
			foreach (Class _class in this.classes)
				yield return _class;
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return ( (IEnumerable<Class>) this).GetEnumerator ();
		}

		/// <summary>
		/// Gets the size of the type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public int GetTypeSize (string type)
		{
			Operands.Operand.InternalSizeType sizeType = GetInternalType (type);

			switch (sizeType) {
				case Operand.InternalSizeType.I1:
				case Operand.InternalSizeType.U1:
					return 1;

				case Operand.InternalSizeType.I2:
				case Operand.InternalSizeType.U2:
					return 2;

				case Operand.InternalSizeType.I4:
				case Operand.InternalSizeType.U4:
				case Operand.InternalSizeType.I:
				case Operand.InternalSizeType.U:
					return 4;

				case Operand.InternalSizeType.I8:
				case Operand.InternalSizeType.U8:
					return 8;

				case Operand.InternalSizeType.R4:
					return 4;

				case Operand.InternalSizeType.R8:
					return 8;

				case Operand.InternalSizeType.Object:
					foreach (Class _class in this.classes) {
						if (_class.ClassDefinition.FullName.Equals (type)) {
							if (_class.ClassDefinition.IsEnum) {
								foreach (FieldDefinition field in _class.ClassDefinition.Fields) {
									if ( (field.Attributes & FieldAttributes.RTSpecialName) != 0) 
										return this.GetTypeSize (field.FieldType.FullName);
								}

							} if (_class.ClassDefinition.IsValueType) {
								int result = 0;

								foreach (FieldReference field in _class.ClassDefinition.Fields)
									result += this.GetTypeSize (field.FieldType.FullName);

								return result;

							} else
								break;
						}
					}

					break;
			}

			throw new Exception ("'" + type + "' not supported.");
		}

		/// <summary>
		/// Gets the type of the internal.
		/// </summary>
		/// <param name="type">The type.</param>
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

			foreach (Class _class in this.classes) {
				if (_class.ClassDefinition.FullName.Equals (type)) {
					if (_class.ClassDefinition.IsEnum) {
						foreach (FieldDefinition field in _class.ClassDefinition.Fields) {
							if ( (field.Attributes & FieldAttributes.RTSpecialName) != 0) 
								return this.GetInternalType (field.FieldType.FullName);
						}

					} else
						return Operands.Operand.InternalSizeType.Object;
				}
			}

			//throw new Exception ("'" + type + "' not supported.");
			return Operand.InternalSizeType.NotSet;
		}

		internal void WriteLine (string value)
		{
			Console.WriteLine ("[*] " + value);
		}
	}
}

