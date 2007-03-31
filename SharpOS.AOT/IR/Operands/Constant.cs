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
    public class Constant : Operand
    {
        public Constant(object value)
        {
            this.value = value;
        }

        public Constant(object value, Operand[] operands)
            : base(null, operands)
        {
            this.value = value;
        }

        private object value;

        public object Value
        {
            get { return value; }
        }

        public override Operand[] Operands
        {
            get
            {
                return new Operand[0];
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (this.value != null)
            {
                stringBuilder.Append(this.Value.ToString());
            }
            else
            {
                stringBuilder.Append("null");
            }

            /*if (this.Operands != null)
            {
                stringBuilder.Append(" (");

                foreach (Operand operand in this.Operands)
                {
                    if (operand != this.Operands[0])
                    {
                        stringBuilder.Append(", ");
                    }

                    stringBuilder.Append(operand.ToString());
                }

                stringBuilder.Append(")");
            }*/

            return stringBuilder.ToString();
        }
    }
}