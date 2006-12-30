using System;
using Mono.Cecil;

namespace SharpOS.AOT
{
	public class CilStackEntry
	{
		private TypeReference _Type;
		private uint _Size;
		
		public CilStackEntry(TypeReference type, uint size)
		{
			_Type = type;
			_Size = size;
		}
		
		public TypeReference Type
		{
			get { return _Type; }
			set { _Type = value; }
		}
		
		public uint Size
		{
			get { return _Size; }
			set { _Size = value; }
		}
	}
}
