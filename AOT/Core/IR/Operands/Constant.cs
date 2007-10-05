// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using SharpOS.AOT.IR.Operators;
using Mono.Cecil;

namespace SharpOS.AOT.IR.Operands {
	[Serializable]
	public class Constant : Operand {
		/// <summary>
		/// Initializes a new instance of the <see cref="Constant"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public Constant (object value)
		{
			this.value = value;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Constant"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="operands">The operands.</param>
		public Constant (object value, Operand[] operands)
			: base (null, operands)
		{
			this.value = value;
		}

		private object value;

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <value>The value.</value>
		public object Value {
			get {
				return value;
			}
		}

		/// <summary>
		/// Gets the value as an uint32. (makes sure it's correctly converted)
		/// </summary>
		/// <value>The value.</value>
		public UInt32 UInt32Value {
			get {
				switch (this.SizeType) {
					case InternalSizeType.I4:
						return unchecked ((UInt32) Convert.ToInt32 (value));
					default:
					case InternalSizeType.U4:
						return Convert.ToUInt32 (value);
				}
			}
		}

		/// <summary>
		/// Gets the value as an uint16. (makes sure it's correctly converted)
		/// </summary>
		/// <value>The value.</value>
		public UInt16 UInt16Value {
			get {
				switch (this.SizeType) {
					case InternalSizeType.I2:
						return unchecked ((UInt16) Convert.ToInt16 (value));
					default:
					case InternalSizeType.U2:
						return Convert.ToUInt16 (value);
				}
			}
		}

		/// <summary>
		/// Gets the value as an Byte. (makes sure it's correctly converted)
		/// </summary>
		/// <value>The value.</value>
		public Byte ByteValue {
			get {
				switch (this.SizeType) {
					case InternalSizeType.I1:
						return unchecked ((Byte) Convert.ToSByte (value));
					default:
					case InternalSizeType.U1:
						return Convert.ToByte (value);
				}
			}
		}


		/// <summary>
		/// Gets or sets the operands.
		/// </summary>
		/// <value>The operands.</value>
		public override Operand[] Operands {
			get {
				return new Operand[0];
			}
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </returns>
		public override string ToString ()
		{
			StringBuilder stringBuilder = new StringBuilder();

			if (this.value != null) 
				stringBuilder.Append (this.Value.ToString());
			else
				stringBuilder.Append ("null");

			return stringBuilder.ToString();
		}
	}
}