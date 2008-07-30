//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//  Phil Garcia (aka tgiphil) <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using SharpOS.Kernel.DeviceSystem;

namespace SharpOS.Kernel.DeviceSystem
{
    public interface IPartitionDevice 
    {
        byte[] ReadBlock(uint block, uint count);
        bool ReadBlock(uint block, uint count, byte[] data);
        bool WriteBlock(uint block, uint count, byte[] data);
        uint BlockCount { get; }
        uint BlockSize { get; }
        bool CanWrite { get; }
    }

}
