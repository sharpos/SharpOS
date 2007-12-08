// 
// (C) 2006-2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
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
	public class CR {

		public static readonly CRType CR0 = new CRType ("CR0", 0);

		public static readonly CRType CR2 = new CRType ("CR2", 2);

		public static readonly CRType CR3 = new CRType ("CR3", 3);

		public static readonly CRType CR4 = new CRType ("CR4", 4);

		/// <summary>
		/// Gets the by ID.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static CRType GetByID (object value)
		{
			if (value == null)
				return null;

			if (value is CRType)
				return value as CRType;

			switch (Register.GetName (value)) {
				case "CR0":
					return CR.CR0;

				case "CR2":
					return CR.CR2;

				case "CR3":
					return CR.CR3;

				case "CR4":
					return CR.CR4;

				default:
					throw new EngineException ("Unknown CR Register '" + value.ToString () + "'");
			}
		}
	}
}