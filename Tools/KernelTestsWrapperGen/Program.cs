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
using System.Diagnostics;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;
using Mono.GetOptions;

namespace KernelTestsWrapperGen {
	public class WrapperOptions : Options
	{
		public WrapperOptions (string [] args)
			: base (args)
		{

		}

		[Option ("Specify the ilasm.exe with full path to compile SharpOS.Kernel.Test.Il.dll if needed.", 'c', "ilasm")]
		public string ILAsm = "";
		
		[Option ("Specify the string that has to be contained in the name of a CMP method in order ", 'f', "filter")]
		public string Filter = "";
	}

	class Program 
	{
		private const string IL_DLL = "SharpOS.Kernel.Tests.IL.dll";
		private const string CS_DLL = "SharpOS.Kernel.Tests.CS.dll";
		private const string WRAPPER_CS = "Wrapper.cs";
		private const string NUNIT_CS = "SharpOS.Kernel.Tests.NUnit.cs";

		static int Main (string [] args)
		{
			WrapperOptions opts = new WrapperOptions (args);

			string path = System.IO.Path.GetDirectoryName (System.Reflection.Assembly.GetExecutingAssembly ().Location);

			if (opts.ILAsm.Trim ().Length > 0)
				return CompileIL (opts.ILAsm.Trim(), path);
			
			return ScriptMain (path, opts.Filter.Trim ());
		}

		public static void ProcessAssembly (bool unitTests, string path, TextWriter tr, string assemblyFile, string filter)
		{
			AssemblyDefinition library = AssemblyFactory.GetAssembly (assemblyFile);

			foreach (TypeDefinition type in library.MainModule.Types) {
				if (type.Name.Equals ("<Module>"))
					continue;

				foreach (MethodDefinition entry in type.Methods) {
					// Skip the method if it is not static, not managed and it does not start with CMP.
					if (!(entry.IsStatic
							&& entry.ImplAttributes == MethodImplAttributes.Managed
							&& entry.Name.StartsWith ("CMP"))) {

						continue;
					}

					string entryFullName = entry.DeclaringType.FullName + "." + entry.Name;

					// If a filter has been defined and it is not contained in the method name then skip it.
					if (filter.Length > 0)
						if (entryFullName.IndexOf (filter) == -1)
							continue;
						else
							Console.WriteLine (string.Format ("Added '{0}'.", entryFullName));

					if (unitTests) {
						tr.WriteLine ("\t[Test]");
						tr.WriteLine ("\tpublic void " + entry.DeclaringType.FullName.Replace (".", "_") + "_" + entry.Name + " ()");
						tr.WriteLine ("\t\t{");
						tr.WriteLine ("\t\t\tAssert.IsTrue (" + entryFullName + " () == 1, \"'" + entry.DeclaringType.FullName + "." + entry.Name + "' failed.\");");
						tr.WriteLine ("\t\t}");

					} else {
						tr.WriteLine ("\t\t\tif (" + entryFullName + " () != 1) {");
						tr.WriteLine ("\t\t\t\tScreen.WriteLine (KRNL.String (\"'" + entry.DeclaringType.FullName + "." + entry.Name + "' failed.\"));");
						//tr.WriteLine ("\t\t\t\treturn;");
						tr.WriteLine ("\t\t\t}");
						tr.WriteLine ("");
					}
				}
			}
		}

		public static TextWriter OpenFile (string name)
		{
			TextWriter tr = new StreamWriter (File.Open (name, FileMode.Create));
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

		public static int ScriptMain (string path, string filter)
		{
			string ilDLL = Path.Combine (path, IL_DLL);
			string csDLL = Path.Combine (path, CS_DLL);

			string kernelTestsPath = Path.Combine (Path.Combine (Path.Combine (path, ".."), "AOT"), "Kernel.Tests");
			
			string wrapperCS = Path.Combine (kernelTestsPath, WRAPPER_CS);
			string nunitCS = Path.Combine (Path.Combine (kernelTestsPath, "NUnit"), NUNIT_CS);


			FileInfo ilDLLFileInfo = new FileInfo (ilDLL);
			FileInfo csDLLFileInfo = new FileInfo (csDLL);
			FileInfo wrapperCSFileInfo = new FileInfo (wrapperCS);
			FileInfo nunitCSFileInfo = new FileInfo (nunitCS);


			if (ilDLLFileInfo == null || csDLLFileInfo == null) {
				Console.WriteLine ("One of the source .DLLs is missing.");
				return 1;
			}

			if (filter.Length > 0
					|| wrapperCSFileInfo == null
					|| wrapperCSFileInfo.LastWriteTime < csDLLFileInfo.LastWriteTime
					|| wrapperCSFileInfo.LastWriteTime < ilDLLFileInfo.LastWriteTime) {
				TextWriter tr = OpenFile (wrapperCS);

				tr.WriteLine ("namespace SharpOS {");
				tr.WriteLine ("\tpublic unsafe partial class KRNL {");
				tr.WriteLine ("\t\tprotected static void RunTests ()");
				tr.WriteLine ("\t\t{");

				ProcessAssembly (false, path, tr, ilDLL, filter);
				ProcessAssembly (false, path, tr, csDLL, filter);

				tr.WriteLine ("\t\t\tScreen.WriteLine (KRNL.String (\"All test cases have completed successfully!\"));");

				tr.WriteLine ("\t\t}");
				tr.WriteLine ("\t}");
				tr.WriteLine ("}");
				tr.Close ();
				
				Console.WriteLine (string.Format ("'{0}' generated.", wrapperCS));

			} else
				Console.WriteLine (string.Format ("Skipping '{0}'.", wrapperCS));


			if (filter.Length > 0
					|| nunitCSFileInfo == null
					|| nunitCSFileInfo.LastWriteTime < csDLLFileInfo.LastWriteTime
					|| nunitCSFileInfo.LastWriteTime < ilDLLFileInfo.LastWriteTime) {

				TextWriter tr = OpenFile (nunitCS);
				tr.WriteLine ("using NUnit.Framework;");

				tr.WriteLine ("");

				tr.WriteLine ("[TestFixture]");
				tr.WriteLine ("public class KernelTests {");

				ProcessAssembly (true, path, tr, ilDLL, filter);
				ProcessAssembly (true, path, tr, csDLL, filter);

				tr.WriteLine ("}");

				tr.Close ();

				Console.WriteLine (string.Format ("'{0}' generated.", nunitCS));

			} else
				Console.WriteLine (string.Format ("Skipping '{0}'.", nunitCS));

			return 0;
		}

		private static int CompileIL (string ilasm, string buildPath)
		{
			string fileList;

			if (!ContinueCompileIL (buildPath, out fileList)) {
				Console.WriteLine ("Skipped.");
				return 0;
			}

			string ilDLL = Path.Combine (buildPath, IL_DLL);

			ProcessStartInfo startInfo = new ProcessStartInfo ();
			startInfo.FileName = ilasm;
			startInfo.Arguments = "/dll /quiet /debug /nologo /output=" + ilDLL + " " + fileList;
			startInfo.RedirectStandardOutput = true;
			startInfo.RedirectStandardError = true;
			startInfo.UseShellExecute = false;

			Process process = Process.Start (startInfo);
			
			string output = process.StandardOutput.ReadToEnd ();
			string error = process.StandardError.ReadToEnd ();

			process.WaitForExit ();

			Console.WriteLine (output);
			Console.WriteLine (error);

			Console.WriteLine ("Done.");
			return process.ExitCode;
		}

		private static bool ContinueCompileIL (string buildPath, out string files)
		{
			bool result = false;

			string ilDLL = Path.Combine (buildPath, IL_DLL);

			FileInfo ilDllFileInfo = new FileInfo (ilDLL);

			if (ilDllFileInfo == null)
				result = true;

			string ilPath = Path.Combine (Path.Combine (Path.Combine (Path.Combine (buildPath, ".."), "AOT"), "Kernel.Tests"), "IL");

			DirectoryInfo directoryInfo = new DirectoryInfo (ilPath);

			FileInfo [] fileList = directoryInfo.GetFiles ("*.il");

			StringBuilder stringBuilder = new StringBuilder ();

			foreach (FileInfo file in fileList) {
				if (!result 
						&& file.LastWriteTime > ilDllFileInfo.LastWriteTime)
					result = true;

				stringBuilder.Append (" ");
				stringBuilder.Append (file.FullName);
			}

			files = stringBuilder.ToString ();

			return result;
		}
	}
}
