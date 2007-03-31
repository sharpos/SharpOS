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
using System.Runtime.Serialization.Formatters.Binary;
using SharpOS.AOT.IR.Operators;
using Mono.Cecil;

namespace SharpOS.AOT.IR.Operands
{
    [Serializable]
    public class Call : Operand
    {
        public Call(MethodReference method, Operand[] operands)
            : base(null, operands)
        {
            this.method = method;
        }

        private MethodReference method;

        public MethodReference Method
        {
            get { return method; }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("Call " + this.Method.ReturnType.ReturnType.Name);
            stringBuilder.Append(" " + this.Method.DeclaringType.FullName + "." + this.method.Name);
            stringBuilder.Append("(");

            foreach (Operand operand in this.Operands)
            {
                if (operand != this.Operands[0])
                {
                    stringBuilder.Append(", ");
                }

                stringBuilder.Append(operand.ToString());
            }

            stringBuilder.Append(")");

            return stringBuilder.ToString();
        }
    }
}
