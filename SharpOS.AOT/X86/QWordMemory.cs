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
	public class QWordMemory : Memory {
		public QWordMemory (SegType segment, R32Type _base, R32Type index, byte scale, Int32 displacement)
			: base (segment, _base, index, scale, displacement)
		{
		}

		public QWordMemory (SegType segment, R32Type _base, R32Type index, byte scale)
			: base (segment, _base, index, scale)
		{
		}

		public QWordMemory (SegType segment, string label)
			: base (segment, label)
		{
		}

		public QWordMemory (string label)
			: base (label)
		{
		}

		public QWordMemory (SegType segment, R16Type _base, R16Type index, Int16 displacement)
			: base (segment, _base, index, displacement)
		{
		}

		public QWordMemory (SegType segment, R16Type _base, R16Type index)
			: base (segment, _base, index)
		{
		}

	}
}