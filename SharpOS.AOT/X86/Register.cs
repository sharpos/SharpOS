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
    public abstract class Register
    {
        public Register(string name, byte index)
        {
            this.name = name;
            this.index = index;
        }

        private string name = string.Empty;

        public string Name
        {
            get { return name; }
        }

        private byte index = 0;

        public byte Index
        {
            get { return index; }
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}