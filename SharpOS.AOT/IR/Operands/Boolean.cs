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
    public class Boolean : Operand
    {
        public Boolean(SharpOS.AOT.IR.Operators.Boolean _operator, Operand expression)
            : base(_operator, new Operand[] { expression })
        {
        }

        public Boolean(SharpOS.AOT.IR.Operators.Operator _operator, Operand first, Operand second)
            : base(_operator, new Operand[] { first, second })
        {
        }

        public Boolean(SharpOS.AOT.IR.Operators.Boolean _operator, Operand first, Operand second, Operand third)
            : base(_operator, new Operand[] { first, second, third })
        {
        }

        public List<Identifier> GetAllIdentifier()
        {
            List<Identifier> identifiers = new List<Identifier>();

            GetAllIdentifier(identifiers);

            return identifiers;
        }

        private void GetAllIdentifier(List<Identifier> identifiers)
        {
            foreach (Operand operand in this.Operands)
            {
                if (operand is Identifier == true)
                {
                    Identifier identifier = operand as Identifier;

                    if (identifiers.Contains(identifier) == false)
                    {
                        identifiers.Add(identifier);
                    }
                }
                else if (operand is Boolean == true)
                {
                    (operand as Boolean).GetAllIdentifier(identifiers);
                }
            }
        }

        public Boolean Negate()
        {
            Operator _operator = null;
            Operand left = null, right = null;

            if (this.Operands.Length > 0)
            {
                left = this.Operands[0];
            }

            if (this.Operands.Length > 1)
            {
                right = this.Operands[1];
            }

            if (this.Operator is SharpOS.AOT.IR.Operators.Boolean)
            {
                _operator = (this.Operator as SharpOS.AOT.IR.Operators.Boolean).Negate();

                if (this.Operands[0] is Boolean == true)
                {
                    left = (left as Boolean).Negate();
                }

                if (this.Operands.Length > 1 && this.Operands[1] is Boolean == true)
                {
                    right = (right as Boolean).Negate();
                }
            }
            else
            {
                _operator = (this.Operator as SharpOS.AOT.IR.Operators.Relational).Negate();
            }

            if (right == null)
            {
                return new Boolean(_operator as SharpOS.AOT.IR.Operators.Boolean, left);
            }

            return new Boolean(_operator, left, right);
        }
    }
}