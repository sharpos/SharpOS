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
		public int Member1;
	}

	class TestB : TestA {
		public int Member2;
	}

	internal class Runtime {

		[AddressOf ("MetadataRoot")]
		private static MetadataRoot Root;

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

		public static unsafe TypeDefRow GetType (AssemblyMetadata assembly, CString8 *name, CString8 *ns)
		{
			TypeDefRow result = null;

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
				break;
			}

			return result;
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

		public static void __RunTests ()
		{
			__TestIsBaseClassOf ();
		}


		public static void __TestIsBaseClassOf ()
		{
			object o1 = new TestA ();
			object o2 = new TestB ();
			InternalSystem.Object io1, io2;
			uint ui = 1109;

			io1 = o1 as InternalSystem.Object;
			io2 = o2 as InternalSystem.Object;

			Testcase.Test (Runtime.IsBaseClassOf (io2.VTable.Type, io1.VTable.Type),
				"Runtime", "IsBaseClassOf where result should be true");
			Testcase.Test (Runtime.IsBaseClassOf (io1.VTable.Type, io2.VTable.Type) == false,
				"Runtime", "IsBaseClassOf where result should be false");
		}

		public static bool IsBaseClassOf (TypeInfo type, TypeInfo baseType)
		{
			TokenType typeTokType, baseTokType, interimTokType;
			uint typeRID, baseRID, interimRID;
			TypeDefRow typeDef, baseDef, interimDef;
			AssemblyMetadata interimAssembly;
			bool result = false;
			TypeDefRow lastInterimDef = null;

			MetadataToken.Decode (type.MetadataToken, out typeTokType, out typeRID);
			MetadataToken.Decode (baseType.MetadataToken, out baseTokType, out baseRID);

			if (IsTypeSystemObject (baseType.Assembly, baseTokType, baseRID))
				return true;

			typeDef = GetType (type.Assembly, typeTokType, typeRID);
			baseDef = GetType (baseType.Assembly, baseTokType, baseRID);
			interimDef = typeDef;
			interimAssembly = type.Assembly;

			MetadataToken.Decode (typeDef.Extends, out interimTokType, out interimRID);

			while (true) {
				int nameLen;
				int nsLen;

				// If we've hit Object then the two types are unrelated

				if (IsTypeSystemObject (interimAssembly, interimTokType, interimRID))
					return false;

				lastInterimDef = interimDef;
				interimDef = GetType (interimAssembly, interimTokType, interimRID, out interimAssembly);

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
