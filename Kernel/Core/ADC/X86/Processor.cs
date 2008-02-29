// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	Cédric Rousseau <cedrou@gmail.com>
//  João Manganeli Neto <joao.masterchief@hotmail.com>
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

	#region ProcessorFeatureFlags
	[Flags]
	public enum ProcessorFeatureFlags : ulong
	{
		ReservedFlagsMask = ~
		(	(1ul << 10) 
		|	(1ul << 20) 
		|	(1ul << 33) 
		|	(1ul << 43) 
		|	(1ul << 44)
		|	(1ul << 48)
		|	(1ul << 49)
		|	(1ul << 54)
		|	(1ul << 56)
		|	(1ul << 57)
		|	(1ul << 58)
		|	(1ul << 59)
		|	(1ul << 60)
		|	(1ul << 61)
		|	(1ul << 62)
		|	(1ul << 63)),

		FPU    = (1ul <<  0),	// x87 floating point unit on-chip
		VME    = (1ul <<  1),	// Virtual-mode enhancements
		DE     = (1ul <<  2),	// Debugging extensions, I/O breakpoints, CR4.DE
		PSE    = (1ul <<  3),	// Page-size extensions (4 MB pages).
		TSC    = (1ul <<  4),	// Time stamp counter. RDTSC and RDTSCP instruction support.
		MSR    = (1ul <<  5),	// AMD model-specific registers (MSRs), with RDMSR and WRMSR instructions.
		PAE    = (1ul <<  6),	// Physical-address extensions (PAE), support for physical addresses > 32b.
		MCE    = (1ul <<  7),	// Machine check exception, CR4.MCE
		CX8    = (1ul <<  8),	// CMPXCHG8B instruction
		APIC   = (1ul <<  9),	// Advanced programmable interrupt controller (APIC) exists and is enabled.
		//     = (1ul << 10),	// Reserved.
		SEP    = (1ul << 11),	// SYSENTER and SYSEXIT instructions
		MTRR   = (1ul << 12),	// Memory-type range registers. MTRRcap supported.
		PGE    = (1ul << 13),	// Page global extension, CR4.PGE.
		MCA    = (1ul << 14),	// Machine check architecture, MCG_CAP.
		CMOV   = (1ul << 15),	// Conditional move instructions, CMOV, FCMOV.
		PAT    = (1ul << 16),	// Page attribute table. PCD, PWT, and PATi are used to alter memory type.
		PSE36  = (1ul << 17),	// Page-size extensions. The PDE[20:13] supplies physical address [39:32].
		PSN    = (1ul << 18),	// MISC_CTL.PSND.
		CLFSH  = (1ul << 19),	// CLFLUSH instruction.
		//     = (1ul << 20),	// Reserved.
		DTES   = (1ul << 21),	// Debug Trace and EMON Store MSRs 
		ACPI   = (1ul << 22),	// THERM_CONTROL MSR 
		MMX    = (1ul << 23),	// MMX instructions.
		FXSR   = (1ul << 24),	// FXSAVE and FXRSTOR instructions.
		SSE    = (1ul << 25),	// SSE extensions.
		SSE2   = (1ul << 26),	// SSE2 extensions.
		SS     = (1ul << 27),	// selfsnoop.
		HTT    = (1ul << 28),	// Hyper-Threading Technology.
		TM1    = (1ul << 29),	// MISC_ENABLE.TM1E THERM_INTERRUPT and THERM_STATUS MSRs 
		IA64   = (1ul << 30),	// IA-64, JMPE Jv, JMPE Ev.
		PBE    = (1ul << 31),	// Pending Break Event, STPCLK, FERR#, MISC_ENABLE.PBE 

		SSE3   = (1ul << 32),	// SSE3 extensions. MXCSR, CR4.OSXMMEXCPT, #XF, if FPU=1 then also FISTTP
		//     = (1ul << 33),	// Reserved.
		DTSE64 = (1ul << 34),	// 64-bit Debug Trace and EMON Store MSRs 
		MON    = (1ul << 35),	// MONITOR/MWAIT instructions.
		DSCPL  = (1ul << 36),	// CPL-qualified Debug Store.
		VMX    = (1ul << 37),	// CR4.VMXE, VM* and VM* 
		SMX    = (1ul << 38),	// CR4.SMXE, GETSEC.
		EST    = (1ul << 39),	// Enhanced SpeedStep Technology.
		TM2    = (1ul << 40),	// MISC_ENABLE.TM2E THERM_INTERRUPT and THERM_STATUS MSRs 
		SSSE3  = (1ul << 41),	// SSSE3, MXCSR, CR4.OSXMMEXCPT, #XF 
		CID    = (1ul << 42),	// context ID: the L1 data cache can be set to adaptive or shared mode MISC_ENABLE.L1DCCM.
		//     = (1ul << 43),	// Reserved.
		//     = (1ul << 44),	// Reserved.
		CX16   = (1ul << 45),	// CMPXCHG16B.
		ETPRD  = (1ul << 46),	// MISC_ENABLE.ETPRD.
		PDCM   = (1ul << 47),	// Performance Debug Capability MSR.
		//     = (1ul << 48),	// Reserved.
		//     = (1ul << 49),	// Reserved.
		DCA    = (1ul << 50),	// Direct Cache Access.
		SSE4_1 = (1ul << 51),	// SSE4.1, MXCSR, CR4.OSXMMEXCPT, #XF.
		SSE4_2 = (1ul << 52),	// SSE4.2, MXCSR, CR4.OSXMMEXCPT, #XF 
		X2APIC = (1ul << 53),	// x2APIC: APIC_BASE.EXTD.
		//     = (1ul << 54),	// Reserved.
		POPCNT = (1ul << 55),	// POPCNT
		//     = (1ul << 56),	// Reserved.
		//     = (1ul << 57),	// Reserved.
		//     = (1ul << 58),	// Reserved.
		//     = (1ul << 59),	// Reserved.
		//     = (1ul << 60),	// Reserved.
		//     = (1ul << 61),	// Reserved.
		//     = (1ul << 62),	// Reserved.
		//     = (1ul << 63),	// Reserved.
	}
	#endregion
	
	// TODO: remove dependency on StringBuilder for names
	// TODO: implement family & model strings
	// TODO: implement other CPUID functionality
	// TODO: have CPUID fallback functionality? (do we care?)
	public unsafe class Processor : IProcessor {

		internal unsafe static void GetProcessorInfo(out UInt32 stepping, out UInt32 family, out UInt32 model, out ProcessorFeatureFlags flags)
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
			flags = (ProcessorFeatureFlags)flags64;
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

		internal unsafe void SetIntel()
		{
			UInt32 eax = 0, ebx = 0, ecx = 0, edx = 0;
			uint brand, family, model, cpu_signature, cpu_extfamily;
			Asm.XOR(R32.EAX, R32.EAX);
			Asm.INC(R32.EAX);
			Asm.CPUID();
			Asm.MOV(&eax, R32.EAX);
			Asm.MOV(&ebx, R32.EBX);
			Asm.MOV(&edx, R32.EDX);
			Asm.MOV(&ecx, R32.ECX);

			brand = ebx & 0x0F;

			cpu_signature = (eax);

			family = ((eax >> 8) & 0x0F);// +((eax >> 20) & 0x0F);

			model = ((eax >> 4) & 0x0F);// | ((eax >> 12) & 0x0F);

			cpu_extfamily = ((eax >> 20) & 0x0F);

			switch (family)
			{
				case 0x4:
					familyName = CString8.Copy("Intel Family 486");
					break;
				case 0x5:
					familyName = CString8.Copy("Intel Family 586");
					break;
				case 0x6:
					familyName = CString8.Copy("Intel Family 686");
					break;
				case 0x7:
					familyName = CString8.Copy("Intel Family Itanium");
					break;
				case 0xF:
					familyName = CString8.Copy("Intel Family Extended");
					break;
				default:
					familyName = CString8.Copy("Unknown Intel Family");
					break;
			}


			switch (brand)
			{
				case 0x00:
					brandName = CString8.Copy("brand ID not supported");
					switch (family)
					{
						case 0x4:
							modelName = CString8.Copy("Intel 486");
							break;
						case 0x5:
							modelName = CString8.Copy("Intel 586");
							break;
						case 0x6:
							switch (model)
							{
								case 0x1:
									modelName = CString8.Copy("Intel Pentium Pro");
									break;
								case 0x3:
									modelName = CString8.Copy("Intel Pentium II");
									break;
								case 0x5:
								case 0x6:
									modelName = CString8.Copy("Intel Celeron");
									break;
								case 0x7:
								case 0x8:
								case 0xA:
								case 0xB:
									modelName = CString8.Copy("Intel Pentium III");
									break;
								case 0x9:
								case 0xD:
									modelName = CString8.Copy("Intel Pentium M");
									break;
								default:
									modelName = CString8.Copy("Unknown Intel P6 Family");
									break;
							}
							break;
						case 0x7:
							modelName = CString8.Copy("Intel Itanium");
							break;
						case 0xF:
							switch (cpu_extfamily)
							{
								case 0x0:
									modelName = CString8.Copy("Intel Pentium 4");
									break;
								case 0x1:
									modelName = CString8.Copy("Intel Itanium 2");
									break;
							}
							break;
						default:
							modelName = CString8.Copy("Unknown Intel Model");
							break;
					}
					break;
				case 0x01:
				case 0x0A:
				case 0x14:
					brandName = CString8.Copy("Intel Celeron");
					modelName = CString8.Copy("Intel Celeron");
					break;
				case 0x02:
				case 0x04:
					brandName = CString8.Copy("Pentium III");
					modelName = CString8.Copy("Pentium III");
					break;
				case 0x03:
					if (cpu_signature == 0x6B1)
					{
						brandName = CString8.Copy("Intel Celeron");
						modelName = CString8.Copy("Intel Celeron");
					}
					else
					{
						brandName = CString8.Copy("Intel Pentium III Xeon");
						modelName = CString8.Copy("Intel Pentium III Xeon");
					}
					break;
				case 0x05:
					brandName = CString8.Copy("Mobile Intel Pentium III-M");
					modelName = CString8.Copy("Mobile Intel Pentium III-M");
					break;
				case 0x07:
				case 0x0F:
				case 0x13:
				case 0x17:
					brandName = CString8.Copy("Mobile Intel Celeron");
					modelName = CString8.Copy("Mobile Intel Pentium III-M");
					break;
				case 0x08:
				case 0x09:
					brandName = CString8.Copy("Intel Pentium 4");
					modelName = CString8.Copy("Intel Pentium 4");
					break;
				case 0x0B:
					brandName = CString8.Copy("Intel Xeon");
					modelName = CString8.Copy("Intel Xeon");
					break;
				case 0x0C:
					brandName = CString8.Copy("Intel Xeon MP");
					modelName = CString8.Copy("Intel Xeon MP");
					break;
				case 0x0E:
					if (cpu_signature == 0xF13)
					{
						brandName = CString8.Copy("Intel Xeon");
						modelName = CString8.Copy("Intel Xeon");
					}
					else
					{
						brandName = CString8.Copy("Mobile Intel Pentium 4");
						modelName = CString8.Copy("Mobile Intel Pentium 4");
					}
					break;
				case 0x12:
					brandName = CString8.Copy("Intel Celeron M");
					modelName = CString8.Copy("Intel Celeron M");
					break;
				case 0x16:
					brandName = CString8.Copy("Intel Pentium M");
					modelName = CString8.Copy("Intel Pentium M");
					break;
				case 0x15:
				case 0x11:
					brandName = CString8.Copy("Mobile Intel");
					modelName = CString8.Copy("Mobile Intel");
					break;
				default:
					brandName = CString8.Copy("Unknown Intel");
					modelName = CString8.Copy("Unknown Intel");
					break;
			}
		}
		
		internal unsafe void SetAMD()
		{
			brandName	= GetBrandName();
			familyName	= CString8.Copy("Not implemented yet");
			modelName	= CString8.Copy("Not implemented yet");
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

		#region Constructor
		internal Processor (uint _index)
		{
			Asm.CLI();

			index = _index;

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

				if (vendorName->Compare("GenuineIntel", 12) == 0)
				{
					SetIntel();
				} else 
				if (vendorName->Compare("AuthenticAMD", 12) == 0)
				{
					SetAMD();
				} else
				{
					brandName = GetBrandName();
					familyName = CString8.Copy("Not implemented yet");
					modelName = CString8.Copy("Not implemented yet");
				}
				
				GetProcessorInfo(out stepping, out family, out model, out featureFlags);

				if (0 != (featureFlags & ProcessorFeatureFlags.IA64))
					archType	= ProcessorType.IA64;
				else
					archType	= ProcessorType.IA32;

				ulong flags = ((ulong)featureFlags) & ((ulong)ProcessorFeatureFlags.ReservedFlagsMask);
				uint featureCount = MemoryUtil.BitCount(flags);

				features = new ProcessorFeature[featureCount];
				featureCount = 0;
				
				// TODO: this could be improved upon if we had:
				//	constructor support
				//	enum.ToString support
				// etc.
				if (0 != (flags & (ulong)ProcessorFeatureFlags.FPU   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("FPU"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.VME   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("VME"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.DE    )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("DE"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.PSE   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("PSE"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.TSC   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("TSC"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.MSR   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("MSR"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.PAE   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("PAE"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.MCE   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("MCE"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.CX8   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("CX8"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.APIC  )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("APIC"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.SEP   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("SEP"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.MTRR  )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("MTRR"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.PGE   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("PGE"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.MCA   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("MCA"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.CMOV  )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("CMOV"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.PAT   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("PAT"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.PSE36 )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("PSE36"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.PSN   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("PSN"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.CLFSH )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("CLFSH"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.DTES  )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("DTES"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.ACPI  )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("ACPI"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.MMX   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("MMX"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.FXSR  )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("FXSR"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.SSE   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("SSE"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.SSE2  )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("SSE2"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.SS    )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("SS"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.HTT   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("HTT"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.TM1   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("TM1"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.IA64  )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("IA64"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.PBE   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("PBE"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.SSE3  )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("SSE3"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.DTSE64)) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("DTSE64"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.MON   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("MON"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.DSCPL )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("DSCPL"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.VMX   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("VMX"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.SMX   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("SMX"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.EST   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("EST"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.TM2   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("TM2"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.SSSE3 )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("SSSE3"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.CID   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("CID"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.CX16  )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("CX16"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.ETPRD )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("ETPRD"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.PDCM  )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("PDCM"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.DCA   )) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("DCA"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.SSE4_1)) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("SSE4.1"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.SSE4_2)) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("SSE4.2"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.X2APIC)) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("X2APIC"); featureCount++; }
				if (0 != (flags & (ulong)ProcessorFeatureFlags.POPCNT)) { features[featureCount] = new ProcessorFeature(); features[featureCount].FeatureName = ("POPCNT"); featureCount++; }
			}
			Asm.STI();
		}
		#endregion

		#region ArchType
		private ProcessorType	archType		= ProcessorType.Unknown;		
		public ProcessorType	ArchType		{ get { return archType; } }
		#endregion
		
		#region VendorType
		private CString8*		vendorName		= null;
		public CString8*		VendorName		{ get { return vendorName; } }
		#endregion
		
		#region BrandName
		private CString8*		brandName		= null;
		public CString8*		BrandName		{ get { return brandName; } }
		#endregion
		
		#region FamilyName
		private CString8*		familyName		= null;
		public CString8*		FamilyName		{ get { return familyName; } }
		#endregion
		
		#region ModelName
		private CString8*		modelName		= null;
		public CString8*		ModelName		{ get { return modelName; } }
		#endregion
		
		#region Features
		private ProcessorFeatureFlags	featureFlags;
		private ProcessorFeature[]		features		= null;
		public ProcessorFeature[]		Features		{ get { return features; } }
		#endregion
				
		#region Index
		private uint			index			= 0;
		public uint				Index			{ get { return index; } }
		#endregion
		
		#region ID
		private uint			id				= 0;
		public uint				ID				{ get { return id; } }
		#endregion

		public void Halt ()
		{
			Asm.STI ();
			Asm.HLT ();
		}
	}
}


