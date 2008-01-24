//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.IO;
using Mono.Cecil;
using Mono.Cecil.Metadata;

namespace SharpOS.AOT.X86 {
	public class MetadataVisitor : BaseMetadataVisitor {
		public MetadataVisitor (Assembly asm)
		{
			this.asm = asm;
		}

		Assembly asm;
		ModuleDefinition currentModule = null;
		string moduleName = null;

		public void Encode (ModuleDefinition def)
		{
			currentModule = def;
			moduleName = def.Name;
			currentModule.Image.MetadataRoot.Accept (this);
		}

		public override void TerminateMetadataRoot (MetadataRoot root)
		{
			Console.WriteLine ("Encoding metadata root as '" + moduleName + " MetadataRoot'");
			this.asm.LABEL (moduleName + " MetadataRoot");
			this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.ModuleMetadata).ToString ());
			this.asm.ADDRESSOF (moduleName + " StringsHeap");
			this.asm.ADDRESSOF (moduleName + " BlobHeap");
			this.asm.ADDRESSOF (moduleName + " GuidHeap");
			this.asm.ADDRESSOF (moduleName + " AssemblyArray");
		}

		void ArrayHeader (int len)
		{
			this.asm.AddObjectFields ("System.Array");
			this.asm.DATA (1U); // Rank
			this.asm.DATA (0U); // LowerBound
			this.asm.DATA ((uint)len); // Length
		}

		void Array (byte[] arr)
		{
			this.ArrayHeader (arr.Length);
			foreach (byte b in arr)
				this.asm.DATA (b);
		}

		public override void VisitBlobHeap (BlobHeap heap)
		{
			// Note: the *Heap classes provided by Mono.Cecil
			// make use of IDictionary and reflection heavily,
			// which I think makes them unsuitable for our work
			// here.

			this.asm.LABEL (moduleName + " BlobHeap");
			this.Array (heap.Data);
		}

		public override void VisitGuidHeap (GuidHeap heap)
		{
			this.asm.LABEL (moduleName + " GuidHeap");
			this.Array (heap.Data);
		}

		public override void VisitStringsHeap (StringsHeap heap)
		{
			this.asm.LABEL (moduleName + " StringsHeap");
			this.Array (heap.Data);
		}

		// Encoding

		void EncodeAssemblyOSTable (AssemblyOSTable table)
		{
			int index = 0;

			foreach (AssemblyOSRow row in table.Rows) {
				this.asm.LABEL (moduleName + " AssemblyOSRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.AssemblyOSRow).ToString ());
				this.asm.DATA (row.OSPlatformID);
				this.asm.DATA (row.OSMajorVersion);
				this.asm.DATA (row.OSMinorVersion);
				++index;
			}

			this.asm.LABEL (moduleName + " AssemblyOSArray");
			this.ArrayHeader (table.Rows.Count);
			for (int x = 0; x < index; ++x)
				this.asm.ADDRESSOF (moduleName + " AssemblyOSRow#" + x);
		}

		void EncodeAssemblyProcessorTable (AssemblyProcessorTable table)
		{
			int index = 0;

			foreach (AssemblyProcessorRow row in table.Rows) {
				this.asm.LABEL (moduleName + " AssemblyProcessorRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.AssemblyProcessorRow).ToString ());
				this.asm.DATA (row.Processor);
				++index;
			}

			this.asm.LABEL (moduleName + " AssemblyProcessorArray");
			this.ArrayHeader (table.Rows.Count);
			for (int x = 0; x < index; ++x)
				this.asm.ADDRESSOF (moduleName + " AssemblyProcessorRow#" + x);
		}

		void EncodeAssemblyRefOSTable (AssemblyRefOSTable table)
		{
			int index = 0;

			foreach (AssemblyRefOSRow row in table.Rows) {
				this.asm.LABEL (moduleName + " AssemblyRefOSRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.AssemblyRefOSRow).ToString ());
				this.asm.DATA (row.OSPlatformID);
				this.asm.DATA (row.OSMajorVersion);
				this.asm.DATA (row.OSMinorVersion);
				this.asm.DATA (row.AssemblyRef);
				++index;
			}

			this.asm.LABEL (moduleName + " AssemblyRefOSArray");
			this.ArrayHeader (table.Rows.Count);
			for (int x = 0; x < index; ++x)
				this.asm.ADDRESSOF (moduleName + " AssemblyRefOSRow#" + x);
		}

		void EncodeAssemblyRefProcessorTable (AssemblyRefProcessorTable table)
		{
			int index = 0;

			foreach (AssemblyRefProcessorRow row in table.Rows) {
				this.asm.LABEL (moduleName + " AssemblyRefProcessorRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.AssemblyRefProcessorRow).ToString ());
				this.asm.DATA (row.Processor);
				this.asm.DATA (row.AssemblyRef);
				++index;
			}

			this.asm.LABEL (moduleName + " AssemblyRefProcessorArray");
			this.ArrayHeader (table.Rows.Count);
			for (int x = 0; x < index; ++x)
				this.asm.ADDRESSOF (moduleName + " AssemblyRefProcessorRow#" + x);
		}

		void EncodeAssemblyRefTable (AssemblyRefTable table)
		{
			int index = 0;

			foreach (AssemblyRefRow row in table.Rows) {
				this.asm.LABEL (moduleName + " AssemblyRefRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.AssemblyRefRow).ToString ());
				this.asm.DATA (row.MajorVersion);
				this.asm.DATA (row.MinorVersion);
				this.asm.DATA (row.BuildNumber);
				this.asm.DATA (row.RevisionNumber);
				this.asm.DATA ((uint)row.Flags);
				this.asm.DATA (row.PublicKeyOrToken);
				this.asm.DATA (row.Name);
				this.asm.DATA (row.Culture);
				this.asm.DATA (row.HashValue);
				++index;
			}

			this.asm.LABEL (moduleName + " AssemblyRefArray");
			this.ArrayHeader (table.Rows.Count);
			for (int x = 0; x < index; ++x)
				this.asm.ADDRESSOF (moduleName + " AssemblyRefRow#" + x);
		}

		void EncodeAssemblyTable (AssemblyTable table)
		{
			int index = 0;

			foreach (AssemblyRow row in table.Rows) {
				this.asm.LABEL (moduleName + " AssemblyRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.AssemblyRow).ToString ());
				this.asm.DATA ((uint) row.HashAlgId);
				this.asm.DATA (row.MajorVersion);
				this.asm.DATA (row.MinorVersion);
				this.asm.DATA (row.BuildNumber);
				this.asm.DATA (row.RevisionNumber);
				this.asm.DATA ((uint) row.Flags);
				this.asm.DATA (row.PublicKey);
				this.asm.DATA (row.Name);
				this.asm.DATA (row.Culture);

				++index;
			}

			this.asm.LABEL (moduleName + " AssemblyArray");
			this.ArrayHeader (table.Rows.Count);
			for (int x = 0; x < index; ++x)
				this.asm.ADDRESSOF (moduleName + " AssemblyRow#" + x);
		}

		void EncodeClassLayoutTable (ClassLayoutTable table)
		{
			int index = 0;

			foreach (ClassLayoutRow row in table.Rows) {
				this.asm.LABEL (moduleName + " ClassLayoutRow#" + index);
				this.asm.AddObjectFields (typeof (SharpOS.AOT.Metadata.ClassLayoutRow).ToString ());
				this.asm.DATA (row.PackingSize);
				this.asm.DATA (row.ClassSize);
				this.asm.DATA (row.Parent);
				++index;
			}

			this.asm.LABEL (moduleName + " ClassLayoutArray");
			this.ArrayHeader (table.Rows.Count);
			for (int x = 0; x < index; ++x)
				this.asm.ADDRESSOF (moduleName + " ClassLayoutRow#" + x);
		}

		void EncodeConstantTable (ConstantTable table)
		{
			// TODO: shall we move constants here as well or not?
			// Also, the ElementType enum in Cecil does not specify
			// a base type.
		}

		void EncodeCustomAttributeTable (CustomAttributeTable table)
		{

		}

		void EncodeDeclSecurityTable (DeclSecurityTable table)
		{

		}

		void EncodeEventMapTable (EventMapTable table)
		{

		}

		void EncodeEventPtrTable (EventPtrTable table)
		{

		}

		void EncodeEventTable (EventTable table)
		{

		}

		void EncodeExportedTypeTable (ExportedTypeTable table)
		{

		}

		void EncodeFieldLayoutTable (FieldLayoutTable table)
		{

		}

		void EncodeFieldMarshalTable (FieldMarshalTable table)
		{

		}

		void EncodeFieldPtrTable (FieldPtrTable table)
		{

		}

		void EncodeFieldRVATable (FieldRVATable table)
		{

		}

		void EncodeFieldTable (FieldTable table)
		{

		}

		void EncodeFileTable (FileTable table)
		{

		}

		void EncodeGenericParamConstraintTable (GenericParamConstraintTable table)
		{

		}

		void EncodeGenericParamTable (GenericParamTable table)
		{

		}

		void EncodeImplMapTable (ImplMapTable table)
		{

		}

		void EncodeInterfaceImplTable (InterfaceImplTable table)
		{

		}

		void EncodeManifestResourceTable (ManifestResourceTable table)
		{

		}

		void EncodeMemberRefTable (MemberRefTable table)
		{

		}

		void EncodeMethodImplTable (MethodImplTable table)
		{

		}

		void EncodeMethodPtrTable (MethodPtrTable table)
		{

		}

		void EncodeMethodSemanticsTable (MethodSemanticsTable table)
		{

		}

		void EncodeMethodSpecTable (MethodSpecTable table)
		{

		}

		void EncodeMethodTable (MethodTable table)
		{

		}

		void EncodeModuleRefTable (ModuleRefTable table)
		{

		}

		void EncodeModuleTable (ModuleTable table)
		{

		}

		void EncodeNestedClassTable (NestedClassTable table)
		{

		}

		void EncodeParamPtrTable (ParamPtrTable table)
		{

		}

		void EncodeParamTable (ParamTable table)
		{

		}

		void EncodePropertyMapTable (PropertyMapTable table)
		{

		}

		void EncodePropertyPtrTable (PropertyPtrTable table)
		{

		}

		void EncodePropertyTable (PropertyTable table)
		{

		}

		void EncodeStandAloneSigTable (StandAloneSigTable table)
		{

		}

		void EncodeTypeDefTable (TypeDefTable table)
		{

		}

		void EncodeTypeRefTable (TypeRefTable table)
		{

		}

		void EncodeTypeSpecTable (TypeSpecTable table)
		{

		}

		public override void VisitTablesHeap (TablesHeap heap)
		{
			foreach (IMetadataTable table in heap.Tables) {
				this.asm.LABEL (moduleName + " " + table.GetType().Name);

				if (table is AssemblyOSTable)
					EncodeAssemblyOSTable (table as AssemblyOSTable);
				if (table is AssemblyProcessorTable)
					EncodeAssemblyProcessorTable (table as AssemblyProcessorTable);
				if (table is AssemblyRefOSTable)
					EncodeAssemblyRefOSTable (table as AssemblyRefOSTable);
				if (table is AssemblyRefProcessorTable)
					EncodeAssemblyRefProcessorTable (table as AssemblyRefProcessorTable);
				if (table is AssemblyRefTable)
					EncodeAssemblyRefTable (table as AssemblyRefTable);
				if (table is AssemblyTable)
					EncodeAssemblyTable (table as AssemblyTable);
				if (table is ClassLayoutTable)
					EncodeClassLayoutTable (table as ClassLayoutTable);
				if (table is ConstantTable)
					EncodeConstantTable (table as ConstantTable);
				if (table is CustomAttributeTable)
					EncodeCustomAttributeTable (table as CustomAttributeTable);
				if (table is DeclSecurityTable)
					EncodeDeclSecurityTable (table as DeclSecurityTable);
				if (table is EventMapTable)
					EncodeEventMapTable (table as EventMapTable);
				if (table is EventPtrTable)
					EncodeEventPtrTable (table as EventPtrTable);
				if (table is EventTable)
					EncodeEventTable (table as EventTable);
				if (table is ExportedTypeTable)
					EncodeExportedTypeTable (table as ExportedTypeTable);
				if (table is FieldLayoutTable)
					EncodeFieldLayoutTable (table as FieldLayoutTable);
				if (table is FieldMarshalTable)
					EncodeFieldMarshalTable (table as FieldMarshalTable);
				if (table is FieldPtrTable)
					EncodeFieldPtrTable (table as FieldPtrTable);
				if (table is FieldRVATable)
					EncodeFieldRVATable (table as FieldRVATable);
				if (table is FieldTable)
					EncodeFieldTable (table as FieldTable);
				if (table is FileTable)
					EncodeFileTable (table as FileTable);
				if (table is GenericParamConstraintTable)
					EncodeGenericParamConstraintTable (table as GenericParamConstraintTable);
				if (table is GenericParamTable)
					EncodeGenericParamTable (table as GenericParamTable);
				if (table is ImplMapTable)
					EncodeImplMapTable (table as ImplMapTable);
				if (table is InterfaceImplTable)
					EncodeInterfaceImplTable (table as InterfaceImplTable);
				if (table is ManifestResourceTable)
					EncodeManifestResourceTable (table as ManifestResourceTable);
				if (table is MemberRefTable)
					EncodeMemberRefTable (table as MemberRefTable);
				if (table is MethodImplTable)
					EncodeMethodImplTable (table as MethodImplTable);
				if (table is MethodPtrTable)
					EncodeMethodPtrTable (table as MethodPtrTable);
				if (table is MethodSemanticsTable)
					EncodeMethodSemanticsTable (table as MethodSemanticsTable);
				if (table is MethodSpecTable)
					EncodeMethodSpecTable (table as MethodSpecTable);
				if (table is MethodTable)
					EncodeMethodTable (table as MethodTable);
				if (table is ModuleRefTable)
					EncodeModuleRefTable (table as ModuleRefTable);
				if (table is ModuleTable)
					EncodeModuleTable (table as ModuleTable);
				if (table is NestedClassTable)
					EncodeNestedClassTable (table as NestedClassTable);
				if (table is ParamPtrTable)
					EncodeParamPtrTable (table as ParamPtrTable);
				if (table is ParamTable)
					EncodeParamTable (table as ParamTable);
				if (table is PropertyMapTable)
					EncodePropertyMapTable (table as PropertyMapTable);
				if (table is PropertyPtrTable)
					EncodePropertyPtrTable (table as PropertyPtrTable);
				if (table is PropertyTable)
					EncodePropertyTable (table as PropertyTable);
				if (table is StandAloneSigTable)
					EncodeStandAloneSigTable (table as StandAloneSigTable);
				if (table is TypeDefTable)
					EncodeTypeDefTable (table as TypeDefTable);
				if (table is TypeRefTable)
					EncodeTypeRefTable (table as TypeRefTable);
				if (table is TypeSpecTable)
					EncodeTypeSpecTable (table as TypeSpecTable);
			}
		}
	}
}