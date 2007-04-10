/**
 *  (C) 2006-2007 The SharpOS Project Team - http://www.sharpos.org
 *
 *  Licensed under the terms of the GNU GPL License version 2.
 *
 *  Author: Mircea-Cristian Racasan <darx_kies@gmx.net>
 *
 */

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
		public Engine ()
		{
		}

		private IAssembly asm = null;

		public IAssembly Assembly {
			get {
				return asm;
			}
		}

		public void Run (IAssembly asm, string assembly, string target)
		{
			this.asm = asm;

			AssemblyDefinition library = AssemblyFactory.GetAssembly (assembly);

			// We first add the data (Classes and Methods)
			foreach (TypeDefinition type in library.MainModule.Types) {
				Console.WriteLine (type.Name);

				if (type.Name.Equals ("<Module>"))
					continue;

				Console.WriteLine (type.FullName);

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

					if (entry.IsStatic == false || entry.ImplAttributes != MethodImplAttributes.Managed) {
						Console.WriteLine ("Not processing '" + entry.DeclaringType.FullName + "." + entry.Name + "'");

						continue;
					}

					Method method = new Method (this, entry);

					_class.Add (method);
				}
			}

			foreach (Class _class in this.classes) {
				foreach (Method _method in _class) {
					_method.Process ();
				}
			}

			asm.Encode (this, target);

			return;
		}

		private List<Class> classes = new List<Class> ();

		IEnumerator<Class> IEnumerable<Class>.GetEnumerator ()
		{
			foreach (Class _class in this.classes)
			yield return _class;
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return ( (IEnumerable<Class>) this).GetEnumerator ();
		}

		public Operands.Operand.InternalSizeType GetSizeType (string type)
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
								return this.GetSizeType (field.FieldType.FullName);
						}

					} else
						break;
				}
			}

			throw new Exception ("'" + type + "' not supported.");
		}

	}
}

