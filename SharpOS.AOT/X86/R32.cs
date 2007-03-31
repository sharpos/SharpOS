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
    public class R32
    {
        public static readonly R32Type EAX = new R32Type("EAX", 0);
        public static readonly R32Type ECX = new R32Type("ECX", 1);
        public static readonly R32Type EDX = new R32Type("EDX", 2);
        public static readonly R32Type EBX = new R32Type("EBX", 3);
        public static readonly R32Type ESP = new R32Type("ESP", 4);
        public static readonly R32Type EBP = new R32Type("EBP", 5);
        public static readonly R32Type ESI = new R32Type("ESI", 6);
        public static readonly R32Type EDI = new R32Type("EDI", 7);

        public static R32Type GetByID(string id)
        {
            if (id.Equals("null") == true)
            {
                return null;
            }

            switch (id.Substring(id.Length - 3))
            {
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
                    throw new Exception("Unknown R32 Register '" + id + "'");
            }
        }
    }
}