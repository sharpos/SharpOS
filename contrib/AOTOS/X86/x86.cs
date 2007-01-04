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
    public abstract class Register
    {
        public Register(string name, byte index)
        {
            this.name = name;
            this.index = index;
        }

        private string name = string.Empty;

        public string Name
        {
            get { return name; }
        }

        private byte index = 0;

        public byte Index
        {
            get { return index; }
        }

        public override string ToString()
        {
            return this.name;
        }
    }

    public class R8Type : Register
    {
        public R8Type(string name, byte index)
            : base(name, index)
        {
        }
    }

    public class R8
    {
        public static readonly R8Type AL = new R8Type("AL", 0);
        public static readonly R8Type CL = new R8Type("CL", 1);
        public static readonly R8Type DL = new R8Type("DL", 2);
        public static readonly R8Type BL = new R8Type("BL", 3);
        public static readonly R8Type AH = new R8Type("AH", 4);
        public static readonly R8Type CH = new R8Type("CH", 5);
        public static readonly R8Type DH = new R8Type("DH", 6);
        public static readonly R8Type BH = new R8Type("BH", 7);

        public static R8Type GetByID(string id)
        {
            if (id.Equals("null") == true)
            {
                return null;
            }

            switch (id.Substring(id.Length - 2))
            {
                case "AL":
                    return R8.AL;
                case "AH":
                    return R8.AH;
                case "BL":
                    return R8.BL;
                case "BH":
                    return R8.BH;
                case "CL":
                    return R8.CL;
                case "CH":
                    return R8.CH;
                case "DL":
                    return R8.DL;
                case "DH":
                    return R8.DH;
                default:
                    throw new Exception("Unknown R8 Register '" + id + "'");
            }
        }
    }

    public class R16Type : Register
    {
        public R16Type(string name, byte index)
            : base(name, index)
        {
        }
    }

    public class R16
    {
        public static readonly R16Type AX = new R16Type("AX", 0);
        public static readonly R16Type CX = new R16Type("CX", 1);
        public static readonly R16Type DX = new R16Type("DX", 2);
        public static readonly R16Type BX = new R16Type("BX", 3);
        public static readonly R16Type SP = new R16Type("SP", 4);
        public static readonly R16Type BP = new R16Type("BP", 5);
        public static readonly R16Type SI = new R16Type("SI", 6);
        public static readonly R16Type DI = new R16Type("DI", 7);


        public static R16Type GetByID(string id)
        {
            if (id.Equals("null") == true)
            {
                return null;
            }

            switch (id.Substring(id.Length - 2))
            {
                case "AX":
                    return R16.AX;
                case "BX":
                    return R16.BX;
                case "CX":
                    return R16.CX;
                case "DX":
                    return R16.DX;
                case "SP":
                    return R16.SP;
                case "BP":
                    return R16.BP;
                case "SI":
                    return R16.SI;
                case "DI":
                    return R16.DI;
                default:
                    throw new Exception("Unknown R16 Register '" + id + "'");
            }
        }
    }

    public class R32Type : Register
    {
        public R32Type(string name, byte index)
            : base(name, index)
        {
        }
    }

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

    public class SegType : Register
    {
        public SegType(string name, byte index, byte value)
            : base(name, index)
        {
            this.value = value;
        }

        private byte value = 0;

        public byte Value
        {
            get { return value; }
        }
    }

    public class Seg
    {
        public static readonly SegType ES = new SegType("ES", 0, 0x26);
        public static readonly SegType CS = new SegType("CS", 1, 0x2E);
        public static readonly SegType SS = new SegType("SS", 2, 0x36);
        public static readonly SegType DS = new SegType("DS", 3, 0x3E);
        public static readonly SegType FS = new SegType("FS", 4, 0x64);
        public static readonly SegType GS = new SegType("GS", 5, 0x65);

        public static SegType GetByID(string id)
        {
            if (id.Equals("null") == true)
            {
                return null;
            }

            switch (id.Substring(id.Length - 2))
            {
                case "DS":
                    return Seg.DS;
                case "ES":
                    return Seg.ES;
                case "CS":
                    return Seg.CS;
                case "SS":
                    return Seg.SS;
                case "FS":
                    return Seg.FS;
                case "GS":
                    return Seg.GS;
                default:
                    throw new Exception("Unknown Seg Register '" + id + "'");
            }
        }
    }

    public class FPType : Register
    {
        public FPType(string name, byte index)
            : base(name, index)
        {
        }
    }

    public class FP
    {
        public static readonly FPType ST0 = new FPType("ST0", 0);
        public static readonly FPType ST1 = new FPType("ST1", 1);
        public static readonly FPType ST2 = new FPType("ST2", 2);
        public static readonly FPType ST3 = new FPType("ST3", 3);
        public static readonly FPType ST4 = new FPType("ST4", 4);
        public static readonly FPType ST5 = new FPType("ST5", 5);
        public static readonly FPType ST6 = new FPType("ST6", 6);
        public static readonly FPType ST7 = new FPType("ST7", 7);

        public static FPType GetByID(string id)
        {
            if (id.Equals("null") == true)
            {
                return null;
            }

            switch (id.Substring(id.Length - 3))
            {
                case "ST0":
                    return FP.ST0;
                case "ST1":
                    return FP.ST1;
                case "ST2":
                    return FP.ST2;
                case "ST3":
                    return FP.ST3;
                case "ST4":
                    return FP.ST4;
                case "ST5":
                    return FP.ST5;
                case "ST6":
                    return FP.ST6;
                case "ST7":
                    return FP.ST7;
                default:
                    throw new Exception("Unknown FP Register '" + id + "'");
            }
        }
    }

    public class DRType : Register
    {
        public DRType(string name, byte index)
            : base(name, index)
        {
        }
    }

    public class DR
    {
        public static readonly DRType DR0 = new DRType("DR0", 0);
        public static readonly DRType DR1 = new DRType("DR1", 1);
        public static readonly DRType DR2 = new DRType("DR2", 2);
        public static readonly DRType DR3 = new DRType("DR3", 3);
        public static readonly DRType DR4 = new DRType("DR4", 4);
        public static readonly DRType DR6 = new DRType("DR6", 6);
        public static readonly DRType DR7 = new DRType("DR7", 7);

        public static DRType GetByID(string id)
        {
            if (id.Equals("null") == true)
            {
                return null;
            }

            switch (id.Substring(id.Length - 3))
            {
                case "DR0":
                    return DR.DR0;
                case "DR1":
                    return DR.DR1;
                case "DR2":
                    return DR.DR2;
                case "DR3":
                    return DR.DR3;
                case "DR4":
                    return DR.DR4;
                case "DR6":
                    return DR.DR6;
                case "DR7":
                    return DR.DR7;
                default:
                    throw new Exception("Unknown DR Register '" + id + "'");
            }
        }
    }

    public class CRType : Register
    {
        public CRType(string name, byte index)
            : base(name, index)
        {
        }
    }

    public class CR
    {
        public static readonly CRType CR0 = new CRType("CR0", 0);
        public static readonly CRType CR2 = new CRType("CR2", 2);
        public static readonly CRType CR3 = new CRType("CR3", 3);
        public static readonly CRType CR4 = new CRType("CR4", 4);

        public static CRType GetByID(string id)
        {
            if (id.Equals("null") == true)
            {
                return null;
            }

            switch (id.Substring(id.Length - 3))
            {
                case "CR0":
                    return CR.CR0;
                case "CR2":
                    return CR.CR2;
                case "CR3":
                    return CR.CR3;
                case "CR4":
                    return CR.CR4;
                default:
                    throw new Exception("Unknown CR Register '" + id + "'");
            }
        }
    }

    public class TRType : Register
    {
        public TRType(string name, byte index)
            : base(name, index)
        {
        }
    }

    public class TR
    {
        public static readonly TRType TR3 = new TRType("TR3", 3);
        public static readonly TRType TR4 = new TRType("TR4", 4);
        public static readonly TRType TR5 = new TRType("TR5", 5);
        public static readonly TRType TR6 = new TRType("TR6", 6);
        public static readonly TRType TR7 = new TRType("TR7", 7);

        public static TRType GetByID(string id)
        {
            if (id.Equals("null") == true)
            {
                return null;
            }

            switch (id.Substring(id.Length - 3))
            {
                case "TR3":
                    return TR.TR3;
                case "TR4":
                    return TR.TR4;
                case "TR5":
                    return TR.TR5;
                case "TR6":
                    return TR.TR6;
                case "TR7":
                    return TR.TR7;
                default:
                    throw new Exception("Unknown TR Register '" + id + "'");
            }
        }
    }

    public class Memory
    {
        private void Check32Values()
        {
            if ((index == R32.ESP && scale > 0) || (index == R32.ESP && scale == 0 && _base == R32.ESP))
            {
                throw new Exception("ESP can't be used as index.");
            }

            if (scale > 3)
            {
                throw new Exception("The Scale can be 0, 1, 2 or 3.");
            }

            if (_base == null && index == null && this.displacementSet == false)
            {
                throw new Exception("No valid 32bit address.");
            }
        }

        private void Check16Values()
        {
            if (_base != null)
            {
                if (_base != R16.BX && _base != R16.BP && _base != R16.SI && _base != R16.DI)
                {
                    throw new Exception("16bit Register '" + _base.Name + "' is not allowed.");
                }

                if (index != null && index != R16.SI && index != R16.DI)
                {
                    throw new Exception("16bit Register '" + index.Name + "' is not allowed.");
                }
            }
            else if (index != null)
            {
                throw new Exception("16bit Index Register is defined and the Base Register is missing.");
            }

            if (_base == null && index == null && displacementSet == false)
            {
                throw new Exception("No valid 16bit address.");
            }
        }

        public Memory(SegType segment, string label)
        {
            this.segment = segment;
            this.reference = label;
            this.Displacement = 0;
        }

        public Memory(string label)
        {
            this.reference = label;
            this.Displacement = 0;
        }

        public Memory(SegType segment, R16Type _base, R16Type index, UInt16 displacement)
        {
            this.bits32Address = false;
            this.segment = segment;
            this._base = _base;
            this.index = index;
            this.Displacement = displacement;

            this.Check16Values();
        }

        public Memory(SegType segment, R16Type _base, R16Type index)
        {
            this.bits32Address = false;
            this.segment = segment;
            this._base = _base;
            this.index = index;

            this.Check16Values();
        }

        public Memory(SegType segment, R32Type _base, R32Type index, byte scale, UInt32 displacement)
        {
            this.segment = segment;
            this._base = _base;
            this.index = index;
            this.scale = scale;
            this.Displacement = displacement;

            Check32Values();
        }

        public Memory(SegType segment, R32Type _base, R32Type index, byte scale)
        {
            this.segment = segment;
            this._base = _base;
            this.index = index;
            this.scale = scale;

            Check32Values();
        }

        private UInt32 displacement = 0;
        private bool displacementSet = false;

        internal UInt32 Displacement
        {
            get { return this.displacement; }
            set { this.displacementSet = true;  this.displacement = value; }
        }

        private bool bits32Address = true;
        private byte scale = 0;
        private Register index = null, _base = null;

        private string reference = string.Empty;

        public string Reference
        {
            get { return reference; }
        }
	
        private SegType segment;

        public SegType Segment
        {
            get { return segment; }
        }

        public bool Encode(bool bits32, byte spareRegister, BinaryWriter binaryWriter)
        {
            byte value = (byte) (spareRegister * 8);

            if (bits32 != this.bits32Address && (this._base != null || this.index != null))
            {
                throw new Exception("Wrong kind of address. (16bit/32bit mix not allowed)");
            }

            if (bits32 == true)
            {
                R32Type _base = (R32Type)this._base, index = (R32Type)this.index;
                UInt32 displacement = this.displacement;
                bool displacementSet = this.displacementSet;
                byte scale = this.scale;

                if ((this._base == null && this.index != null && this.scale == 0)
                    || this.index == R32.ESP)
                {
                    _base = (R32Type)this.index;
                    index = (R32Type)this._base;
                }

                if (_base == null && index != null && scale == 1)
                {
                    _base = index;
                    scale = 0;
                }

                bool fixEBP = false;

                if (displacementSet == false && _base == R32.EBP) // && index == null) || (_base == null && index == R32.EBP)))
                {
                    fixEBP = true;
                    displacementSet = true;
                    displacement = 0;
                }

                if (displacementSet != false)
                {
                    if (_base == null && index == null)
                    {
                        value += 5;

                        binaryWriter.Write(value);

                        binaryWriter.Write((UInt32)displacement);

                        return true;
                    }
                    else if (_base != null)
                    {
                        if (fixEBP == true) //displacement < 256)
                        {
                            value += 1 * 64; // 8bit
                        }
                        else
                        {
                            value += 2 * 64; // 16bit or 32bit
                        }
                    }
                }

                if (_base != R32.ESP && index == null)
                {
                    value += _base.Index;
                }
                else
                {
                    value += 0x04;
                }

                binaryWriter.Write(value);

                if (_base == R32.ESP || index != null)
                {
                    value = (byte)(scale * 64);

                    if (index != null)
                    {
                        value += (byte)(index.Index * 8);
                    }
                    else
                    {
                        value += 0x20;
                    }

                    if (_base != null)
                    {
                        value += _base.Index;
                    }
                    else
                    {
                        value += 0x05;
                    }

                    binaryWriter.Write(value);

                    if (this.displacementSet == false && _base == null)
                    {
                        binaryWriter.Write((UInt32)0);
                    }
                }

                if (displacementSet != false)
                {
                    if (fixEBP == true) //displacement < 256)
                    {
                        binaryWriter.Write((byte) displacement);
                    }
                    else
                    {
                        binaryWriter.Write((UInt32)displacement);
                    }
                }
            }
            else
            {
                byte rm;
                UInt16 displacement = (UInt16) this.displacement;

                if (this._base == R16.BX && this.index == R16.SI)
                {
                    rm = 0;
                }
                else if (this._base == R16.BX && this.index == R16.DI)
                {
                    rm = 1;
                }
                else if (this._base == R16.BP && this.index == R16.SI)
                {
                    rm = 2;
                }
                else if (this._base == R16.BP && this.index == R16.DI)
                {
                    rm = 3;
                }
                else if (this._base == R16.SI && this.index == null)
                {
                    rm = 4;
                }
                else if (this._base == R16.DI && this.index == null)
                {
                    rm = 5;
                }
                else if (this._base == R16.BP && this.index == null)
                {
                    rm = 6;

                    if (displacementSet == false)
                    {
                        value += 0x46;
                        binaryWriter.Write(value);
                        binaryWriter.Write((byte) 0);
                        return true;
                    }
                }
                else if (this._base == R16.BX && this.index == null)
                {
                    rm = 7;
                }
                else
                {
                    rm = 6;
                }

                value += rm;

                if (this._base != null || this.index != null)
                {
                    if (displacementSet != false)
                    {
                        value += 0x80;
                    }
                }

                binaryWriter.Write(value);

                if (displacementSet != false)
                {
                    binaryWriter.Write((UInt16)displacement);
                }
            }

            return true;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (this._base != null)
            {
                stringBuilder.Append(this._base.Name);
            }

            if (this.index != null)
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append(" + ");
                }

                stringBuilder.Append(this.index.Name);

                stringBuilder.Append("*");
                stringBuilder.Append(System.Math.Pow(2, this.scale));
            }

            if (this.displacementSet != false)
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append(" + ");
                }

                stringBuilder.Append(string.Format("0x{0:x}", this.displacement));
            }

            if (this.segment != null)
            {
                stringBuilder.Insert(0, this.segment.Name + ":");
            }

            return "[" + stringBuilder.ToString() + "]";
        }
    }

    public class ByteMemory : Memory
    {
        public ByteMemory(SegType segment, R32Type _base, R32Type index, byte scale, UInt32 displacement)
            : base(segment, _base, index, scale, displacement)
        {
        }

        public ByteMemory(SegType segment, R32Type _base, R32Type index, byte scale)
            : base(segment, _base, index, scale)
        {
        }

        public ByteMemory(SegType segment, string label)
            : base(segment, label)
        {
        }
        
        public ByteMemory(string label)
            : base(label)
        {
        }

        public ByteMemory(SegType segment, R16Type _base, R16Type index, UInt16 displacement)
            : base(segment, _base, index, displacement)
        {
        }

        public ByteMemory(SegType segment, R16Type _base, R16Type index)
            : base(segment, _base, index)
        {
        }
    }

    public class WordMemory : Memory
    {
        public WordMemory(SegType segment, R32Type _base, R32Type index, byte scale, UInt32 displacement)
            : base(segment, _base, index, scale, displacement)
        {
        }

        public WordMemory(SegType segment, R32Type _base, R32Type index, byte scale)
            : base(segment, _base, index, scale)
        {
        }

        public WordMemory(SegType segment, string label)
            : base(segment, label)
        {
        }
        
        public WordMemory(string label)
            : base(label)
        {
        }

        public WordMemory(SegType segment, R16Type _base, R16Type index, UInt16 displacement)
            : base(segment, _base, index, displacement)
        {
        }
        
        public WordMemory(SegType segment, R16Type _base, R16Type index)
            : base(segment, _base, index)
        {
        }
    }
    
    public class DWordMemory : Memory
    {
        public DWordMemory(SegType segment, R32Type _base, R32Type index, byte scale, UInt32 displacement)
            : base(segment, _base, index, scale, displacement)
        {
        }

        public DWordMemory(SegType segment, R32Type _base, R32Type index, byte scale)
            : base(segment, _base, index, scale)
        {
        }

        public DWordMemory(SegType segment, string label)
            : base(segment, label)
        {
        }
        
        public DWordMemory(string label)
            : base(label)
        {
        }

        public DWordMemory(SegType segment, R16Type _base, R16Type index, UInt16 displacement)
            : base(segment, _base, index, displacement)
        {
        }

        public DWordMemory(SegType segment, R16Type _base, R16Type index)
            : base(segment, _base, index)
        {
        }
    }

    public class QWordMemory : Memory
    {
        public QWordMemory(SegType segment, R32Type _base, R32Type index, byte scale, UInt32 displacement)
            : base(segment, _base, index, scale, displacement)
        {
        }

        public QWordMemory(SegType segment, R32Type _base, R32Type index, byte scale)
            : base(segment, _base, index, scale)
        {
        }

        public QWordMemory(SegType segment, string label)
            : base(segment, label)
        {
        }
        
        public QWordMemory(string label)
            : base(label)
        {
        }

        public QWordMemory(SegType segment, R16Type _base, R16Type index, UInt16 displacement)
            : base(segment, _base, index, displacement)
        {
        }
        
        public QWordMemory(SegType segment, R16Type _base, R16Type index)
            : base(segment, _base, index)
        {
        }
    }

    public class TWordMemory : Memory
    {
        public TWordMemory(SegType segment, R32Type _base, R32Type index, byte scale, UInt32 displacement)
            : base(segment, _base, index, scale, displacement)
        {
        }

        public TWordMemory(SegType segment, R32Type _base, R32Type index, byte scale)
            : base(segment, _base, index, scale)
        {
        }

        public TWordMemory(SegType segment, string label)
            : base(segment, label)
        {
        }
        
        public TWordMemory(string label)
            : base(label)
        {
        }

        public TWordMemory(SegType segment, R16Type _base, R16Type index, UInt16 displacement)
            : base(segment, _base, index, displacement)
        {
        }
        
        public TWordMemory(SegType segment, R16Type _base, R16Type index)
            : base(segment, _base, index)
        {
        }
    }

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
	
        private string  name = string.Empty;

	    public string  Name
	    {
		    get { return name;}
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

            return (UInt32) memoryStream.Length;
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
                        binaryWriter.Write((byte) 0x66);
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
                        binaryWriter.Write((UInt32) ((UInt32) ((UInt32[]) this.value)[valueIndex++] - binaryWriter.BaseStream.Length - 4));
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

    internal class Bits32Instruction : Instruction
    {
        public Bits32Instruction(bool value)
            :
            base(true, string.Empty, string.Empty, "[BITS", value ? "32]" : "16]", null, null, null, value, null)
        {
        }

    }

    internal class OffsetInstruction : Instruction
    {
        public OffsetInstruction(UInt32 value)
            : base(true, string.Empty, string.Empty, "TIMES", value.ToString() + "-($-$$) DB 0", null, null, null, value, null)
        {
        }
    }

    internal class OrgInstruction : Instruction
    {
        public OrgInstruction(UInt32 value)
            : base(true, string.Empty, string.Empty, "ORG", value.ToString(), null, null, null, value, null)
        {
        }
    }

    internal class DataInstruction : Instruction
    {
        public DataInstruction(bool indent, string label, string reference, string name, string parameters, Memory rmMemory, Register rmRegister, Register register, object value, string[] encoding)
            : base(indent, label, reference, name, parameters, rmMemory, rmRegister, register, value, encoding)
        {
        }

        public override bool Encode(bool bits32, BinaryWriter binaryWriter)
        {
            if (this.Value is string)
            {
                string value = (string) this.Value;

                for (int i = 0; i < value.Length; i++)
                {
                    binaryWriter.Write(value[i]);
                }
            }
            else if (this.Value is byte)
            {
                binaryWriter.Write((byte)this.Value);
            }
            else if (this.Value is UInt16)
            {
                binaryWriter.Write((UInt16)this.Value);
            }
            else if (this.Value is UInt32)
            {
                binaryWriter.Write((UInt32)this.Value);
            }
            else
            {
                throw new Exception("Wrong data type.");
            }

            return true;
        }
    }

    internal class ByteDataInstruction : DataInstruction
    {
        public ByteDataInstruction(string name, string values)
            : base(false, name, string.Empty, name, "DB \"" + values + "\"", null, null, null, values, null)
        {
        }

        public ByteDataInstruction(string name, byte value)
            : base(false, name, string.Empty, name, "DB " + string.Format("0x{0:X2}", value), null, null, null, value, null)
        {
        }

        public ByteDataInstruction(string values)
            : base(false, string.Empty, string.Empty, string.Empty, "DB \"" + values + "\"", null, null, null, values, null)
        {
        }

        public ByteDataInstruction(byte value)
            : base(false, string.Empty, string.Empty, string.Empty, "DB " + string.Format("0x{0:X2}", value), null, null, null, value, null)
        {
        }
    }

    internal class WordDataInstruction : DataInstruction
    {
        public WordDataInstruction(string name, UInt16 value)
            : base(false, name, string.Empty, name, "DW " + string.Format("0x{0:X4}", value), null, null, null, value, null)
        {
        }

        public WordDataInstruction(UInt16 value)
            : base(false, string.Empty, string.Empty, string.Empty, "DW " + string.Format("0x{0:X4}", value), null, null, null, value, null)
        {
        }
    }

    internal class DWordDataInstruction : DataInstruction
    {
        public DWordDataInstruction(string name, UInt32 value)
            : base(false, name, string.Empty, name, "DD " + string.Format("0x{0:X8}", value), null, null, null, value, null)
        {
        }

        public DWordDataInstruction(UInt32 value)
            : base(false, string.Empty, string.Empty, string.Empty, "DD " + string.Format("0x{0:X8}", value), null, null, null, value, null)
        {
        }
    }

    internal class LabelInstruction : Instruction
    {
        public LabelInstruction(string label)
            : base(false, label, string.Empty, label + ":", string.Empty, null, null, null, label, null)
        {
        }
    }

    public partial class Assembly
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

        public bool Encode(Engine engine, string destination)
        {
            MemoryStream memoryStream = new MemoryStream();

            foreach (Method method in engine)
            {
                this.GetAssemblyCode(method);
            }

            this.Encode(memoryStream);
            
            FileStream fileStream = new FileStream(destination, FileMode.Create);
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
                    instruction.RMMemory.Displacement = org + this.GetLabelAddress(instruction.RMMemory.Reference);
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
                        , Convert.ToUInt16((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value));
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
                    , Convert.ToUInt32((call.Operands[4] as SharpOS.AOT.IR.Operands.Constant).Value));
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
                        , Convert.ToUInt16((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value));
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
                    , Convert.ToUInt32((call.Operands[4] as SharpOS.AOT.IR.Operands.Constant).Value));
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
                        , Convert.ToUInt16((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value));
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
                    , Convert.ToUInt32((call.Operands[4] as SharpOS.AOT.IR.Operands.Constant).Value));
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
                        , Convert.ToUInt16((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value));
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
                    , Convert.ToUInt32((call.Operands[4] as SharpOS.AOT.IR.Operands.Constant).Value));
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
                        , Convert.ToUInt16((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value));
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
                    , Convert.ToUInt32((call.Operands[4] as SharpOS.AOT.IR.Operands.Constant).Value));
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
                        , Convert.ToUInt16((call.Operands[3] as SharpOS.AOT.IR.Operands.Constant).Value));
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
                    , Convert.ToUInt32((call.Operands[4] as SharpOS.AOT.IR.Operands.Constant).Value));
            }
            else
            {
                throw new Exception("'" + call.Method.Name + "' has wrong parameters.");
            }
        }

        private void SupportedType(string methodName, string name)
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
        }

        private void SetValue(SharpOS.AOT.IR.Operands.Operand operand, R32Type source)
        {
            if (operand is SharpOS.AOT.IR.Operands.Local)
            {
                UInt32 index = (UInt32)(-(operand as SharpOS.AOT.IR.Operands.Local).Index * 4);
                this.MOV(new DWordMemory(null, R32.EBP, null, 0, index), source);
            }
            else if (operand is SharpOS.AOT.IR.Operands.Argument)
            {
                UInt32 index = (UInt32)(4 + (operand as SharpOS.AOT.IR.Operands.Argument).Index * 4);
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
                UInt32 index = (UInt32)(-(operand as SharpOS.AOT.IR.Operands.Local).Index * 4);
                this.MOV(target, new DWordMemory(null, R32.EBP, null, 0, index));
            }
            else if (operand is SharpOS.AOT.IR.Operands.Argument)
            {
                UInt32 index = (UInt32)(4 + (operand as SharpOS.AOT.IR.Operands.Argument).Index * 4);
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

        private bool GetAssemblyCode(Method method)
        {
            foreach (ParameterDefinition parameter in method.MethodDefinition.Parameters)
            {
                SupportedType(method.MethodDefinition.Name, parameter.ParameterType.FullName);
            }

            foreach (VariableDefinition variable in method.MethodDefinition.Body.Variables)
            {
                SupportedType(method.MethodDefinition.Name, variable.VariableType.FullName);
            }

            //blocks.UpdateIndex();

            string fullname = method.MethodDefinition.DeclaringType.FullName + "." + method.MethodDefinition.Name;

            if (method.MethodDefinition.Name.StartsWith("BootSector") == false)
            {
                this.LABEL(fullname);
                this.PUSH(R32.EBP);
                this.MOV(R32.EBP, R32.ESP);
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
                this.POP(R32.EBP);
                this.RET();
            }

            return true;
        }
    }
}
