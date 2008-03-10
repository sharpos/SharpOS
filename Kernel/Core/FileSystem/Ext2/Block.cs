//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//  Jae Hyun Roh <wonbear@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Collections.Generic;
using System.Text;

using SharpOS.Kernel.ADC;
using SharpOS.Kernel.Foundation;

namespace SharpOS.Kernel.FileSystem.Ext2
{
	public unsafe struct Block {
        public unsafe void SetBlock(uint offset, byte* buffer)
        {
            this._offset = offset;
            this.dirty = false;

            this.buffer = buffer;
        }

        private uint _offset;
        public byte* buffer;
		private bool dirty;

        public bool Dirty {
            get {
                return this.dirty;
            }
            set {
                this.dirty = value;
            }
        }

        public uint Offset {
            get {
                return this._offset;
            }
        }

        public byte* Buffer {
            get {
                return this.buffer;
            }
        }

        public uint GetUInt(uint offset)
        {
            uint value = this.buffer[offset++];
            value += (uint)(this.buffer[offset++] << 8);
            value += (uint)(this.buffer[offset++] << 16);
            value += (uint)(this.buffer[offset++] << 24);

            return value;
        }

        public void SetUInt(uint offset, uint value)
        {
            this.dirty = true;

            this.buffer[offset++] = (byte)(value & 0xFF);
            this.buffer[offset++] = (byte)((value >> 8) & 0xFF);
            this.buffer[offset++] = (byte)((value >> 16) & 0xFF);
            this.buffer[offset++] = (byte)((value >> 24) & 0xFF);
        }

        public ushort GetUShort(uint offset)
        {
            ushort value = this.buffer[offset++];
            value += (ushort)(this.buffer[offset++] << 8);

            return value;
        }

        public void SetUShort(uint offset, ushort value)
        {
            this.dirty = true;

            this.buffer[offset++] = (byte)(value & 0xFF);
            this.buffer[offset++] = (byte)((value >> 8) & 0xFF);
        }

        public int GetInt(uint offset)
        {
            return (int)this.GetUInt(offset);
        }

        public void SetInt(uint offset, int value)
        {
            this.SetUInt(offset, (uint)value);
        }

        public byte GetByte(uint offset)
        {
            return this.buffer[offset];
        }

        public void SetByte(uint offset, byte value)
        {
            this.dirty = true;

            this.buffer[offset] = value;
        }

        public SharpOS.Kernel.Foundation.CString8* GetString(uint length, uint offset)
        {
            return (CString8*)(buffer + offset);
        }

        public byte* GetByteArray(uint count, uint offset)
        {
            byte* result = (byte*)MemoryManager.Allocate(count);

            for (int i = 0; i < count; ++i)
                result[i] = this.buffer[offset + count];

            return result;
        }

        public void SetByteArray(uint count, uint offset, byte* values)
        {
            this.dirty = true;

            for (int i = 0; i < count; i++)
                this.buffer[offset + i] = values[i];
        }

        public uint* GetUIntArray(uint count, uint offset)
        {
            uint* result = (uint*)MemoryManager.Allocate(sizeof(uint) * count);

            for (int i = 0; i < count; i++)
            {
                uint value = this.buffer[offset++];
                value += (uint)(this.buffer[offset++] << 8);
                value += (uint)(this.buffer[offset++] << 16);
                value += (uint)(this.buffer[offset++] << 24);

                result[i] = value;
            }

            return result;
        }

        public void SetUIntArray(uint count, uint offset, uint* values)
        {
            this.dirty = true;

            for (int i = 0; i < count; i++)
            {
                uint value = values[i];

                this.buffer[offset++] = (byte)(value & 0xFF);
                this.buffer[offset++] = (byte)((value >> 8) & 0xFF);
                this.buffer[offset++] = (byte)((value >> 16) & 0xFF);
                this.buffer[offset++] = (byte)((value >> 24) & 0xFF);
            }
        }
	}
}
