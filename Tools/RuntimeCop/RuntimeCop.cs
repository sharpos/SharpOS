//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Mono.Cecil;
using Mono.GetOptions;

public class RuntimeCop {
	public RuntimeCop (CopOptions options)
	{
		this.options = options;

		if (this.options.RemainingArguments.Length == 0) {
			throw new ArgumentException ();
		}

		this.corlib = this.options.RemainingArguments [0];
	}

	CopOptions options;
	string corlib;
	List<MethodDefinition> internalStubs = new List<MethodDefinition> ();

	public int Run ()
	{
		AssemblyDefinition library;

		Console.WriteLine ("Loading `{0}'", corlib);
		library = AssemblyFactory.GetAssembly (corlib);

		foreach (ModuleDefinition module in library.Modules)
			ScanModule (module);

		Console.WriteLine ("Finished scanning.\n");

		foreach (MethodDefinition def in internalStubs)
			Console.WriteLine (def.DeclaringType.FullName + "\t\t\t" + def);

		return 0;
	}

	public void ScanModule (ModuleDefinition module)
	{
		foreach (TypeDefinition type in module.Types)
			ScanType (type);
	}

	public void ScanType (TypeDefinition type)
	{
		foreach (TypeDefinition nestedType in type.NestedTypes)
			ScanType (nestedType);

		foreach (MethodDefinition method in type.Constructors)
			ScanMethod (method);

		foreach (MethodDefinition method in type.Methods)
			ScanMethod (method);
	}

	public void ScanMethod (MethodDefinition method)
	{
		if (method.IsInternalCall)
			internalStubs.Add (method);
	}

	public static int Main (string [] args)
	{
		RuntimeCop cop;

		try {
			cop = new RuntimeCop (new CopOptions (args));
		} catch (ArgumentException) {
			Console.Error.WriteLine ("Bad arguments, see -help");
			return 1;
		}

		return cop.Run ();
	}
}
