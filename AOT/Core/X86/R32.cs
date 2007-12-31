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
	/// <summary>
	/// This module provides constants for designating x86 32-bit registers, and a function
	/// for retrieving one given its name.
	/// </summary>
	public static class R32 {
		#region Public Constants
		/// <summary>
		/// This constant represents the x86 32-bit EAX register
		/// </summary>

		public static readonly R32Type EAX = new R32Type ("EAX", 0);

		/// <summary>
		/// This constant represents the x86 32-bit ECX register
		/// </summary>
		public static readonly R32Type ECX = new R32Type ("ECX", 1);

		/// <summary>
		/// This constant represents the x86 32-bit EDX register
		/// </summary>
		public static readonly R32Type EDX = new R32Type ("EDX", 2);

		/// <summary>
		/// This constant represents the x86 32-bit EBX register
		/// </summary>
		public static readonly R32Type EBX = new R32Type ("EBX", 3);

		/// <summary>
		/// This constant represents the x86 32-bit ESP register
		/// </summary>
		public static readonly R32Type ESP = new R32Type ("ESP", 4);

		/// <summary>
		/// This constant represents the x86 32-bit EBP register
		/// </summary>
		public static readonly R32Type EBP = new R32Type ("EBP", 5);

		/// <summary>
		/// This constant represents the x86 32-bit ESI register
		/// </summary>
		public static readonly R32Type ESI = new R32Type ("ESI", 6);

		/// <summary>
		/// This constant represents the x86 32-bit EDI register
		/// </summary>
		public static readonly R32Type EDI = new R32Type ("EDI", 7);

		#endregion

		/// <summary>
		/// Given the name of an x86 32-bit register, this function returns the constant
		/// object that represents that register to the AOT
		/// </summary>
		/// <param name="value">The name of the x86 32-bit register to retrieve or a R32Type value</param>
		/// <returns>Returns an instance of R32Type that represents an x86 32-bit register</returns>
		public static R32Type GetByID (object value)
		{
			if (value == null)
				return null;

			if (value is R32Type)
				return value as R32Type;

			switch (Register.GetName (value)) {
			case "EAX":
				return R32.EAX;

			case "EBX":
				return R32.EBX;

			case "ECX":
				return R32.ECX;

			case "EDX":
				return R32.EDX;

			case "ESP":
				return R32.ESP;

			case "EBP":
				return R32.EBP;

			case "ESI":
				return R32.ESI;

			case "EDI":
				return R32.EDI;

			default:
				throw new EngineException ("Unknown R32 Register '" + value.ToString () + "'");
			}
		}
	}
}
