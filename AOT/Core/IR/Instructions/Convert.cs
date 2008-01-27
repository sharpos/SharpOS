// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Collections.Generic;
using System.Text;
using SharpOS.AOT.IR.Operands;
using Mono.Cecil;


namespace SharpOS.AOT.IR.Instructions {
	/// <summary>
	/// 
	/// </summary>
	public class Convert : Instruction {
		public enum Type {
			Conv_I,
			Conv_I1,
			Conv_I2,
			Conv_I4,
			Conv_I8,
			Conv_Ovf_I,
			Conv_Ovf_I_Un,
			Conv_Ovf_I1,
			Conv_Ovf_I1_Un,
			Conv_Ovf_I2,
			Conv_Ovf_I2_Un,
			Conv_Ovf_I4,
			Conv_Ovf_I4_Un,
			Conv_Ovf_I8,
			Conv_Ovf_I8_Un,
			Conv_Ovf_U,
			Conv_Ovf_U_Un,
			Conv_Ovf_U1,
			Conv_Ovf_U1_Un,
			Conv_Ovf_U2,
			Conv_Ovf_U2_Un,
			Conv_Ovf_U4,
			Conv_Ovf_U4_Un,
			Conv_Ovf_U8,
			Conv_Ovf_U8_Un,
			Conv_R_Un,
			Conv_R4,
			Conv_R8,
			Conv_U,
			Conv_U1,
			Conv_U2,
			Conv_U4,
			Conv_U8
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Convert"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="result">The result.</param>
		/// <param name="value">The value.</param>
		public Convert (Type type, Register result, Register value)
			: base (type.ToString (), result, new Operand [] { value })
		{
			this.type = type;

			result.InternalType = this.GetResultType (type);
		}

		private Type type;

		/// <summary>
		/// Gets the type of the convert.
		/// </summary>
		/// <value>The type of the convert.</value>
		public Type ConvertType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>
		/// Gets the type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public InternalType GetResultType (Type type)
		{
			switch (type) {

			case Type.Conv_I1:
			case Type.Conv_Ovf_I1:
			case Type.Conv_Ovf_I1_Un:
				return InternalType.I4; //I1;

			case Type.Conv_U1:
			case Type.Conv_Ovf_U1:
			case Type.Conv_Ovf_U1_Un:
				return InternalType.I4; //U1;

			case Type.Conv_I2:
			case Type.Conv_Ovf_I2:
			case Type.Conv_Ovf_I2_Un:
				return InternalType.I4; //I2;

			case Type.Conv_U2:
			case Type.Conv_Ovf_U2:
			case Type.Conv_Ovf_U2_Un:
				return InternalType.I4; //U2;

			case Type.Conv_I:
			case Type.Conv_Ovf_I:
			case Type.Conv_Ovf_I_Un:
				return InternalType.I;

			case Type.Conv_I4:
			case Type.Conv_Ovf_I4:
			case Type.Conv_Ovf_I4_Un:
				return InternalType.I4;

			case Type.Conv_U:
			case Type.Conv_Ovf_U:
			case Type.Conv_Ovf_U_Un:
				return InternalType.I; //U4;

			case Type.Conv_U4:
			case Type.Conv_Ovf_U4:
			case Type.Conv_Ovf_U4_Un:
				return InternalType.I4; //U4;

			case Type.Conv_I8:
			case Type.Conv_Ovf_I8:
			case Type.Conv_Ovf_I8_Un:
				return InternalType.I8;

			case Type.Conv_U8:
			case Type.Conv_Ovf_U8:
			case Type.Conv_Ovf_U8_Un:
				return InternalType.I8; //U8;

			case Type.Conv_R4:
			case Type.Conv_R_Un:
				return InternalType.F; //4;

			case Type.Conv_R8:
				return InternalType.F; //8;

			default:
				throw new NotImplementedEngineException ("'" + type + "' not supported.");
			}
		}
	}
}