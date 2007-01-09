/**
 *  (C) 2006-2007 Mircea-Cristian Racasan <darx_kies@gmx.net>
 * 
 *  Licensed under the terms of the GNU GPL License version 2.
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

namespace SharpOS.AOT.X86
{
    public class ByteMemory : Memory
    {
        public ByteMemory(SegType segment, R32Type _base, R32Type index, byte scale, Int32 displacement)
            : base(segment, _base, index, scale, displacement)
        {
        }

        public ByteMemory(SegType segment, R32Type _base, R32Type index, byte scale)
            : base(segment, _base, index, scale)
        {
        }

        public ByteMemory(SegType segment, string label)
            : base(segment, label)
        {
        }

        public ByteMemory(string label)
            : base(label)
        {
        }

        public ByteMemory(SegType segment, R16Type _base, R16Type index, Int16 displacement)
            : base(segment, _base, index, displacement)
        {
        }

        public ByteMemory(SegType segment, R16Type _base, R16Type index)
            : base(segment, _base, index)
        {
        }
    }
}