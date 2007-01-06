using System;
using Mono.Cecil;

namespace SharpOS.AOT
{
	public unsafe static class CilTypes
	{
		private static TypeReference _I;
		private static TypeReference _Int8;
		private static TypeReference _Int16;
		private static TypeReference _Int32;
		private static TypeReference _Int64;
		private static TypeReference _U;
		private static TypeReference _UInt8;
		private static TypeReference _UInt16;
		private static TypeReference _UInt32;
		private static TypeReference _UInt64;
		private static TypeReference _F;
		private static TypeReference _Single;
		private static TypeReference _Double;
		
		
		static CilTypes()
		{
			AssemblyDefinition mscorlib = AssemblyFactory.GetAssembly("/usr/lib/mono/2.0/mscorlib.dll");
			
			_I = mscorlib.MainModule.Import(typeof(int));
			_Int8 = mscorlib.MainModule.Import(typeof(sbyte));
			_Int16 = mscorlib.MainModule.Import(typeof(short));
			_Int32 = mscorlib.MainModule.Import(typeof(int));
			_Int64 = mscorlib.MainModule.Import(typeof(long));
			_U = mscorlib.MainModule.Import(typeof(uint));
			_UInt8 = mscorlib.MainModule.Import(typeof(byte));
			_UInt16 = mscorlib.MainModule.Import(typeof(ushort));
			_UInt32 = mscorlib.MainModule.Import(typeof(uint));
			_UInt64 = mscorlib.MainModule.Import(typeof(ulong));
			_F = mscorlib.MainModule.Import(typeof(double));
			_Single = mscorlib.MainModule.Import(typeof(float));
			_Double = mscorlib.MainModule.Import(typeof(double));
			
			mscorlib = null;
		}
		
		public static TypeReference I
		{
			get { return _I; }
		}
		
		public static TypeReference Int8
		{
			get { return _Int8; }
		}
		
		public static TypeReference Int16
		{
			get { return _Int16; }
		}
		
		public static TypeReference Int32
		{
			get { return _Int32; }
		}
		
		public static TypeReference Int64
		{
			get { return _Int64; }
		}
		
		public static TypeReference U
		{
			get { return _U; }
		}
		
		public static TypeReference UInt8
		{
			get { return _UInt8; }
		}
		
		public static TypeReference UInt16
		{
			get { return _UInt16; }
		}
		
		public static TypeReference UInt32
		{
			get { return _UInt32; }
		}
		
		public static TypeReference UInt64
		{
			get { return _UInt64; }
		}
		
		public static TypeReference F
		{
			get { return _F; }
		}
		
		public static TypeReference Single
		{
			get { return _Single; }
		}
		
		public static TypeReference Double
		{
			get { return _Double; }
		}
		
		public static uint SizeOf(TypeReference type)
		{
			if (type == CilTypes.Int8 || type == CilTypes.UInt8) return 1;
			else if (type == CilTypes.Int16 || type == CilTypes.UInt16) return 2;
			else if (type == CilTypes.Int32 || type == CilTypes.UInt32 || type == CilTypes.Single || type == CilTypes.F) return 4; // TODO: Check that native float is Single
			else if (type == CilTypes.Int64 || type == CilTypes.UInt64 || type == CilTypes.Double) return 8;
			else throw new Exception();
		}
		
		public static bool IsSigned(TypeReference type)
		{
			if (type == CilTypes.I || type == CilTypes.Int8 || type == CilTypes.Int16 || type == CilTypes.Int32 || type == CilTypes.Int64) return true;
			else if (type == CilTypes.UInt8 || type == CilTypes.UInt16 || type == CilTypes.UInt32 || type == CilTypes.UInt64) return false;
			else throw new Exception();
		}
	}
}
