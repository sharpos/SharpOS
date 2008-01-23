//
//
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

		void EncodeAssemblyTable (AssemblyTable table)
		{
			const int rowSize = 28;
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

		public override void VisitTablesHeap (TablesHeap heap)
		{
			foreach (IMetadataTable table in heap.Tables) {
				this.asm.LABEL (moduleName + " " + table.GetType().Name);

				if (table is AssemblyTable)
					EncodeAssemblyTable (table as AssemblyTable);
			}
		}
	}
}