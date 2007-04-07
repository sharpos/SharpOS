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

namespace SharpOS.AOT.X86
{
    internal class ByteDataInstruction : DataInstruction
    {
        public ByteDataInstruction(string name, string values)
            : base(false, name, string.Empty, name, "DB \"" + values + "\"", null, null, null, values, null)
        {
        }

        public ByteDataInstruction(string name, byte value)
            : base(false, name, string.Empty, name, "DB " + string.Format("0x{0:X2}", value), null, null, null, value, null)
        {
        }

        public ByteDataInstruction(string values)
            : base(false, string.Empty, string.Empty, string.Empty, "DB \"" + values + "\"", null, null, null, values, null)
        {
        }

        public ByteDataInstruction(byte value)
            : base(false, string.Empty, string.Empty, string.Empty, "DB " + string.Format("0x{0:X2}", value), null, null, null, value, null)
        {
        }
    }
}