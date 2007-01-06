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
				state.Locals.Add(new LocalSize(var, CilTypes.SizeOf(var.VariableType)));
			
			for (int i = 0; i < method.Body.Instructions.Count; ++i)
			{
				Instruction ins = method.Body.Instructions[i];
				
				if (isConvert(ins.OpCode))
					convert(ins, state);
				else if (isLoadConstant(ins.OpCode))
					loadConstant(ins, state);
				else if (isStoreLocal(ins.OpCode))
					storeLocal(ins, state);
			}
			
			return null;
		}
		
		#region Instruction Handlers
		
		private void convert(Instruction ins, State state)
		{
			// (U)int to (u)int conversion
			if (isIntType(state.Stack.Peek().Type) && ins.OpCode == OpCodes.Conv_I || ins.OpCode == OpCodes.Conv_I1 || ins.OpCode == OpCodes.Conv_I2 || ins.OpCode == OpCodes.Conv_I4 || ins.OpCode == OpCodes.Conv_I8 || ins.OpCode == OpCodes.Conv_U || ins.OpCode == OpCodes.Conv_U1 || ins.OpCode == OpCodes.Conv_U2 || ins.OpCode == OpCodes.Conv_U4 || ins.OpCode == OpCodes.Conv_U8)
			{
				uint sizeFrom = state.Stack.Peek().Size;
				bool signFrom = CilTypes.IsSigned(state.Stack.Peek().Type);
				TypeReference typeTo = null;
				
				if (ins.OpCode == OpCodes.Conv_I) typeTo = CilTypes.I;
				else if (ins.OpCode == OpCodes.Conv_I1) typeTo = CilTypes.Int8;
				else if (ins.OpCode == OpCodes.Conv_I2) typeTo = CilTypes.Int16;
				else if (ins.OpCode == OpCodes.Conv_I4) typeTo = CilTypes.Int32;
				else if (ins.OpCode == OpCodes.Conv_I8) typeTo = CilTypes.Int64;
				else if (ins.OpCode == OpCodes.Conv_U) typeTo = CilTypes.U;
				else if (ins.OpCode == OpCodes.Conv_U1) typeTo = CilTypes.UInt8;
				else if (ins.OpCode == OpCodes.Conv_U2) typeTo = CilTypes.UInt16;
				else if (ins.OpCode == OpCodes.Conv_U4) typeTo = CilTypes.UInt32;
				else if (ins.OpCode == OpCodes.Conv_U8) typeTo = CilTypes.UInt64;
				
				uint sizeTo = CilTypes.SizeOf(typeTo);
				bool signTo = CilTypes.IsSigned(typeTo);
				
				if (sizeTo > sizeFrom)
				{
					if (signFrom == false || signTo == false)
					{
						// No need to sign extend, 0 out additional space
						for(int i = 1; i <= sizeTo - sizeFrom; ++i)
						{
							state.Operations.Add(new Mov(OperationSizes.Byte, Registers.EBP, state.Stack.Offset + i, 0));
						}
						
						state.Stack.Pop();
						state.Stack.Push(new CilStackEntry(typeTo));
					}
					else
					{
						// Sign extend what we have to target size
						if (sizeFrom == 1)
						{
							state.Operations.Add(new Mov(OperationSizes.Byte, Registers.AL, state.Stack.LastEntryOffset, 0));
							
							if (sizeTo == 2) convertByteToWord(state);
							else if (sizeTo == 4) convertByteToDoubleWord(state);
							else if (sizeTo == 8) convertByteToQuadWord(state);
						}
						else if (sizeFrom == 2)
						{
							state.Operations.Add(new Mov(OperationSizes.Word, Registers.AX, state.Stack.LastEntryOffset, 0));
							
							if (sizeTo == 4) convertWordToDoubleWord(state);
							else if (sizeTo == 8) convertWordToQuadWord(state);
						}
						else if (sizeFrom == 4)
						{
							state.Operations.Add(new Mov(OperationSizes.DoubleWord, Registers.EAX, state.Stack.LastEntryOffset, 0));
							
							convertDoubleWordToQuadWord(state);
						}
						
						// Write new value to CIL stack
						state.Stack.Pop();
						
						if (sizeTo == 2)
							state.Operations.Add(new Mov(OperationSizes.Word, Registers.EBP, state.Stack.Offset, Registers.AX));
						else if (sizeTo == 4)
							state.Operations.Add(new Mov(OperationSizes.DoubleWord, Registers.EBP, state.Stack.Offset, Registers.EAX));
						else if (sizeTo == 8)
						{
							state.Operations.Add(new Mov(OperationSizes.DoubleWord, Registers.EBP, state.Stack.Offset, Registers.EAX));
							state.Operations.Add(new Mov(OperationSizes.DoubleWord, Registers.EBP, state.Stack.Offset + 4, Registers.EDX));
						}
						
						state.Stack.Push(new CilStackEntry(typeTo));
					}
				}
				else
				{
					// We can safely truncate because the C# compiler has already made its checks
					state.Stack.Pop();
					state.Stack.Push(new CilStackEntry(typeTo));
				}
			}
		}
		
		private void loadConstant(Instruction ins, State state)
		{
			if (ins.OpCode == OpCodes.Ldc_I4 || ins.OpCode == OpCodes.Ldc_I4_0 || ins.OpCode == OpCodes.Ldc_I4_1 || ins.OpCode == OpCodes.Ldc_I4_2 || ins.OpCode == OpCodes.Ldc_I4_3 || ins.OpCode == OpCodes.Ldc_I4_4 || ins.OpCode == OpCodes.Ldc_I4_5 || ins.OpCode == OpCodes.Ldc_I4_6 || ins.OpCode == OpCodes.Ldc_I4_7 || ins.OpCode == OpCodes.Ldc_I4_8 || ins.OpCode == OpCodes.Ldc_I4_M1)
			{
				int constant = 0;
				
				if (ins.OpCode == OpCodes.Ldc_I4) constant = (int)ins.Operand;
				else if(ins.OpCode == OpCodes.Ldc_I4_0) constant = 0;
				else if(ins.OpCode == OpCodes.Ldc_I4_1)	constant = 1;
				else if(ins.OpCode == OpCodes.Ldc_I4_2)	constant = 2;
				else if(ins.OpCode == OpCodes.Ldc_I4_3)	constant = 3;
				else if(ins.OpCode == OpCodes.Ldc_I4_4)	constant = 4;
				else if(ins.OpCode == OpCodes.Ldc_I4_5)	constant = 5;
				else if(ins.OpCode == OpCodes.Ldc_I4_6)	constant = 6;
				else if(ins.OpCode == OpCodes.Ldc_I4_7)	constant = 7;
				else if(ins.OpCode == OpCodes.Ldc_I4_8)	constant = 8;
				else if (ins.OpCode == OpCodes.Ldc_I4_M1) constant = -1;
				
				state.Operations.Add(new Mov(OperationSizes.DoubleWord, Registers.EBP, state.Stack.Offset, constant));
				
				state.Stack.Push(new CilStackEntry(CilTypes.Int32));				
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
	
		private bool isConvert(OpCode opCode)
		{
			return (opCode == OpCodes.Conv_I || opCode == OpCodes.Conv_I1 || opCode == OpCodes.Conv_I2 || opCode == OpCodes.Conv_I4 || opCode == OpCodes.Conv_I8 || opCode == OpCodes.Conv_Ovf_I || opCode == OpCodes.Conv_Ovf_I_Un || opCode == OpCodes.Conv_Ovf_I1 || opCode == OpCodes.Conv_Ovf_I1_Un || opCode == OpCodes.Conv_Ovf_I2 || opCode == OpCodes.Conv_Ovf_I2_Un || opCode == OpCodes.Conv_Ovf_I4 || opCode == OpCodes.Conv_Ovf_I4_Un || opCode == OpCodes.Conv_Ovf_I8 || opCode == OpCodes.Conv_Ovf_I8_Un || opCode == OpCodes.Conv_U || opCode == OpCodes.Conv_U1 || opCode == OpCodes.Conv_U2 || opCode == OpCodes.Conv_U4 || opCode == OpCodes.Conv_U8);
		}
		private bool isLoadConstant(OpCode opCode)
		{
			return (opCode == OpCodes.Ldc_I4 || opCode == OpCodes.Ldc_I4_0 || opCode == OpCodes.Ldc_I4_1 || opCode == OpCodes.Ldc_I4_2 || opCode == OpCodes.Ldc_I4_3 || opCode == OpCodes.Ldc_I4_4 || opCode == OpCodes.Ldc_I4_5 || opCode == OpCodes.Ldc_I4_6 || opCode == OpCodes.Ldc_I4_7 || opCode == OpCodes.Ldc_I4_8 || opCode == OpCodes.Ldc_I4_M1 || opCode == OpCodes.Ldc_I8 || opCode == OpCodes.Ldc_R4 || opCode == OpCodes.Ldc_R8);
		}
		
		private bool isStoreLocal(OpCode opCode)
		{
			return (opCode == OpCodes.Stloc || opCode == OpCodes.Stloc_0 || opCode == OpCodes.Stloc_1 || opCode == OpCodes.Stloc_2 || opCode == OpCodes.Stloc_3 || opCode == OpCodes.Stloc_S);
		}
		
		#endregion
		
		#region Type Tests
		
		private bool isIntType(TypeReference type)
		{
			return (type == CilTypes.I || type == CilTypes.Int8 || type == CilTypes.Int16 || type == CilTypes.Int32 || type == CilTypes.Int32 || type == CilTypes.UInt8 || type == CilTypes.UInt16 || type == CilTypes.UInt32 || type == CilTypes.UInt64);
		}
		
		#endregion
		
		#region Helper Methods
		
		private void convertByteToWord(State state)
		{
			state.Operations.Add(new Cbw());
		}
		
		private void convertWordToDoubleWord(State state)
		{
			state.Operations.Add(new Cwde());
		}
		
		private void convertDoubleWordToQuadWord(State state)
		{
			state.Operations.Add(new Cdq());
		}
		
		private void convertByteToDoubleWord(State state)
		{
			convertByteToWord(state);
			convertWordToDoubleWord(state);
		}
		
		private void convertByteToQuadWord(State state)
		{
			convertByteToDoubleWord(state);
			convertDoubleWordToQuadWord(state);
		}
		
		private void convertWordToQuadWord(State state)
		{
			convertWordToDoubleWord(state);
			convertDoubleWordToQuadWord(state);
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
