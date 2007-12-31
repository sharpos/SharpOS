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
	public class R16 {

		public static readonly R16Type AX = new R16Type ("AX", 0);

		public static readonly R16Type CX = new R16Type ("CX", 1);

		public static readonly R16Type DX = new R16Type ("DX", 2);

		public static readonly R16Type BX = new R16Type ("BX", 3);

		public static readonly R16Type SP = new R16Type ("SP", 4);

		public static readonly R16Type BP = new R16Type ("BP", 5);

		public static readonly R16Type SI = new R16Type ("SI", 6);

		public static readonly R16Type DI = new R16Type ("DI", 7);


		/// <summary>
		/// Gets the by ID.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static R16Type GetByID (object value)
		{
			if (value == null)
				return null;

			if (value is R16Type)
				return value as R16Type;

			switch (Register.GetName (value)) {
			case "AX":
				return R16.AX;

			case "BX":
				return R16.BX;

			case "CX":
				return R16.CX;

			case "DX":
				return R16.DX;

			case "SP":
				return R16.SP;

			case "BP":
				return R16.BP;

			case "SI":
				return R16.SI;

			case "DI":
				return R16.DI;

			default:
				throw new EngineException ("Unknown R16 Register '" + value.ToString () + "'");
			}
		}
	}
}