/**
 *  (C) 2006-2007 The SharpOS Project Team - http://www.sharpos.org
 * 
 *  Licensed under the terms of the GNU GPL License version 2.
 * 
 *  Author: Mircea-Cristian Racasan <darx_kies@gmx.net>
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using SharpOS.AOT.IR;
using SharpOS.AOT.IR.Instructions;
using SharpOS.AOT.IR.Operands;
using SharpOS.AOT.IR.Operators;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;


namespace SharpOS.AOT.IR
{
    public interface IAssembly
    {
        bool Encode(Engine engine, string target);
        int AvailableRegistersCount { get; }
        bool Spill(Operands.Operand.InternalSizeType type);
        bool IsRegister(string value);
        bool IsInstruction(string value);
        SharpOS.AOT.IR.Operands.Operand.InternalSizeType GetRegisterSizeType(string value);
        int GetRegisterIndex(string value);
    }
}
