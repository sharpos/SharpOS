using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace SharpOS.AOT
{
	internal class ExecutableBuilder
	{
		private CompilerOptions options;
		private AssemblyDefinition input = null;
		private FileStream output = null;
		
		public ExecutableBuilder(CompilerOptions options)
		{
			this.options = options;
		}
		
		public void Build()
		{
			// Set up input and output
			input = AssemblyFactory.GetAssembly(options.AssemblyFileName); // TODO: Wrap in a try/catch
			output = File.Open(options.OutputFileName, FileMode.Create);
			
			// TODO: Create executable header information here
			
			// Perform work on each type in assembly
			foreach(TypeDefinition t in input.MainModule.Types)
			{
				// Write static methods
				Console.WriteLine(t.FullName);
				foreach(MethodDefinition method in t.Methods)
				{
					if (method.IsStatic) buildMethod(method);
				}
			}
			
			
			// TODO: Perform optimizations on generated instructions here
			
			// TODO: Create executable footer information, if needed, here
			
			output.Close();
		}
		
		private void buildMethod(MethodDefinition method)
		{
			MethodOptions methodOps = new MethodOptions();
			
			// Calculate size needed on stack
			uint methodHeaderSize = 0;
			uint totalSize = methodHeaderSize;			
			
			foreach(VariableDefinition var in method.Body.Variables)
			{
				uint size = (uint)sizeOf(var.VariableType);
				methodOps.LocalVariableOffsets.Add(size);
				totalSize += size;
			}
			
			methodOps.MethodHeaderOffset = methodHeaderSize;
			methodOps.MethodOffset = totalSize;
			
			// Write NASM instructions
			StreamWriter o = new StreamWriter("output");
			
			foreach(Instruction instruction in method.Body.Instructions)
			{
				o.Write(processInstruction(instruction, methodOps));
				o.WriteLine();
			}
			
			o.Close();
		}
		
		private int sizeOf(TypeReference type)
		{
			if (type.FullName == "System.Byte*")
				return 8;
			else
				return 0;
		}
		
		private string processInstruction(Instruction instruction, MethodOptions methodOps)
		{
			string rtn = String.Empty;
			
			if (instruction.OpCode == OpCodes.Ldc_I4 || instruction.OpCode == OpCodes.Ldc_I4_S)
			{
				rtn = loadConstant(instruction, methodOps);
			}
			else if (instruction.OpCode == OpCodes.Stloc_0 || instruction.OpCode == OpCodes.Stloc_1)
			{
				rtn = storeLocal(instruction, methodOps);
			}
			else if (instruction.OpCode == OpCodes.Ldloc_0 || instruction.OpCode == OpCodes.Ldloc_1)
			{
				rtn = loadLocal(instruction, methodOps);
			}
			else if (instruction.OpCode == OpCodes.Stind_I1)
			{
				rtn = storeIndirect(instruction, methodOps);
			}
			
			return rtn;
		}
		
		/// <summary>
		/// Generates instructions to convert the value at the top of the stack to a specified size.
		/// </summary>
		private string convert(Instruction instruction, MethodOptions methodOps)
		{
			int currentSize = (int)methodOps.CilStack.Peek().Size;
			int targetSize = 0;
			if (instruction.OpCode == OpCodes.Conv_I1)
			{
				targetSize = 1;
			}
			else if (instruction.OpCode == OpCodes.Conv_I2)
			{
				targetSize = 2;
			}
			else if (instruction.OpCode == OpCodes.Conv_I || instruction.OpCode == OpCodes.Conv_I4)
			{
				targetSize = 4;
			}
			
			methodOps.CilStack.Peek().Size = (uint)targetSize;
			
			return String.Empty;
			
			// TODO: Truncate the value and generate instructions to push the new value (either sign or zero extended) back onto the stack
		}
				
		/// <summary>
		/// Generates instructions to store a constant to the top of the CIL stack.
		/// </summary>
		private string loadConstant(Instruction instruction, MethodOptions methodOps)
		{
			string rtn = String.Empty;
			
			if (instruction.OpCode == OpCodes.Ldc_I4 || instruction.OpCode == OpCodes.Ldc_I4_S)
			{
				rtn = mov("DWORD", "[EBP+" + totalCilStackOffset(methodOps) + "]", instruction.Operand.ToString());
				methodOps.CilStack.Push(new CilStackEntry(null, 4));
			}
			
			return rtn;
		}
		
		/// <summary>
		/// Generates instructions to store the value at the top of the CIL stack to the specified location.
		/// </summary>
		private string storeLocal(Instruction instruction, MethodOptions methodOps)
		{
			string rtn = String.Empty;
			
			uint local = 0;
			if (instruction.OpCode == OpCodes.Stloc_0)
			{
				local = 0;
			}
			else if (instruction.OpCode == OpCodes.Stloc_1)
			{
				local = 1;
			}
			else if (instruction.OpCode == OpCodes.Stloc_2)
			{
				local = 2;
			}
			else if (instruction.OpCode == OpCodes.Stloc_3)
			{
				local = 3;
			}
			else if (instruction.OpCode == OpCodes.Stloc || instruction.OpCode == OpCodes.Stloc_S)
			{
				local = (uint)instruction.Operand;
			}
			
			rtn = mov("DWORD", "EBX", "[EBP+" + (totalCilStackOffset(methodOps) - methodOps.CilStack.Peek().Size) + "]");
			rtn += mov("DWORD", "[EBP+" + methodOps.LocalVariableOffsets[(int)local] + "]",  "EBX");
			
			methodOps.CilStack.Pop();
			
			return rtn;
		}
		
		/// <summary>
		/// Generates instructions to push the value of the specified local to the top of the CIL stack.
		/// </summary>
		private string loadLocal(Instruction instruction, MethodOptions methodOps)
		{
			string rtn = String.Empty;
			
			int local = 0;
			if (instruction.OpCode == OpCodes.Ldloc_0)
			{
				local = 0;
			}
			else if (instruction.OpCode == OpCodes.Ldloc_1)
			{
				local = 1;
			}
			else if (instruction.OpCode == OpCodes.Ldloc_2)
			{
				local = 2;
			}
			else if (instruction.OpCode == OpCodes.Ldloc_3)
			{
				local = 3;
			}
			else if (instruction.OpCode == OpCodes.Ldloc || instruction.OpCode == OpCodes.Ldloc_S)
			{
				local = (int)instruction.Operand;
			}
			
			rtn = mov("DWORD", "EBX", "[EBP+" + methodOps.LocalVariableOffsets[local] + "]");
			rtn += mov("DWORD", "[EBP+" + totalCilStackOffset(methodOps) + "]", "EBX");
				
			methodOps.CilStack.Push(new CilStackEntry(null, 4));
			
			return rtn;
		}
		
		/// <summary>
		/// Generates instructions to store the value at the top of the CIL stack to the address of the next entry on the CIL stack.
		/// </summary>
		private string storeIndirect(Instruction instruction, MethodOptions methodOps)
		{
			string rtn = String.Empty;
			
			int size = 0;
			if (instruction.OpCode == OpCodes.Stind_I1)
			{
				size = 1;
			}
			
			// TODO: Value is always put in an 8-bit register
			rtn = mov(Enum.GetName(typeof(OperationSizes), size), "BL", "[EBP+" + (totalCilStackOffset(methodOps) - methodOps.CilStack.Peek().Size) + "]");
			methodOps.CilStack.Pop();
			
			rtn += mov(Enum.GetName(typeof(OperationSizes), methodOps.CilStack.Peek().Size), "EAX", "[EBP+" + (totalCilStackOffset(methodOps) - methodOps.CilStack.Peek().Size) + "]");
			methodOps.CilStack.Pop();
			
			rtn += mov(Enum.GetName(typeof(OperationSizes), size), "[EAX]", "BL");
						
			return rtn;
		}
				
		private string mov(string size, string destination, string source)
		{
			return "\t" + "mov " + size + " " + destination + ", " + source + "\n";
		}
		
		/// <summary>
		/// Returns the total size of the current CIL stack.
		/// </summary>
		private uint cilStackOffset(CilStack offsets)
		{
			uint rtn = 0;
			
			foreach(CilStackEntry a in offsets)
				rtn += a.Size;
			
			return rtn;
		}
		
		/// <summary>
		/// Returns the total size of the stack from EBP.
		/// </summary>
		private uint totalCilStackOffset(MethodOptions methodOps)
		{
			return methodOps.MethodOffset + cilStackOffset(methodOps.CilStack);
		}
	}
}
