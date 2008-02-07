// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Text;
using System.IO;
using System.Xml;

namespace VSUpdate {
	class Program {
		static void Main (string [] args)
		{
			if (args.Length != 1) {
				Console.WriteLine ("Usage: VSUpdate <path>");
				return;
			}

			string start = args [0];

			if (!Directory.Exists (start)) {
				Console.WriteLine (string.Format ("'{0}' is not a directory.", start));
				return;
			}

			DirectoryInfo directoryInfo = new DirectoryInfo (start);

			Scan (directoryInfo);
		}

		private static void Scan (DirectoryInfo directoryInfo)
		{
			foreach (FileInfo fileInfo in directoryInfo.GetFiles ()) {
				if (fileInfo.Extension.ToLower ().Equals (".csproj")) {
					Process (fileInfo); 
					return;
				}
			}

			foreach (DirectoryInfo _directoryInfo in directoryInfo.GetDirectories ()) {
				Scan (_directoryInfo);
			}
		}

		private static void Process (FileInfo fileInfo)
		{
			XmlDocument xmlDocument = new XmlDocument ();

			xmlDocument.Load (fileInfo.FullName);

			
		}
	}
}
