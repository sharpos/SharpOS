// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;

namespace KernelTestsWrapperGen {
	class Program {
		static void Main (string [] args)
		{
			ScriptMain (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly ().Location));
		}

		public static void ProcessAssembly (bool unitTests, string path, TextWriter tr, string assemblyFile)
		{
			AssemblyDefinition library = AssemblyFactory.GetAssembly (assemblyFile);

			foreach (TypeDefinition type in library.MainModule.Types) {
				if (type.Name.Equals ("<Module>"))
					continue;

				foreach (MethodDefinition entry in type.Methods) {
					if (!(entry.IsStatic
							&& entry.ImplAttributes == MethodImplAttributes.Managed
							&& entry.Name.StartsWith ("CMP"))) {

						continue;
					}

					if (unitTests) {
						tr.WriteLine ("\t[Test]");
						tr.WriteLine ("\tpublic void " + entry.DeclaringType.FullName.Replace (".", "_") + "_" + entry.Name + " ()");
						tr.WriteLine ("\t\t{");
						tr.WriteLine ("\t\t\tAssert.IsTrue (" + entry.DeclaringType.FullName + "." + entry.Name + " () == 1, \"'" + entry.DeclaringType.FullName + "." + entry.Name + "' failed.\");");
						tr.WriteLine ("\t\t}");

					} else {
						tr.WriteLine ("\t\t\tif (" + entry.DeclaringType.FullName + "." + entry.Name + " () != 1) {");
						tr.WriteLine ("\t\t\t\tScreen.WriteLine (KRNL.String (\"'" + entry.DeclaringType.FullName + "." + entry.Name + "' failed.\"));");
						tr.WriteLine ("\t\t\t\treturn;");
						tr.WriteLine ("\t\t\t}");
						tr.WriteLine ("");
					}
				}
			}
		}

		public static TextWriter OpenFile (string path, string name)
		{
			path = Path.Combine (path, "..");
			path = Path.Combine (path, "AOT");
			path = Path.Combine (path, "Kernel.Tests");

			TextWriter tr = new StreamWriter (File.Open (Path.Combine (path, name), FileMode.Create));
			tr.WriteLine ("//");
			tr.WriteLine ("// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)");
			tr.WriteLine ("//");
			tr.WriteLine ("// Authors:");
			tr.WriteLine ("//	Mircea-Cristian Racasan <darx_kies@gmx.net>");
			tr.WriteLine ("//");
			tr.WriteLine ("// Licensed under the terms of the GNU GPL License version 2.");
			tr.WriteLine ("//");

			tr.WriteLine ("");

			return tr;
		}

		public static void ScriptMain (string path)
		{
			string ilDLL = Path.Combine (path, "SharpOS.Kernel.Tests.IL.dll");
			string csDLL = Path.Combine (path, "SharpOS.Kernel.Tests.CS.dll");

			TextWriter tr = OpenFile (path, "Wrapper.cs");

			tr.WriteLine ("namespace SharpOS {");
			tr.WriteLine ("\tpublic unsafe partial class KRNL {");
			tr.WriteLine ("\t\tprotected static void RunTests ()");
			tr.WriteLine ("\t\t{");

			ProcessAssembly (false, path, tr, ilDLL);
			ProcessAssembly (false, path, tr, csDLL);

			tr.WriteLine ("\t\t\tScreen.WriteLine (KRNL.String (\"All test cases have completed successfully!\"));");

			tr.WriteLine ("\t\t}");
			tr.WriteLine ("\t}");
			tr.WriteLine ("}");
			tr.Close ();


			tr = OpenFile (path, Path.Combine ("NUnit", "SharpOS.Kernel.Tests.NUnit.cs"));
			tr.WriteLine ("using NUnit.Framework;");

			tr.WriteLine ("");

			tr.WriteLine ("[TestFixture]");
			tr.WriteLine ("public class KernelTests {");

			ProcessAssembly (true, path, tr, ilDLL);
			ProcessAssembly (true, path, tr, csDLL);

			tr.WriteLine ("}");

			tr.Close ();
		}
	}
}
