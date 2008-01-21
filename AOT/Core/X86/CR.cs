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
	[System.Flags]
	public enum CR0Flags : uint {
		PE = (1U << 0),		// protected mode
		MP = (1U << 1),		// math present
		EM = (1U << 2),		// emulate numeric extension
		TS = (1U << 3),		// task switched
		ET = (1U << 4),		// extension type
		NE = (1U << 5),		// numeric error enable

		WP = (1U << 16),	// write protect

		AM = (1U << 18),	// alignment mask

		NW = (1U << 29),	// not write-through
		CD = (1U << 30),	// cache disable
		PG = (1U << 31)		// paging enable
	}

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