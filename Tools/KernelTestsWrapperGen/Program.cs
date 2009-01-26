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
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;
using Mono.GetOptions;

namespace KernelTestsWrapperGen {
	public class WrapperOptions : Options {
		public WrapperOptions (string [] args)
			: base (args)
		{

		}

		[Option ("Specify the ilasm.exe with full path to compile SharpOS.Kernel.Test.Il.dll if needed.", 'c', "ilasm")]
		public string ILAsm = "";

		[Option ("Specify the string that has to be contained in the name of a CMP method in order ", 'f', "filter")]
		public string Filter = "";
	}

	class Program {
		private const string IL_DLL = "SharpOS.Kernel.Tests.IL.dll";
		private const string CS_DLL = "SharpOS.Kernel.Tests.CS.dll";
		private const string WRAPPER_CS = "KernelTestsWrapper.cs";
		private const string NUNIT_CS = "SharpOS.Kernel.Tests.NUnit.cs";

		static int Main (string [] args)
		{
			WrapperOptions opts = new WrapperOptions (args);

			string path = System.IO.Path.GetDirectoryName (System.Reflection.Assembly.GetExecutingAssembly ().Location);

			if (opts.ILAsm.Trim ().Length > 0)
				return CompileIL (opts.ILAsm.Trim (), path);

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
						tr.WriteLine ("\t{");
						tr.WriteLine ("\t\tAssert.IsTrue (" + entryFullName + " () == 1, \"'" + entry.DeclaringType.FullName + "." + entry.Name + "' failed.\");");
						tr.WriteLine ("\t}");

					} else {
						tr.WriteLine ("\t\t\tif (" + entryFullName + " () != 1) {");
						
						string errorMessage = "\"'" + entry.DeclaringType.FullName + "." + entry.Name + "' failed.\"";

						tr.WriteLine ("\t\t\t\tTextMode.WriteLine (" + errorMessage + ");");
						tr.WriteLine ("\t\t\t\tDebug.COM1.WriteLine (" + errorMessage + ");");
						tr.WriteLine ("\t\t\t\tfailures++;");
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
			tr.WriteLine ("// Licensed under the terms of the GNU GPL v3,");
			tr.WriteLine ("//  with Classpath Linking Exception for Libraries");
			tr.WriteLine ("//");

			tr.WriteLine ("");

			return tr;
		}

		public static int ScriptMain (string path, string filter)
		{
			string ilDLL = Path.Combine (path, IL_DLL);
			string csDLL = Path.Combine (path, CS_DLL);

			string kernelTestsPath = Path.Combine (Path.Combine (Path.Combine (path, ".."), "Kernel"), "Core");

			string wrapperCS = Path.Combine (kernelTestsPath, WRAPPER_CS);
			string nunitCS = Path.Combine (Path.Combine (Path.Combine (Path.Combine (Path.Combine (path, ".."), "Kernel"), "Tests"), "NUnit"), NUNIT_CS);

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

				tr.WriteLine ("using SharpOS.Kernel.ADC;");
				tr.WriteLine ();

				tr.WriteLine ("namespace SharpOS.Kernel.Tests {");
				tr.WriteLine ("\tpublic unsafe class Wrapper {");
				tr.WriteLine ("\t\tpublic static void Run ()");
				tr.WriteLine ("\t\t{");
				tr.WriteLine ("#if KERNEL_TESTS");
				tr.WriteLine ("\t\t\tint failures = 0;");

				ProcessAssembly (false, path, tr, ilDLL, filter);
				ProcessAssembly (false, path, tr, csDLL, filter);

				tr.WriteLine ("\t\t\tif (failures > 0)");
				tr.WriteLine ("\t\t\t\tTextMode.WriteLine (\"Not all tests passed!\");");
				tr.WriteLine ("\t\t\telse");
				tr.WriteLine ("\t\t\t\tTextMode.WriteLine (\"All test cases have completed successfully!\");");

				tr.WriteLine ("#endif");
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
			List<string> files = new List<string> ();

			if (!ContinueCompileIL (buildPath, files)) {
				Console.WriteLine ("Skipped.");
				return 0;
			}

			string outputFile = '"' + Path.Combine (buildPath, IL_DLL) + '"';
			string fileList = "";

			foreach (string filename in files)
				fileList += " " + '"' + filename + '"';

			ProcessStartInfo startInfo = new ProcessStartInfo ();
			startInfo.FileName = ilasm;
			startInfo.Arguments = " /dll /quiet /debug /nologo /output=" + outputFile + fileList;
			startInfo.RedirectStandardOutput = true;
			startInfo.RedirectStandardError = true;
			startInfo.UseShellExecute = false;

			try {
				Process process = Process.Start (startInfo);

				string output = process.StandardOutput.ReadToEnd ();
				string error = process.StandardError.ReadToEnd ();

				process.WaitForExit ();

				Console.WriteLine (output);
				Console.WriteLine (error);

				Console.WriteLine ("Done.");

				return process.ExitCode;

			} catch (Exception exception) {
				Console.WriteLine ("Failed: " + exception.Message);
			}

			return -1;
		}

		private static bool ContinueCompileIL (string buildPath, List<string> files)
		{
			bool result = false;

			string ilDLL = Path.Combine (buildPath, IL_DLL);

			FileInfo ilDllFileInfo = new FileInfo (ilDLL);

			if (ilDllFileInfo == null)
				result = true;

			string ilPath = Path.Combine (Path.Combine (Path.Combine (Path.Combine (buildPath, ".."), "Kernel"), "Tests"), "IL");

			DirectoryInfo directoryInfo = new DirectoryInfo (ilPath);

			FileInfo [] fileList = directoryInfo.GetFiles ("*.il");

			foreach (FileInfo file in fileList) {
				if (!result && file.LastWriteTime > ilDllFileInfo.LastWriteTime)
					result = true;
				
				files.Add (file.FullName);
			}

			return result;
		}
	}
}
