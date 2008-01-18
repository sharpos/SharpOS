//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT;
using AOTAttr = SharpOS.AOT.Attributes;
using SharpOS.Kernel.Foundation;
using System;

namespace SharpOS.Kernel.ADC {

	// TODO: put this in x86 and wrap a class around it which returns a 
	//	string to make it portable..
	[Flags]
	public enum FeatureFlags : ulong
	{
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

	// TODO: turn this into an interface eventually, untill then pretend it's an interface..
	public unsafe class IProcessor {
		public virtual void				Setup()			{ }
		
		public virtual uint				ID				{ get { return 0; } }
		
		public virtual ProcessorType	ArchType		{ get { return ProcessorType.Unknown; } }
		
		public virtual CString8*		VendorName		{ get { return CString8.Copy (""); } }
		public virtual CString8*		BrandName		{ get { return CString8.Copy (""); } }
		public virtual CString8*		FamilyName		{ get { return CString8.Copy (""); } }
		public virtual CString8*		ModelName		{ get { return CString8.Copy (""); } }
		public virtual UInt64			Features		{ get { return 0; } }
	}
}
