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
    internal class DataInstruction : Instruction
    {
        public DataInstruction(bool indent, string label, string reference, string name, string parameters, Memory rmMemory, Register rmRegister, Register register, object value, string[] encoding)
            : base(indent, label, reference, name, parameters, rmMemory, rmRegister, register, value, encoding)
        {
        }

        public override bool Encode(bool bits32, BinaryWriter binaryWriter)
        {
            if (this.Value is string)
            {
                string value = (string)this.Value;

                for (int i = 0; i < value.Length; i++)
                {
                    binaryWriter.Write(value[i]);
                }
            }
            else if (this.Value is byte)
            {
                binaryWriter.Write((byte)this.Value);
            }
            else if (this.Value is UInt16)
            {
                binaryWriter.Write((UInt16)this.Value);
            }
            else if (this.Value is UInt32)
            {
                binaryWriter.Write((UInt32)this.Value);
            }
            else
            {
                throw new Exception("Wrong data type.");
            }

            return true;
        }
    }
}