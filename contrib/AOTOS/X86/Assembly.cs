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
    public partial class Assembly: IAssembly
    {
        public Assembly()
        {
        }

        public Assembly(bool bits32)
        {
            this.bits32 = bits32;
        }

        private bool bits32 = true;

        public bool Bits32
        {
            get { return bits32; }
        }

        protected List<Instruction> instructions = new List<Instruction>();

        public Instruction this[int index]
        {
            get
            {
                return this.instructions[index];
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (Instruction instruction in this.instructions)
            {
                stringBuilder.Append(instruction.ToString() + "\n");
            }

            return stringBuilder.ToString();
        }

        public void BITS32(bool value) 
        {
            this.instructions.Add(new Bits32Instruction(value));
        }

        public void DATA(string name, string values) 
        {
            this.instructions.Add(new ByteDataInstruction(name, values));
        }

        public void DATA(string name, byte value) 
        {
            this.instructions.Add(new ByteDataInstruction(name, value));
        }

        public void DATA(string name, UInt16 value) 
        {
            this.instructions.Add(new WordDataInstruction(name, value));
        }

        public void DATA(string name, UInt32 value) 
        {
            this.instructions.Add(new DWordDataInstruction(name, value)); 
        }

        public void DATA(string values) 
        {
            this.instructions.Add(new ByteDataInstruction(values));
        }

        public void DATA(byte value) 
        {
            this.instructions.Add(new ByteDataInstruction(value)); 
        }

        public void DATA(UInt16 value) 
        {
            this.instructions.Add(new WordDataInstruction(value));
        }

        public void DATA(UInt32 value) 
        {
            this.instructions.Add(new DWordDataInstruction(value));
        }
        
        public void OFFSET(UInt32 value) 
        {
            this.instructions.Add(new OffsetInstruction(value));
        }

        public void ORG(UInt32 value)
        {
            this.instructions.Add(new OrgInstruction(value));
        }

        public void LABEL(string label)
        {
            this.instructions.Add(new LabelInstruction(label));
        }

        public void MOV(R16Type target, string label)
        {
            this.instructions.Add(new Instruction(true, string.Empty, label, "MOV", target.ToString() + ", " + label, null, null, target, new UInt32[] { 0 }, new string[] { "o16", "B8+r", "iw" }));
        }

        public void MOV(R32Type target, string label)
        {
            this.instructions.Add(new Instruction(true, string.Empty, label, "MOV", target.ToString() + ", " + label, null, null, target, new UInt32[] { 0 }, new string[] { "o32", "B8+r", "id" }));
        }

        private UInt32 GetLabelAddress(string label)
        {
            UInt32 address = 0;
            bool found = false;

            foreach (Instruction instruction in this.instructions)
            {
                if (instruction is Bits32Instruction)
                {
                    this.bits32 = (bool)instruction.Value;
                }

                if (instruction.Label.ToLower().Equals(label.ToLower()) == true)
                {
                    found = true;
                    break;
                }

                if (instruction is OffsetInstruction)
                {
                    address = (UInt32)instruction.Value;
                }
                else
                {
                    address += instruction.Size(this.bits32);
                }
            }

            if (found == false)
            {
                throw new Exception("Label '" + label + "' has not been found.");
            }

            return address;
        }

        public bool Encode(Engine engine, string target)
        {
            MemoryStream memoryStream = new MemoryStream();

            foreach (Method method in engine)
            {
                this.GetAssemblyCode(method);
            }

            this.Encode(memoryStream);
            
            FileStream fileStream = new FileStream(target, FileMode.Create);
            memoryStream.WriteTo(fileStream);
            fileStream.Close();
            
            return true;
        }

        public bool Encode(MemoryStream memoryStream)
        {
            UInt32 org = 0;

            BinaryWriter binaryWriter = new BinaryWriter(memoryStream);

            foreach (Instruction instruction in this.instructions)
            {
                if (instruction is OrgInstruction)
                {
                    org = (UInt32)instruction.Value;
                }

                if (instruction is Bits32Instruction)
                {
                    this.bits32 = (bool) instruction.Value;
                }
                 
                if (instruction.Reference.Length > 0)
                {
                    instruction.Value = new UInt32[] { this.GetLabelAddress(instruction.Reference)};

                    if (instruction.Relative == false)
                    {
                        ((UInt32[])instruction.Value)[0] += org;
                    }
                }

                if (instruction.RMMemory != null && instruction.RMMemory.Reference.Length > 0)
                {
                    instruction.RMMemory.Displacement = (int) (org + this.GetLabelAddress(instruction.RMMemory.Reference));
                }
            }

            foreach (Instruction instruction in this.instructions)
            {
                if (instruction is Bits32Instruction)
                {
                    this.bits32 = (bool)instruction.Value;
                }

                if (instruction is OffsetInstruction)
                {
                    UInt32 offset = (UInt32)instruction.Value;

                    if (offset < binaryWriter.BaseStream.Length)
                    {
                        throw new Exception("Wrong offset '" + offset.ToString() + "'.");
                    }

                    while (binaryWriter.BaseStream.Length < offset)
                    {
                        binaryWriter.Write((byte)0);
                    }
                    
                    continue;
                }

                instruction.Encode(this.bits32, binaryWriter);
            }

            return true;
        }

        public Memory GetMemory(SharpOS.AOT.IR.Operands.Call call)
        {
            if (call.Operands.Length == 1)
            {
                return new Memory((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
            }
            else if (call.Operands.Length == 2)
            {
                return new Memory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), (call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
            }
            else if (call.Operands.Length == 3)
            {
                return new Memory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R16.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R16.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
            }
            else if (call.Operands.Length == 4)
            {
                if (call.Method.Parameters[1].ParameterType.FullName.IndexOf("16") != -1)
                {
                    return new Memory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                        , R16.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                        , R16.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                        , Convert.ToInt16((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value));
                }
                else
                {
                    return new Memory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , Convert.ToByte((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value));
                }
            }
            else if (call.Operands.Length == 5)
            {
                return new Memory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , Convert.ToByte((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value)
                    , Convert.ToInt32((call.Operands[4] as SharpOS.AOT.IR.Operands.Constant).Value));
            }
            else
            {
                throw new Exception("'" + call.Method.Name + "' has wrong parameters.");
            }
        }

        public ByteMemory GetByteMemory(SharpOS.AOT.IR.Operands.Call call)
        {
            if (call.Operands.Length == 1)
            {
                return new ByteMemory((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
            }
            else if (call.Operands.Length == 2)
            {
                return new ByteMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), (call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
            }
            else if (call.Operands.Length == 3)
            {
                return new ByteMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R16.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R16.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
            }
            else if (call.Operands.Length == 4)
            {
                if (call.Method.Parameters[1].ParameterType.FullName.IndexOf("16") != -1)
                {
                    return new ByteMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                        , R16.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                        , R16.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                        , Convert.ToInt16((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value));
                }
                else
                {
                    return new ByteMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , Convert.ToByte((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value));
                }
            }
            else if (call.Operands.Length == 5)
            {
                return new ByteMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , Convert.ToByte((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value)
                    , Convert.ToInt32((call.Operands[4] as SharpOS.AOT.IR.Operands.Constant).Value));
            }
            else
            {
                throw new Exception("'" + call.Method.Name + "' has wrong parameters.");
            }
        }

        public WordMemory GetWordMemory(SharpOS.AOT.IR.Operands.Call call)
        {
            if (call.Operands.Length == 1)
            {
                return new WordMemory((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
            }
            else if (call.Operands.Length == 2)
            {
                return new WordMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), (call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
            }
            else if (call.Operands.Length == 3)
            {
                return new WordMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R16.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R16.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
            }
            else if (call.Operands.Length == 4)
            {
                if (call.Method.Parameters[1].ParameterType.FullName.IndexOf("16") != -1)
                {
                    return new WordMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                        , R16.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                        , R16.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                        , Convert.ToInt16((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value));
                }
                else
                {
                    return new WordMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , Convert.ToByte((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value));
                }
            }
            else if (call.Operands.Length == 5)
            {
                return new WordMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , Convert.ToByte((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value)
                    , Convert.ToInt32((call.Operands[4] as SharpOS.AOT.IR.Operands.Constant).Value));
            }
            else
            {
                throw new Exception("'" + call.Method.Name + "' has wrong parameters.");
            }
        }

        public DWordMemory GetDWordMemory(SharpOS.AOT.IR.Operands.Call call)
        {
            if (call.Operands.Length == 1)
            {
                return new DWordMemory((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
            }
            else if (call.Operands.Length == 2)
            {
                return new DWordMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), (call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
            }
            else if (call.Operands.Length == 3)
            {
                return new DWordMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R16.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R16.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
            }
            else if (call.Operands.Length == 4)
            {
                if (call.Method.Parameters[1].ParameterType.FullName.IndexOf("16") != -1)
                {
                    return new DWordMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                        , R16.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                        , R16.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                        , Convert.ToInt16((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value));
                }
                else
                {
                    return new DWordMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , Convert.ToByte((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value));
                }
            }
            else if (call.Operands.Length == 5)
            {
                return new DWordMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , Convert.ToByte((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value)
                    , Convert.ToInt32((call.Operands[4] as SharpOS.AOT.IR.Operands.Constant).Value));
            }
            else
            {
                throw new Exception("'" + call.Method.Name + "' has wrong parameters.");
            }
        }

        public QWordMemory GetQWordMemory(SharpOS.AOT.IR.Operands.Call call)
        {
            if (call.Operands.Length == 1)
            {
                return new QWordMemory((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
            }
            else if (call.Operands.Length == 2)
            {
                return new QWordMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), (call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
            }
            else if (call.Operands.Length == 3)
            {
                return new QWordMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R16.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R16.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
            }
            else if (call.Operands.Length == 4)
            {
                if (call.Method.Parameters[1].ParameterType.FullName.IndexOf("16") != -1)
                {
                    return new QWordMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                        , R16.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                        , R16.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                        , Convert.ToInt16((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value));
                }
                else
                {
                    return new QWordMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , Convert.ToByte((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value));
                }
            }
            else if (call.Operands.Length == 5)
            {
                return new QWordMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , Convert.ToByte((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value)
                    , Convert.ToInt32((call.Operands[4] as SharpOS.AOT.IR.Operands.Constant).Value));
            }
            else
            {
                throw new Exception("'" + call.Method.Name + "' has wrong parameters.");
            }
        }

        public TWordMemory GetTWordMemory(SharpOS.AOT.IR.Operands.Call call)
        {
            if (call.Operands.Length == 1)
            {
                return new TWordMemory((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
            }
            else if (call.Operands.Length == 2)
            {
                return new TWordMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()), (call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
            }
            else if (call.Operands.Length == 3)
            {
                return new TWordMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R16.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R16.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString()));
            }
            else if (call.Operands.Length == 4)
            {
                if (call.Method.Parameters[1].ParameterType.FullName.IndexOf("16") != -1)
                {
                    return new TWordMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                        , R16.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                        , R16.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                        , Convert.ToInt16((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value));
                }
                else
                {
                    return new TWordMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , Convert.ToByte((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value));
                }
            }
            else if (call.Operands.Length == 5)
            {
                return new TWordMemory(Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , R32.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Constant).Value.ToString())
                    , Convert.ToByte((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value)
                    , Convert.ToInt32((call.Operands[4] as SharpOS.AOT.IR.Operands.Constant).Value));
            }
            else
            {
                throw new Exception("'" + call.Method.Name + "' has wrong parameters.");
            }
        }

        /*private void SupportedType(string methodName, string name)
        {
            if (name.Equals("System.Byte") != true
                && name.Equals("System.Byte*") != true
                && name.Equals("System.UInt16") != true
                && name.Equals("System.UInt16*") != true
                && name.Equals("System.Int16") != true
                && name.Equals("System.UInt32") != true
                && name.Equals("System.Int32") != true
                && name.Equals("System.Boolean") != true)
            {
                throw new Exception("'" + name + "' is no supported parameter. (" + methodName + ")");
            }
        }*/

        private void SetValue(SharpOS.AOT.IR.Operands.Operand operand, R32Type source)
        {
            if (operand is SharpOS.AOT.IR.Operands.Local)
            {
                Int32 index = -(operand as SharpOS.AOT.IR.Operands.Local).Index * 4;
                this.MOV(new DWordMemory(null, R32.EBP, null, 0, index), source);
            }
            else if (operand is SharpOS.AOT.IR.Operands.Argument)
            {
                Int32 index = 4 + (operand as SharpOS.AOT.IR.Operands.Argument).Index * 4;
                this.MOV(new DWordMemory(null, R32.EBP, null, 0, index), source);
            }
            else if (operand is SharpOS.AOT.IR.Operands.Reference)
            {
                SharpOS.AOT.IR.Operands.Reference reference = operand as SharpOS.AOT.IR.Operands.Reference;
                this.GetValue(R32.ESI, reference.Operands[0]);

                this.MOV(new DWordMemory(null, R32.ESI, null, 0), source);
            }
            else
            {
                throw new Exception("'" + operand.GetType() + "' is not supported.");
            }
        }

        private void GetValue(R32Type target, SharpOS.AOT.IR.Operands.Operand operand)
        {
            if (operand is SharpOS.AOT.IR.Operands.Local)
            {
                Int32 index = -(operand as SharpOS.AOT.IR.Operands.Local).Index * 4;
                this.MOV(target, new DWordMemory(null, R32.EBP, null, 0, index));
            }
            else if (operand is SharpOS.AOT.IR.Operands.Argument)
            {
                Int32 index = 4 + (operand as SharpOS.AOT.IR.Operands.Argument).Index * 4;
                this.MOV(target, new DWordMemory(null, R32.EBP, null, 0, index));
            }
            else if (operand is SharpOS.AOT.IR.Operands.Constant)
            {
                UInt32 value = Convert.ToUInt32((operand as SharpOS.AOT.IR.Operands.Constant).Value);

                this.MOV(target, value);
            }
            else if (operand is SharpOS.AOT.IR.Operands.Arithmetic)
            {
                SharpOS.AOT.IR.Operands.Arithmetic arithmetic = operand as SharpOS.AOT.IR.Operands.Arithmetic;

                // TODO FIX ME!!!
                this.GetValue(R32.EBX, arithmetic.Operands[0]);
                this.GetValue(R32.ECX, arithmetic.Operands[1]);
                this.ADD(R32.EBX, R32.ECX);
                this.MOV(target, R32.EBX);
            }
            else
            {
                throw new Exception("'" + operand.GetType() + "' is not supported.");
            }
        }

        public int AvailableRegistersCount
        {
            get
            {
                return 3;
            }
        }

        public bool Spill(Operand.InternalSizeType type)
        {
            if (type == Operand.InternalSizeType.NotSet)
            {
                throw new Exception("Size Type not set.");
            }

            if (type == Operand.InternalSizeType.I8
                || type == Operand.InternalSizeType.R4
                || type == Operand.InternalSizeType.R8)
            {
                return true;
            }

            return false;
        }

        private bool GetAssemblyCode(Method method)
        {
            /*SupportedType(method.MethodFullName, method.MethodDefinition.ReturnType.ReturnType.FullName);

            foreach (ParameterDefinition parameter in method.MethodDefinition.Parameters)
            {
                SupportedType(method.MethodFullName, parameter.ParameterType.FullName);
            }

            foreach (VariableDefinition variable in method.MethodDefinition.Body.Variables)
            {
                SupportedType(method.MethodFullName, variable.VariableType.FullName);
            }*/

            //blocks.UpdateIndex();

            string fullname = method.MethodDefinition.DeclaringType.FullName + "." + method.MethodDefinition.Name;

            if (method.MethodDefinition.Name.StartsWith("BootSector") == false)
            {
                this.LABEL(fullname);
                this.PUSH(R32.EBP);
                this.MOV(R32.EBP, R32.ESP);
                this.PUSH(R32.EBX);
                this.PUSH(R32.ESI);
                this.PUSH(R32.EDI);

                if (method.StackSize > 0)
                {
                    this.SUB(R32.ESP, (UInt32) (method.StackSize * 4));
                }
            }

            foreach (Block block in method)
            {
                this.LABEL(fullname + "_" + block.StartOffset.ToString());

                foreach (SharpOS.AOT.IR.Instructions.Instruction instruction in block)
                {
                    if (instruction is SharpOS.AOT.IR.Instructions.Call
                        && (instruction as SharpOS.AOT.IR.Instructions.Call).Method.Method.DeclaringType.FullName.Equals("Nutex.X86.Asm") == true)
                    {
                        SharpOS.AOT.IR.Instructions.Call call = instruction as SharpOS.AOT.IR.Instructions.Call;

                        string parameterTypes = string.Empty;

                        foreach (ParameterDefinition parameter in call.Method.Method.Parameters)
                        {
                            if (parameterTypes.Length > 0)
                            {
                                parameterTypes += " ";
                            }

                            parameterTypes += parameter.ParameterType.Name;
                        }

                        parameterTypes = call.Method.Method.Name + " " + parameterTypes;
                        parameterTypes = parameterTypes.Trim();

                        this.GetAssemblyInstruction(call.Method, parameterTypes);
                    }
                    else if (instruction is SharpOS.AOT.IR.Instructions.Call)
                    {
                        SharpOS.AOT.IR.Instructions.Call call = (instruction as SharpOS.AOT.IR.Instructions.Call);

                        for (int i = 0; i < call.Method.Operands.Length; i++)
                        {
                            Operand operand = call.Method.Operands[call.Method.Operands.Length - i - 1];

                            if (operand is Arithmetic)
                            {
                                this.GetValue(R32.EAX, operand);
                                this.PUSH(R32.EAX);
                            }
                            else if (operand is Argument)
                            {
                                this.GetValue(R32.EAX, operand);
                                this.PUSH(R32.EAX);
                            }
                            else if (operand is Constant)
                            {
                                this.PUSH(Convert.ToUInt32((operand as Constant).Value));
                            }
                            else
                            {
                                // TODO check for other type of operands
                                throw new Exception("'" + call.Method.Operands[i].GetType() + "' is not supported.");
                            }
                        }

                        this.CALL(call.Method.Method.DeclaringType.FullName + "." + call.Method.Method.Name);
                        this.ADD(R32.ESP, (UInt32)(4 * call.Method.Method.Parameters.Count));
                    }
                    else if (instruction is SharpOS.AOT.IR.Instructions.Assign)
                    {
                        SharpOS.AOT.IR.Instructions.Assign assign = (instruction as SharpOS.AOT.IR.Instructions.Assign);

                        this.GetValue(R32.EAX, assign.Value);
                        this.SetValue(assign.Asignee, R32.EAX);
                    }
                    else if (instruction is SharpOS.AOT.IR.Instructions.ConditionalJump)
                    {
                        SharpOS.AOT.IR.Instructions.ConditionalJump jump = instruction as SharpOS.AOT.IR.Instructions.ConditionalJump;

                        string label = fullname + "_" + block.Outs[0].StartOffset.ToString();

                        if (jump.Value is SharpOS.AOT.IR.Operands.Boolean)
                        {
                            SharpOS.AOT.IR.Operands.Boolean expression = jump.Value as SharpOS.AOT.IR.Operands.Boolean;

                            if (expression.Operator is SharpOS.AOT.IR.Operators.Relational)
                            {
                                SharpOS.AOT.IR.Operators.Relational relational = expression.Operator as SharpOS.AOT.IR.Operators.Relational;

                                this.GetValue(R32.EAX, expression.Operands[0]);
                                this.GetValue(R32.EBX, expression.Operands[1]);

                                this.CMP(R32.EAX, R32.EBX);

                                if (relational.Type == Operator.RelationalType.Equal)
                                {
                                    this.JE(label);
                                }
                                else if (relational.Type == Operator.RelationalType.LessThan)
                                {
                                    this.JL(label);
                                }
                                else if (relational.Type == Operator.RelationalType.GreaterThan)
                                {
                                    this.JG(label);
                                }
                                else if (relational.Type == Operator.RelationalType.LessThanOrEqual)
                                {
                                    this.JLE(label);
                                }
                                else if (relational.Type == Operator.RelationalType.GreaterThanOrEqual)
                                {
                                    this.JGE(label);
                                }
                                else
                                {
                                    throw new Exception("'" + relational.Type + "' is not supported.");
                                }
                            }
                            else
                            {
                                throw new Exception("'" + expression.Operator.GetType() + "' is not supported.");
                            }
                        }
                        else
                        {
                            throw new Exception("'" + jump.Value.GetType() + "' is not supported.");
                        }
                    }
                    else if (instruction is SharpOS.AOT.IR.Instructions.Jump)
                    {
                        SharpOS.AOT.IR.Instructions.Jump jump = instruction as SharpOS.AOT.IR.Instructions.Jump;

                        this.JMP(fullname + "_" + block.Outs[0].StartOffset.ToString());
                    }
                }
            }

            if (method.MethodDefinition.Name.StartsWith("BootSector") == false)
            {
                this.LEA(R32.ESP, new DWordMemory(null, R32.EBP, null, 0, -12));
                this.POP(R32.EDI);
                this.POP(R32.ESI);
                this.POP(R32.EBX);
                this.POP(R32.EBP);
                this.RET();
            }

            return true;
        }
    }
}
