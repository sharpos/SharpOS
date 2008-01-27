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
using Mono.Cecil;


namespace SharpOS.AOT.IR.Operands {
	/// <summary>
	/// Instruction operand.
	/// </summary>
	public abstract class Operand {

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Operand"/> class.
		/// </summary>
		public Operand ()
		{
		}

		#endregion

		/// <summary>
		/// 
		/// </summary>
		protected InternalType internalType = InternalType.NotSet;

		/// <summary>
		/// Gets or sets the type of the internal.
		/// </summary>
		/// <value>The type of the internal.</value>
		public virtual InternalType InternalType
		{
			get
			{
				return this.internalType;
			}
			set
			{
				this.internalType = value;
			}
		}

		protected Class type = null;

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>The type.</value>
		public Class Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}
		
		/// <summary>
		/// Gets the non register.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public static Operand GetNonRegister (Operand value, Type type)
		{
			Operand operand = value;

			while (operand is IR.Operands.Register) {
				IR.Operands.Register register = operand as IR.Operands.Register;

				if (register.Parent.Use.Length != 1)
					throw new EngineException (string.Format ("Could not propagate '{0}'.", value.ToString ()));

				operand = register.Parent.Use [0];
			}

			if (operand is NullConstant)
				return null;

			if (operand.GetType () != type)
				throw new EngineException (string.Format ("'{0}' expected but '{1}' found.", type.ToString (), operand.GetType ().ToString ()));

			return operand;
		}


	}
}