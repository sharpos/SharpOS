/**
 *  (C) 2006-2007 Mircea-Cristian Racasan <darx_kies@gmx.net>
 * 
 *  Licensed under the terms of the GNU GPL License version 2.
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SharpOS.AOT.IR;
using SharpOS.AOT.IR.Operands;
using SharpOS.AOT.IR.Operators;
using SharpOS.AOT.IR.Instructions;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;

namespace SharpOS.AOT.IR
{
    public class Block: IEnumerable<SharpOS.AOT.IR.Instructions.Instruction>
    {
        public Block(Method method)
        {
            this.method = method;
        }

        public Block()
        {
        }

        private Method method = null;

        public Method Method
        {
            get { return method; }
        }

        IEnumerator<SharpOS.AOT.IR.Instructions.Instruction> IEnumerable<SharpOS.AOT.IR.Instructions.Instruction>.GetEnumerator()
        {
            foreach (SharpOS.AOT.IR.Instructions.Instruction instruction in this.instructions)
            {
                yield return instruction;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<SharpOS.AOT.IR.Instructions.Instruction>) this).GetEnumerator();
        }

        public delegate void BlockVisitor(Block block);

        public void Visit(BlockVisitor visitor)
        {
            foreach (SharpOS.AOT.IR.Instructions.Instruction instruction in this.instructions)
            {
                instruction.VisitBlock(visitor);
            }

            visitor(this);
        }

        private int GetStackDelta(Mono.Cecil.Cil.Instruction instruction)
        {
            int result = 0;

            switch (instruction.OpCode.StackBehaviourPop)
            {
                case StackBehaviour.Pop1:
                case StackBehaviour.Popi:
                case StackBehaviour.Popref:
                    result--;
                    break;

                case StackBehaviour.Pop1_pop1:
                case StackBehaviour.Popi_pop1:
                case StackBehaviour.Popi_popi:
                case StackBehaviour.Popi_popi8:
                case StackBehaviour.Popi_popr4:
                case StackBehaviour.Popi_popr8:
                case StackBehaviour.Popref_pop1:
                case StackBehaviour.Popref_popi:
                    result -= 2;
                    break;

                case StackBehaviour.Popi_popi_popi:
                case StackBehaviour.Popref_popi_popi:
                case StackBehaviour.Popref_popi_popi8:
                case StackBehaviour.Popref_popi_popr4:
                case StackBehaviour.Popref_popi_popr8:
                case StackBehaviour.Popref_popi_popref:
                    result -= 3;
                    break;

                default: break;
            }

            switch (instruction.OpCode.StackBehaviourPush)
            {
                case StackBehaviour.Push1:
                case StackBehaviour.Pushi:
                case StackBehaviour.Pushi8:
                case StackBehaviour.Pushr4:
                case StackBehaviour.Pushr8:
                case StackBehaviour.Pushref:
                case StackBehaviour.Varpush:
                    result++;
                    break;

                case StackBehaviour.Push1_push1:
                    result += 2;
                    break;

                default: break;
            }

            return result;
        }

        private SharpOS.AOT.IR.Instructions.Instruction Convert(Operands.Operand.ConvertType type)
        {
            //this.instructions[this.instructions.Count - 1].Value.ConvertTo = type;

            SharpOS.AOT.IR.Instructions.Instruction instruction = new Assign(new Register(stack - 1), new Register(stack - 1));
            instruction.Value.ConvertTo = type;

            return instruction;
        }

        public void ConvertFromCIL(bool secondPass)
        {
            stack = 0; 

            this.instructions = new List<SharpOS.AOT.IR.Instructions.Instruction>();

            // Catch
            if (this.method.MethodDefinition.Body.ExceptionHandlers.Count > 0 && this.cil.Count > 0)
            {
                foreach (ExceptionHandler exceptionHandler in this.method.MethodDefinition.Body.ExceptionHandlers)
                {
                    if (exceptionHandler.CatchType != null 
                        && exceptionHandler.HandlerStart == this.cil[0])
                    {
                        this.AddInstruction(new Assign(new Register(stack++), new ExceptionValue()));
                        break;
                    }
                }
            }

            foreach (Mono.Cecil.Cil.Instruction cilInstruction in this.cil)
            {
                SharpOS.AOT.IR.Instructions.Instruction instruction = null;

                if (cilInstruction.OpCode == OpCodes.Nop)
                {
                    continue;
                }

                // Convert
                else if (cilInstruction.OpCode == OpCodes.Conv_I)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_I);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_I1)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_I1);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_I2)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_I2);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_I4)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_I4);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_I8)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_I8);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_I)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_Ovf_I);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_I_Un)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_Ovf_I_Un);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_I1)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_Ovf_I1);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_I1_Un)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_Ovf_I1_Un);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_I2)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_Ovf_I2);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_I2_Un)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_Ovf_I2_Un);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_I4)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_Ovf_I4);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_I4_Un)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_Ovf_I4_Un);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_I8)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_Ovf_I8);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_I8_Un)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_Ovf_I8_Un);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_U)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_Ovf_U);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_U_Un)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_Ovf_U_Un);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_U1)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_Ovf_U1);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_U1_Un)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_Ovf_U1_Un);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_U2)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_Ovf_U2);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_U2_Un)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_Ovf_U2_Un);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_U4)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_Ovf_U4);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_U4_Un)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_Ovf_U4_Un);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_U8)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_Ovf_U8);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_Ovf_U8_Un)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_Ovf_U8_Un);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_R_Un)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_R_Un);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_R4)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_R4);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_R8)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_R8);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_U)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_U);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_U1)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_U1);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_U2)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_U2);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_U4)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_U4);
                }
                else if (cilInstruction.OpCode == OpCodes.Conv_U8)
                {
                    instruction = this.Convert(Operands.Operand.ConvertType.Conv_U8);
                }

                // Arithmetic
                else if (cilInstruction.OpCode == OpCodes.Add)
                {
                    instruction = new Assign(new Register(stack - 2), new Arithmetic(new Binary(Operator.BinaryType.Add), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Add_Ovf)
                {
                    instruction = new Assign(new Register(stack - 2), new Arithmetic(new Binary(Operator.BinaryType.AddSignedWithOverflowCheck), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Add_Ovf_Un)
                {
                    instruction = new Assign(new Register(stack - 2), new Arithmetic(new Binary(Operator.BinaryType.AddUnsignedWithOverflowCheck), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Sub)
                {
                    instruction = new Assign(new Register(stack - 2), new Arithmetic(new Binary(Operator.BinaryType.Sub), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Sub_Ovf)
                {
                    instruction = new Assign(new Register(stack - 2), new Arithmetic(new Binary(Operator.BinaryType.SubSignedWithOverflowCheck), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Sub_Ovf_Un)
                {
                    instruction = new Assign(new Register(stack - 2), new Arithmetic(new Binary(Operator.BinaryType.SubUnsignedWithOverflowCheck), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Mul)
                {
                    instruction = new Assign(new Register(stack - 2), new Arithmetic(new Binary(Operator.BinaryType.Mul), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Mul_Ovf)
                {
                    instruction = new Assign(new Register(stack - 2), new Arithmetic(new Binary(Operator.BinaryType.MulSignedWithOverflowCheck), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Mul_Ovf_Un)
                {
                    instruction = new Assign(new Register(stack - 2), new Arithmetic(new Binary(Operator.BinaryType.MulUnsignedWithOverflowCheck), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Div)
                {
                    instruction = new Assign(new Register(stack - 2), new Arithmetic(new Binary(Operator.BinaryType.Div), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Div_Un)
                {
                    instruction = new Assign(new Register(stack - 2), new Arithmetic(new Binary(Operator.BinaryType.DivUnsigned), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Rem)
                {
                    instruction = new Assign(new Register(stack - 2), new Arithmetic(new Binary(Operator.BinaryType.Remainder), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Rem_Un)
                {
                    instruction = new Assign(new Register(stack - 2), new Arithmetic(new Binary(Operator.BinaryType.RemainderUnsigned), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Neg)
                {
                    instruction = new Assign(new Register(stack - 1), new Arithmetic(new Unary(Operator.UnaryType.Negation), new Register(stack - 1)));
                }

                // Bitwise
                else if (cilInstruction.OpCode == OpCodes.And)
                {
                    instruction = new Assign(new Register(stack - 2), new Arithmetic(new Binary(Operator.BinaryType.And), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Or)
                {
                    instruction = new Assign(new Register(stack - 2), new Arithmetic(new Binary(Operator.BinaryType.Or), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Xor)
                {
                    instruction = new Assign(new Register(stack - 2), new Arithmetic(new Binary(Operator.BinaryType.Xor), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Not)
                {
                    instruction = new Assign(new Register(stack - 2), new Arithmetic(new Unary(Operator.UnaryType.Not), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Shl)
                {
                    instruction = new Assign(new Register(stack - 2), new Arithmetic(new Binary(Operator.BinaryType.SHL), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Shr)
                {
                    instruction = new Assign(new Register(stack - 2), new Arithmetic(new Binary(Operator.BinaryType.SHR), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Shr_Un)
                {
                    instruction = new Assign(new Register(stack - 2), new Arithmetic(new Binary(Operator.BinaryType.SHRUnsigned), new Register(stack - 2), new Register(stack - 1)));
                }

                // Branch
                else if (cilInstruction.OpCode == OpCodes.Beq || cilInstruction.OpCode == OpCodes.Beq_S)
                {
                    instruction = new ConditionalJump(new SharpOS.AOT.IR.Operands.Boolean(new Relational(Operator.RelationalType.Equal), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Bge || cilInstruction.OpCode == OpCodes.Bge_S)
                {
                    instruction = new ConditionalJump(new SharpOS.AOT.IR.Operands.Boolean(new Relational(Operator.RelationalType.GreaterThanOrEqual), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Bge_Un || cilInstruction.OpCode == OpCodes.Bge_Un_S)
                {
                    instruction = new ConditionalJump(new SharpOS.AOT.IR.Operands.Boolean(new Relational(Operator.RelationalType.GreaterThanOrEqualUnsignedOrUnordered), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Bgt || cilInstruction.OpCode == OpCodes.Bgt_S)
                {
                    instruction = new ConditionalJump(new SharpOS.AOT.IR.Operands.Boolean(new Relational(Operator.RelationalType.GreaterThan), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Bgt_Un || cilInstruction.OpCode == OpCodes.Bgt_Un_S)
                {
                    instruction = new ConditionalJump(new SharpOS.AOT.IR.Operands.Boolean(new Relational(Operator.RelationalType.GreaterThanUnsignedOrUnordered), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Ble || cilInstruction.OpCode == OpCodes.Ble_S)
                {
                    instruction = new ConditionalJump(new SharpOS.AOT.IR.Operands.Boolean(new Relational(Operator.RelationalType.LessThanOrEqual), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Ble_Un || cilInstruction.OpCode == OpCodes.Ble_Un_S)
                {
                    instruction = new ConditionalJump(new SharpOS.AOT.IR.Operands.Boolean(new Relational(Operator.RelationalType.LessThanOrEqualUnsignedOrUnordered), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Blt || cilInstruction.OpCode == OpCodes.Blt_S)
                {
                    instruction = new ConditionalJump(new SharpOS.AOT.IR.Operands.Boolean(new Relational(Operator.RelationalType.LessThan), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Blt_Un || cilInstruction.OpCode == OpCodes.Blt_Un_S)
                {
                    instruction = new ConditionalJump(new SharpOS.AOT.IR.Operands.Boolean(new Relational(Operator.RelationalType.LessThanUnsignedOrUnordered), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Bne_Un || cilInstruction.OpCode == OpCodes.Bne_Un_S)
                {
                    instruction = new ConditionalJump(new SharpOS.AOT.IR.Operands.Boolean(new Relational(Operator.RelationalType.NotEqualOrUnordered), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Brfalse || cilInstruction.OpCode == OpCodes.Brfalse_S)
                {
                    instruction = new ConditionalJump(new SharpOS.AOT.IR.Operands.Boolean(new SharpOS.AOT.IR.Operators.Boolean(Operator.BooleanType.False), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Brtrue || cilInstruction.OpCode == OpCodes.Brtrue_S)
                {
                    instruction = new ConditionalJump(new SharpOS.AOT.IR.Operands.Boolean(new SharpOS.AOT.IR.Operators.Boolean(Operator.BooleanType.True), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Br || cilInstruction.OpCode == OpCodes.Br_S)
                {
                    instruction = new Jump();
                }
                else if (cilInstruction.OpCode == OpCodes.Leave 
                    || cilInstruction.OpCode == OpCodes.Leave_S
                    || cilInstruction.OpCode == OpCodes.Endfinally)
                {
                    instruction = new Jump();
                }
                else if (cilInstruction.OpCode == OpCodes.Throw)
                {
                    instruction = new SharpOS.AOT.IR.Instructions.System(new SharpOS.AOT.IR.Operands.Miscellaneous(new SharpOS.AOT.IR.Operators.Miscellaneous(Operator.MiscellaneousType.Throw), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Rethrow)
                {
                    instruction = new SharpOS.AOT.IR.Instructions.System(new SharpOS.AOT.IR.Operands.Miscellaneous(new SharpOS.AOT.IR.Operators.Miscellaneous(Operator.MiscellaneousType.Throw)));
                }

                // Misc
                else if (cilInstruction.OpCode == OpCodes.Ret)
                {
                    if (stack > 0)
                    {
                        instruction = new Return(new Register(stack - 1));
                    }
                    else
                    {
                        instruction = new Return();
                    }
                }
                else if (cilInstruction.OpCode == OpCodes.Switch)
                {
                    instruction = new Switch(new Register(stack - 1));
                }
                else if (cilInstruction.OpCode == OpCodes.Pop)
                {
                    instruction = new Pop(new Register(stack - 1));
                }
                else if (cilInstruction.OpCode == OpCodes.Dup)
                {
                    instruction = new Assign(new Register(stack), new Register(stack - 1));
                }

                // Check
                else if (cilInstruction.OpCode == OpCodes.Ceq)
                {
                    instruction = new Assign(new Register(stack - 2), new SharpOS.AOT.IR.Operands.Boolean(new SharpOS.AOT.IR.Operators.Relational(Operator.RelationalType.Equal), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Cgt)
                {
                    instruction = new Assign(new Register(stack - 2), new SharpOS.AOT.IR.Operands.Boolean(new SharpOS.AOT.IR.Operators.Relational(Operator.RelationalType.GreaterThan), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Cgt_Un)
                {
                    instruction = new Assign(new Register(stack - 2), new SharpOS.AOT.IR.Operands.Boolean(new SharpOS.AOT.IR.Operators.Relational(Operator.RelationalType.GreaterThanUnsignedOrUnordered), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Clt)
                {
                    instruction = new Assign(new Register(stack - 2), new SharpOS.AOT.IR.Operands.Boolean(new SharpOS.AOT.IR.Operators.Relational(Operator.RelationalType.LessThan), new Register(stack - 2), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Clt_Un)
                {
                    instruction = new Assign(new Register(stack - 2), new SharpOS.AOT.IR.Operands.Boolean(new SharpOS.AOT.IR.Operators.Relational(Operator.RelationalType.LessThanUnsignedOrUnordered), new Register(stack - 2), new Register(stack - 1)));
                }

                // Load
                else if (cilInstruction.OpCode == OpCodes.Ldlen)
                {
                    instruction = new Assign(new Register(stack - 2), new Arithmetic(new Unary(Operator.UnaryType.ArraySize), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Ldtoken)
                {
                    instruction = new Assign(new Register(stack), new Constant(cilInstruction.Operand));
                }
                else if (cilInstruction.OpCode == OpCodes.Ldnull)
                {
                    // TODO change that to something else not "null"
                    instruction = new Assign(new Register(stack), new Constant("null"));
                }
                else if (cilInstruction.OpCode == OpCodes.Ldstr)
                {
                    //instruction = new Assign(new Register(stack), new Constant("\"" + cilInstruction.Operand.ToString() + "\""));
                    instruction = new Assign(new Register(stack), new Constant(cilInstruction.Operand.ToString()));
                }

                // Load Constants
                else if (cilInstruction.OpCode == OpCodes.Ldc_I4_0)
                {
                    instruction = new Assign(new Register(stack), new Constant(0));
                    (instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;
                }
                else if (cilInstruction.OpCode == OpCodes.Ldc_I4_1)
                {
                    instruction = new Assign(new Register(stack), new Constant(1));
                    (instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;
                }
                else if (cilInstruction.OpCode == OpCodes.Ldc_I4_2)
                {
                    instruction = new Assign(new Register(stack), new Constant(2));
                    (instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;
                }
                else if (cilInstruction.OpCode == OpCodes.Ldc_I4_3)
                {
                    instruction = new Assign(new Register(stack), new Constant(3));
                    (instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;
                }
                else if (cilInstruction.OpCode == OpCodes.Ldc_I4_4)
                {
                    instruction = new Assign(new Register(stack), new Constant(4));
                    (instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;
                }
                else if (cilInstruction.OpCode == OpCodes.Ldc_I4_5)
                {
                    instruction = new Assign(new Register(stack), new Constant(5));
                    (instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;
                }
                else if (cilInstruction.OpCode == OpCodes.Ldc_I4_6)
                {
                    instruction = new Assign(new Register(stack), new Constant(6));
                    (instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;
                }
                else if (cilInstruction.OpCode == OpCodes.Ldc_I4_7)
                {
                    instruction = new Assign(new Register(stack), new Constant(7));
                    (instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;
                }
                else if (cilInstruction.OpCode == OpCodes.Ldc_I4_8)
                {
                    instruction = new Assign(new Register(stack), new Constant(8));
                    (instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;
                }
                else if (cilInstruction.OpCode == OpCodes.Ldc_I4_M1)
                {
                    instruction = new Assign(new Register(stack), new Constant(-1));
                    (instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;
                }
                else if (cilInstruction.OpCode == OpCodes.Ldc_I4_S)
                {
                    instruction = new Assign(new Register(stack), new Constant(cilInstruction.Operand));
                    (instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;
                }
                else if (cilInstruction.OpCode == OpCodes.Ldc_I4)
                {
                    instruction = new Assign(new Register(stack), new Constant(cilInstruction.Operand));
                    (instruction.Value as Constant).SizeType = Operand.InternalSizeType.I4;
                }
                else if (cilInstruction.OpCode == OpCodes.Ldc_I8)
                {
                    instruction = new Assign(new Register(stack), new Constant(cilInstruction.Operand));
                    (instruction.Value as Constant).SizeType = Operand.InternalSizeType.I8;
                }
                else if (cilInstruction.OpCode == OpCodes.Ldc_R4)
                {
                    instruction = new Assign(new Register(stack), new Constant(cilInstruction.Operand));
                    (instruction.Value as Constant).SizeType = Operand.InternalSizeType.R4;
                }
                else if (cilInstruction.OpCode == OpCodes.Ldc_R8)
                {
                    instruction = new Assign(new Register(stack), new Constant(cilInstruction.Operand));
                    (instruction.Value as Constant).SizeType = Operand.InternalSizeType.R8;
                }
                
                // Indirect Load
                else if (cilInstruction.OpCode == OpCodes.Ldind_I)
                {
                    Reference reference = new Reference(new Register(stack - 1));
                    reference.SizeType = Operand.InternalSizeType.I;

                    Register register = new Register(stack - 1);
                    register.SizeType = Operand.InternalSizeType.I;

                    instruction = new Assign(register, reference);
                }
                else if (cilInstruction.OpCode == OpCodes.Ldind_I1)
                {
                    Reference reference = new Reference(new Register(stack - 1));
                    reference.SizeType = Operand.InternalSizeType.I1;

                    Register register = new Register(stack - 1);
                    register.SizeType = Operand.InternalSizeType.I1;

                    instruction = new Assign(register, reference);
                }
                else if (cilInstruction.OpCode == OpCodes.Ldind_I2)
                {
                    Reference reference = new Reference(new Register(stack - 1));
                    reference.SizeType = Operand.InternalSizeType.I2;

                    Register register = new Register(stack - 1);
                    register.SizeType = Operand.InternalSizeType.I2;

                    instruction = new Assign(register, reference);
                }
                else if (cilInstruction.OpCode == OpCodes.Ldind_I4)
                {
                    Reference reference = new Reference(new Register(stack - 1));
                    reference.SizeType = Operand.InternalSizeType.I4;

                    Register register = new Register(stack - 1);
                    register.SizeType = Operand.InternalSizeType.I4;

                    instruction = new Assign(register, reference);
                }
                else if (cilInstruction.OpCode == OpCodes.Ldind_I8)
                {
                    Reference reference = new Reference(new Register(stack - 1));
                    reference.SizeType = Operand.InternalSizeType.I8;

                    Register register = new Register(stack - 1);
                    register.SizeType = Operand.InternalSizeType.I8;

                    instruction = new Assign(register, reference);
                }
                else if (cilInstruction.OpCode == OpCodes.Ldind_R4)
                {
                    Reference reference = new Reference(new Register(stack - 1));
                    reference.SizeType = Operand.InternalSizeType.R4;

                    Register register = new Register(stack - 1);
                    register.SizeType = Operand.InternalSizeType.R4;

                    instruction = new Assign(register, reference);
                }
                else if (cilInstruction.OpCode == OpCodes.Ldind_R8)
                {
                    Reference reference = new Reference(new Register(stack - 1));
                    reference.SizeType = Operand.InternalSizeType.R8;

                    Register register = new Register(stack - 1);
                    register.SizeType = Operand.InternalSizeType.R8;

                    instruction = new Assign(register, reference);
                }
                else if (cilInstruction.OpCode == OpCodes.Ldind_U1)
                {
                    Reference reference = new Reference(new Register(stack - 1));
                    reference.SizeType = Operand.InternalSizeType.U1;

                    Register register = new Register(stack - 1);
                    register.SizeType = Operand.InternalSizeType.U1;

                    instruction = new Assign(register, reference);
                }
                else if (cilInstruction.OpCode == OpCodes.Ldind_U2)
                {
                    Reference reference = new Reference(new Register(stack - 1));
                    reference.SizeType = Operand.InternalSizeType.U2;

                    Register register = new Register(stack - 1);
                    register.SizeType = Operand.InternalSizeType.U2;

                    instruction = new Assign(register, reference);
                }
                else if (cilInstruction.OpCode == OpCodes.Ldind_U4)
                {
                    Reference reference = new Reference(new Register(stack - 1));
                    reference.SizeType = Operand.InternalSizeType.U4;

                    Register register = new Register(stack - 1);
                    register.SizeType = Operand.InternalSizeType.U4;

                    instruction = new Assign(register, reference);
                }

                // Load Locales
                /*else if (cilInstruction.OpCode == OpCodes.Ldloca || cilInstruction.OpCode == OpCodes.Ldloca_S)
                {
                    instruction = new Assign(new Register(stack), this.Method.GetLocal((cilInstruction.Operand as VariableDefinition).Index));
                }*/
                else if (cilInstruction.OpCode == OpCodes.Ldloc || cilInstruction.OpCode == OpCodes.Ldloc_S)
                {
                    instruction = new Assign(new Register(stack), this.Method.GetLocal((cilInstruction.Operand as VariableDefinition).Index));
                }
                else if (cilInstruction.OpCode == OpCodes.Ldloc_0)
                {
                    instruction = new Assign(new Register(stack), this.Method.GetLocal(0));
                }
                else if (cilInstruction.OpCode == OpCodes.Ldloc_1)
                {
                    instruction = new Assign(new Register(stack), this.Method.GetLocal(1));
                }
                else if (cilInstruction.OpCode == OpCodes.Ldloc_2)
                {
                    instruction = new Assign(new Register(stack), this.Method.GetLocal(2));
                }
                else if (cilInstruction.OpCode == OpCodes.Ldloc_3)
                {
                    instruction = new Assign(new Register(stack), this.Method.GetLocal(3));
                }

                // Indirect Store 
                else if (cilInstruction.OpCode == OpCodes.Stind_I)
                {
                    Reference reference = new Reference(new Register(stack - 2));
                    reference.SizeType = Operand.InternalSizeType.I;

                    Register register = new Register(stack - 1);
                    register.SizeType = Operand.InternalSizeType.I;

                    instruction = new Assign(reference, register);
                }
                else if (cilInstruction.OpCode == OpCodes.Stind_I1)
                {
                    Reference reference = new Reference(new Register(stack - 2));
                    reference.SizeType = Operand.InternalSizeType.I1;

                    Register register = new Register(stack - 1);
                    register.SizeType = Operand.InternalSizeType.I1;

                    instruction = new Assign(reference, register);
                }
                else if (cilInstruction.OpCode == OpCodes.Stind_I2)
                {
                    Reference reference = new Reference(new Register(stack - 2));
                    reference.SizeType = Operand.InternalSizeType.I2;

                    Register register = new Register(stack - 1);
                    register.SizeType = Operand.InternalSizeType.I2;

                    instruction = new Assign(reference, register);
                }
                else if (cilInstruction.OpCode == OpCodes.Stind_I4)
                {
                    Reference reference = new Reference(new Register(stack - 2));
                    reference.SizeType = Operand.InternalSizeType.I4;

                    Register register = new Register(stack - 1);
                    register.SizeType = Operand.InternalSizeType.I4;

                    instruction = new Assign(reference, register);
                }
                else if (cilInstruction.OpCode == OpCodes.Stind_I8)
                {
                    Reference reference = new Reference(new Register(stack - 2));
                    reference.SizeType = Operand.InternalSizeType.I8;

                    Register register = new Register(stack - 1);
                    register.SizeType = Operand.InternalSizeType.I8;

                    instruction = new Assign(reference, register);
                }
                else if (cilInstruction.OpCode == OpCodes.Stind_R4)
                {
                    Reference reference = new Reference(new Register(stack - 2));
                    reference.SizeType = Operand.InternalSizeType.R4;

                    Register register = new Register(stack - 1);
                    register.SizeType = Operand.InternalSizeType.R4;

                    instruction = new Assign(reference, register);
                }
                else if (cilInstruction.OpCode == OpCodes.Stind_R8)
                {
                    Reference reference = new Reference(new Register(stack - 2));
                    reference.SizeType = Operand.InternalSizeType.R8;

                    Register register = new Register(stack - 1);
                    register.SizeType = Operand.InternalSizeType.R8;

                    instruction = new Assign(reference, register);
                }

                // Store Locales
                else if (cilInstruction.OpCode == OpCodes.Stloc || cilInstruction.OpCode == OpCodes.Stloc_S)
                {
                    instruction = new Assign(this.Method.GetLocal((cilInstruction.Operand as VariableDefinition).Index), new Register(stack - 1));
                }
                else if (cilInstruction.OpCode == OpCodes.Stloc_0)
                {
                    instruction = new Assign(this.Method.GetLocal(0), new Register(stack - 1));
                }
                else if (cilInstruction.OpCode == OpCodes.Stloc_1)
                {
                    instruction = new Assign(this.Method.GetLocal(1), new Register(stack - 1));
                }
                else if (cilInstruction.OpCode == OpCodes.Stloc_2)
                {
                    instruction = new Assign(this.Method.GetLocal(2), new Register(stack - 1));
                }
                else if (cilInstruction.OpCode == OpCodes.Stloc_3)
                {
                    instruction = new Assign(this.Method.GetLocal(3), new Register(stack - 1));
                }

                // Arguments Load
                else if (cilInstruction.OpCode == OpCodes.Ldarg || cilInstruction.OpCode == OpCodes.Ldarg_S)
                {
                    instruction = new Assign(new Register(stack), this.Method.GetArgument((cilInstruction.Operand as ParameterDefinition).Sequence));
                }
                /*else if (cilInstruction.OpCode == OpCodes.Ldarga || cilInstruction.OpCode == OpCodes.Ldarga_S)
                {
                    if (cilInstruction.Operand is ParameterDefinition)
                    {
                        instruction = new Assign(new Register(stack), this.Method.GetArgument((cilInstruction.Operand as ParameterDefinition).Sequence));
                    }
                    else
                    {
                        instruction = new Assign(new Register(stack), this.Method.GetArgument((int)cilInstruction.Operand));
                    }
                }*/
                else if (cilInstruction.OpCode == OpCodes.Ldarg_0)
                {
                    instruction = new Assign(new Register(stack), this.Method.GetArgument(1));
                }
                else if (cilInstruction.OpCode == OpCodes.Ldarg_1)
                {
                    instruction = new Assign(new Register(stack), this.Method.GetArgument(2));
                }
                else if (cilInstruction.OpCode == OpCodes.Ldarg_2)
                {
                    instruction = new Assign(new Register(stack), this.Method.GetArgument(3));
                }
                else if (cilInstruction.OpCode == OpCodes.Ldarg_3)
                {
                    instruction = new Assign(new Register(stack), this.Method.GetArgument(4));
                }

                // Argument Store
                else if (cilInstruction.OpCode == OpCodes.Starg || cilInstruction.OpCode == OpCodes.Starg_S)
                {
                    instruction = new Assign(this.Method.GetArgument((cilInstruction.Operand as ParameterDefinition).Sequence), new Register(stack - 1));
                }

                // Call
                else if (cilInstruction.OpCode == OpCodes.Call
                    || cilInstruction.OpCode == OpCodes.Callvirt
                    || cilInstruction.OpCode == OpCodes.Jmp)
                {
                    MethodReference call = (cilInstruction.Operand as MethodReference);

                    Operand[] operands;

                    // If it is not static include the register of the instance into the operands
                    if (call.HasThis == true)
                    {
                        operands = new Operand[call.Parameters.Count + 1];
                    }
                    else
                    {
                        operands = new Operand[call.Parameters.Count];
                    }

                    for (int i = 0; i < operands.Length; i++)
                    {
                        operands[i] = new Register(stack - operands.Length + i);
                    }

                    if (call.ReturnType.ReturnType.FullName.Equals("System.Void") == true)
                    {
                        instruction = new SharpOS.AOT.IR.Instructions.Call(new SharpOS.AOT.IR.Operands.Call(call, operands));

                        stack--;
                    }
                    else
                    {
                        instruction = new Assign(new Register(stack - operands.Length), new SharpOS.AOT.IR.Operands.Call(call, operands));
                    }

                    stack -= operands.Length;
                }
                else if (cilInstruction.OpCode == OpCodes.Newobj)
                {
                    MethodReference call = (cilInstruction.Operand as MethodReference);

                    Operand[] operands = new Operand[call.Parameters.Count];

                    for (int i = 0; i < call.Parameters.Count; i++)
                    {
                        operands[i] = new Register(stack - call.Parameters.Count + i);
                    }

                    instruction = new Assign(new Register(stack - call.Parameters.Count), new SharpOS.AOT.IR.Operands.Call(call, operands));

                    stack -= call.Parameters.Count;
                }
                /*else if (cilInstruction.OpCode == OpCodes.Ldfld || cilInstruction.OpCode == OpCodes.Ldflda)
                {
                    instruction = new Assign(new Register(stack - 1), new Field((cilInstruction.Operand as FieldDefinition).DeclaringType.FullName + "." + (cilInstruction.Operand as FieldDefinition).Name, new Register(stack - 1)));
                    (instruction.Value as Identifier).SizeType = Operand.InternalSizeType.U;
                }*/
                else if (cilInstruction.OpCode == OpCodes.Ldsfld || cilInstruction.OpCode == OpCodes.Ldsflda)
                {
                    instruction = new Assign(new Register(stack), new Field((cilInstruction.Operand as FieldReference).DeclaringType.FullName + "." + (cilInstruction.Operand as FieldReference).Name));
                    (instruction.Value as Identifier).SizeType = Operand.InternalSizeType.U;
                }
                /*else if (cilInstruction.OpCode == OpCodes.Stfld)
                {
                    instruction = new Assign(new Field((cilInstruction.Operand as FieldDefinition).DeclaringType.FullName + "." + (cilInstruction.Operand as FieldDefinition).Name, new Register(stack - 2)), new Register(stack - 1));
                    (instruction as Assign).Asignee.SizeType = Operand.InternalSizeType.U;
                    (instruction.Value as Identifier).SizeType = Operand.InternalSizeType.U;
                }*/
                else if (cilInstruction.OpCode == OpCodes.Stsfld)
                {
                    instruction = new Assign(new Field((cilInstruction.Operand as FieldReference).DeclaringType.FullName + "." + (cilInstruction.Operand as FieldReference).Name, new Register(stack - 1)), new Register(stack - 1));
                    (instruction as Assign).Asignee.SizeType = Operand.InternalSizeType.U;
                    (instruction.Value as Identifier).SizeType = Operand.InternalSizeType.U;
                }

                // Array
                else if (cilInstruction.OpCode == OpCodes.Newarr)
                {
                    instruction = new Assign(new Register(stack - 2), new SharpOS.AOT.IR.Operands.Miscellaneous(new SharpOS.AOT.IR.Operators.Miscellaneous(Operator.MiscellaneousType.NewArray), new Register(stack - 1)));
                }
                else if (cilInstruction.OpCode == OpCodes.Stelem_Ref
                    || cilInstruction.OpCode == OpCodes.Stelem_Any
                    || cilInstruction.OpCode == OpCodes.Stelem_I
                    || cilInstruction.OpCode == OpCodes.Stelem_I1
                    || cilInstruction.OpCode == OpCodes.Stelem_I2
                    || cilInstruction.OpCode == OpCodes.Stelem_I4
                    || cilInstruction.OpCode == OpCodes.Stelem_I8
                    || cilInstruction.OpCode == OpCodes.Stelem_R4
                    || cilInstruction.OpCode == OpCodes.Stelem_R8)
                {
                    instruction = new Assign(new ArrayElement(new Register(stack - 3), new Register(stack - 2)), new Register(stack - 1));
                }
                else if (cilInstruction.OpCode == OpCodes.Ldelem_Ref
                   || cilInstruction.OpCode == OpCodes.Ldelem_I
                   || cilInstruction.OpCode == OpCodes.Ldelem_I1
                   || cilInstruction.OpCode == OpCodes.Ldelem_I2
                   || cilInstruction.OpCode == OpCodes.Ldelem_I4
                   || cilInstruction.OpCode == OpCodes.Ldelem_I8
                   || cilInstruction.OpCode == OpCodes.Ldelem_R4
                   || cilInstruction.OpCode == OpCodes.Ldelem_R8
                   || cilInstruction.OpCode == OpCodes.Ldelem_U1
                   || cilInstruction.OpCode == OpCodes.Ldelem_U2
                   || cilInstruction.OpCode == OpCodes.Ldelem_U4
                   || cilInstruction.OpCode == OpCodes.Stelem_Any
                   || cilInstruction.OpCode == OpCodes.Ldelema)
                {
                    // TODO Signed/Unsigned
                    instruction = new Assign(new Register(stack - 1), new ArrayElement(new Register(stack - 2), new Register(stack - 1)));
                }

                else
                {
                    throw new Exception("Instruction '" + cilInstruction.OpCode.Name + "' is not implemented. (Found in '" + this.method.MethodFullName  + "')");
                }

                if (instruction != null)
                {
                    this.AddInstruction(instruction);    
                }

                
                /*Console.WriteLine("--------------------------------");
                Console.WriteLine(cilInstruction.OpCode.Name);
                Console.WriteLine(instruction.ToString());*/
                

                stack += GetStackDelta(cilInstruction);
            }

            if (secondPass == true && stack != 0 && !(stack == 1 && this.type == BlockType.Return && this[this.InstructionsCount - 1].Value != null))
            {
                throw new Exception("Could not fix the stack in '" + this.method.ToString() + "'.");
            }
        }

        public void Merge(Block block)
        {
            if (this.type == BlockType.OneWay)
            {
                this.cil.Remove(this.cil[this.cil.Count - 1]);
            }

            this.type = block.type;
            this.outs = block.outs;

            foreach (Mono.Cecil.Cil.Instruction instruction in block.cil)
            {
                this.cil.Add(instruction);
            }
        }

        private int stack = 0;

        public int Stack
        {
            get { return stack; }
        }

        
        /*private int backwardBranches = 0;
        private void visited = false;
        private void active = false;
        private int index = 0;

        public void Visit(ref int index)
        {
            if (this.visited == false)
            {
                this.visited = true;
                this.active = true;

                this.index = index++;

                foreach (Block block in this.outs)
                {
                    block.Visit(ref index);
                }

                this.active = false;
            }
            else if (this.active == true)
            {
                this.backwardBranches++;
            }

            return;
        }*/

        public enum BlockType
        {
            Return
            , Throw
            , OneWay
            , TwoWay
            , NWay
            , Fall
        }

        private List<Mono.Cecil.Cil.Instruction> cil = new List<Mono.Cecil.Cil.Instruction>();

        public List<Mono.Cecil.Cil.Instruction> CIL        
        {
            get { return cil; }
            set { cil = value; }
        }

        private List<SharpOS.AOT.IR.Instructions.Instruction> instructions = new List<SharpOS.AOT.IR.Instructions.Instruction>();

        public SharpOS.AOT.IR.Instructions.Instruction this[int index]
        {
            get
            {
                return this.instructions[index];
            }
        }

        public void InsertInstruction(int position, SharpOS.AOT.IR.Instructions.Instruction instruction)
        {
            instruction.Block = this;

            this.instructions.Insert(position, instruction);
        }

        public void SetInstruction(int position, SharpOS.AOT.IR.Instructions.Instruction instruction)
        {
            instruction.Block = this;

            for (int i = 0; i < this.instructions.Count; i++)
            {
                for (int j = 0; j < this.instructions[i].Branches.Count; j++)
                {
                    if (this.instructions[i].Branches[j] == this.instructions[position])
                    {
                        this.instructions[i].Branches[j] = instruction;
                    }
                }
            }

            this.instructions[position] = instruction;
        }

        public void AddInstruction(SharpOS.AOT.IR.Instructions.Instruction instruction)
        {
            instruction.Block = this;

            this.instructions.Add(instruction);
        }

        public void RemoveInstruction(SharpOS.AOT.IR.Instructions.Instruction instruction)
        {
            this.instructions.Remove(instruction);
        }

        public void RemoveInstruction(int position)
        {
            this.instructions.RemoveAt(position);
        }

        public int InstructionsCount
        {
            get
            {
                return this.instructions.Count;
            }
        }

        private BlockType type;

        public BlockType Type
        {
            get { return type; }
            set { type = value; }
        }

        private List<Block> ins = new List<Block>();

        public List<Block> Ins
        {
            get { return ins; }
        }

        private List<Block> outs = new List<Block>();

        public List<Block> Outs
        {
            get { return outs; }
        }

        private List<Block> dominators = new List<Block>();

        public List<Block> Dominators
        {
            get { return dominators; }
            set { dominators = value; }
        }

        private Block immediateDominator = null;

        public Block ImmediateDominator
        {
            get { return immediateDominator; }
            set { immediateDominator = value; }
        }

        private List<Block> immediateDominatorOf = new List<Block>();

        public List<Block> ImmediateDominatorOf
        {
            get { return immediateDominatorOf; }
            set { immediateDominatorOf = value; }
        }

        private List<Block> dominanceFrontiers = new List<Block>();

        public List<Block> DominanceFrontiers
        {
            get { return dominanceFrontiers; }
            set { dominanceFrontiers = value; }
        }

        private int index = 0;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        public long StartOffset
        {
            get
            {
                if (this.cil.Count > 0)
                {
                    return this.cil[0].Offset;
                }

                if (this.InstructionsCount > 0)
                {
                    return this[0].StartOffset;
                }

                return 0;
            }
        }

        public long EndOffset
        {
            get
            {
                if (this.cil.Count > 0)
                {
                    return this.cil[this.cil.Count - 1].Offset;
                }

                if (this.InstructionsCount > 0)
                {
                    return this[this.InstructionsCount - 1].EndOffset;
                }

                return 0;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            Dump(string.Empty, stringBuilder);

            return stringBuilder.ToString();
        }

        public void UpdateIndex()
        {
            int index = 0;

            SharpOS.AOT.IR.Instructions.Instruction.InstructionVisitor visitor = delegate(SharpOS.AOT.IR.Instructions.Instruction instruction)
            {
                instruction.Index = index++;
            };

            foreach (SharpOS.AOT.IR.Instructions.Instruction instruction in this)
            {
                instruction.VisitInstruction(visitor);
            }
        }

        public void Dump(StringBuilder stringBuilder)
        {
            this.UpdateIndex();

            this.Dump(string.Empty, stringBuilder);
        }

        public void Dump(string prefix, StringBuilder stringBuilder)
        {
            string ins = string.Empty;

            for (int i = 0; i < this.ins.Count; i++)
            {
                if (ins.Length > 0)
                {
                    ins += " ";
                }

                ins += this.ins[i].Index.ToString();
            }

            string outs = string.Empty;

            for (int i = 0; i < this.outs.Count; i++)
            {
                if (outs.Length > 0)
                {
                    outs += " ";
                }

                outs += this.outs[i].Index.ToString();
            }

            stringBuilder.Append(prefix + "-------------------------------\n");
            //stringBuilder.Append(prefix + String.Format(">>> {4} ({0}) [{1}] [{2}] [{3}]\n", this.StartOffset, this.type, ins, outs, this.index));
            stringBuilder.Append(prefix + String.Format(">>> {0} [{1}] [{2}] [{3}]\n", this.index, this.type, ins, outs));
            stringBuilder.Append(prefix + "-------------------------------\n");

            for (int i = 0; i < this.InstructionsCount; i++)
            {
                SharpOS.AOT.IR.Instructions.Instruction instruction = this[i];

                instruction.Dump(prefix + "\t", stringBuilder);
            }

#if false
            foreach (Instruction instruction in this.cil)
            {
                string operand = string.Empty;

                if (instruction.Operand != null)
                {
                    if (instruction.Operand is Instruction)
                    {
                        operand = (instruction.Operand as Instruction).Offset.ToString();
                    }
                    else if (instruction.Operand is Instruction[])
                    {
                        foreach (Instruction instruction2 in (instruction.Operand as Instruction[]))
                        {
                            if (operand.Length > 0)
                            {
                                operand += " ";
                            }

                            operand += instruction2.Offset.ToString();
                        }

                        operand = "(" + operand + ")";
                    }
                    else
                    {
                        operand = instruction.Operand.ToString();
                    }
                }

                Console.WriteLine("{0}: {1} {2}", instruction.Offset, instruction.OpCode.Name, operand);
            }
#endif
        }
    }

}
