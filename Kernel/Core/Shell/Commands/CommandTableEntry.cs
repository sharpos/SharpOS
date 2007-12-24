// 
// (C) 2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	Bruce Markham <illuminus86@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Collections.Generic;
using System.Text;
using SharpOS.Foundation;
using System.Runtime.InteropServices;

namespace SharpOS.Shell.Commands
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct CommandTableEntry
    {
        public CString8* name;
        public CString8* shortDescription;
 
        public void* func_Execute;
        public void* func_GetHelp;

        public CommandTableEntry* nextEntry;

        public void DISPOSE()
        {
        }
    }
}