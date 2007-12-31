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
	public class FP {

		public static readonly FPType ST0 = new FPType ("ST0", 0);

		public static readonly FPType ST1 = new FPType ("ST1", 1);

		public static readonly FPType ST2 = new FPType ("ST2", 2);

		public static readonly FPType ST3 = new FPType ("ST3", 3);

		public static readonly FPType ST4 = new FPType ("ST4", 4);

		public static readonly FPType ST5 = new FPType ("ST5", 5);

		public static readonly FPType ST6 = new FPType ("ST6", 6);

		public static readonly FPType ST7 = new FPType ("ST7", 7);

		/// <summary>
		/// Gets the by ID.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static FPType GetByID (object value)
		{
			if (value == null)
				return null;

			if (value is FPType)
				return value as FPType;

			switch (Register.GetName (value)) {
			case "ST0":
				return FP.ST0;

			case "ST1":
				return FP.ST1;

			case "ST2":
				return FP.ST2;

			case "ST3":
				return FP.ST3;

			case "ST4":
				return FP.ST4;

			case "ST5":
				return FP.ST5;

			case "ST6":
				return FP.ST6;

			case "ST7":
				return FP.ST7;

			default:
				throw new EngineException ("Unknown FP Register '" + value.ToString () + "'");
			}
		}
	}
}