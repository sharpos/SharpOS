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
using SharpOS.AOT.IR;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;

namespace SharpOS.AOT.X86 {
	public class Seg {

		public static readonly SegType ES = new SegType ("ES", 0, 0x26);

		public static readonly SegType CS = new SegType ("CS", 1, 0x2E);

		public static readonly SegType SS = new SegType ("SS", 2, 0x36);

		public static readonly SegType DS = new SegType ("DS", 3, 0x3E);

		public static readonly SegType FS = new SegType ("FS", 4, 0x64);

		public static readonly SegType GS = new SegType ("GS", 5, 0x65);

		/// <summary>
		/// Gets the by ID.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static SegType GetByID (object value)
		{
			if (value == null)
				return null;

			if (value is SegType)
				return value as SegType;

			switch (Register.GetName (value)) {
			case "DS":
				return Seg.DS;

			case "ES":
				return Seg.ES;

			case "CS":
				return Seg.CS;

			case "SS":
				return Seg.SS;

			case "FS":
				return Seg.FS;

			case "GS":
				return Seg.GS;

			default:
				throw new EngineException ("Unknown Seg Register '" + value.ToString () + "'");
			}
		}
	}
}