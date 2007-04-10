/**
 *  (C) 2006-2007 The SharpOS Project Team - http://www.sharpos.org
 *
 *  Licensed under the terms of the GNU GPL License version 2.
 *
 *  Author: Mircea-Cristian Racasan <darx_kies@gmx.net>
 *
 */

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
		public Identifier (string name, int index)
		{
			this.index = index;
			this.value = name;
		}

		public Identifier (string name, Operand[] operands)
			: base (null, operands)
		{
			this.value = name;
		}

		public override Operand[] Operands {
			get {
				return new Operand[] { this };
			}
		}

		private int index = 0;

		public int Index {
			get {
				return this.index;
			}
		}

		private string value;

		public string Value {
			get {
				return this.value;
			}
		}

		private bool forceSpill = false;

		public bool ForceSpill {
			set {
				this.forceSpill = value;
			}
			get {
				return this.forceSpill;
			}
		}

		public override bool Equals (object obj)
		{
			return (obj is Identifier == true
				&& this.ToString().Equals ( (obj as Identifier).ToString()) == true);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ID {
			get {
				StringBuilder stringBuilder = new StringBuilder();

				stringBuilder.Append (this.Value.ToString());

				stringBuilder.Append ("_" + this.Version);

				return stringBuilder.ToString();
			}
		}

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