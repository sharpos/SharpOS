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
	/// Base class for Arguments, Local Variables...
	/// </summary>
	public abstract class Identifier : Operand {
		/// <summary>
		/// Initializes a new instance of the <see cref="Identifier"/> class.
		/// </summary>
		/// <param name="typeName">Name of the type.</param>
		/// <param name="index">The index.</param>
		public Identifier (string typeName, int index)
		{
			this.index = index;
			this.typeName = typeName;
		}

		private string typeName = string.Empty;

		private int index = 0;

		/// <summary>
		/// Gets or sets the index.
		/// </summary>
		/// <value>The index.</value>
		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		private int version = 0;

		/// <summary>
		/// Gets or sets the SSA version.
		/// </summary>
		/// <value>The version.</value>
		public int Version
		{
			get
			{
				return version;
			}
			set
			{
				version = value;
			}
		}

		protected bool forceSpill = false;

		/// <summary>
		/// If set, the Register Allocation doesn't allocate a register for this identifier.
		/// </summary>
		/// <value>Allow this identifier to get a register.</value>
		public bool ForceSpill
		{
			get
			{
				return forceSpill;
			}
			set
			{
				forceSpill = value;
			}
		}

		private int register = int.MinValue;

		/// <summary>
		/// Gets or sets the register.
		/// </summary>
		/// <value>The register.</value>
		public virtual int Register
		{
			get
			{
				return register;
			}
			set
			{
				register = value;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is register set.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is register set; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsRegisterSet
		{
			get
			{
				return register != int.MinValue;
			}
		}

		private int stack = int.MinValue;

		/// <summary>
		/// Gets or sets the stack.
		/// </summary>
		/// <value>The stack.</value>
		public virtual int Stack
		{
			get
			{
				return stack;
			}
			set
			{
				stack = value;
			}
		}

		/// <summary>
		/// Gets the ID.
		/// </summary>
		/// <value>The ID.</value>
		public string ID
		{
			get
			{
				string result = this.typeName;

				result += this.index.ToString ();

				result += "_";

				result += this.version.ToString ();

				return result;
			}
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		public override string ToString ()
		{
			if (this.InternalType == InternalType.NotSet)
				return this.ID;
			else {
				StringBuilder sb = new StringBuilder (this.ID);
				sb.Append("__");
				sb.Append(this.internalType.ToString());

				return sb.ToString();
			}
		}

		/// <summary>
		/// Gets the full name of the type.
		/// </summary>
		/// <value>The full name of the type.</value>
		public string TypeFullName {
			get {
				return this.Type.TypeFullName;
			}
		}

		public string TypeName {
			get {
				return this.Type.TypeName;
			}
		}
	}
}