// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System.Runtime.InteropServices;
using SharpOS.AOT.Metadata;
using SharpOS.Korlib.Runtime;

namespace SharpOS.Korlib.Runtime {
	[SharpOS.AOT.Attributes.ExceptionHandlingClause]
	[StructLayout (LayoutKind.Sequential)]
	internal unsafe class ExceptionHandlingClause {
		public ExceptionHandlerType ExceptionType;
		public TypeInfo TypeInfo;
		public void* TryBegin;
		public void* TryEnd;
		public void* FilterBegin;
		public void* FilterEnd;
		public void* HandlerBegin;
		public void* HandlerEnd;
	}
}
