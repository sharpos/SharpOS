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
    public class Instruction
    {
        public Instruction(bool indent, string label, string reference, string name, string parameters, Memory rmMemory, Register rmRegister, Register register, object value, string[] encoding)
        {
            this.label = label;
            this.reference = reference;
            this.name = name;
            this.parameters = parameters;
            this.indent = indent;
            this.encoding = encoding;
            this.value = value;
            this.register = register;
            this.rmRegister = rmRegister;
            this.rmMemory = rmMemory;
        }

        private Register register = null;
        private Register rmRegister = null;
        private string[] encoding = null;

        public string[] Encoding
        {
            get
            {
                return this.encoding;
            }
        }

        public void Set(Instruction instruction)
        {
            this.encoding = instruction.encoding;
        }

        private Memory rmMemory = null;

        public Memory RMMemory
        {
            get { return this.rmMemory; }
        }

        private object value = null;

        public object Value
        {
            get { return value; }
            set { this.value = value; }
        }

        private string reference = string.Empty;

        public string Reference
        {
            get { return reference; }
        }

        private string label = string.Empty;

        public string Label
        {
            get { return label; }
        }

        private string name = string.Empty;

        public string Name
        {
            get { return name; }
        }

        public string ShortName
        {
            get
            {
                string result = name;

                if (result.IndexOf("_") != -1)
                {
                    string[] values = result.Split('_');

                    result = values[0];
                }

                return result;
            }
        }

        private string parameters = string.Empty;

        public string Parameters
        {
            get { return parameters; }
        }

        private bool indent = true;

        public bool Indent
        {
            get { return indent; }
        }

        public bool Relative
        {
            get
            {
                bool relative = false;

                foreach (string encodingValue in this.encoding)
                {
                    if (encodingValue.StartsWith("r") == true)
                    {
                        relative = true;
                        break;
                    }
                }

                return relative;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            int indentSize = 20;

            if (this.indent == true)
            {
                stringBuilder.Append(string.Empty.PadRight(indentSize));

                stringBuilder.Append(this.ShortName + " " + this.parameters);
            }
            else
            {
                stringBuilder.Append(this.ShortName.PadRight(indentSize));
                stringBuilder.Append(this.parameters);
            }

            return stringBuilder.ToString();
        }

        public UInt32 Size(bool bits32)
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryWriter binaryWriter = new BinaryWriter(memoryStream);

            Encode(bits32, binaryWriter);

            return (UInt32)memoryStream.Length;
        }

        public virtual bool Encode(bool bits32, BinaryWriter binaryWriter)
        {
            int i = 0;
            int valueIndex = 0;

            if (this.encoding == null || this.encoding.Length == 0)
            {
                return true;
            }

            if (this.rmMemory != null && this.rmMemory.Segment != null)
            {
                binaryWriter.Write(this.rmMemory.Segment.Value);
            }

            string hex = "0123456789ABCDEF";

            for (; i < this.encoding.Length; i++)
            {
                string token = this.encoding[i].ToUpper();

                if (token.Equals("O16") == true || token.Equals("O32") == true)
                {
                    if ((bits32 == true && token == "O16")
                        || (bits32 == false && token == "O32"))
                    {
                        binaryWriter.Write((byte)0x66);
                    }
                }
                else if (token.Equals("A16") == true || token.Equals("A32") == true)
                {
                    if ((bits32 == true && token == "A16")
                        || (bits32 == false && token == "A32"))
                    {
                        binaryWriter.Write((byte)0x67);
                    }
                }
                else if (token.Length == 2
                    && hex.IndexOf(token[0]) != -1
                    && hex.IndexOf(token[1]) != -1)
                {
                    byte value = (byte)(hex.IndexOf(token[0]) * 16 + hex.IndexOf(token[1]));

                    binaryWriter.Write(value);
                }
                else if (token == "RW/RD")
                {
                    if (bits32 == true)
                    {
                        binaryWriter.Write((UInt32)((UInt32)((UInt32[])this.value)[valueIndex++] - binaryWriter.BaseStream.Length - 4));
                    }
                    else
                    {
                        binaryWriter.Write((UInt16)((UInt16)((UInt32[])this.value)[valueIndex++] - binaryWriter.BaseStream.Length - 2));
                    }
                }
                else if (token == "OW/OD")
                {
                    if (bits32 == true)
                    {
                        binaryWriter.Write((UInt32)((UInt32)((UInt32[])this.value)[valueIndex++]));
                    }
                    else
                    {
                        binaryWriter.Write((UInt16)((UInt16)((UInt32[])this.value)[valueIndex++]));
                    }
                }
                else if (token == "IB")
                {
                    binaryWriter.Write((byte)((UInt32[])this.value)[valueIndex++]);
                }
                else if (token == "IW")
                {
                    binaryWriter.Write((UInt16)((UInt32[])this.value)[valueIndex++]);
                }
                else if (token == "ID")
                {
                    binaryWriter.Write((UInt32)((UInt32[])this.value)[valueIndex++]);
                }
                else if (token == "RB")
                {
                    binaryWriter.Write((byte)((byte)((UInt32[])this.value)[valueIndex++] - binaryWriter.BaseStream.Length - 1));
                }
                else if (token == "RW")
                {
                    binaryWriter.Write((UInt16)((UInt16)((UInt32[])this.value)[valueIndex++] - binaryWriter.BaseStream.Length - 2));
                }
                else if (token == "RD")
                {
                    binaryWriter.Write((UInt32)((UInt32)((UInt32[])this.value)[valueIndex++] - binaryWriter.BaseStream.Length - 4));
                }
                else if (token.EndsWith("+R") == true)
                {
                    token = token.Substring(0, token.Length - 2);

                    byte value = (byte)(hex.IndexOf(token[0]) * 16 + hex.IndexOf(token[1]));

                    binaryWriter.Write((byte)(value + this.register.Index));
                }
                else if (token.Equals("/R") == true)
                {
                    if (this.register != null && this.rmRegister != null)
                    {
                        byte value = (byte)(0xC0 + this.register.Index * 8 + this.rmRegister.Index);

                        binaryWriter.Write(value);
                    }
                    else if (this.register != null && this.rmMemory != null)
                    {
                        this.rmMemory.Encode(bits32, this.register.Index, binaryWriter);
                    }
                    else if (this.register != null)
                    {
                        byte value = (byte)(0xC0 + this.register.Index * 8 + this.register.Index);

                        binaryWriter.Write(value);
                    }
                }
                else if (token.StartsWith("/") == true)
                {
                    token = token.Substring(1);

                    if (this.value != null)
                    {
                        if (this.register != null || this.rmRegister != null)
                        {
                            byte value = (byte)(0xC0 + byte.Parse(token) * 8);

                            if (this.register != null)
                            {
                                value += this.register.Index;
                            }
                            else if (this.rmRegister != null)
                            {
                                value += this.rmRegister.Index;
                            }

                            binaryWriter.Write(value);
                        }
                        else if (this.rmMemory != null)
                        {
                            this.rmMemory.Encode(bits32, byte.Parse(token), binaryWriter);
                        }
                    }
                    else if (this.rmMemory != null)
                    {
                        this.rmMemory.Encode(bits32, byte.Parse(token), binaryWriter);
                    }
                    else if (this.rmRegister != null)
                    {
                        byte value = (byte)(0xC0 + byte.Parse(token) * 8);

                        value += this.rmRegister.Index;

                        binaryWriter.Write(value);
                    }
                    else
                    {
                        byte value = (byte)(0xC0 + byte.Parse(token) * 8);

                        binaryWriter.Write(value);
                    }
                }
            }

            return true;
        }
    }
}