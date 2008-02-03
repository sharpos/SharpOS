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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using SharpOS.AOT.IR.Operands;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;


namespace SharpOS.AOT.IR {
	/// <summary>
	/// 
	/// </summary>
	public interface IAssembly {
		/// <summary>
		/// Encodes the specified engine.
		/// </summary>
		/// <param name="engine">The engine.</param>
		/// <param name="target">The target.</param>
		/// <returns></returns>
		bool Encode (Engine engine, string target);

		/// <summary>
		/// Gets the available registers count.
		/// </summary>
		/// <value>The available registers count.</value>
		int AvailableRegistersCount
		{
			get;
		}

		/// <summary>
		/// Gets the size of the int.
		/// </summary>
		/// <value>The size of the int.</value>
		int IntSize
		{
			get;
		}

		/// <summary>
		/// Spills the specified type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		bool Spill (Operands.InternalType type);

		/// <summary>
		/// Determines whether the specified value is register.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		/// 	<c>true</c> if the specified value is register; otherwise, <c>false</c>.
		/// </returns>
		bool IsRegister (string value);

		/// <summary>
		/// Determines whether the specified value is a memory address.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		/// 	<c>true</c> if the specified value is a memory address; otherwise, <c>false</c>.
		/// </returns>
		bool IsMemoryAddress (string value);

		/// <summary>
		/// Determines whether the specified value is an instruction.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		/// 	<c>true</c> if the specified value is an instruction; otherwise, <c>false</c>.
		/// </returns>
		bool IsInstruction (string value);


		/// <summary>
		/// Ignores the content of the type.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		bool IgnoreTypeContent (string value);

		/// <summary>
		/// Gets the type of the register size.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		InternalType GetRegisterSizeType (string value);
	}
}
