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
using System.Text;
using System.Collections.Generic;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;

namespace SharpOS.AOT.X86 {
	public class SegType : Register {
		/// <summary>
		/// Initializes a new instance of the <see cref="SegType"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		public SegType (string name, byte index, byte value)
			: base (name, index)
		{
			this.value = value;
		}

		private byte value = 0;

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <value>The value.</value>
		public byte Value
		{
			get
			{
				return value;
			}
		}
	}
}