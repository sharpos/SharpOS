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
	public class Identifier : Operand {
		/// <summary>
		/// Initializes a new instance of the <see cref="Identifier"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="index">The index.</param>
		public Identifier (string name, int index)
		{
			this.index = index;
			this.value = name;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Identifier"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="operands">The operands.</param>
		public Identifier (string name, Operand[] operands)
			: base (null, operands)
		{
			this.value = name;
		}

		/// <summary>
		/// Gets or sets the operands.
		/// </summary>
		/// <value>The operands.</value>
		public override Operand[] Operands {
			get {
				return new Operand[] { this };
			}
		}

		private int index = 0;

		/// <summary>
		/// Gets the index.
		/// </summary>
		/// <value>The index.</value>
		public int Index {
			get {
				return this.index;
			}
		}

		private string value;

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <value>The value.</value>
		public string Value {
			get {
				return this.value;
			}
		}

		private bool forceSpill = false;

		/// <summary>
		/// Gets or sets a value indicating whether [force spill].
		/// </summary>
		/// <value><c>true</c> if [force spill]; otherwise, <c>false</c>.</value>
		public bool ForceSpill {
			set {
				this.forceSpill = value;
			}
			get {
				return this.forceSpill;
			}
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>.
		/// </summary>
		/// <param name="obj">The <see cref="T:System.Object"></see> to compare with the current <see cref="T:System.Object"></see>.</param>
		/// <returns>
		/// true if the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>; otherwise, false.
		/// </returns>
		public override bool Equals (object obj)
		{
			return (obj is Identifier
				&& this.ToString().Equals ( (obj as Identifier).ToString()));
		}

		/// <summary>
		/// Serves as a hash function for a particular type. <see cref="M:System.Object.GetHashCode"></see> is suitable for use in hashing algorithms and data structures like a hash table.
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object"></see>.
		/// </returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>
		/// Gets the ID.
		/// </summary>
		/// <value>The ID.</value>
		public override string ID {
			get {
				StringBuilder stringBuilder = new StringBuilder();

				stringBuilder.Append (this.Value.ToString());

				stringBuilder.Append ("_" + this.Version);

				return stringBuilder.ToString();
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

			stringBuilder.Append (this.ID);

			if (this.Stamp != int.MinValue) 
				stringBuilder.Append ("__" + this.Stamp);

			if (this.SizeType != InternalSizeType.NotSet) 
				stringBuilder.Append ("__" + this.SizeType);

			return stringBuilder.ToString();
		}
	}
}