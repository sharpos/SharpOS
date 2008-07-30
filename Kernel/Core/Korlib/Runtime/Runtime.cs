//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

#define DEBUG_EXCEPTION_HANDLING

using System.Runtime.InteropServices;
using SharpOS.AOT.Metadata;
using SharpOS.AOT.Attributes;
using SharpOS.Kernel;
using SharpOS.Kernel.Foundation;
using SharpOS.Kernel.ADC;

namespace SharpOS.Korlib.Runtime {
#pragma warning disable 649
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
#pragma warning restore 649

	internal class Runtime {
#pragma warning disable 649
		[AddressOf ("MetadataRoot")]
		internal static MetadataRoot Root;

		[AddressOf ("MethodBoundaries")]
		internal static MethodBoundary [] MethodBoundaries;
#pragma warning restore 649

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
			uint token;

			return ResolveTypeRef (assembly, row, out destAssembly, out token);
		}

		public static unsafe TypeDefRow ResolveTypeRef (AssemblyMetadata assembly, TypeRefRow row,
			out AssemblyMetadata destAssembly, out uint typeDefToken)
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
			typeDefToken = 0;

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
			uint token;

			return GetType (assembly, type, rid, out destAssembly, out token);
		}

		public static TypeDefRow GetType (AssemblyMetadata assembly, TokenType type, uint rid,
			out AssemblyMetadata destAssembly, out uint typeDefToken)
		{
			if (type != TokenType.TypeRef && type != TokenType.TypeDef) {
				Debug.COM1.Write ("Token type: ");
				Debug.COM1.WriteLine (MetadataToken.GetTokenTypeString (type));
				throw new System.Exception ("token type must be either TypeRef or TypeDef");
			}

			Diagnostics.Assert (type == TokenType.TypeRef || type == TokenType.TypeDef,
				"Runtime.GetType(): token type must be either TypeRef or TypeDef");

			if (type == TokenType.TypeDef) {
				Diagnostics.Assert (rid < assembly.TypeDef.Length,
					"Runtime.GetType(): token out of range for TypeDef table");

				destAssembly = assembly;
				typeDefToken = rid;
				return assembly.TypeDef [rid - 1];
			} else {
				TypeRefRow row = assembly.TypeRef [rid - 1];

				return ResolveTypeRef (assembly, row, out destAssembly, out typeDefToken);
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

			Debug.COM1.Write (ns);
			Debug.COM1.Write (".");
			Debug.COM1.Write (name);

			MemoryManager.Free (name);
			MemoryManager.Free (ns);
		}

		public unsafe static void PrintTypeName (AssemblyMetadata assembly, TypeRefRow type)
		{
			CString8 *name, ns;

			name = GetString (assembly, type.Name);
			ns = GetString (assembly, type.Namespace);

			Debug.COM1.Write (ns);
			Debug.COM1.Write (".");
			Debug.COM1.Write (name);

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
			Debug.COM1.Write (" ");
			Debug.COM1.Write ("TypeDefRow#");
			Debug.COM1.Write (index);
			Debug.COM1.Write (" ");
			Debug.COM1.Write ((int)row.Flags, true);
			Debug.COM1.Write (" ");
			Debug.COM1.Write ((int)row.Name, true);
			Debug.COM1.Write (" ");
			Debug.COM1.Write ((int)row.Namespace, true);
			Debug.COM1.Write (" ");
			Debug.COM1.Write ((int)row.Extends, true);
			Debug.COM1.Write (" ");
			Debug.COM1.Write ((int)row.FieldList, true);
			Debug.COM1.Write (" ");
			Debug.COM1.Write ((int)row.MethodList, true);
			Debug.COM1.Write (" ");
			PrintTypeName (assembly, row);
			Debug.COM1.WriteLine ();
		}

#pragma warning disable 649
		[AddressOf ("SharpOS.Korlib.Runtime.TestD TypeInfo")]
		public static TypeInfo testd;

		[AddressOf ("SharpOS.Korlib.Runtime.TestD VTable")]
		public static VTable testdvt;
#pragma warning restore 649

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
				Debug.COM1.WriteLine ("*** testd.Assembly == null");
			else
				Debug.COM1.WriteLine ("*** testd.Assembly != null");

			if (testd.MetadataToken == 0)
				Debug.COM1.WriteLine ("*** testd.MetadataToken == 0");
			else
				Debug.COM1.WriteLine ("*** testd.MetadataToken != 0");

			if (HasValidMetadataToken (testd.Assembly, testd.MetadataToken))
				Debug.COM1.WriteLine ("*** testd.MetadataToken is valid");
			else
				Debug.COM1.WriteLine ("*** testd.MetadataToken is NOT valid");

			if (testdvt.Type == testd)
				Debug.COM1.WriteLine ("*** testdvt.Type == testd");
			else
				Debug.COM1.WriteLine ("*** testdvt.Type != testd");

			if (ptr >= kernelEnd)
				Debug.COM1.WriteLine ("ERROR");
			else
				Debug.COM1.WriteLine ("Location OK");

			__TestMethodBoundaries ();
			__TestObjectConversion ();
			__TestIsBaseClassOf ();
			
		}

		private unsafe static void __TestMethodBoundaries ()
		{
			Testcase.Test (MethodBoundaries.Length > 0, "Runtime", "MethodBoundaries.Length should be greater than 0");

#if false
			for (int i = 0; i < MethodBoundaries.Length; i++)
				PrintMethodBoundary (MethodBoundaries [i]);
#endif

			StackFrame [] callingStack = ExceptionHandling.GetCallingStack ();

			Testcase.Test (callingStack != null, "Runtime", "callingStack != null");

			PrintCallingStack (0, callingStack);
		}

		private unsafe static void PrintCallingStack (int skipFrames, StackFrame [] callingStack)
		{
			Debug.COM1.WriteLine ("=============================================================");
			Debug.COM1.WriteLine ("Stack Trace:");

			for (int i = skipFrames; i < callingStack.Length; i++) {
				Debug.COM1.Write (" at ");

				if (callingStack [i] == null) {
					Debug.COM1.WriteLine ("(unknown)");
				} else {
					Debug.COM1.Write (callingStack [i].MethodBoundary.Name);
					Debug.COM1.Write (" [IP=0x");
					Debug.COM1.WriteNumber ((int) callingStack [i].IP, true);
					Debug.COM1.Write (", BP=0x");
					Debug.COM1.WriteNumber ((int) callingStack [i].BP, true);
					Debug.COM1.WriteLine ("]");
				}
			}
		}

		private unsafe static void PrintCallingStackToScreen (StackFrame [] callingStack)
		{
			for (int i = 2; i < callingStack.Length; i++) {
				TextMode.Write (" at ");

				if (callingStack [i] == null) {
					TextMode.WriteLine ("(unknown)");
				} else {
					TextMode.Write (callingStack [i].MethodBoundary.Name);
					TextMode.Write (" [IP=0x");
					TextMode.Write ((int) callingStack [i].IP, true);
					TextMode.Write (", BP=0x");
					TextMode.Write ((int) callingStack [i].BP, true);
					TextMode.WriteLine ("]");
				}
			}
		}

		private static void PrintMethodBoundary (MethodBoundary methodBoundary)
		{
			Debug.COM1.Write ("MethodBoundary: ");
			Debug.COM1.WriteLine (methodBoundary.Name);

			if (methodBoundary.ExceptionHandlingClauses != null) {
				Debug.COM1.Write ("\tExceptions: ");
				Debug.COM1.WriteNumber (methodBoundary.ExceptionHandlingClauses.Length, false);
				Debug.COM1.WriteLine ();

				for (int i = 0; i < methodBoundary.ExceptionHandlingClauses.Length; i++)
					PrintExceptionHandlingClauses (methodBoundary.ExceptionHandlingClauses [i]);
			}
		}

		private static void PrintExceptionHandlingClauses (ExceptionHandlingClause exceptionHandlingClause)
		{
			Debug.COM1.Write ("\t\tType: ");
			Debug.COM1.WriteNumber ((int) exceptionHandlingClause.ExceptionType, true);
			Debug.COM1.WriteLine ();

			if (exceptionHandlingClause.ExceptionType == ExceptionHandlerType.Catch) {
				Testcase.Test (exceptionHandlingClause.TypeInfo != null, "Runtime", "exceptionHandlingClause.TypeInfo != null");

				if (exceptionHandlingClause.TypeInfo != null) {
					Debug.COM1.Write ("\t\tCatch Type: ");
					Debug.COM1.WriteLine (exceptionHandlingClause.TypeInfo.Name);
				}
			}
		}

		public static unsafe void __TestObjectConversion ()
		{
			TestA o1 = new TestA ();
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

			Runtime.Free (o1);
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

			Runtime.Free (o1);
			Runtime.Free (o2);
			Runtime.Free (o3);
			Runtime.Free (o4);
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

		[SharpOS.AOT.Attributes.IsInst]
		internal static unsafe object IsInst (InternalSystem.Object obj, TypeInfo type)
		{
			if (obj != null && IsBaseClassOf (obj.VTable.Type, type))
				return obj;
			else
				return null;
		}

		[SharpOS.AOT.Attributes.CastClass]
		internal static unsafe object CastClass (InternalSystem.Object obj, TypeInfo type)
		{
			if (obj != null && IsBaseClassOf (obj.VTable.Type, type))
				return obj;
			
			Throw (new InternalSystem.InvalidCastException (), 3);

			// It doesn't come so far
			return null;
		}

		[SharpOS.AOT.Attributes.OverflowHandler]
		internal static unsafe void OverflowHandler ()
		{
			Throw (new InternalSystem.OverflowException (), 3);
		}

		[SharpOS.AOT.Attributes.NullReferenceHandler]
		internal static unsafe void NullReferenceHandler ()
		{
			Throw (new InternalSystem.NullReferenceException (), 3);
		}

		[SharpOS.AOT.Attributes.Throw]
		internal static unsafe void Throw (InternalSystem.Exception exception)
		{
			Throw (exception, 3);
		}
		
		internal static unsafe void Throw (InternalSystem.Exception exception, int skipFrames)
		{
			// TODO check the exception's type to make sure it is an exception

			StackFrame [] stackFrames = ExceptionHandling.GetCallingStack ();

			if (exception.CallingStack == null) {
				exception.CallingStack = stackFrames;
				exception.IgnoreStackFramesCount = skipFrames;
				exception.CurrentStackFrame = skipFrames;
			}

#if DEBUG_EXCEPTION_HANDLING
			PrintCallingStack (exception.IgnoreStackFramesCount, exception.CallingStack);
			PrintCallingStack (0, stackFrames);
#endif
			while (true) {
				bool getNewHandler = false;
				int start;
				int end;

				GetExceptionHandler (exception, out start, out end);

				for (int i = start; i < end && !getNewHandler; i++) {
					int candidates;

					do {
						int clauseIndex = 0;
						candidates = 0;
						MethodBoundary methodBoundary = exception.CallingStack [i].MethodBoundary;
						ExceptionHandlingClause handler = null;

						for (int j = 0; j < exception.CallingStack [i].MethodBoundary.ExceptionHandlingClauses.Length; j++) {
							ExceptionHandlingClause exceptionHandlingClause = methodBoundary.ExceptionHandlingClauses [j];

							if (exception.CallingStack [i].IgnoreMethodBoundaryClause [j])
								continue;

							if (exception.CallingStack [i].IP < exceptionHandlingClause.TryBegin
									|| exception.CallingStack [i].IP >= exceptionHandlingClause.TryEnd)
								continue;

							if (exceptionHandlingClause.ExceptionType == ExceptionHandlerType.Catch
									&& !Runtime.IsBaseClassOf (exception.VTable.Type, exceptionHandlingClause.TypeInfo))
								continue;

							if (handler == null) {
								clauseIndex = j;
								handler = exceptionHandlingClause;

							} else {
								if ((exceptionHandlingClause.TryBegin >= handler.TryBegin
											&& exceptionHandlingClause.TryEnd < handler.TryEnd)
										|| (exceptionHandlingClause.TryBegin >= handler.TryBegin
											&& exceptionHandlingClause.TryEnd < handler.TryEnd)) {
									clauseIndex = j;
									handler = exceptionHandlingClause;
								}

								candidates++;
							}
						}

						void* bp = stackFrames [stackFrames.Length - (exception.CallingStack.Length - i + 1)].BP;
						getNewHandler = CallHandler (exception, getNewHandler, handler, i, clauseIndex, bp);

					} while (candidates != 0 && !getNewHandler);
				}
			}
		}

		/// <summary>
		/// Calls the handler.
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <param name="getNewHandler">if set to <c>true</c> [get new handler].</param>
		/// <param name="handler">The handler.</param>
		/// <param name="i">The i.</param>
		/// <param name="clauseIndex">Index of the clause.</param>
		/// <param name="callerBP">The caller BP.</param>
		/// <returns></returns>
		private unsafe static bool CallHandler (InternalSystem.Exception exception, bool getNewHandler, ExceptionHandlingClause handler, int i, int clauseIndex, void* callerBP)
		{
			if (handler != null) {
#if DEBUG_EXCEPTION_HANDLING
				Debug.COM1.Write ("Calling the found handler from Frame: #");
				Debug.COM1.WriteNumber ((int) (i - exception.IgnoreStackFramesCount), false);
				Debug.COM1.Write ("/Clause: #");
				Debug.COM1.WriteNumber ((int) clauseIndex, false);
				Debug.COM1.WriteLine ();
#endif

				exception.CallingStack [i].IgnoreMethodBoundaryClause [clauseIndex] = true;

				if (handler.ExceptionType == ExceptionHandlerType.Finally
						|| handler.ExceptionType == ExceptionHandlerType.Fault) {
					ExceptionHandling.CallFinallyFault (exception, handler, callerBP);

				} else if (handler.ExceptionType == ExceptionHandlerType.Filter) {
					// If it is a filter and it fails look for the next Exception Handler
					if (ExceptionHandling.CallFilter (exception, handler, callerBP) == 0)
						getNewHandler = true;

					else
						ExceptionHandling.CallHandler (exception, handler, callerBP);

				} else if (handler.ExceptionType == ExceptionHandlerType.Catch)
					ExceptionHandling.CallHandler (exception, handler, callerBP);
			}

			return getNewHandler;
		}

		/// <summary>
		/// Gets the exception handler.
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <param name="start">The start.</param>
		/// <param name="end">The end.</param>
		private unsafe static void GetExceptionHandler (InternalSystem.Exception exception, out int start, out int end)
		{
			ExceptionHandlingClause handler = null;
			int i = exception.CurrentStackFrame;

			// In the first pass we just look for a possible Exception Handler
			for (; i < exception.CallingStack.Length && handler == null; i++) {
				MethodBoundary methodBoundary = exception.CallingStack [i].MethodBoundary;

				for (int j = 0; j < exception.CallingStack [i].MethodBoundary.ExceptionHandlingClauses.Length; j++) {
					ExceptionHandlingClause exceptionHandlingClause = methodBoundary.ExceptionHandlingClauses [j];

					if (exception.CallingStack [i].IgnoreMethodBoundaryClause [j])
						continue;

					if (exceptionHandlingClause.ExceptionType == ExceptionHandlerType.Finally)
						continue;

					if (exceptionHandlingClause.ExceptionType == ExceptionHandlerType.Fault)
						continue;

					if (exception.CallingStack [i].IP < exceptionHandlingClause.TryBegin
							|| exception.CallingStack [i].IP >= exceptionHandlingClause.TryEnd)
						continue;

					if (exceptionHandlingClause.ExceptionType == ExceptionHandlerType.Catch
							&& !Runtime.IsBaseClassOf (exception.VTable.Type, exceptionHandlingClause.TypeInfo))
						continue;

					if (handler == null)
						handler = exceptionHandlingClause;

					else if ((exceptionHandlingClause.TryBegin >= handler.TryBegin
								&& exceptionHandlingClause.TryEnd < handler.TryEnd)
							|| (exceptionHandlingClause.TryBegin >= handler.TryBegin
								&& exceptionHandlingClause.TryEnd < handler.TryEnd))
						handler = exceptionHandlingClause;
				}
			}

			// TODO this should check out if the error occured in a thread or a process
			if (handler == null) {
				TextMode.Write ("Unhandled exception: ");
				TextMode.Write (exception.VTable.Type.Name);
				TextMode.Write (": ");
				TextMode.WriteLine (exception.Message);
				PrintCallingStackToScreen (exception.CallingStack);

				Debug.COM1.Write ("Unhandled exception: ");
				Debug.COM1.Write (exception.VTable.Type.Name);
				Debug.COM1.Write (": ");
				Debug.COM1.WriteLine (exception.Message);
				PrintCallingStack (exception.IgnoreStackFramesCount, exception.CallingStack);

				Diagnostics.Panic ("No exception handler found");
			}

			start = exception.CurrentStackFrame;
			end = i;
			exception.CurrentStackFrame = i - 1;
		}

		internal static unsafe InternalSystem.String AllocNewString (int size)
		{
			// TODO add GC support here

			void* vtableptr = (void*)Stubs.GetLabelAddress ("System.String VTable");
			VTable vtable = (VTable)GetObjectFromPointer (vtableptr);

			int allocSize = 0;
			if (size > 0) {
				// allocate size-1, because string object contains always one char
				allocSize += sizeof (char) * (size - 1);
			}
			allocSize += (int)vtable.Size;

			void* result = (void*)SharpOS.Kernel.ADC.MemoryManager.Allocate ((uint)allocSize);

			InternalSystem.Object _object = (InternalSystem.Object)GetObjectFromPointer (result);
			_object.VTable = vtable;

			return _object as InternalSystem.String;
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
		}

		public static unsafe bool IsBaseClassOf (TypeInfo type, TypeInfo baseType)
		{
			byte *systemObject = Stubs.CString ("System.Object");
			CString8 *str = null;

			// Special case for System.Object

			if (ByteString.Compare (systemObject, baseType.Name) == 0)
				return true;

			// If the type infos are the same, then the result is true

			Debug.COM1.Write ("type: 0x");
			Debug.COM1.Write ((int)type.MetadataToken, true);
			Debug.COM1.Write (" '");
			Debug.COM1.Write (type.Name);
			Debug.COM1.WriteLine ("'");


			Debug.COM1.Write ("basetype: 0x");
			Debug.COM1.Write ((int)baseType.MetadataToken, true);
			Debug.COM1.Write (" '");
			Debug.COM1.Write (baseType.Name);
			Debug.COM1.WriteLine ("'");

			if (type == baseType)
				return true;

			return IsBaseClassOf (type.Assembly, type.MetadataToken, baseType.Assembly, baseType.MetadataToken);
		}

		public static unsafe bool IsBaseClassOf (AssemblyMetadata typeAsm, uint type, AssemblyMetadata baseAsm, uint baseType)
		{
			TokenType typeTokType, baseTokType, interimTokType;
			uint typeRID, baseRID, interimRID;
			TypeDefRow typeDef, baseDef, interimDef;
			AssemblyMetadata interimAssembly;
			TypeDefRow lastInterimDef = null;

			if (typeAsm == baseAsm && type == baseType)
				return true;

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
		}
	}
}
