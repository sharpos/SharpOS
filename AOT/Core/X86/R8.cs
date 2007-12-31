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
	public class R8 {

		public static readonly R8Type AL = new R8Type ("AL", 0);

		public static readonly R8Type CL = new R8Type ("CL", 1);

		public static readonly R8Type DL = new R8Type ("DL", 2);

		public static readonly R8Type BL = new R8Type ("BL", 3);

		public static readonly R8Type AH = new R8Type ("AH", 4);

		public static readonly R8Type CH = new R8Type ("CH", 5);

		public static readonly R8Type DH = new R8Type ("DH", 6);

		public static readonly R8Type BH = new R8Type ("BH", 7);

		/// <summary>
		/// Gets the by ID.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static R8Type GetByID (object value)
		{
			if (value == null)
				return null;

			if (value is R8Type)
				return value as R8Type;

			switch (Register.GetName (value)) {
			case "AL":
				return R8.AL;

			case "AH":
				return R8.AH;

			case "BL":
				return R8.BL;

			case "BH":
				return R8.BH;

			case "CL":
				return R8.CL;

			case "CH":
				return R8.CH;

			case "DL":
				return R8.DL;

			case "DH":
				return R8.DH;

			default:
				throw new EngineException ("Unknown R8 Register '" + value.ToString () + "'");
			}
		}
	}
}