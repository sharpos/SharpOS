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
using System.Runtime.Serialization.Formatters.Binary;
using SharpOS.AOT.IR.Operators;
using Mono.Cecil;

namespace SharpOS.AOT.IR.Operands
{
    [Serializable]
    public abstract class Operand
    {
        public enum ConvertType
        {
            NotSet
            , Conv_I
            , Conv_I1
            , Conv_I2
            , Conv_I4
            , Conv_I8
            , Conv_Ovf_I
            , Conv_Ovf_I_Un
            , Conv_Ovf_I1
            , Conv_Ovf_I1_Un
            , Conv_Ovf_I2
            , Conv_Ovf_I2_Un
            , Conv_Ovf_I4
            , Conv_Ovf_I4_Un
            , Conv_Ovf_I8
            , Conv_Ovf_I8_Un
            , Conv_Ovf_U
            , Conv_Ovf_U_Un
            , Conv_Ovf_U1
            , Conv_Ovf_U1_Un
            , Conv_Ovf_U2
            , Conv_Ovf_U2_Un
            , Conv_Ovf_U4
            , Conv_Ovf_U4_Un
            , Conv_Ovf_U8
            , Conv_Ovf_U8_Un
            , Conv_R_Un
            , Conv_R4
            , Conv_R8
            , Conv_U
            , Conv_U1
            , Conv_U2
            , Conv_U4
            , Conv_U8
        }

        public Operand()
        {
        }

        private ConvertType convertTo = ConvertType.NotSet;

        public ConvertType ConvertTo
        {
            get { return convertTo; }
            set { convertTo = value; }
        }

        public Operand(Operator _operator, Operand[] operands)
        {
            this._operator = _operator;
            this.operands = operands;
        }

        private Operator _operator = null;

        public Operator Operator
        {
            get { return _operator; }
        }

        protected Operand[] operands = null;

        public virtual Operand[] Operands
        {
            get 
            {
                return operands;
            }
            set
            {
                this.operands = value;
            }
        }

        private int version = 0;

        public int Version
        {
            get { return version; }
            set { version = value; }
        }

        public void Replace(Dictionary<string, Operand> registerValues)
        {
            if (this.operands == null)
            {
                return;
            }

            for (int i = 0; i < this.operands.Length; i++)
            {
                Operand operand = this.operands[i];

                operand.Replace(registerValues);

                if (operand is Register && registerValues.ContainsKey(operand.ToString()) == true)
                {
                    this.operands[i] = registerValues[operand.ToString()];
                }
            }
        }

        public SharpOS.AOT.IR.Operands.Operand Clone()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();

            binaryFormatter.Serialize(memoryStream, this);
            memoryStream.Seek(0, SeekOrigin.Begin);

            SharpOS.AOT.IR.Operands.Operand operand = (SharpOS.AOT.IR.Operands.Operand)binaryFormatter.Deserialize(memoryStream);

            return operand;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string operatorValue = string.Empty;

            /*if (this._operator != null)
            {
                stringBuilder.Append(this._operator.ToString() + " ");
            }

            if (this.operands != null && this.operands.Length > 0)
            {
                foreach (Operand operand in operands)
                {
                    if (operand != operands[0])
                    {
                        stringBuilder.Append(", ");
                    }

                    stringBuilder.Append(operand.ToString());
                }

            }*/

            if (this._operator != null)
            {
                operatorValue = this._operator.ToString();
            }

            if (this.operands != null && this.operands.Length > 0)
            {
                if (this.operands.Length == 1)
                {
                    stringBuilder.Append(operatorValue + " (" + this.operands[0].ToString() + ")");
                }
                else if (this.operands.Length == 2)
                {
                    stringBuilder.Append("(" + this.operands[0].ToString() + ") " + operatorValue + " (" + this.operands[1].ToString() + ")");
                }
                else
                {
                    stringBuilder.Append(operatorValue + " (");

                    foreach (Operand expression in operands)
                    {
                        if (expression != operands[0])
                        {
                            stringBuilder.Append(", ");
                        }

                        stringBuilder.Append(expression.ToString());
                    }

                    stringBuilder.Append(")");
                }
            }
            else 
            {
                stringBuilder.Append(operatorValue);
            }
        
            return stringBuilder.ToString().Trim();
        }
    }

    [Serializable]
    public class Arithmetic : Operand
    {
        public Arithmetic(Unary _operator, Operand operand)
            : base(_operator, new Operand[] { operand })
        {

        }

        public Arithmetic(Binary _operator, Operand first, Operand second)
            : base(_operator, new Operand[] { first, second })
        {

        }
    }

    [Serializable]
    public class ArrayElement : Identifier
    {
        public ArrayElement(Operand array, Operand index):base (array.ToString(), new Operand[] {array, index})
        {
        }

        public Operand Array
        {
            get
            {
                return this.operands[0];
            }
        }

        public Operand IDX
        {
            get
            {
                return this.operands[1];
            }
        }

        public override string ToString()
        {
            return this.Array + "[" + this.IDX + "]";
        }
    }

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

    [Serializable]
    public class Conditional : Boolean
    {
        public Conditional(Boolean first, Operand second, Operand third)
            : base(new SharpOS.AOT.IR.Operators.Boolean(Operator.BooleanType.Conditional), first, second, third)
        {
        }
    }

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

    [Serializable]
    public class Identifier : Operand
    {
        public Identifier(string name, int index)
        {
            this.index = index;
            this.value = name;
        }

        public Identifier(string name, Operand[] operands)
            : base(null, operands)
        {
            this.value = name;
        }

        public override Operand[] Operands
        {
            get
            {
                return new Operand[] { this };
            }
        }

        private int index = 0;

        public int Index
        {
            get { return this.index; }
        }

        private string value;

        public string Value
        {
            get
            {
                return this.value;
            }
        }

        public override bool Equals(object obj)
        {
            return (obj is Identifier == true
                && this.ToString().Equals((obj as Identifier).ToString()) == true);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(this.Value.ToString());

            stringBuilder.Append("_" + this.Version);

            return stringBuilder.ToString();
        }
    }

    [Serializable]
    public class ExceptionValue : Identifier
    {
        public ExceptionValue()
            : base("ExceptionValue", 0)
        {
        }
    }

    [Serializable]
    public class Field : Identifier
    {
        public Field(string name, Operand operand)
            : base(name, new Operand[] { operand })
        {
        }

        public Field(string name)
            : base(name, 0)
        {
        }
    }

    [Serializable]
    public class Register : Identifier
    {
        public Register(int i)
            : base("Reg" + i, i)
        {
        }
    }

    [Serializable]
    public class Reference : Identifier
    {
        public Reference(Operand operand): base("ref", new Operand[] { operand })
        {

        }
    }

    [Serializable]
    public class Argument : Identifier
    {
        public Argument(int i)
            : base("Arg" + i, i)
        {
        }
    }

    [Serializable]
    public class Local : Identifier
    {
        public Local(int i)
            : base("Loc" + i, i)
        {
        }
    }

    [Serializable]
    public class Miscellaneous : Operand
    {
        public Miscellaneous(Operator _operator)
            : base(_operator, null)
        {
        }

        public Miscellaneous(Operator _operator, Operand operand)
            : base(_operator, new Operand[] { operand })
        {
        }

        public Miscellaneous(Operator _operator, Operand first, Operand second)
            : base(_operator, new Operand[] { first, second })
        {
        }

        public Miscellaneous(Operator _operator, Operand[] operands)
            : base(_operator, operands)
        {
        }
    }

    [Serializable]
    public class Call : Operand
    {
        public Call(MethodReference method, Operand[] operands): base(null, operands)
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
