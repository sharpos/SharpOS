// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Runtime.InteropServices;
using SharpOS.Kernel;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using ADC = SharpOS.Kernel.ADC;
using SharpOS.Kernel.Foundation;

namespace SharpOS.Kernel.ADC.X86 {

	// TODO: remove dependency on StringBuilder for names
	// TODO: implement family & model strings
	// TODO: implement other CPUID functionality
	// TODO: have CPUID fallback functionality? (do we care?)
	// TODO: wrap feature flags to make them platform independent
	public unsafe class Processor : IProcessor {

		internal unsafe static void GetProcessorInfo(out UInt32 stepping, out UInt32 family, out UInt32 model, out FeatureFlags flags)
		{
			UInt32 eax = 0, ebx = 0, ecx = 0, edx = 0;

			// Processor Info & Feature Bits
			Asm.XOR (R32.EAX, R32.EAX);
			Asm.INC (R32.EAX);
			Asm.CPUID ();
			Asm.MOV (&eax, R32.EAX);
			Asm.MOV (&ebx, R32.EBX);
			Asm.MOV (&edx, R32.EDX);
			Asm.MOV (&ecx, R32.ECX);


			// Signature
			// 3:0   - Stepping 
			// 7:4   - Base Model 
			// 11:8  - Base Family
			// 13:12 - Reserved / Processor Type 
			// 19:16 - Extended Model 
			// 27:20 - Extended Family 
			//
			// Family = Base Family + Extended Family
			// Model  = Base Model + (Extended Model << 4)
			stepping = (eax & 0x0F);
			model    = ((eax >> 4) & 0x0F) | ((eax >> 12) & 0x0F);
			family   = ((eax >> 8) & 0x0F) + ((eax >> 20) & 0x0F);


			// Miscellaneous Information
			// 7:0   - brand ID
			// 15:8  - CLFLUSH size
			// 23:16 - LogicalProcessorCount is the number of threads per
			//			CPU core times the number of CPU cores per processor
			// 31:24 - Initial local APIC physical ID.
			UInt32 miscellaneousInformation = ebx; // not sure what to do with this...


			// Feature flags
			UInt64 flags64 = 0;
			UInt32* flags32 = (UInt32*)&flags64;
			*flags32 = edx;
			flags32 += sizeof(UInt32);
			*flags32 = ecx;
			flags = (FeatureFlags)flags64;
		}
		
		internal unsafe static CString8* GetVendorName ()
		{
			UInt32 ebx = 0, ecx = 0, edx = 0;

			// Vendor Name
			Asm.XOR(R32.EAX, R32.EAX);
			Asm.CPUID();
			Asm.MOV(&ebx, R32.EBX);
			Asm.MOV(&edx, R32.EDX);
			Asm.MOV(&ecx, R32.ECX);

			textBuffer->Clear();
			textBuffer->AppendSubstring((byte*)&ebx, 0, 4);
			textBuffer->AppendSubstring((byte*)&edx, 0, 4);
			textBuffer->AppendSubstring((byte*)&ecx, 0, 4);
			return CString8.Copy(textBuffer->buffer);
		}

		internal unsafe static CString8* GetBrandName()
		{
			UInt32 eax = 0, ebx = 0, ecx = 0, edx = 0;

			// Check is brand name is available
			// (ebx&0xff)==0 means it isn't
			Asm.XOR(R32.EAX, R32.EAX);
			Asm.INC(R32.EAX);
			Asm.CPUID();
			Asm.MOV(&ebx, R32.EBX);

			if ((ebx & 0xff) == 0)
			{
				return CString8.Copy("Unknown");
			} else
			{
				textBuffer->Clear();
				for (uint i = 0x80000002; i <= 0x80000004; i++)
				{
					Asm.MOV(R32.EAX, &i);
					Asm.CPUID();
					Asm.MOV(&eax, R32.EAX);
					Asm.MOV(&ebx, R32.EBX);
					Asm.MOV(&edx, R32.EDX);
					Asm.MOV(&ecx, R32.ECX);

					textBuffer->AppendSubstring((byte*)&eax, 0, 4);
					textBuffer->AppendSubstring((byte*)&ebx, 0, 4);
					textBuffer->AppendSubstring((byte*)&ecx, 0, 4);
					textBuffer->AppendSubstring((byte*)&edx, 0, 4);
				}
				return CString8.Copy(textBuffer->buffer);
			}
		}

		internal unsafe static bool HaveCPUID()
		{
			byte result = 0;

			Asm.PUSHFD ();
			Asm.POP (R32.EAX);
			Asm.MOV (R32.ECX, R32.EAX);
			Asm.XOR (R32.EAX, 0x200000);
			Asm.PUSH (R32.EAX);
			Asm.POPFD ();
			Asm.PUSHFD ();
			Asm.POP (R32.EAX);
			Asm.CMP (R32.EAX, R32.ECX);
			Asm.SETZ (&result);

			return (result != 1);
		}

		private static StringBuilder* textBuffer;

		public override void Setup ()
		{
			Asm.CLI();

			bool haveCPU = HaveCPUID ();
			if (!haveCPU)
			{
				archType	= ProcessorType.IA32;
				vendorName	= CString8.Copy("Unknown");
				brandName	= CString8.Copy("Unknown");
				familyName	= CString8.Copy("Unknown");
				modelName	= CString8.Copy("Unknown");
			}
			if (haveCPU)
			{
				UInt32 stepping;
				UInt32 family;
				UInt32 model;
				
				textBuffer = StringBuilder.CREATE((uint)(20));
				vendorName	= GetVendorName ();
				brandName	= GetBrandName ();

				GetProcessorInfo(out stepping, out family, out model, out featureFlags);

				if (0 != (featureFlags & FeatureFlags.IA64))
					archType	= ProcessorType.IA64;
				else
					archType	= ProcessorType.IA32;
							
				familyName = CString8.Copy("Not implemented yet");
				modelName = CString8.Copy("Not implemented yet");
			}
			Asm.STI();		
		}

		private ProcessorType			archType		= ProcessorType.Unknown;
		private CString8*				vendorName		= null;
		private CString8*				brandName		= null;
		private CString8*				familyName		= null;
		private CString8*				modelName		= null;
		private FeatureFlags			featureFlags;
		
		public override ProcessorType	ArchType		{ get { return archType; } }
		public override CString8*		VendorName		{ get { return vendorName; } }
		public override CString8*		BrandName		{ get { return brandName; } }
		public override CString8*		FamilyName		{ get { return familyName; } }
		public override CString8*		ModelName		{ get { return modelName; } }
		public override UInt64			Features		{ get { return (UInt64)featureFlags; } }
	}
}


