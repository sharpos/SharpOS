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
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using SharpOS;
using SharpOS.AOT.IR;
using SharpOS.AOT.X86;

namespace SharpOS.AOT.COFF {
	internal abstract class Symbol {
		public enum StorageClassType : byte {
			C_NULL = 0,
			C_AUTO = 1,
			C_EXT = 2,
			C_STAT = 3,
			C_REG = 4,
			C_EXTDEF = 5,
			C_LABEL = 6,
			C_ULABEL = 7,
			C_MOS = 8,
			C_ARG = 9,
			C_STRTAG = 10,
			C_MOU = 11,
			C_UNTAG = 12,
			C_TPDEF = 13,
			C_USTATIC = 14,
			C_ENTAG = 15,
			C_MOE = 16,
			C_REGPARM = 17,
			C_FIELD = 18,
			C_AUTOARG = 19,
			C_LASTENT = 20,
			C_BLOCK = 100,
			C_FCN = 101,
			C_EOS = 102,
			C_FILE = 103,
			C_LINE = 104,
			C_ALIAS = 105,
			C_HIDDEN = 106,
			C_EFCN = 255
		}

		private int index;

		/// <summary>
		/// Gets or sets the index.
		/// </summary>
		/// <value>The index.</value>
		public int Index
		{
			get
			{
				return index;
			}
			set
			{
				index = value;
			}
		}
	}

	internal class Label : Symbol {
		public Label (string name)
		{
			this.name = name;
		}

		private string name;

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name
		{
			get
			{
				return name;
			}
		}
	}

	internal class Function : Label {
		public Function (string name)
			: base (name)
		{
		}
	}

	internal class Static : Label {
		public Static (string name)
			: base (name)
		{
		}
	}
}
