//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Cédric Rousseau <cedrou@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//
//#define LOG_IO

using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Generic;

namespace SharpOS.Tools.DiagnosticTool {
	public interface INamedPipe {
		bool Opened { get; }
		uint BytesWritten { get; }
		uint BytesRead { get; }
		string FileName { get; }

		Client.Status Open ();
		void Close ();
		Client.Status Read (byte [] buf, ref uint read);
		Client.Status Write (byte [] buf);

	}
}
