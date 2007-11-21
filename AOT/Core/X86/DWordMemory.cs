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
using SharpOS.AOT.IR.Instructions;
using SharpOS.AOT.IR.Operands;
using SharpOS.AOT.IR.Operators;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;

namespace SharpOS.AOT.X86 {
	public class DWordMemory : Memory {
		/// <summary>
		/// Initializes a new instance of the <see cref="DWordMemory"/> class.
		/// </summary>
		/// <param name="segment">The segment.</param>
		/// <param name="_base">The _base.</param>
		/// <param name="index">The index.</param>
		/// <param name="scale">The scale.</param>
		/// <param name="displacement">The displacement.</param>
		public DWordMemory (SegType segment, R32Type _base, R32Type index, byte scale, Int32 displacement)
			: base (segment, _base, index, scale, displacement)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DWordMemory"/> class.
		/// </summary>
		/// <param name="segment">The segment.</param>
		/// <param name="_base">The _base.</param>
		/// <param name="index">The index.</param>
		/// <param name="scale">The scale.</param>
		public DWordMemory (SegType segment, R32Type _base, R32Type index, byte scale)
			: base (segment, _base, index, scale)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DWordMemory"/> class.
		/// </summary>
		/// <param name="segment">The segment.</param>
		/// <param name="label">The label.</param>
		public DWordMemory (SegType segment, string label)
			: base (segment, label)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DWordMemory"/> class.
		/// </summary>
		/// <param name="label">The label.</param>
		public DWordMemory (string label)
			: base (label)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DWordMemory"/> class.
		/// </summary>
		/// <param name="segment">The segment.</param>
		/// <param name="_base">The _base.</param>
		/// <param name="index">The index.</param>
		/// <param name="displacement">The displacement.</param>
		public DWordMemory (SegType segment, R16Type _base, R16Type index, Int16 displacement)
			: base (segment, _base, index, displacement)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DWordMemory"/> class.
		/// </summary>
		/// <param name="segment">The segment.</param>
		/// <param name="_base">The _base.</param>
		/// <param name="index">The index.</param>
		public DWordMemory (SegType segment, R16Type _base, R16Type index)
			: base (segment, _base, index)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DWordMemory"/> class.
		/// </summary>
		/// <param name="memory">The memory.</param>
		public DWordMemory (DWordMemory memory)
		{
			displacement = memory.displacement;
			displacementSet = memory.displacementSet;


			bits32Address = memory.bits32Address;
			scale = memory.scale;
			index = memory.index;
			_base = memory._base;

			reference = memory.reference;


			segment = memory.segment;
		}
	}
}