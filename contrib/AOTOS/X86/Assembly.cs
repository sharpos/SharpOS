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

        public bool IsRegister(string value)
        {
            return value.StartsWith("SharpOS.AOT.X86.");
        }

        public bool IsInstruction(string value)
        {
            return value.StartsWith("SharpOS.AOT.X86.");
        }

        private bool IsAssemblyStub(string value)
        {
            return value.Equals("SharpOS.AOT.X86.Asm");
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

        private Memory GetMemoryInternal(SharpOS.AOT.IR.Operands.Call call)
        {
            if (call.Operands.Length == 1)
            {
                string parameter = (call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString();

                if (call.Method.DeclaringType.FullName.EndsWith(".Memory") == true)
                {
                    return new Memory(parameter);
                }
                else if (call.Method.DeclaringType.FullName.EndsWith(".ByteMemory") == true)
                {
                    return new ByteMemory(parameter);
                }
                else if (call.Method.DeclaringType.FullName.EndsWith(".WordMemory") == true)
                {
                    return new WordMemory(parameter);
                }
                else if (call.Method.DeclaringType.FullName.EndsWith(".DWordMemory") == true)
                {
                    return new DWordMemory(parameter);
                }
                else if (call.Method.DeclaringType.FullName.EndsWith(".QWordMemory") == true)
                {
                    return new QWordMemory(parameter);
                }
                else if (call.Method.DeclaringType.FullName.EndsWith(".TWordMemory") == true)
                {
                    return new TWordMemory(parameter);
                }
                else
                {
                    throw new Exception("'" + call.Method.DeclaringType.FullName + "' is not supported.");
                }
            }
            else if (call.Operands.Length == 2)
            {
                SegType segment = Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
                string label = (call.Operands[1] as SharpOS.AOT.IR.Operands.Constant).Value.ToString();
                
                if (call.Method.DeclaringType.FullName.EndsWith(".Memory") == true)
                {
                    return new Memory(segment, label);
                }
                else if (call.Method.DeclaringType.FullName.EndsWith(".ByteMemory") == true)
                {
                    return new ByteMemory(segment, label);
                }
                else if (call.Method.DeclaringType.FullName.EndsWith(".WordMemory") == true)
                {
                    return new WordMemory(segment, label);
                }
                else if (call.Method.DeclaringType.FullName.EndsWith(".DWordMemory") == true)
                {
                    return new DWordMemory(segment, label);
                }
                else if (call.Method.DeclaringType.FullName.EndsWith(".QWordMemory") == true)
                {
                    return new QWordMemory(segment, label);
                }
                else if (call.Method.DeclaringType.FullName.EndsWith(".TWordMemory") == true)
                {
                    return new TWordMemory(segment, label);
                }
                else
                {
                    throw new Exception("'" + call.Method.DeclaringType.FullName + "' is not supported.");
                }
            }
            else if (call.Operands.Length == 3)
            {
                SegType segment = Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
                R16Type _base = R16.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Field).Value.ToString());
                R16Type index = R16.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Field).Value.ToString());
                
                if (call.Method.DeclaringType.FullName.EndsWith(".Memory") == true)
                {
                    return new Memory(segment, _base, index);
                }
                else if (call.Method.DeclaringType.FullName.EndsWith(".ByteMemory") == true)
                {
                    return new ByteMemory(segment, _base, index);
                }
                else if (call.Method.DeclaringType.FullName.EndsWith(".WordMemory") == true)
                {
                    return new WordMemory(segment, _base, index);
                }
                else if (call.Method.DeclaringType.FullName.EndsWith(".DWordMemory") == true)
                {
                    return new DWordMemory(segment, _base, index);
                }
                else if (call.Method.DeclaringType.FullName.EndsWith(".QWordMemory") == true)
                {
                    return new QWordMemory(segment, _base, index);
                }
                else if (call.Method.DeclaringType.FullName.EndsWith(".TWordMemory") == true)
                {
                    return new TWordMemory(segment, _base, index);
                }
                else
                {
                    throw new Exception("'" + call.Method.DeclaringType.FullName + "' is not supported.");
                }
            }
            else if (call.Operands.Length == 4)
            {
                if (call.Method.Parameters[1].ParameterType.FullName.IndexOf("16") != -1)
                {
                    SegType segment = Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
                    R16Type _base = R16.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Field).Value.ToString());
                    R16Type index = R16.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Field).Value.ToString());
                    Int16 displacement = Convert.ToInt16((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value);

                    if (call.Method.DeclaringType.FullName.EndsWith(".Memory") == true)
                    {
                        return new Memory(segment, _base, index, displacement);
                    }
                    else if (call.Method.DeclaringType.FullName.EndsWith(".ByteMemory") == true)
                    {
                        return new ByteMemory(segment, _base, index, displacement);
                    }
                    else if (call.Method.DeclaringType.FullName.EndsWith(".WordMemory") == true)
                    {
                        return new WordMemory(segment, _base, index, displacement);
                    }
                    else if (call.Method.DeclaringType.FullName.EndsWith(".DWordMemory") == true)
                    {
                        return new DWordMemory(segment, _base, index, displacement);
                    }
                    else if (call.Method.DeclaringType.FullName.EndsWith(".QWordMemory") == true)
                    {
                        return new QWordMemory(segment, _base, index, displacement);
                    }
                    else if (call.Method.DeclaringType.FullName.EndsWith(".TWordMemory") == true)
                    {
                        return new TWordMemory(segment, _base, index, displacement);
                    }
                    else
                    {
                        throw new Exception("'" + call.Method.DeclaringType.FullName + "' is not supported.");
                    }
                }
                else
                {
                    SegType segment = Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
                    R32Type _base = R32.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Field).Value.ToString());
                    R32Type index = R32.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Field).Value.ToString());
                    Byte scale = Convert.ToByte((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value);

                    if (call.Method.DeclaringType.FullName.EndsWith(".Memory") == true)
                    {
                        return new Memory(segment, _base, index, scale);
                    }
                    else if (call.Method.DeclaringType.FullName.EndsWith(".ByteMemory") == true)
                    {
                        return new ByteMemory(segment, _base, index, scale);
                    }
                    else if (call.Method.DeclaringType.FullName.EndsWith(".WordMemory") == true)
                    {
                        return new WordMemory(segment, _base, index, scale);
                    }
                    else if (call.Method.DeclaringType.FullName.EndsWith(".DWordMemory") == true)
                    {
                        return new DWordMemory(segment, _base, index, scale);
                    }
                    else if (call.Method.DeclaringType.FullName.EndsWith(".QWordMemory") == true)
                    {
                        return new QWordMemory(segment, _base, index, scale);
                    }
                    else if (call.Method.DeclaringType.FullName.EndsWith(".TWordMemory") == true)
                    {
                        return new TWordMemory(segment, _base, index, scale);
                    }
                    else
                    {
                        throw new Exception("'" + call.Method.DeclaringType.FullName + "' is not supported.");
                    }
                }
            }
            else if (call.Operands.Length == 5)
            {
                SegType segment = Seg.GetByID((call.Operands[0] as SharpOS.AOT.IR.Operands.Constant).Value.ToString());
                R32Type _base = R32.GetByID((call.Operands[1] as SharpOS.AOT.IR.Operands.Field).Value.ToString());
                R32Type index = call.Operands[2] is Constant? null: R32.GetByID((call.Operands[2] as SharpOS.AOT.IR.Operands.Field).Value.ToString());
                Byte scale = Convert.ToByte((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value);
                Int32 displacement = Convert.ToInt32((call.Operands[4] as SharpOS.AOT.IR.Operands.Constant).Value);

                if (call.Method.DeclaringType.FullName.EndsWith(".Memory") == true)
                {
                    return new Memory(segment, _base, index, scale, displacement);
                }
                else if (call.Method.DeclaringType.FullName.EndsWith(".ByteMemory") == true)
                {
                    return new ByteMemory(segment, _base, index, scale, displacement);
                }
                else if (call.Method.DeclaringType.FullName.EndsWith(".WordMemory") == true)
                {
                    return new WordMemory(segment, _base, index, scale, displacement);
                }
                else if (call.Method.DeclaringType.FullName.EndsWith(".DWordMemory") == true)
                {
                    return new DWordMemory(segment, _base, index, scale, displacement);
                }
                else if (call.Method.DeclaringType.FullName.EndsWith(".QWordMemory") == true)
                {
                    return new QWordMemory(segment, _base, index, scale, displacement);
                }
                else if (call.Method.DeclaringType.FullName.EndsWith(".TWordMemory") == true)
                {
                    return new TWordMemory(segment, _base, index, scale, displacement);
                }
                else
                {
                    throw new Exception("'" + call.Method.DeclaringType.FullName + "' is not supported.");
                }
            }
            else
            {
                throw new Exception("'" + call.Method.Name + "' has wrong parameters.");
            }
        }

        public Memory GetMemory(SharpOS.AOT.IR.Operands.Call call)
        {
            return GetMemoryInternal(call);
        }

        public ByteMemory GetByteMemory(SharpOS.AOT.IR.Operands.Call call)
        {
            return GetMemoryInternal(call) as ByteMemory;
        }

        public WordMemory GetWordMemory(SharpOS.AOT.IR.Operands.Call call)
        {
            return GetMemoryInternal(call) as WordMemory;
        }

        public DWordMemory GetDWordMemory(SharpOS.AOT.IR.Operands.Call call)
        {
            return GetMemoryInternal(call) as DWordMemory;
        }

        public QWordMemory GetQWordMemory(SharpOS.AOT.IR.Operands.Call call)
        {
            return GetMemoryInternal(call) as QWordMemory;
        }

        public TWordMemory GetTWordMemory(SharpOS.AOT.IR.Operands.Call call)
        {
            return GetMemoryInternal(call) as TWordMemory;
        }

        /*private void SetValuex(SharpOS.AOT.IR.Operands.Operand operand, R32Type source)
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
        }*/

        /*private void GetValuex(R32Type target, SharpOS.AOT.IR.Operands.Operand operand)
        {
            if (operand is SharpOS.AOT.IR.Operands.Local
                || operand is SharpOS.AOT.IR.Operands.Register)
            {
                if (operand.IsRegisterSet == true)
                {
                    this.MOV(target, this.GetRegister(operand.Register));
                }
                else
                {
                    Int32 index = -(operand as SharpOS.AOT.IR.Operands.Local).Index * 4;

                    this.MOV(target, new DWordMemory(null, R32.EBP, null, 0, index));
                }
            }
            else if (operand is SharpOS.AOT.IR.Operands.Argument)
            {
                this.MOV(target, GetArgument((operand as SharpOS.AOT.IR.Operands.Argument).Index));
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
        }*/

        private DWordMemory GetArgument(int index)
        {
            return new DWordMemory(null, R32.EBP, null, 0, 4 + index * 4);
        }

        private SharpOS.AOT.X86.R32Type GetRegister(int i)
        {
            switch (i)
            {
                case 0:
                    return R32.EBX;

                case 1:
                    return R32.ESI;

                case 2:
                    return R32.EDI;

                default:
                    throw new Exception("'" + i.ToString() + "' is no valid register.");
            }
        }

        private SharpOS.AOT.X86.DWordMemory GetStackAddress(int i)
        {
            return new DWordMemory(null, R32.EBP, null, 0, -((3 + i) * 4));
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
            string fullname = method.MethodFullName;

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
                    if (instruction is SharpOS.AOT.IR.Instructions.Call == true
                        && this.IsAssemblyStub((instruction as SharpOS.AOT.IR.Instructions.Call).Method.Method.DeclaringType.FullName) == true)
                    {
                        HandleAssemblyStub(method, block, instruction);
                    }
                    else if (instruction is SharpOS.AOT.IR.Instructions.Call == true)
                    {
                        HandleCall(method, block, instruction);
                    }
                    else if (instruction is SharpOS.AOT.IR.Instructions.Assign == true)
                    {
                        HandleAssign(method, block, instruction);
                    }
                    else if (instruction is SharpOS.AOT.IR.Instructions.ConditionalJump == true)
                    {
                        HandleConditionalJump(method, block, instruction);
                    }
                    else if (instruction is SharpOS.AOT.IR.Instructions.Jump == true)
                    {
                        HandleJump(method, block, instruction);
                    }
                    else if (instruction is SharpOS.AOT.IR.Instructions.Return == true)
                    {
                        HandleReturn(method, block, instruction);
                    }
                    else
                    {
                        throw new Exception("'" + instruction + "' is not supported.");
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

        private void HandleConditionalJump(Method method, Block block, SharpOS.AOT.IR.Instructions.Instruction instruction)
        {
            SharpOS.AOT.IR.Instructions.ConditionalJump jump = instruction as SharpOS.AOT.IR.Instructions.ConditionalJump;

            string label = method.MethodFullName + "_" + block.Outs[0].StartOffset.ToString();

            if (jump.Value is SharpOS.AOT.IR.Operands.Boolean)
            {
                SharpOS.AOT.IR.Operands.Boolean expression = jump.Value as SharpOS.AOT.IR.Operands.Boolean;

                if (expression.Operator is SharpOS.AOT.IR.Operators.Relational == true)
                {
                    // TODO i4, i8, r4, r8 check
                    /*SharpOS.AOT.IR.Operators.Relational relational = expression.Operator as SharpOS.AOT.IR.Operators.Relational;

                    this.GetValue(R32.EAX, expression.Operands[0]);
                    this.GetValue(R32.EBX, expression.Operands[1]);

                    this.CMP(R32.EAX, R32.EBX);

                    if (relational.Type == Operator.RelationalType.Equal)
                    {
                        this.JE(label);
                    }
                    else if (relational.Type == Operator.RelationalType.NotEqualOrUnordered)
                    {
                        this.JNE(label);
                    }

                    else if (relational.Type == Operator.RelationalType.LessThan)
                    {
                        this.JL(label);
                    }
                    else if (relational.Type == Operator.RelationalType.LessThanOrEqual)
                    {
                        this.JLE(label);
                    }
                    
                    else if (relational.Type == Operator.RelationalType.GreaterThan)
                    {
                        this.JG(label);
                    }
                    else if (relational.Type == Operator.RelationalType.GreaterThanOrEqual)
                    {
                        this.JGE(label);
                    }

                    else if (relational.Type == Operator.RelationalType.LessThanUnsignedOrUnordered)
                    {
                        this.JB(label);
                    }
                    else if (relational.Type == Operator.RelationalType.LessThanOrEqualUnsignedOrUnordered)
                    {
                        this.JBE(label);
                    }

                    else if (relational.Type == Operator.RelationalType.GreaterThanUnsignedOrUnordered)
                    {
                        this.JA(label);
                    }
                    else if (relational.Type == Operator.RelationalType.GreaterThanOrEqualUnsignedOrUnordered)
                    {
                        this.JAE(label);
                    }
                    else
                    {
                        throw new Exception("'" + relational.Type + "' is not supported.");
                    }*/
                }
                else if (expression.Operator is SharpOS.AOT.IR.Operators.Boolean == true)
                {
                    // TODO i4, i8, r4, r8 check?

                    SharpOS.AOT.IR.Operators.Boolean boolean = expression.Operator as SharpOS.AOT.IR.Operators.Boolean;

                    if (expression.Operands[0].IsRegisterSet == true)
                    {
                        R32Type register = this.GetRegister(expression.Operands[0].Register);

                        this.TEST(register, register);
                    }
                    else
                    {
                        this.MOV(R32.EAX, this.GetStackAddress(expression.Operands[0].Stack));

                        this.TEST(R32.EAX, R32.EAX);
                    }

                    if (boolean.Type == Operator.BooleanType.True)
                    {
                        this.JNE(label);
                    }
                    else if (boolean.Type == Operator.BooleanType.False)
                    {
                        this.JE(label);
                    }
                    else
                    {
                        throw new Exception("'" + expression.Operator.GetType() + "' is not supported.");
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

        private void HandleJump(Method method, Block block, SharpOS.AOT.IR.Instructions.Instruction instruction)
        {
            SharpOS.AOT.IR.Instructions.Jump jump = instruction as SharpOS.AOT.IR.Instructions.Jump;

            this.JMP(method.MethodFullName + "_" + block.Outs[0].StartOffset.ToString());
        }

        private void HandleAssemblyStub(Method method, Block block, SharpOS.AOT.IR.Instructions.Instruction instruction)
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

        private void HandleCall(Method method, Block block, SharpOS.AOT.IR.Instructions.Instruction instruction)
        {
            SharpOS.AOT.IR.Instructions.Call call = (instruction as SharpOS.AOT.IR.Instructions.Call);

            for (int i = 0; i < call.Method.Operands.Length; i++)
            {
                Operand operand = call.Method.Operands[call.Method.Operands.Length - i - 1];

                if (operand is Arithmetic == true)
                {
                    if (operand.SizeType == Operand.InternalSizeType.I1
                        || operand.SizeType == Operand.InternalSizeType.I2
                        || operand.SizeType == Operand.InternalSizeType.I4)
                    {
                        this.MovRegisterArithmetic(R32.EAX, operand as Arithmetic);
                        
                        this.PUSH(R32.EAX);
                    }
                    else
                    {
                        throw new Exception("'" + operand + "' is not supported.");
                    }
                }
                else if (operand is Argument == true)
                {
                    if (operand.SizeType == Operand.InternalSizeType.I1
                        || operand.SizeType == Operand.InternalSizeType.I2
                        || operand.SizeType == Operand.InternalSizeType.I4)
                    {
                        this.PUSH(GetArgument((operand as SharpOS.AOT.IR.Operands.Argument).Index));
                    }
                    else
                    {
                        throw new Exception("'" + operand + "' is not supported.");
                    }
                }
                else if (operand is Constant == true)
                {
                    if (operand.SizeType == Operand.InternalSizeType.I1
                        || operand.SizeType == Operand.InternalSizeType.I2
                        || operand.SizeType == Operand.InternalSizeType.I4)
                    {
                        this.PUSH(Convert.ToUInt32((operand as Constant).Value));
                    }
                    else
                    {
                        throw new Exception("'" + operand + "' is not supported.");
                    }
                }
                else
                {
                    throw new Exception("'" + call.Method.Operands[i].GetType() + "' is not supported.");
                }
            }

            this.CALL(call.Method.Method.DeclaringType.FullName + "." + call.Method.Method.Name);
            this.ADD(R32.ESP, (UInt32)(4 * call.Method.Method.Parameters.Count));
        }

        private void MovRegisterOperand(R32Type register, Operand operand)
        {
            if (operand.IsRegisterSet)
            {
                this.MovRegisterRegister(register, this.GetRegister(operand.Register));
            }
            else
            {
                this.MOV(register, this.GetStackAddress(operand.Stack));
            }
        }

        private void MovRegisterArithmetic(R32Type register, Arithmetic operand)
        {
            if (operand.Operator is Binary == true)
            {
                Binary.BinaryType type = (operand.Operator as Binary).Type;

                if (type == Binary.BinaryType.Add)
                {
                    // TODO
                }
                else if (type == Binary.BinaryType.Mul)
                {
                    // TODO
                }
                else
                {
                    throw new Exception("'" + type + "' is not supported.");
                }
            }
            else
            {
                throw new Exception("'" + operand.Operator + "' is not supported.");
            }
        }

        private void MovRegisterConstant(R32Type register, UInt32 constant)
        {
            if (constant == 0)
            {
                this.XOR(register, register);
            }
            else
            {
                this.MOV(register, constant);
            }
        }

        private void MovMemoryConstant(DWordMemory address, UInt32 constant)
        {
            if (constant == 0)
            {
                this.XOR(R32.EDX, R32.EDX);
                this.MOV(address, R32.EDX);
            }
            else
            {
                this.MOV(address, constant);
            }
            
        }

        private void MovRegisterRegister(R32Type target, R32Type source)
        {
            if (target != source)
            {
                this.MOV(target, source);
            }
        }

        private void HandleAssign(Method method, Block block, SharpOS.AOT.IR.Instructions.Instruction instruction)
        {
            SharpOS.AOT.IR.Instructions.Assign assign = (instruction as SharpOS.AOT.IR.Instructions.Assign);

            if (assign.Asignee is Reference == true)
            {
                // TODO
            }
            else if (assign.Value is Constant == true)
            {
                if (assign.Asignee.SizeType == Operand.InternalSizeType.I1
                    || assign.Asignee.SizeType == Operand.InternalSizeType.I2
                    || assign.Asignee.SizeType == Operand.InternalSizeType.I4)
                {
                    if (assign.Asignee.IsRegisterSet == true)
                    {
                        this.MovRegisterConstant(this.GetRegister(assign.Asignee.Register), Convert.ToUInt32((assign.Value as Constant).Value));
                    }
                    else
                    {
                        this.MovMemoryConstant(this.GetStackAddress(assign.Asignee.Stack), Convert.ToUInt32((assign.Value as Constant).Value));
                    }
                }
                else
                {
                    throw new Exception("'" + instruction + "' is not supported.");
                }
            }
            else if (assign.Value is Identifier == true)
            {
                if (assign.Asignee.IsRegisterSet == true
                    && assign.Value.IsRegisterSet == true)
                {
                    this.MovRegisterRegister(this.GetRegister(assign.Asignee.Register), this.GetRegister(assign.Value.Register));
                }
                else if (assign.Asignee.IsRegisterSet == false
                    && assign.Value.IsRegisterSet == false)
                {
                    if (assign.Asignee.SizeType == Operand.InternalSizeType.I1
                        || assign.Asignee.SizeType == Operand.InternalSizeType.I2
                        || assign.Asignee.SizeType == Operand.InternalSizeType.I4)
                    {
                        this.MOV(R32.EAX, this.GetStackAddress(assign.Value.Stack));
                        this.MOV(this.GetStackAddress(assign.Asignee.Stack), R32.EAX);
                    }
                    else
                    {
                        throw new Exception("'" + instruction + "' is not supported.");
                    }
                }
                else if (assign.Asignee.IsRegisterSet == false
                    && assign.Value.IsRegisterSet == true)
                {
                    if (assign.Asignee.SizeType == Operand.InternalSizeType.I1
                        || assign.Asignee.SizeType == Operand.InternalSizeType.I2
                        || assign.Asignee.SizeType == Operand.InternalSizeType.I4)
                    {
                        this.MOV(this.GetStackAddress(assign.Asignee.Stack), this.GetRegister(assign.Value.Register));
                    }
                    else
                    {
                        throw new Exception("'" + instruction + "' is not supported.");
                    }
                }
                else if (assign.Asignee.IsRegisterSet == true
                    && assign.Value.IsRegisterSet == false)
                {
                    if (assign.Asignee.SizeType == Operand.InternalSizeType.I1
                        || assign.Asignee.SizeType == Operand.InternalSizeType.I2
                        || assign.Asignee.SizeType == Operand.InternalSizeType.I4)
                    {
                        this.MOV(this.GetRegister(assign.Asignee.Register), this.GetStackAddress(assign.Value.Stack));
                    }
                    else
                    {
                        throw new Exception("'" + instruction + "' is not supported.");
                    }
                }
                else
                {
                    // Just in case....
                    throw new Exception("'" + instruction + "' is not supported.");
                }
            }
            else if (assign.Value is Arithmetic == true)
            {
                if (assign.Asignee.SizeType == Operand.InternalSizeType.I1
                    || assign.Asignee.SizeType == Operand.InternalSizeType.I2
                    || assign.Asignee.SizeType == Operand.InternalSizeType.I4)
                {
                    if (assign.Asignee.IsRegisterSet == true)
                    {
                        this.MovRegisterArithmetic(this.GetRegister(assign.Asignee.Register), assign.Value as Arithmetic);
                    }
                    else
                    {
                        this.MovRegisterArithmetic(R32.EAX, assign.Value as Arithmetic);
                        this.MOV(this.GetStackAddress(assign.Asignee.Stack), R32.EAX);
                    }
                }
                else
                {
                    throw new Exception("'" + instruction + "' is not supported.");
                }
            }
            else if (assign.Value is SharpOS.AOT.IR.Operands.Boolean == true)
            {
                // TODO
            }
            else
            {
                throw new Exception("'" + instruction + "' is not supported.");
            }
        }

        private void HandleReturn(Method method, Block block, SharpOS.AOT.IR.Instructions.Instruction instruction)
        {
            // TODO
        }
    }
}
