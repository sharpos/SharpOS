using System;
using Mono.Cecil;

namespace SharpOS.AOT
{
	public class CilStackEntry
	{
		private TypeReference _Type;
		private uint _Size;
		
		public CilStackEntry(TypeReference type)
		{
			_Type = type;
			_Size = CilTypes.SizeOf(type);
		}
		
		public TypeReference Type
		{
			get { return _Type; }
			set
			{
				_Type = value;
				_Size = CilTypes.SizeOf(value);
			}
		}
		
		public uint Size
		{
			get { return _Size; }
		}
	}
}
