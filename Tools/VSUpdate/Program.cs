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
using System.Collections.Generic;
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

			Console.WriteLine (string.Format ("Updating: {0}", fileInfo.FullName));

			string xmlns = "";

			if (xmlDocument.DocumentElement.NamespaceURI != "") {
				xmlns = xmlDocument.DocumentElement.GetAttribute ("xmlns");
				xmlDocument.DocumentElement.SetAttribute ("xmlns", "");
				xmlDocument.LoadXml (xmlDocument.OuterXml);
			}

			XmlNodeList xmlNodeList = xmlDocument.SelectNodes("/Project/ItemGroup/Compile");

			if (xmlNodeList.Count == 0)
				return;

			XmlNode itemGroup = xmlNodeList [0].ParentNode;

			List<string> files = new List<string> ();

			GetFiles (fileInfo.DirectoryName.Length, fileInfo.Directory, files);

			itemGroup.RemoveAll ();

			foreach (string value in files) {
				XmlAttribute attribute = xmlDocument.CreateAttribute ("Include");
				attribute.Value = value;

				XmlNode node = xmlDocument.CreateElement ("Compile");
				node.Attributes.Append (attribute);

				itemGroup.AppendChild (node);
			}

			if (xmlns.Length > 0)
				xmlDocument.DocumentElement.SetAttribute ("xmlns", xmlns);

			xmlDocument.Save (fileInfo.FullName);
		}

		private static void GetFiles (int stripPrefix, DirectoryInfo directoryInfo, List<string> files)
		{
			foreach (DirectoryInfo _entry in directoryInfo.GetDirectories ())
				GetFiles (stripPrefix, _entry, files);

			foreach (FileInfo _entry in directoryInfo.GetFiles ("*.cs"))
				files.Add (_entry.FullName.Substring (stripPrefix + 1));
		}	
	}
}
