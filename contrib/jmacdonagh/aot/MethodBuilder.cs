using System;
using System.Collections.Generic;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace SharpOS.AOT
{
	/// <summary>
	/// Generates a List of OperationBase objects which natively implement a MethodDefinition CIL method.
	/// </summary>
	public sealed class MethodBuilder
	{
		private MethodDefinition method;
		
		/// <summary>
		/// Instantiates a new MethodBuilder instance with a MethodDefinition to build.
		/// </summary>
		public MethodBuilder(MethodDefinition method)
		{
			this.method = method;
		}
		
		public List<OperationBase> Build()
		{
			// Initialize state
			State state = new State();
			
			foreach(VariableDefinition var in method.Body.Variables)
				state.Locals.Add(new LocalSize(var, sizeOf(var.VariableType)));
			
			for (int i = 0; i < method.Body.Instructions.Count; ++i)
			{
				Instruction ins = method.Body.Instructions[i];
				
				if (isLoadConstant(ins.OpCode))
					loadConstant(ins, state);
				else if (isStoreLocal(ins.OpCode))
					storeLocal(ins, state);
			}
			
			return null;
		}
		
		private uint sizeOf(TypeReference type)
		{
			return 0;
		}
		
		#region Instruction Handlers
		
		private void loadConstant(Instruction ins, State state)
		{
			if (ins.OpCode == OpCodes.Ldc_I4 || ins.OpCode == OpCodes.Ldc_I4_0 || ins.OpCode == OpCodes.Ldc_I4_1 || ins.OpCode == OpCodes.Ldc_I4_2 || ins.OpCode == OpCodes.Ldc_I4_3 || ins.OpCode == OpCodes.Ldc_I4_4 || ins.OpCode == OpCodes.Ldc_I4_5 || ins.OpCode == OpCodes.Ldc_I4_6 || ins.OpCode == OpCodes.Ldc_I4_7 || ins.OpCode == OpCodes.Ldc_I4_8 || ins.OpCode == OpCodes.Ldc_I4_M1)
			{
				int constant = 0;
				
				if (ins.OpCode == OpCodes.Ldc_I4)
					constant = (int)ins.Operand;
				else if(ins.OpCode == OpCodes.Ldc_I4_0)
					constant = 0;
				else if(ins.OpCode == OpCodes.Ldc_I4_1)
					constant = 1;
				else if(ins.OpCode == OpCodes.Ldc_I4_2)
					constant = 2;
				else if(ins.OpCode == OpCodes.Ldc_I4_3)
					constant = 3;
				else if(ins.OpCode == OpCodes.Ldc_I4_4)
					constant = 4;
				else if(ins.OpCode == OpCodes.Ldc_I4_5)
					constant = 5;
				else if(ins.OpCode == OpCodes.Ldc_I4_6)
					constant = 6;
				else if(ins.OpCode == OpCodes.Ldc_I4_7)
					constant = 7;
				else if(ins.OpCode == OpCodes.Ldc_I4_8)
					constant = 8;
				else if (ins.OpCode == OpCodes.Ldc_I4_M1)
					constant = -1;
				
				state.Operations.Add(new Mov(OperationSizes.DoubleWord, Registers.EBP, state.Stack.Offset, constant));
			} // TODO: Support floating point and 64-bit numbers
		}
		
		private void storeLocal(Instruction ins, State state)
		{
			if (state.Stack.Count == 0)
				throw new InvalidOperationException("The instruction stloc was performed on an empty stack.");
			
			// Calculate local index
			uint local = 0;
			if (ins.OpCode == OpCodes.Stloc || ins.OpCode == OpCodes.Stloc_S)
				local = (uint)ins.Operand;
			else if (ins.OpCode == OpCodes.Stloc_0)
				local = 0;
			else if (ins.OpCode == OpCodes.Stloc_1)
				local = 1;
			else if (ins.OpCode == OpCodes.Stloc_2)
				local = 2;
			else if (ins.OpCode == OpCodes.Stloc_3)
				local = 3;
			
			if (local >= 65536)
				throw new InvalidOperationException("An attempt was made to store into an invalid local index.");
		}
		
		#endregion
		
		#region OpCode Tests
		
		public bool isLoadConstant(OpCode opCode)
		{
			return (opCode == OpCodes.Ldc_I4 || opCode == OpCodes.Ldc_I4_0 || opCode == OpCodes.Ldc_I4_1 || opCode == OpCodes.Ldc_I4_2 || opCode == OpCodes.Ldc_I4_3 || opCode == OpCodes.Ldc_I4_4 || opCode == OpCodes.Ldc_I4_5 || opCode == OpCodes.Ldc_I4_6 || opCode == OpCodes.Ldc_I4_7 || opCode == OpCodes.Ldc_I4_8 || opCode == OpCodes.Ldc_I4_M1 || opCode == OpCodes.Ldc_I8 || opCode == OpCodes.Ldc_R4 || opCode == OpCodes.Ldc_R8);
		}
		
		public bool isStoreLocal(OpCode opCode)
		{
			return (opCode == OpCodes.Stloc || opCode == OpCodes.Stloc_0 || opCode == OpCodes.Stloc_1 || opCode == OpCodes.Stloc_2 || opCode == OpCodes.Stloc_3 || opCode == OpCodes.Stloc_S);
		}
		
		#endregion
		
		#region Helper Classes
		
		private class State
		{
			private uint _MethodHeaderOffset;
			private List<LocalSize> _Locals = new List<LocalSize>();
			private List<OperationBase> _Operations = new List<OperationBase>();
			private CilStack _Stack = new CilStack();
			
			public uint MethodHeaderOffset
			{
				get { return _MethodHeaderOffset; }
				set { _MethodHeaderOffset = value; }
			}
			
			public List<LocalSize> Locals
			{
				get { return _Locals; }
			}
			
			public List<OperationBase> Operations
			{
				get { return _Operations; }
			}
			
			public CilStack Stack
			{
				get { return _Stack; }
			}
		}
		
		private struct LocalSize
		{
			public LocalSize(VariableDefinition variable, uint size)
			{
				Variable = variable;
				Size = size;
			}
			
			public VariableDefinition Variable;
			public uint Size;
		}
		
		#endregion
	}
}
