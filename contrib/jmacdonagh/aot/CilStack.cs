using System;
using System.Collections.Generic;

namespace SharpOS.AOT
{
	public class CilStack : List<CilStackEntry>
	{
		public void Push(CilStackEntry item)
		{
			Add(item);
		}
		
		public CilStackEntry Pop()
		{
			if (Count > 0)
			{
				CilStackEntry rtn = this[Count - 1];
				RemoveAt(Count - 1);
				return rtn;
			}
			else
				return null;			
		}
		
		public CilStackEntry Peek()
		{
			return Count > 0 ? this[Count - 1] : null;
		}
		
		public int Offset
		{
			get
			{
				int offset = 0;
				
				foreach(CilStackEntry e in this)
					offset += (int)e.Size;
				
				return offset;
			}
		}
		
		public int LastEntryOffset
		{
			get
			{
				if (Count > 0)
					return Offset - (int)this[Count - 1].Size;
				else
					throw new InvalidOperationException("There are no entries on the stack.");
			}
		}
	}
}
