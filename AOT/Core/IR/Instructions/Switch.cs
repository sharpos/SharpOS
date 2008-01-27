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
using System.Collections.Generic;
using System.Text;
using SharpOS.AOT.IR.Operands;
using Mono.Cecil;


namespace SharpOS.AOT.IR.Instructions {
	/// <summary>
	/// 
	/// </summary>
	public class Switch : Instruction {
		/// <summary>
		/// Initializes a new instance of the <see cref="Switch"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="blocks">The blocks.</param>
		public Switch (Register value, Block [] blocks)
			: base ("Switch", null, new Operand [] { value })
		{
			this.blocks = blocks;
		}

		private Block [] blocks;

		/// <summary>
		/// Gets the blocks.
		/// </summary>
		/// <value>The blocks.</value>
		public Block [] Blocks
		{
			get
			{
				return this.blocks;
			}
		}
	}
}