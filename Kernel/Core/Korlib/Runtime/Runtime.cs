//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System.Runtime.InteropServices;
using SharpOS.AOT.Metadata;
using SharpOS.AOT.Attributes;
using SharpOS.Kernel;
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.ADC;

namespace SharpOS.Korlib.Runtime {

	class TestA {
		public byte Member1;
	}

	class TestB : TestA {
		public byte Member2;
	}

	class TestC : TestB {
		public byte Member3;
	}

	struct TestD {
		public byte Member1;
	}

	internal class Runtime {

		[AddressOf ("MetadataRoot")]
		private static MetadataRoot Root;

		[AddressOf ("MethodBoundaries")]
		internal static MethodBoundary [] MethodBoundaries;

		public unsafe static AssemblyMetadata [] GetAssemblyMetadata ()
		{
			return Root.Assemblies;
		}

		public unsafe static CString8 *GetString (AssemblyMetadata assembly, uint str)
		{
			int length = 0;
			CString8 *buf;
			int index = 0;

			Diagnostics.Assert (str < assembly.StringsHeap.Length,
				"Runtime.ReadString(): parameter `str' is out of range");
			Diagnostics.Assert (assembly != null,
				"Runtime.ReadString(): parameter `assembly' is null");

			length = GetStringLength (assembly, str);
			buf = CString8.Create (length);

			for (int x = (int) str; index < length; ++x) {
				buf->SetChar (index, assembly.StringsHeap [x]);
				++index;
			}

			return buf;
		}

		public static TypeDefRow ResolveTypeRef (AssemblyMetadata assembly, TypeRefRow row)
		{
			AssemblyMetadata dest;
			return ResolveTypeRef (assembly, row, out dest);
		}

		public static unsafe TypeDefRow ResolveTypeRef (AssemblyMetadata assembly, TypeRefRow row,
			out AssemblyMetadata destAssembly)
		{
			CString8 *typeName = GetString (assembly, row.Name);
			CString8 *typeNamespace = GetString (assembly, row.Namespace);
			TokenType resScopeType;
			uint resScopeRID;
			TypeDefRow result = null;

			Diagnostics.Assert (assembly != null,
				"Runtime.ResolveTypeDef(): parameter `assembly' is null");
			Diagnostics.Assert (row != null,
				"Runtime.ResolveTypeDef(): parameter `row' is null");

			MetadataToken.Decode (row.ResolutionScope, out resScopeType, out resScopeRID);

			Diagnostics.Assert (resScopeType == TokenType.Assembly || resScopeType == TokenType.Module ||
					resScopeType == TokenType.AssemblyRef || resScopeType == TokenType.ModuleRef,
				"Runtime.ResolveTypeRef(): resolution scope token is of an invalid type");

			destAssembly = null;

			if (resScopeType == TokenType.Assembly || resScopeType == TokenType.Module || resScopeType == TokenType.ModuleRef) {
				Diagnostics.Assert (resScopeRID == 0,
					"Runtime.ResolveTypeRef(): resolution scope of Assembly must be zero!");
				destAssembly = assembly;
				return GetType (assembly, typeName, typeNamespace);
			} else if (resScopeType == TokenType.AssemblyRef) {
				Diagnostics.Assert (resScopeRID < assembly.AssemblyRef.Length,
					"Runtime.ResolveTypeRef(): AssemblyRef metadata token out of range");
				AssemblyMetadata assembly2 = ResolveAssemblyRef (assembly, assembly.AssemblyRef [resScopeRID]);

				destAssembly = assembly2;
				return GetType (assembly2, typeName, typeNamespace);
			}

			MemoryManager.Free (typeName);
			MemoryManager.Free (typeNamespace);

			return result;
		}

		public static unsafe AssemblyMetadata ResolveAssemblyRef (AssemblyMetadata assembly, AssemblyRefRow row)
		{
			// TODO: check more than just the name here

			Diagnostics.Assert (assembly != null,
				"Runtime.ResolveAssemblyRef(): parameter `assembly' is null");
			Diagnostics.Assert (row != null,
				"Runtime.ResolveAssemblyRef(): parameter `row' is null");

			CString8 *name = GetString (assembly, row.Name);
			AssemblyMetadata result = null;

			for (int x = 0; x < Root.Assemblies.Length; ++x) {
				AssemblyMetadata assembly2 = Root.Assemblies [x];
				CString8 *name2;

				if (assembly2.Assembly.Length == 0)
					continue;	// unnamed assembly

				name2 = GetString (assembly2, assembly2.Assembly [0].Name);

				if (name->Compare (name2) == 0)
					result = assembly2;

				MemoryManager.Free (name2);

				if (result != null)
					break;
			}

			MemoryManager.Free (name);
			return result;
		}

		public static int GetStringLength (AssemblyMetadata assembly, uint str)
		{
			int len = 0;

			Diagnostics.Assert (assembly != null,
				"Runtime.GetStringLength(): parameter `assembly' is null");

			Diagnostics.Assert (str < assembly.StringsHeap.Length,
				"Runtime.GetStringLength(): invalid StringsHeap token");

			for (int z = (int) str; z < assembly.StringsHeap.Length; ++z) {
				if (assembly.StringsHeap [z] == (byte) '\0')
					break;
				++len;
			}

			return len;
		}

		public static ModuleRow ResolveModuleRef (AssemblyMetadata assembly, ModuleRefRow row)
		{
			AssemblyMetadata assem2;

			Diagnostics.Assert (assembly != null,
				"Runtime.ResolveModuleRef(): parameter `assembly' is null");

			Diagnostics.Assert (row != null,
				"Runtime.ResolveModuleRef(): parameter `row' is null");

			return ResolveModuleRef (assembly, row, out assem2);
		}

		public static unsafe ModuleRow ResolveModuleRef (AssemblyMetadata assembly, ModuleRefRow row, out AssemblyMetadata destAssembly)
		{
			CString8 *moduleName;
			ModuleRow result = null;

			Diagnostics.Assert (assembly != null,
				"Runtime.ResolveModuleRef(): parameter `assembly' is null");
			Diagnostics.Assert (row != null,
				"Runtime.ResolveModuleRef(): parameter `row' is null");

			moduleName = GetString (assembly, row.Name);
			destAssembly = null;

			for (int x = 0; x < Root.Assemblies.Length; ++x) {
				AssemblyMetadata assembly2 = Root.Assemblies [x];

				for (int y = 0; y < assembly2.Module.Length; ++y) {
					ModuleRow mod = assembly2.Module [y];
					int len = GetStringLength (assembly2, mod.Name);

					if (moduleName->Compare (0, assembly2.StringsHeap, (int) mod.Name, len) == 0) {
						result = mod;
						destAssembly = assembly2;
						break;
					}
				}
			}

			return result;
		}

		public static unsafe ModuleRow GetModule (CString8 *name)
		{
			for (int x = 0; x < Root.Assemblies.Length; ++x) {
				ModuleRow result = GetModule (Root.Assemblies [x], name);

				if (result != null)
					return result;
			}

			return null;
		}

		public static unsafe ModuleRow GetModule (AssemblyMetadata assembly, CString8 *name)
		{
			Diagnostics.Assert (assembly == null,
				"Runtime.GetModule(): parameter `assembly' is null");
			Diagnostics.Assert (name == null,
				"Runtime.GetModule(): parameter `name' is null");

			for (int x = 0; x < assembly.Module.Length; ++x) {
				ModuleRow row = assembly.Module [x];
				int len = GetStringLength (assembly, row.Name);

				if (name->Compare (0, assembly.StringsHeap, (int) row.Name, len) == 0)
					return row;
			}

			return null;
		}

		public static TypeDefRow GetType (MetadataToken token, out AssemblyMetadata dest)
		{
			dest = null;

			for (int x = 0; x < Root.Assemblies.Length; ++x) {
				AssemblyMetadata def;
				TypeDefRow result = GetType (Root.Assemblies [x], token.Type, token.RID, out def);

				if (result != null) {
					return result;
					dest = def;
				}
			}

			return null;
		}

		public static TypeDefRow GetType (MetadataToken token)
		{
			AssemblyMetadata dest;
			return GetType (token, out dest);
		}

		public static TypeDefRow GetType (AssemblyMetadata assembly, MetadataToken token, out AssemblyMetadata dest)
		{
			return GetType (assembly, token.Type, token.RID, out dest);
		}

		public static TypeDefRow GetType (AssemblyMetadata assembly, MetadataToken token)
		{
			AssemblyMetadata dest;
			return GetType (assembly, token, out dest);
		}

		public static TypeDefRow GetType (AssemblyMetadata assembly, TokenType type, uint rid)
		{
			AssemblyMetadata dest;
			return GetType (assembly, type, rid, out dest);
		}

		public static TypeDefRow GetType (AssemblyMetadata assembly, TokenType type, uint rid,
			out AssemblyMetadata destAssembly)
		{
			Diagnostics.Assert (type == TokenType.TypeRef || type == TokenType.TypeDef,
				"Runtime.GetType(): token type must be either TypeRef or TypeDef");

			if (type == TokenType.TypeDef) {
				Diagnostics.Assert (rid < assembly.TypeDef.Length,
					"Runtime.GetType(): token out of range for TypeDef table");

				destAssembly = assembly;
				return assembly.TypeDef [rid - 1];
			} else {
				TypeRefRow row = assembly.TypeRef [rid - 1];

				return ResolveTypeRef (assembly, row, out destAssembly);
			}
		}

		public static unsafe TypeDefRow GetType (CString8 *ns, CString8 *name, out AssemblyMetadata assembly, out uint token)
		{
			token = 0;
			assembly = null;

			Diagnostics.Assert (ns != null,
				"Runtime.GetType(): parameter `ns' is null");
			Diagnostics.Assert (name != null,
				"Runtime.GetType(): parameter `name' is null");

			for (int x = 0; x < Root.Assemblies.Length; ++x) {
				TypeDefRow row = GetType (Root.Assemblies [x], ns, name, out token);

				if (row != null) {
					assembly = Root.Assemblies [x];
					return row;
				}
			}

			return null;
		}

		public static unsafe TypeDefRow GetType (CString8 *ns, CString8 *name, out AssemblyMetadata assembly)
		{
			uint token;

			return GetType (ns, name, out assembly, out token);
		}

		public static unsafe TypeDefRow GetType (CString8 *ns, CString8 *name)
		{
			AssemblyMetadata assembly;
			uint token;

			return GetType (ns, name, out assembly, out token);
		}

		public static unsafe TypeDefRow GetType (AssemblyMetadata assembly, CString8 *ns, CString8 *name)
		{
			uint token;

			return GetType (assembly, name, ns, out token);
		}

		public static unsafe TypeDefRow GetType (AssemblyMetadata assembly, CString8 *ns, CString8 *name, out uint token)
		{
			TypeDefRow result = null;
			token = 0;

			for (int x = 0; x < assembly.TypeDef.Length; ++x) {
				int nameLength = 0;
				int nsLength = 0;
				TypeDefRow inspect = assembly.TypeDef [x];

				nameLength = GetStringLength (assembly, inspect.Name);
				nsLength = GetStringLength (assembly, inspect.Namespace);

				if (name->Compare (0, assembly.StringsHeap, (int) inspect.Name, nameLength) != 0)
					continue;
				if (ns->Compare (0, assembly.StringsHeap, (int) inspect.Namespace, nsLength) != 0)
					continue;

				result = inspect;
				token = (uint)TokenType.TypeDef | (uint)x+1U;
				break;
			}

			return result;
		}

		public static unsafe TypeRefRow GetTypeRef (CString8 *ns, CString8 *name, out AssemblyMetadata assembly, out uint token)
		{
			assembly = null;
			token = 0;

			for (int x = 0; x < Root.Assemblies.Length; ++x) {
				TypeRefRow result = GetTypeRef (Root.Assemblies [x], ns, name, out token);

				if (result != null) {
					assembly = Root.Assemblies [x];
					return result;
				}
			}

			return null;
		}

		public static unsafe TypeRefRow GetTypeRef (AssemblyMetadata assembly, string ns, string name, out uint token)
		{
			CString8 *cns, cname;

			cns = CString8.Copy (ns);
			cname = CString8.Copy (name);

			try {
				return GetTypeRef (assembly, cns, cname, out token);
			} finally {
				MemoryManager.Free (cns);
				MemoryManager.Free (cname);
			}
		}

		public static unsafe TypeRefRow GetTypeRef (AssemblyMetadata assembly, CString8 *ns, CString8 *name, out uint token)
		{
			TypeRefRow result = null;
			token = 0;

			for (int x = 0; x < assembly.TypeRef.Length; ++x) {
				int nameLength = 0;
				int nsLength = 0;
				TypeRefRow inspect = assembly.TypeRef [x];

				nameLength = GetStringLength (assembly, inspect.Name);
				nsLength = GetStringLength (assembly, inspect.Namespace);

				if (name->Compare (0, assembly.StringsHeap, (int) inspect.Name, nameLength) != 0)
					continue;
				if (ns->Compare (0, assembly.StringsHeap, (int) inspect.Namespace, nsLength) != 0)
					continue;

				result = inspect;
				token = (uint)TokenType.TypeRef | (uint)x+1U;
				break;
			}

			return result;
		}

		/// <summary>
		/// Immediately frees an object allocated using 'new'.
		/// </summary>
		public static unsafe void Free (object o)
		{
			// TODO: remove this and mark as deprecated when GC is working
			MemoryManager.Free (GetPointerFromObject (o));
		}

		public static unsafe bool IsTypeSystemObject (AssemblyMetadata assembly, TokenType type, uint rid)
		{
			CString8 *systemNS = CString8.Copy ("System");
			CString8 *objectName = CString8.Copy ("Object");
			int nameLen, nsLen;
			bool result = false;

			Diagnostics.Assert (type == TokenType.TypeRef || type == TokenType.TypeDef,
				"Runtime.IsTypeSystemObject(): invalid metadata token, must be of type `TypeDef' or `TypeRef' only");

			if (type == TokenType.TypeRef) {
				TypeRefRow row;

				// Handle this specially in case mscorlib is not included
				// (otherwise we will definitely get null from GetType())

				Diagnostics.Assert (rid < assembly.TypeRef.Length,
					"Runtime.IsTypeSystemObject(): invalid metadata token, too large for TypeRef table!");

				row = assembly.TypeRef [rid - 1];

				nsLen = GetStringLength (assembly, row.Namespace);
				nameLen = GetStringLength (assembly, row.Name);

				if (systemNS->Compare (0, assembly.StringsHeap, (int) row.Namespace, nsLen) == 0 &&
				    objectName->Compare (0, assembly.StringsHeap, (int) row.Name, nameLen) == 0)
					result = true;
			} else if (type == TokenType.TypeDef) {
				TypeDefRow row;

				row = GetType (assembly, type, rid);
				nsLen = GetStringLength (assembly, row.Namespace);
				nameLen = GetStringLength (assembly, row.Name);

				if (systemNS->Compare (0, assembly.StringsHeap, (int) row.Namespace, nsLen) == 0 &&
				    objectName->Compare (0, assembly.StringsHeap, (int) row.Name, nameLen) == 0)
					result = true;
			}

			MemoryManager.Free (systemNS);
			MemoryManager.Free (objectName);

			return result;
		}

		public unsafe static void PrintTypeName (AssemblyMetadata assembly, TypeDefRow type)
		{
			CString8 *name, ns;

			name = GetString (assembly, type.Name);
			ns = GetString (assembly, type.Namespace);

			Serial.Write (ns);
			Serial.Write (".");
			Serial.Write (name);

			MemoryManager.Free (name);
			MemoryManager.Free (ns);
		}

		public unsafe static void PrintTypeName (AssemblyMetadata assembly, TypeRefRow type)
		{
			CString8 *name, ns;

			name = GetString (assembly, type.Name);
			ns = GetString (assembly, type.Namespace);

			Serial.Write (ns);
			Serial.Write (".");
			Serial.Write (name);

			MemoryManager.Free (name);
			MemoryManager.Free (ns);
		}


		[SharpOS.AOT.Attributes.PointerToObject]
		public unsafe static InternalSystem.Object GetObjectFromPointer (void* pointer)
		{
			return null;
		}

		[SharpOS.AOT.Attributes.ObjectToPointer]
		public unsafe static void *GetPointerFromObject (object obj)
		{
			return null;
		}

		public unsafe static void DumpTypeDef (AssemblyMetadata assembly, TypeDefRow row, int index)
		{
			Serial.Write (" ");
			Serial.Write ("TypeDefRow#");
			Serial.Write (index);
			Serial.Write (" ");
			Serial.Write ((int)row.Flags, true);
			Serial.Write (" ");
			Serial.Write ((int)row.Name, true);
			Serial.Write (" ");
			Serial.Write ((int)row.Namespace, true);
			Serial.Write (" ");
			Serial.Write ((int)row.Extends, true);
			Serial.Write (" ");
			Serial.Write ((int)row.FieldList, true);
			Serial.Write (" ");
			Serial.Write ((int)row.MethodList, true);
			Serial.Write (" ");
			PrintTypeName (assembly, row);
			Serial.WriteLine ();
		}

		[AddressOf ("SharpOS.Korlib.Runtime.TestD TypeInfo")]
		public static TypeInfo testd;

		[AddressOf ("SharpOS.Korlib.Runtime.TestD VTable")]
		public static VTable testdvt;

		public unsafe static void __RunTests ()
		{
			byte *kernelStart, kernelEnd;
			byte *ptr;

			{
				void *ks, ke;

				EntryModule.GetKernelLocation (out ks, out ke);

				kernelStart = (byte*) ks;
				kernelEnd = (byte*) ke;
			}

			ptr = (byte*)GetPointerFromObject (testdvt);

			if (testd.Assembly == null)
				Serial.WriteLine ("*** testd.Assembly == null");
			else
				Serial.WriteLine ("*** testd.Assembly != null");

			if (testd.MetadataToken == 0)
				Serial.WriteLine ("*** testd.MetadataToken == 0");
			else
				Serial.WriteLine ("*** testd.MetadataToken != 0");

			if (HasValidMetadataToken (testd.Assembly, testd.MetadataToken))
				Serial.WriteLine ("*** testd.MetadataToken is valid");
			else
				Serial.WriteLine ("*** testd.MetadataToken is NOT valid");

			if (testdvt.Type == testd)
				Serial.WriteLine ("*** testdvt.Type == testd");
			else
				Serial.WriteLine ("*** testdvt.Type != testd");

			if (ptr >= kernelEnd)
				Serial.WriteLine ("ERROR");
			else
				Serial.WriteLine ("Location OK");
			
			__TestMethodBoundaries ();
			__TestObjectConversion ();
			__TestIsBaseClassOf ();
		}

		private static void __TestMethodBoundaries ()
		{
			Testcase.Test (MethodBoundaries.Length > 0, "Runtime", "MethodBoundaries.Length should be greater than 0");

			for (int i = 0; i < MethodBoundaries.Length; i++)
				PrintMethodBoundary (MethodBoundaries [i]);

			MethodBoundary [] callingStack = MemoryUtil.GetCallingStack ();

			Testcase.Test (callingStack != null, "Runtime", "callingStack != null");

			for (int i = 0; i < callingStack.Length; i++) {
				Serial.Write ("\tCalled Method: ");

				if (callingStack [i] == null)
					Serial.WriteLine ("<empty>");
				else
					Serial.WriteLine (callingStack [i].Name);
			}
		}

		private static void PrintMethodBoundary (MethodBoundary methodBoundary)
		{
			Serial.Write ("MethodBoundary: ");
			Serial.WriteLine (methodBoundary.Name);

			if (methodBoundary.ExceptionHandlingClauses != null) {
				Serial.Write ("\tExceptions: ");
				Serial.WriteNumber (methodBoundary.ExceptionHandlingClauses.Length, false);
				Serial.WriteLine ();

				for (int i = 0; i < methodBoundary.ExceptionHandlingClauses.Length; i++)
					PrintExceptionHandlingClauses (methodBoundary.ExceptionHandlingClauses [i]);
			}
		}

		private static void PrintExceptionHandlingClauses (ExceptionHandlingClause exceptionHandlingClause)
		{
			Serial.Write ("\t\tType: ");
			Serial.WriteNumber ((int) exceptionHandlingClause.ExceptionType, true);
			Serial.WriteLine ();

			if (exceptionHandlingClause.ExceptionType == ExceptionHandlerType.Catch) {
				Testcase.Test (exceptionHandlingClause.TypeInfo != null, "Runtime", "exceptionHandlingClause.TypeInfo != null");

				if (exceptionHandlingClause.TypeInfo != null) {
					Serial.Write ("\t\tCatch Type: ");
					Serial.WriteLine (exceptionHandlingClause.TypeInfo.Name);
				}
			}
		}

		public static unsafe void __TestObjectConversion ()
		{
			TestA o1 = new TestA ();
			bool result = false;
			void *ptr = GetPointerFromObject (o1);
			object o2;

			o1.Member1 = 4;
			o2 = GetObjectFromPointer (ptr);

			Testcase.Test (o1 == o2, "Runtime",
				"Object conversion ((o1 (to) pointer (back to) object) == o1)");
			Testcase.Test (o1 == o2, "Runtime",
				"Object conversion (round trip by-reference comparison)");
			Testcase.Test (o1.Member1 == (o2 as TestA).Member1, "Runtime",
				"Object conversion (member check)");

			(o2 as TestA).Member1 = 6;

			Testcase.Test (o1.Member1 == 6, "Runtime",
				"Object conversion (modify member via second reference, member check)");

			Free (o1);
		}

		static bool HasValidMetadataToken (AssemblyMetadata assembly, uint token)
		{
			TokenType type;
			uint rid;

			MetadataToken.Decode (token, out type, out rid);

			if (type != TokenType.TypeDef && type != TokenType.TypeRef)
				return false;

			if (type == TokenType.TypeDef && rid-1 >= assembly.TypeDef.Length)
				return false;
			else if (type == TokenType.TypeRef && rid-1 >= assembly.TypeRef.Length)
				return false;

			return true;
		}

		static bool IsValidAssemblyMetadata (AssemblyMetadata asm)
		{
			for (int x = 0; x < Root.Assemblies.Length; ++x) {
				if (Root.Assemblies [x] == asm)
					return true;
			}

			return false;
		}

		public static void __TestIsBaseClassOf ()
		{
			object o1 = new TestA ();
			object o2 = new TestB ();
			object o3 = new TestC ();
			object o4 = new TestD ();

			InternalSystem.Object io1, io2, io3, io4;
			uint ui = 1109;

			io1 = o1 as InternalSystem.Object;
			io2 = o2 as InternalSystem.Object;
			io3 = o3 as InternalSystem.Object;
			io4 = o4 as InternalSystem.Object;

			Testcase.Test (testdvt == io4.VTable,
				"Runtime", "Bad pointer in object for VTable (valuetype");
			Testcase.Test (testdvt.Type == io4.VTable.Type,
				"Runtime", "Bad pointer in vtable for TypeInfo (valuetype");

			Testcase.Test (io1.VTable != null,
				"Runtime", "InternalObject.VTable should never be null (ref io1)");
			Testcase.Test (io2.VTable != null,
				"Runtime", "InternalObject.VTable should never be null (ref io2)");
			Testcase.Test (io3.VTable != null,
				"Runtime", "InternalObject.VTable should never be null (ref io3)");
			Testcase.Test (io4.VTable != null,
				"Runtime", "InternalObject.VTable should never be null (valuetype io4)");

			Testcase.Test (io1.VTable.Type != null,
				"Runtime", "InternalObject.VTable.Type should never be null (ref io1)");
			Testcase.Test (io2.VTable.Type != null,
				"Runtime", "InternalObject.VTable.Type should never be null (ref io2)");
			Testcase.Test (io3.VTable.Type != null,
				"Runtime", "InternalObject.VTable.Type should never be null (ref io3)");
			Testcase.Test (io4.VTable.Type != null,
				"Runtime", "InternalObject.VTable.Type should never be null (valuetype io4)");

			Testcase.Test (io1.VTable.Type.Assembly != null,
				"Runtime", "InternalObject.VTable.Type.Assembly should never be null (ref io1)");
			Testcase.Test (io2.VTable.Type.Assembly != null,
				"Runtime", "InternalObject.VTable.Type.Assembly should never be null (ref io2)");
			Testcase.Test (io3.VTable.Type.Assembly != null,
				"Runtime", "InternalObject.VTable.Type.Assembly should never be null (ref io3)");
			Testcase.Test (io4.VTable.Type.Assembly != null,
				"Runtime", "InternalObject.VTable.Type.Assembly should never be null (valuetype io4)");

			Testcase.Test (IsValidAssemblyMetadata (io1.VTable.Type.Assembly),
				"Runtime", "InternalObject.VTable.Type.Assembly is listed in the metadata root (ref io1)");
			Testcase.Test (IsValidAssemblyMetadata (io2.VTable.Type.Assembly),
				"Runtime", "InternalObject.VTable.Type.Assembly is listed in the metadata root (ref io2)");
			Testcase.Test (IsValidAssemblyMetadata (io3.VTable.Type.Assembly),
				"Runtime", "InternalObject.VTable.Type.Assembly is listed in the metadata root (ref io3)");
			Testcase.Test (IsValidAssemblyMetadata (io4.VTable.Type.Assembly),
				"Runtime", "InternalObject.VTable.Type.Assembly is listed in the metadata root (valuetype io4)");

			Testcase.Test (io1.VTable.Type.MetadataToken != 0,
				"Runtime", "InternalObject.VTable.Type.MetadataToken should never be 0x0 (ref io1)");
			Testcase.Test (io2.VTable.Type.MetadataToken != 0,
				"Runtime", "InternalObject.VTable.Type.MetadataToken should never be 0x0 (ref io2)");
			Testcase.Test (io3.VTable.Type.MetadataToken != 0,
				"Runtime", "InternalObject.VTable.Type.MetadataToken should never be 0x0 (ref io3)");
			Testcase.Test (io4.VTable.Type.MetadataToken != 0,
				"Runtime", "InternalObject.VTable.Type.MetadataToken should never be 0x0 (valuetype io4)");

			Testcase.Test (HasValidMetadataToken (io1.VTable.Type.Assembly, io1.VTable.Type.MetadataToken),
				"Runtime", "InternalObject.VTable.Type has valid metadata token (ref io1)");
			Testcase.Test (HasValidMetadataToken (io2.VTable.Type.Assembly, io2.VTable.Type.MetadataToken),
				"Runtime", "InternalObject.VTable.Type has valid metadata token (ref io2)");
			Testcase.Test (HasValidMetadataToken (io3.VTable.Type.Assembly, io3.VTable.Type.MetadataToken),
				"Runtime", "InternalObject.VTable.Type has valid metadata token (ref io3)");
			Testcase.Test (HasValidMetadataToken (io4.VTable.Type.Assembly, io4.VTable.Type.MetadataToken),
				"Runtime", "InternalObject.VTable.Type has valid metadata token (valuetype io4)");

			Testcase.Test (Runtime.IsBaseClassOf (io2.VTable.Type, io1.VTable.Type),
				"Runtime", "IsBaseClassOf (new TestB (), new TestA ())");
			Testcase.Test (Runtime.IsBaseClassOf (io1.VTable.Type, io2.VTable.Type) == false,
				"Runtime", "IsBaseClassOf (new TestA (), new TestB ()) should be false");
			Testcase.Test (Runtime.IsBaseClassOf (io3.VTable.Type, io2.VTable.Type),
				"Runtime", "IsBaseClassOf (new TestC (), new TestB ())");
			Testcase.Test (Runtime.IsBaseClassOf (io3.VTable.Type, io1.VTable.Type),
				"Runtime", "IsBaseClassOf (new TestC (), new TestA ())");
			//Testcase.Test (Runtime.IsBaseClassOf (io4.VTable.Type, "System", "ValueType") == false,
			//	"Runtime", "IsBaseClassOf (new TestD (), ('System.ValueType'))");
		}

		public static TypeDefRow GetType (TypeInfo type)
		{
			TokenType tokType;
			uint rid;

			MetadataToken.Decode (type.MetadataToken, out tokType, out rid);

			return GetType (type.Assembly, tokType, rid);
		}

		public static bool IsValueType (TypeInfo type)
		{
			return IsBaseClassOf (type, "System", "ValueType");
		}

		public static VTable GetVTable (object obj)
		{
			return (obj as InternalSystem.Object).VTable;
		}

		public static TypeInfo GetTypeInfo (object obj)
		{
			return GetVTable (obj).Type;
		}

		public static TypeDefRow GetObjectType (object obj)
		{
			return GetType (GetTypeInfo (obj));
		}

		[SharpOS.AOT.Attributes.AllocObject]
		internal static unsafe InternalSystem.Object AllocObject (VTable vtable)
		{
			// TODO add GC support here

			/*TextMode.Write ("Alloc Object of Size: ");
			TextMode.Write ((int) vtable.Size);
			TextMode.Write (" Type: ");
			TextMode.Write (vtable.Type.Name);
			TextMode.WriteLine ();*/


			void* result = (void*) SharpOS.Kernel.ADC.MemoryManager.Allocate (vtable.Size);

			InternalSystem.Object _object = GetObjectFromPointer (result);
			_object.VTable = vtable;

			return _object;
		}

		[SharpOS.AOT.Attributes.AllocArray]
		internal static unsafe InternalSystem.Object AllocArray (VTable vtable, int size)
		{
			// TODO add GC support here

			/*TextMode.Write ("Alloc Object of Size: ");
			TextMode.Write ((int) size);
			TextMode.Write (" Type: ");
			TextMode.Write (vtable.Type.Name);
			TextMode.WriteLine ();*/

			void* result = (void*) SharpOS.Kernel.ADC.MemoryManager.Allocate ((uint) size);

			InternalSystem.Object _object = GetObjectFromPointer (result);
			_object.VTable = vtable;

			// TODO set the rank, rank data and initialize the data

			/*InternalSystem.Array _array = _object as InternalSystem.Array;
			_array.Rank = 1;
			_array.FirstEntry.LowerBound = 0;
			_array.FirstEntry.Length = count;*/

			return _object;
		}

		public static unsafe bool IsBaseClassOf (TypeInfo type, string baseNS, string baseType)
		{
			CString8 *cBaseNS = CString8.Copy (baseNS);
			CString8 *cBaseType = CString8.Copy (baseType);

			return IsBaseClassOf (type, cBaseNS, cBaseType);
		}

		public static unsafe bool IsBaseClassOf (TypeInfo type, CString8 *baseNS, CString8 *baseType)
		{
			AssemblyMetadata assembly;
			AssemblyMetadata baseAsm;
			uint token;
			TypeRefRow row;
			TypeDefRow def;

			row = GetTypeRef (baseNS, baseType, out baseAsm, out token);

			if (row != null) {
				return IsBaseClassOf (type.Assembly, type.MetadataToken, baseAsm, token);
			} else {
				def = GetType (baseNS, baseType, out baseAsm, out token);

				Diagnostics.Assert (def != null,
					"Runtime.IsBaseClasOf(): couldn't find TypeRef/Def for named type");

				return IsBaseClassOf (type.Assembly, type.MetadataToken, baseAsm, token);
			}

			return false;
		}

		public static bool IsBaseClassOf (TypeInfo type, TypeInfo baseType)
		{
			return IsBaseClassOf (type.Assembly, type.MetadataToken, baseType.Assembly, baseType.MetadataToken);
		}

		public static unsafe bool IsBaseClassOf (AssemblyMetadata typeAsm, uint type, AssemblyMetadata baseAsm, uint baseType)
		{
			TokenType typeTokType, baseTokType, interimTokType;
			uint typeRID, baseRID, interimRID;
			TypeDefRow typeDef, baseDef, interimDef;
			AssemblyMetadata interimAssembly;
			bool result = false;
			TypeDefRow lastInterimDef = null;


			MetadataToken.Decode (type, out typeTokType, out typeRID);
			MetadataToken.Decode (baseType, out baseTokType, out baseRID);

			if (IsTypeSystemObject (baseAsm, baseTokType, baseRID))
				return true;

			// Do not assume that 'baseDef' is more than null!

			typeDef = GetType (typeAsm, typeTokType, typeRID);
			baseDef = GetType (baseAsm, baseTokType, baseRID);
			interimDef = typeDef;
			interimAssembly = typeAsm;

			MetadataToken.Decode (typeDef.Extends, out interimTokType, out interimRID);

			while (true) {
				int nameLen;
				int nsLen;

				// If we've hit Object then the two types are unrelated

				if (IsTypeSystemObject (interimAssembly, interimTokType, interimRID))
					return false;

				lastInterimDef = interimDef;
				interimDef = GetType (interimAssembly, interimTokType, interimRID, out interimAssembly);

				// If baseDef is null, it's usually because of a TypeRef which pointed to an assembly that was not
				// included when AOTing, the primary example of this is mscorlib. To handle this, we make sure
				// that the involved tokens are TypeRefs, and if the interimRID == baseRID then we have reached
				// the base type we want.

				if (baseDef == null && interimDef == null && interimTokType == TokenType.TypeRef &&
				    baseTokType == TokenType.TypeRef && interimAssembly == baseAsm && baseRID == interimRID)
					return true;

				Diagnostics.Assert (interimDef != null,
					"Runtime.IsBaseClassOf(): Failed to find TypeDef for the base class of a type");

				Diagnostics.Assert (lastInterimDef != interimDef,
					"Runtime.IsBaseClassOf(): ERROR: TypeDef's base class is itself!!!");

				if (interimDef == baseDef)
					return true;

				// Reiterate

				MetadataToken.Decode (interimDef.Extends, out interimTokType, out interimRID);
			}

			return false;
		}
	}
}
