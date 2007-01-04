/**
 *  (C) 2006-2007 Mircea-Cristian Racasan <darx_kies@gmx.net>
 * 
 *  Licensed under the terms of the GNU GPL License version 2.
 * 
 */

using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using SharpOS.AOT.IR.Operands;

namespace SharpOS.AOT.IR.Instructions
{
    [Serializable]
    public abstract class Instruction
    {
        public Instruction(Operand value)
        {
            this.value = value;
        }

        private Block block = null;

        public Block Block
        {
            get { return block; }
            set { block = value; }
        }
	

        private Operand value;

        public Operand Value
        {
            get { return value; }
            set { this.value = value; }
        }

        private int index = -1;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        public delegate void InstructionVisitor(SharpOS.AOT.IR.Instructions.Instruction instruction);

        public virtual void VisitInstruction(InstructionVisitor visitor)
        {
            visitor(this);
        }

        public virtual void VisitBlock(Block.BlockVisitor visitor)
        {
        }

        public virtual void UpdateIndex(ref int index)
        {
            this.index = index++;
        }

        public string FormatedIndex
        {
            get
            {
                if (this.index == -1)
                {
                    return string.Empty;
                }

                return String.Format("{0} ", this.index).PadLeft(4);
            }
        }

        public void Replace(Dictionary<string, Operand> registerValues)
        {
            if (this.value == null)
            {
                return;
            }

            if (this.value is Register && registerValues.ContainsKey(this.value.ToString()) == true)
            {
                this.value = registerValues[this.value.ToString()];
            }
            else
            {
                this.value.Replace(registerValues);
            }
        }

        private long startOffset = 0;

        public virtual long StartOffset
        {
            get { return startOffset; }
            set { startOffset = value; }
        }

        private long endOffset = 0;

        public virtual long EndOffset
        {
            get { return endOffset; }
            set { endOffset = value; }
        }

        private List<SharpOS.AOT.IR.Instructions.Instruction> branches = new List<SharpOS.AOT.IR.Instructions.Instruction>();

        public List<SharpOS.AOT.IR.Instructions.Instruction> Branches
        {
            get { return branches; }
            set { branches = value; }
        }
	
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            Dump(string.Empty, stringBuilder);

            string value = stringBuilder.ToString().Trim();

            int position = value.IndexOf(" ");

            /*if (position != -1)
            {
                value = value.Substring(position + 1);
            }*/

            return value;
        }

        public virtual void Dump(string prefix, StringBuilder stringBuilder)
        {
            if (this.value != null)
            {
                stringBuilder.Append(prefix + this.FormatedIndex + this.value.ToString() + "\n");
            }
        }

        public SharpOS.AOT.IR.Instructions.Instruction Clone()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            
            binaryFormatter.Serialize(memoryStream, this);
            memoryStream.Seek(0, SeekOrigin.Begin);
            
            SharpOS.AOT.IR.Instructions.Instruction instruction = (SharpOS.AOT.IR.Instructions.Instruction)binaryFormatter.Deserialize(memoryStream);

            return instruction;
        }
    }

    [Serializable]
    public class End : SharpOS.AOT.IR.Instructions.Instruction
    {
        public static readonly End END = new End();

        private End()
            : base(null)
        {
            this.Index = int.MaxValue;
        }
    }

    [Serializable]
    public class Assign : SharpOS.AOT.IR.Instructions.Instruction
    {
        public Assign(Identifier asignee, Operand value): base(value)
        {
            this.asignee = asignee;
        }

        private Identifier asignee;

        public Identifier Asignee
        {
            get { return asignee; }
        }

        public override void Dump(string prefix, StringBuilder stringBuilder)
        {
            stringBuilder.Append(prefix + this.FormatedIndex + this.asignee + " = " + this.Value + "\n");
        }
    }

    [Serializable]
    public class ConditionalJump : Jump
    {
        public ConditionalJump(Operands.Boolean condition): base(condition)
        {
        }

    }

    [Serializable]
    public class Jump : SharpOS.AOT.IR.Instructions.Instruction
    {
        public Jump()
            :base (null)
        {
        }

        public Jump(Operands.Boolean condition)
            : base(condition)
        {
        }
        
        public override void Dump(string prefix, StringBuilder stringBuilder)
        {
            /*stringBuilder.Append(prefix + this.FormatedIndex + "Jump [");

            for (int i = 0; i < this.Branches.Count; i++)
            {
                if (i > 0)
                {
                    stringBuilder.Append(", ");
                }

                stringBuilder.Append(this.Branches[i].FormatedIndex.Trim());
            }

            stringBuilder.Append("]");*/

            stringBuilder.Append(prefix + this.FormatedIndex + "Jmp");

            if (this.Value != null)
            {
                stringBuilder.Append(" " + this.Value);
            }


            stringBuilder.Append("\n");
        }
    }

    [Serializable]
    public class Pop : SharpOS.AOT.IR.Instructions.Instruction
    {
        public Pop(Identifier identifier) : base(identifier)
        {
        }

        public override void Dump(string prefix, StringBuilder stringBuilder)
        {
            stringBuilder.Append(prefix + this.FormatedIndex + "Pop " + this.Value.ToString() + "\n");
        }
    }

    [Serializable]
    public class Return : SharpOS.AOT.IR.Instructions.Instruction
    {
        public Return()
            : base(null)
        {
        }

        public Return(Operand value): base (value)
        {
        }

        public override void Dump(string prefix, StringBuilder stringBuilder)
        {
            stringBuilder.Append(prefix + this.FormatedIndex + "Ret");

            if (this.Value != null)
            {
                stringBuilder.Append(" " + this.Value.ToString());
            }

            stringBuilder.Append("\n");
        }
    }

    [Serializable]
    public class Switch : SharpOS.AOT.IR.Instructions.Instruction
    {
        public Switch(Operand value): base (value)
        {
        }

        public override void Dump(string prefix, StringBuilder stringBuilder)
        {
            stringBuilder.Append(prefix + this.FormatedIndex + "Switch " + this.Value + "\n");
        }
    }

    [Serializable]
    public class System : SharpOS.AOT.IR.Instructions.Instruction
    {
        public System(Miscellaneous _operator)
            : base(_operator)
        {
        }
    }

    [Serializable]
    public class Call : SharpOS.AOT.IR.Instructions.Instruction
    {
        public Call(SharpOS.AOT.IR.Operands.Call value)
            : base(value)
        {
        }

        public SharpOS.AOT.IR.Operands.Call Method
        {
            get
            {
                return this.Value as SharpOS.AOT.IR.Operands.Call;
            }
        }

        public override void Dump(string prefix, StringBuilder stringBuilder)
        {
            stringBuilder.Append(prefix + this.FormatedIndex + this.Method.ToString() + "\n");
        }
    }

    [Serializable]
    public class PHI : SharpOS.AOT.IR.Instructions.Assign
    {
        public PHI(Identifier asignee, SharpOS.AOT.IR.Operands.Miscellaneous identifiers)
            : base(asignee, identifiers)
        {
        }

        public override void Dump(string prefix, StringBuilder stringBuilder)
        {
            stringBuilder.Append(prefix + this.FormatedIndex);
                
            stringBuilder.Append(this.Asignee.ToString());

            stringBuilder.Append(" = {");

            for (int i = 0; i < this.Value.Operands.Length; i++)
            {
                if (i > 0)
                {
                    stringBuilder.Append(", ");
                }

                stringBuilder.Append(this.Value.Operands[i].ToString());
            }

            stringBuilder.Append("}\n");
        }
    }
}
