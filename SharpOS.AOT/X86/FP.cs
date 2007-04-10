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
using SharpOS.AOT.IR;
using SharpOS.AOT.IR.Instructions;
using SharpOS.AOT.IR.Operands;
using SharpOS.AOT.IR.Operators;
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

		public static FPType GetByID (object value)
		{
			if (value is FPType == true)
				return value as FPType;

			if (value is SharpOS.AOT.IR.Operands.Field == false)
				throw new Exception ("'" + value.ToString() + "' is not supported.");

			string id = (value as SharpOS.AOT.IR.Operands.Field).Value.ToString();

			if (id.Equals ("null") == true)
				return null;

			switch (id.Substring (id.Length - 3)) {

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
					throw new Exception ("Unknown FP Register '" + id + "'");
			}
		}
	}
}