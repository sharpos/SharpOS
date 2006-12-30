using System;
using System.Reflection;

namespace SharpOS.AOT
{
	public static class AOTCompiler
	{
		public static void Main(string[] args)
		{
			// Parse out command line options
			CompilerOptions options = new CompilerOptions();
			
			for (int i = 0; i < args.Length; ++i)
			{
				// TODO: Support --environment-independent MyNamespace.MyClass,...
				// TODO: Support --native-injection MyNamespace.MyClass.MyMethodStub:myfile.bin,...
				
				if (args[i] == "-eia" || args[i] == "--environment-independent-code-all")
				{
					options.CompileAllAsEnvironmentIndependent = true;
				}
				else if (args[i] == "-a" || args[i] == "--input-assembly")
				{
					if (args.Length - 1 >= i + 1) // TODO: Throw exception on else
						options.AssemblyFileName = args[++i];
				}
				else if (args[i] == "-o" || args[i] == "--out")
				{
					if (args.Length - 1 >= i + 1) // TODO: Throw exception on else
						options.OutputFileName = args[++i];
				}
				// TODO: Throw exception on else
			}
			
			// Check options
			if (String.IsNullOrEmpty(options.AssemblyFileName))
			{
				throw new Exception("No input file.");
			}
			
			if (String.IsNullOrEmpty(options.OutputFileName))
			{
				options.OutputFileName = options.AssemblyFileName + ".aot";				
			}
			
			// Execute the build process
			ExecutableBuilder builder = new ExecutableBuilder(options);
			builder.Build();
		}
	}
}