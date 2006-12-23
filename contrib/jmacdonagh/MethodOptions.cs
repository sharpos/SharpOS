using System;
using System.Collections;
using System.Collections.Generic;

namespace SharpOS.AOT
{	
	public class MethodOptions
	{
		private uint _MethodHeaderOffset;
		private List<uint> _LocalVariableOffsets = new List<uint>();
		private uint _MethodOffset;
		private CilStack _CilStack = new CilStack();
		
		public uint MethodHeaderOffset
		{
			get { return _MethodHeaderOffset; }
			set { _MethodHeaderOffset = value; }
		}
		
		public List<uint> LocalVariableOffsets
		{
			get { return _LocalVariableOffsets; }
		}
		
		public uint MethodOffset
		{
			get { return _MethodOffset; }
			set { _MethodOffset = value; }
		}
		
		public CilStack CilStack
		{
			get { return _CilStack; }
			set { _CilStack = value; }
		}
	}
}
