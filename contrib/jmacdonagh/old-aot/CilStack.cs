using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace SharpOS.AOT
{
	public class CilStack : List<CilStackEntry>
	{
		public void Push(CilStackEntry entry)
		{
			this.Add(entry);
		}
		
		public CilStackEntry Pop()
		{
			CilStackEntry rtn = this[this.Count - 1];
			
			this.RemoveAt(this.Count - 1);
			
			return rtn;
		}
		
		public CilStackEntry Peek()
		{
			return this[Count - 1];
		}
	}
}
