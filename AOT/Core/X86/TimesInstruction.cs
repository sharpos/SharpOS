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
	internal class TimesInstruction : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="TimesInstruction"/> class.
		/// </summary>
		/// <param name="length">The length.</param>
		/// <param name="value">The value.</param>
		public TimesInstruction (UInt32 length, Byte value)
			: base (true, string.Empty, string.Empty, "TIMES", length.ToString () + " DB " + value.ToString (), null, null, null, value, null)
		{
			this.length = length;
		}

		private UInt32 length;

		/// <summary>
		/// Gets the length.
		/// </summary>
		/// <value>The length.</value>
		public UInt32 Length
		{
			get
			{
				return length;
			}
		}
	}
}