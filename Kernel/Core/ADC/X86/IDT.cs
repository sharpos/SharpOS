//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	Ásgeir Halldórsson <asgeir.halldorsson@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

//#define DISPLAY_IDT_SETUP_SUMMARY

using System;
using System.Runtime.InteropServices;
using SharpOS.Kernel;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;
using ADC = SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.ADC.X86 {
	/// <summary>
	/// The Interrupt Descriptor Table (IDT) is a data structure used by the x86 architecture 
	/// to implement an interrupt vector table. The IDT is used by the processor to determine 
	/// the correct response to interrupts and exceptions.
	/// </summary>
	public unsafe class IDT {
		private const uint		STACK_SIZE		= 8192;
		private static uint		IDT_Stack_Top	= ((uint) Stubs.StaticAlloc (STACK_SIZE + 4)) + STACK_SIZE;
		
		#region Interrupt list
		public enum Interrupt {
			// CPU Exceptions

			DivideError = 0x00,	// AAM/DIV/IDIV divide by zero, DIV/IDIV result too large
			Debug = 0x01,	//
			NonMaskableInterrupt = 0x02,	// non-maskable interrupt
			BreakPoint = 0x03,	// INT 3, debugger breakpoint
			Overflow = 0x04,	// INTO instruction detected overflow 
			BoundaryRangeExceeded = 0x05,	// BOUND instruction detected overrange 
			UndefinedOpcode = 0x06,	// invalid instruction opcode 
			DeviceNotAvailable = 0x07,	// no coprocessor (ESC, WAIT instructions)
			DoubleFault = 0x08,	// exceptions during exception handler invocation
			//Reserved				= 0x09,	
			InvalidTSS = 0x0A,	// implicit TSS accesses
			NotPresent = 0x0B,	// Segment register loads, explicit/implicit segment register accesses
			StackSegment = 0x0C,	// SS loads, explicit/implicit SS accesses
			GeneralProtection = 0x0D,	//
			PageFault = 0x0E,	//
			//Reserved				= 0x0F,	
			MathFault = 0x10,	// coprocessor error (ESC, WAIT instructions)
			AlignmentChecking = 0x11,	// Misaligned accesses / Lock accross cache line or page boundary (486+ only)
			MachineCheck = 0x12,	// Internal error, bus error, or bus error detected by external agent (Pentium+ only)
			ExtendedMathFault = 0x13,	//
			//Reserved				= 0x14,	
			//Reserved				= 0x15,	
			//Reserved				= 0x16,	
			//Reserved				= 0x17,	
			//Reserved				= 0x18,	
			//Reserved				= 0x19,	
			//Reserved				= 0x1A,	
			//Reserved				= 0x1B,	
			//Reserved				= 0x1C,	
			//Reserved				= 0x1D,	
			//Reserved				= 0x1E,	
			//Reserved				= 0x1F,	

			LastException = 0x1F,

			// Hardware Interrupts

			IRQ0 = 0x20,
			IRQ1 = 0x21,
			IRQ2 = 0x22,
			IRQ3 = 0x23,
			IRQ4 = 0x24,
			IRQ5 = 0x25,
			IRQ6 = 0x26,
			IRQ7 = 0x27,
			IRQ8 = 0x28,
			IRQ9 = 0x29,
			IRQ10 = 0x2A,
			IRQ11 = 0x2B,
			IRQ12 = 0x2C,
			IRQ13 = 0x2D,
			IRQ14 = 0x2E,
			IRQ15 = 0x2F,

			SystemTimer = Interrupt.IRQ0,
			Keyboard = Interrupt.IRQ1,
			COM2Default = Interrupt.IRQ3,
			COM4User = Interrupt.IRQ3,
			COM1Default = Interrupt.IRQ4,
			COM3User = Interrupt.IRQ4,
			ParallelPort2 = Interrupt.IRQ5,
			FloppyDiskController = Interrupt.IRQ6,
			ParallelPort1 = Interrupt.IRQ7,
			SoundCard = Interrupt.IRQ7,
			RealTimeClock = Interrupt.IRQ8,
			PS2Mouse = Interrupt.IRQ12,
			ISA = Interrupt.IRQ13,
			CoProcessor386 = Interrupt.IRQ13,
			PrimaryIDE = Interrupt.IRQ14,
			SecondaryIDE = Interrupt.IRQ15,

			LastHardwareIRQ = Interrupt.IRQ15,

			SysCall = 0x30, // This is the only interrupt with a ring 3 gate descriptor. 
		}
		#endregion

		#region Function Label Constants
		private const string IDT_POINTER = "IDTPointer";
		private const string IDT_TABLE = "IDTTable";
		private const string ISR_DEFAULT_HANDLER = "ISRDefaultHandler";
		private const string IRQ_CLEAN_UP = "IRQ_CLEAN_UP";
		private const string ISR_PAGE_FAULT = "ISR_PAGE_FAULT";
		#endregion

		#region Tables
		private const ushort Entries = 256;
		private static DTPointer* idtPointer = (DTPointer*) Stubs.LabelledAlloc (IDT_POINTER, DTPointer.SizeOf);
		private static IDTDescriptor* idtTable = (IDTDescriptor*) Stubs.StaticAlloc (Entries * IDTDescriptor.SizeOf);
		private static uint* ISRTable = (uint*) Stubs.LabelledAlloc (IDT_TABLE, Entries * 4);
		#endregion

		#region ISROptions
		[Flags]
		public enum ISROptions : byte {
			Call_Gate = 4,
			Task_Gate = 5,
			Interrupt_Gate = 6,
			Trap_Gate = 7,
			OperandSize_16Bit = 0,
			OperandSize_32Bit = 8,
			Privilege_Ring_0 = 0,
			Privilege_Ring_1 = 32,
			Privilege_Ring_2 = 64,
			Privilege_Ring_3 = 96,
			Present = 128
		}
		#endregion

		#region IDT Descriptor class
		[StructLayout (LayoutKind.Sequential)]
		public struct IDTDescriptor {
			public const uint SizeOf = 8;

			private ushort OffsetLow;
			public ushort Selector;
			private byte Unused;
			public ISROptions Options;
			private ushort OffsetHigh;

			public uint Offset
			{
				get
				{
					return (uint) (((uint) this.OffsetLow) | (((uint) this.OffsetHigh) << 16));
				}
				set
				{
					this.OffsetLow = (ushort) ((value) & 0xFFFF);
					this.OffsetHigh = (ushort) ((value >> 16) & 0xFFFF);
				}
			}

			public void Setup (ushort selector, uint offset, ISROptions options)
			{
				this.Selector = selector;
				this.Offset = offset;
				this.Options = options;
			}
		}
		#endregion


		#region ISRData struct
		[StructLayout (LayoutKind.Sequential)]
		public struct ISRData {
			public const int SizeOf = 3 * 4;
			
			public uint		FrameBP;
			public uint		FrameIP;
			public Stack*	Stack;	
		}
		#endregion

		#region Stack
		[StructLayout (LayoutKind.Sequential)]
		public struct Stack {
	
			public uint		EDI;
			public uint		ESI;
			public uint		EBP;
			public uint		ESP;
			public uint		EBX;
			public uint		EDX;
			public uint		ECX;
			public uint		EAX;	
			public uint		SS;
			public uint		FS;
			public uint		GS;
			public uint		ES;
			public uint		DS;
			public uint		IrqIndex;
			public uint		Error;
			// ...above should be pushed on the new stack, not the old stack!

			public uint		EIP;
			public uint		CS;
			public uint		EFlags;
			public uint		UserESP;
		};
		#endregion

		#region Setup
		internal static void Setup ()
		{
			idtPointer->Setup ((ushort) (sizeof (IDTDescriptor) * Entries - 1), (uint) idtTable);

#if DISPLAY_IDT_SETUP_SUMMARY // TO TOGGLE, REFER TO TOP OF FILE
			ADC.TextMode.Write ("IDT Pointer: 0x");
			ADC.TextMode.Write ((int) idtPointer->Address, true);
			ADC.TextMode.Write (" - 0x");
			ADC.TextMode.Write (idtPointer->Size, true);
			ADC.TextMode.WriteLine ();
#endif

			for (int i = 0; i < Entries; i++)
				ISRTable [i] = Stubs.GetFunctionPointer (ISR_DEFAULT_HANDLER);

			ISRTable [(int) Interrupt.PageFault] = Stubs.GetFunctionPointer (ISR_PAGE_FAULT);

			SetupISR ();

			Asm.LIDT (new SharpOS.AOT.X86.Memory (IDT_POINTER));

			Asm.STI ();
		}
		#endregion

		#region RegisterIRQ
		public static void RegisterIRQ (Interrupt irq, uint address)
		{
			byte index = (byte) irq;
			ISRTable [index] = address;
			PIC.EnableIRQ (index);
		}

		#endregion

		#region IRQCleanUp
		[SharpOS.AOT.Attributes.Label (IRQ_CLEAN_UP)]
		private static unsafe void IRQCleanUp (ISRData data)
		{
			PIC.SendEndOfInterrupt ((byte) data.Stack->IrqIndex);
		}
		#endregion

		#region ISRPageFault
		[SharpOS.AOT.Attributes.Label (ISR_PAGE_FAULT)]
		private static unsafe void ISRPageFault (ISRData data)
		{
			uint cr2 = 0;

			Asm.PUSH (R32.EAX);

			Asm.MOV (R32.EAX, CR.CR2);
			Asm.MOV (&cr2, R32.EAX);

			Asm.POP (R32.EAX);

			// FIXME: If I dont call Write function the variables in align and map get offset or something
			//ADC.TextMode.Write ("");

			//ADC.TextMode.Write("");
			//ADC.TextMode.WriteLine("Page fault invoked!.\n");

			/*
			ADC.TextMode.Write("Page: ");
			ADC.TextMode.Write((int)cr2);
			ADC.TextMode.Write(", EIP: ");
			ADC.TextMode.Write((int)data.Stack->EIP);
			ADC.TextMode.WriteLine();
			*/

			// Align to correct page
			void* align = ADC.Pager.PageAlign ((void*) cr2, 0);
			void* alloc = SharpOS.Kernel.Memory.PageAllocator.Alloc ();

			if (alloc == null) {
				Diagnostics.Panic ("Out of memory exception");
			}

			ADC.Pager.MapPage (align, alloc, 0, SharpOS.Kernel.Memory.PageAttributes.Present | SharpOS.Kernel.Memory.PageAttributes.ReadWrite);
		}
		#endregion

		#region ISRDefaultHandler
		[SharpOS.AOT.Attributes.Label (ISR_DEFAULT_HANDLER)]
		private static unsafe void ISRDefaultHandler (ISRData data)
		{
			uint cr2 = 0;
			Asm.MOV (R32.EAX, CR.CR2);
			Asm.MOV (&cr2, R32.EAX);

			Diagnostics.SetErrorTextAttributes ();
			ADC.TextMode.WriteLine ("Error: The default ISR handler was invoked!.\n");
			ADC.TextMode.WriteLine ("Interrupt=0x", (int) data.Stack->IrqIndex);
			switch ((Interrupt) data.Stack->IrqIndex) {
			case Interrupt.DivideError:
				ADC.TextMode.WriteLine ("          Divide Error");
				break;
			case Interrupt.Debug:
				ADC.TextMode.WriteLine ("          Debug");
				break;
			case Interrupt.NonMaskableInterrupt:
				ADC.TextMode.WriteLine ("          NonMaskable Interrupt");
				break;
			case Interrupt.BreakPoint:
				ADC.TextMode.WriteLine ("          Break Point");
				break;
			case Interrupt.Overflow:
				ADC.TextMode.WriteLine ("          Overflow");
				break;
			case Interrupt.BoundaryRangeExceeded:
				ADC.TextMode.WriteLine ("          Boundary Range Exceeded");
				break;
			case Interrupt.UndefinedOpcode:
				ADC.TextMode.WriteLine ("          Undefined Opcode");
				break;
			case Interrupt.DeviceNotAvailable:
				ADC.TextMode.WriteLine ("          Device Not Available");
				break;
			case Interrupt.DoubleFault:
				ADC.TextMode.WriteLine ("          Double Fault");
				break;
			case Interrupt.InvalidTSS:
				ADC.TextMode.WriteLine ("          Invalid TSS");
				break;
			case Interrupt.NotPresent:
				ADC.TextMode.WriteLine ("          Not Present");
				break;
			case Interrupt.StackSegment:
				ADC.TextMode.WriteLine ("          Stack Segment");
				break;
			case Interrupt.GeneralProtection:
				ADC.TextMode.WriteLine ("          General Protection");
				break;
			case Interrupt.PageFault:
				ADC.TextMode.WriteLine ("          Page Fault");
				break;
			case Interrupt.MathFault:
				ADC.TextMode.WriteLine ("          Math Fault");
				break;
			case Interrupt.AlignmentChecking:
				ADC.TextMode.WriteLine ("          Alignment Checking");
				break;
			case Interrupt.MachineCheck:
				ADC.TextMode.WriteLine ("          Machine Check");
				break;
			case Interrupt.ExtendedMathFault:
				ADC.TextMode.WriteLine ("          Extended Math Fault");
				break;
			}
			ADC.TextMode.WriteLine ();
			ADC.TextMode.WriteLine ("Register dump:");

			ADC.TextMode.Write ("  CR2=0x", (int) cr2);
			ADC.TextMode.WriteLine ();

			ADC.TextMode.Write ("  EIP=0x", (int) data.Stack->EIP);
			ADC.TextMode.WriteLine ();

			ADC.TextMode.Write ("  EAX=0x", (int) data.Stack->EAX);
			ADC.TextMode.Write ("  ECX=0x", (int) data.Stack->ECX);
			ADC.TextMode.Write ("  EDX=0x", (int) data.Stack->EDX);
			ADC.TextMode.Write ("  EBX=0x", (int) data.Stack->EBX);
			ADC.TextMode.WriteLine ();


			ADC.TextMode.Write ("  ESP=0x", (int) data.Stack->ESP);
			ADC.TextMode.Write ("  EBP=0x", (int) data.Stack->EBP);
			ADC.TextMode.Write ("  ESI=0x", (int) data.Stack->ESI);
			ADC.TextMode.Write ("  EDI=0x", (int) data.Stack->EDI);
			ADC.TextMode.WriteLine ();

			ADC.TextMode.Write ("   DS=0x", (int) data.Stack->DS);
			ADC.TextMode.Write ("   ES=0x", (int) data.Stack->ES);
			ADC.TextMode.Write ("   FS=0x", (int) data.Stack->FS);
			ADC.TextMode.Write ("   GS=0x", (int) data.Stack->GS);
			ADC.TextMode.Write ("   SS=0x", (int) data.Stack->SS);
			ADC.TextMode.Write ("   CS=0x", (int) data.Stack->CS);
			ADC.TextMode.WriteLine ();

			Asm.CLI ();
			Asm.HLT ();
		}
		#endregion

		#region SetupISR
		private static unsafe void SetupISR ()
		{
			ISROptions type = (ISROptions.Present |
								ISROptions.Privilege_Ring_0 |
								ISROptions.OperandSize_32Bit |
								ISROptions.Interrupt_Gate);

			idtTable [0].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_0"), type);
			idtTable [1].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_1"), type);
			idtTable [2].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_2"), type);
			idtTable [3].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_3"), type);
			idtTable [4].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_4"), type);
			idtTable [5].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_5"), type);
			idtTable [6].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_6"), type);
			idtTable [7].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_7"), type);
			idtTable [8].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_8"), type);
			idtTable [9].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_9"), type);
			idtTable [10].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_10"), type);
			idtTable [11].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_11"), type);
			idtTable [12].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_12"), type);
			idtTable [13].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_13"), type);
			idtTable [14].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_14"), type);
			idtTable [15].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_15"), type);
			idtTable [16].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_16"), type);
			idtTable [17].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_17"), type);
			idtTable [18].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_18"), type);
			idtTable [19].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_19"), type);
			idtTable [20].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_20"), type);
			idtTable [21].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_21"), type);
			idtTable [22].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_22"), type);
			idtTable [23].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_23"), type);
			idtTable [24].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_24"), type);
			idtTable [25].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_25"), type);
			idtTable [26].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_26"), type);
			idtTable [27].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_27"), type);
			idtTable [28].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_28"), type);
			idtTable [29].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_29"), type);
			idtTable [30].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_30"), type);
			idtTable [31].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_31"), type);
			idtTable [32].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_32"), type);
			idtTable [33].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_33"), type);
			idtTable [34].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_34"), type);
			idtTable [35].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_35"), type);
			idtTable [36].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_36"), type);
			idtTable [37].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_37"), type);
			idtTable [38].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_38"), type);
			idtTable [39].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_39"), type);
			idtTable [40].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_40"), type);
			idtTable [41].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_41"), type);
			idtTable [42].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_42"), type);
			idtTable [43].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_43"), type);
			idtTable [44].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_44"), type);
			idtTable [45].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_45"), type);
			idtTable [46].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_46"), type);
			idtTable [47].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_47"), type);
			idtTable [48].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_48"), type);
			idtTable [49].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_49"), type);
			idtTable [50].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_50"), type);
			idtTable [51].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_51"), type);
			idtTable [52].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_52"), type);
			idtTable [53].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_53"), type);
			idtTable [54].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_54"), type);
			idtTable [55].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_55"), type);
			idtTable [56].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_56"), type);
			idtTable [57].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_57"), type);
			idtTable [58].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_58"), type);
			idtTable [59].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_59"), type);
			idtTable [60].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_60"), type);
			idtTable [61].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_61"), type);
			idtTable [62].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_62"), type);
			idtTable [63].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_63"), type);
			idtTable [64].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_64"), type);
			idtTable [65].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_65"), type);
			idtTable [66].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_66"), type);
			idtTable [67].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_67"), type);
			idtTable [68].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_68"), type);
			idtTable [69].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_69"), type);
			idtTable [70].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_70"), type);
			idtTable [71].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_71"), type);
			idtTable [72].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_72"), type);
			idtTable [73].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_73"), type);
			idtTable [74].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_74"), type);
			idtTable [75].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_75"), type);
			idtTable [76].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_76"), type);
			idtTable [77].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_77"), type);
			idtTable [78].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_78"), type);
			idtTable [79].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_79"), type);
			idtTable [80].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_80"), type);
			idtTable [81].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_81"), type);
			idtTable [82].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_82"), type);
			idtTable [83].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_83"), type);
			idtTable [84].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_84"), type);
			idtTable [85].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_85"), type);
			idtTable [86].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_86"), type);
			idtTable [87].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_87"), type);
			idtTable [88].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_88"), type);
			idtTable [89].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_89"), type);
			idtTable [90].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_90"), type);
			idtTable [91].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_91"), type);
			idtTable [92].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_92"), type);
			idtTable [93].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_93"), type);
			idtTable [94].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_94"), type);
			idtTable [95].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_95"), type);
			idtTable [96].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_96"), type);
			idtTable [97].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_97"), type);
			idtTable [98].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_98"), type);
			idtTable [99].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_99"), type);
			idtTable [100].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_100"), type);
			idtTable [101].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_101"), type);
			idtTable [102].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_102"), type);
			idtTable [103].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_103"), type);
			idtTable [104].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_104"), type);
			idtTable [105].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_105"), type);
			idtTable [106].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_106"), type);
			idtTable [107].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_107"), type);
			idtTable [108].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_108"), type);
			idtTable [109].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_109"), type);
			idtTable [110].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_110"), type);
			idtTable [111].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_111"), type);
			idtTable [112].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_112"), type);
			idtTable [113].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_113"), type);
			idtTable [114].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_114"), type);
			idtTable [115].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_115"), type);
			idtTable [116].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_116"), type);
			idtTable [117].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_117"), type);
			idtTable [118].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_118"), type);
			idtTable [119].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_119"), type);
			idtTable [120].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_120"), type);
			idtTable [121].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_121"), type);
			idtTable [122].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_122"), type);
			idtTable [123].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_123"), type);
			idtTable [124].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_124"), type);
			idtTable [125].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_125"), type);
			idtTable [126].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_126"), type);
			idtTable [127].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_127"), type);
			idtTable [128].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_128"), type);
			idtTable [129].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_129"), type);
			idtTable [130].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_130"), type);
			idtTable [131].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_131"), type);
			idtTable [132].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_132"), type);
			idtTable [133].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_133"), type);
			idtTable [134].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_134"), type);
			idtTable [135].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_135"), type);
			idtTable [136].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_136"), type);
			idtTable [137].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_137"), type);
			idtTable [138].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_138"), type);
			idtTable [139].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_139"), type);
			idtTable [140].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_140"), type);
			idtTable [141].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_141"), type);
			idtTable [142].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_142"), type);
			idtTable [143].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_143"), type);
			idtTable [144].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_144"), type);
			idtTable [145].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_145"), type);
			idtTable [146].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_146"), type);
			idtTable [147].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_147"), type);
			idtTable [148].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_148"), type);
			idtTable [149].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_149"), type);
			idtTable [150].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_150"), type);
			idtTable [151].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_151"), type);
			idtTable [152].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_152"), type);
			idtTable [153].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_153"), type);
			idtTable [154].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_154"), type);
			idtTable [155].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_155"), type);
			idtTable [156].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_156"), type);
			idtTable [157].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_157"), type);
			idtTable [158].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_158"), type);
			idtTable [159].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_159"), type);
			idtTable [160].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_160"), type);
			idtTable [161].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_161"), type);
			idtTable [162].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_162"), type);
			idtTable [163].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_163"), type);
			idtTable [164].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_164"), type);
			idtTable [165].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_165"), type);
			idtTable [166].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_166"), type);
			idtTable [167].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_167"), type);
			idtTable [168].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_168"), type);
			idtTable [169].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_169"), type);
			idtTable [170].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_170"), type);
			idtTable [171].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_171"), type);
			idtTable [172].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_172"), type);
			idtTable [173].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_173"), type);
			idtTable [174].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_174"), type);
			idtTable [175].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_175"), type);
			idtTable [176].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_176"), type);
			idtTable [177].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_177"), type);
			idtTable [178].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_178"), type);
			idtTable [179].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_179"), type);
			idtTable [180].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_180"), type);
			idtTable [181].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_181"), type);
			idtTable [182].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_182"), type);
			idtTable [183].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_183"), type);
			idtTable [184].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_184"), type);
			idtTable [185].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_185"), type);
			idtTable [186].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_186"), type);
			idtTable [187].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_187"), type);
			idtTable [188].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_188"), type);
			idtTable [189].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_189"), type);
			idtTable [190].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_190"), type);
			idtTable [191].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_191"), type);
			idtTable [192].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_192"), type);
			idtTable [193].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_193"), type);
			idtTable [194].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_194"), type);
			idtTable [195].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_195"), type);
			idtTable [196].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_196"), type);
			idtTable [197].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_197"), type);
			idtTable [198].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_198"), type);
			idtTable [199].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_199"), type);
			idtTable [200].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_200"), type);
			idtTable [201].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_201"), type);
			idtTable [202].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_202"), type);
			idtTable [203].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_203"), type);
			idtTable [204].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_204"), type);
			idtTable [205].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_205"), type);
			idtTable [206].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_206"), type);
			idtTable [207].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_207"), type);
			idtTable [208].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_208"), type);
			idtTable [209].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_209"), type);
			idtTable [210].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_210"), type);
			idtTable [211].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_211"), type);
			idtTable [212].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_212"), type);
			idtTable [213].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_213"), type);
			idtTable [214].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_214"), type);
			idtTable [215].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_215"), type);
			idtTable [216].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_216"), type);
			idtTable [217].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_217"), type);
			idtTable [218].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_218"), type);
			idtTable [219].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_219"), type);
			idtTable [220].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_220"), type);
			idtTable [221].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_221"), type);
			idtTable [222].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_222"), type);
			idtTable [223].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_223"), type);
			idtTable [224].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_224"), type);
			idtTable [225].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_225"), type);
			idtTable [226].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_226"), type);
			idtTable [227].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_227"), type);
			idtTable [228].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_228"), type);
			idtTable [229].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_229"), type);
			idtTable [230].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_230"), type);
			idtTable [231].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_231"), type);
			idtTable [232].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_232"), type);
			idtTable [233].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_233"), type);
			idtTable [234].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_234"), type);
			idtTable [235].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_235"), type);
			idtTable [236].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_236"), type);
			idtTable [237].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_237"), type);
			idtTable [238].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_238"), type);
			idtTable [239].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_239"), type);
			idtTable [240].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_240"), type);
			idtTable [241].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_241"), type);
			idtTable [242].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_242"), type);
			idtTable [243].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_243"), type);
			idtTable [244].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_244"), type);
			idtTable [245].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_245"), type);
			idtTable [246].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_246"), type);
			idtTable [247].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_247"), type);
			idtTable [248].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_248"), type);
			idtTable [249].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_249"), type);
			idtTable [250].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_250"), type);
			idtTable [251].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_251"), type);
			idtTable [252].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_252"), type);
			idtTable [253].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_253"), type);
			idtTable [254].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_254"), type);
			idtTable [255].Setup (GDT.CodeSelector, Stubs.GetFunctionPointer ("ISR_255"), type);
		}
		#endregion

		#region ISRHandlers
		[SharpOS.AOT.Attributes.Naked]
		private static unsafe void ISRHandlers ()
		{
			#region ISR Dispatchers
			Asm.LABEL ("ISR_0");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 0);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_1");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 1);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_2");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 2);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_3");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 3);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_4");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 4);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_5");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 5);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_6");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 6);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_7");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 7);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_8");
			Asm.CLI ();
			Asm.PUSH ((uint) 8);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_9");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 9);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_10");
			Asm.CLI ();
			Asm.PUSH ((uint) 10);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_11");
			Asm.CLI ();
			Asm.PUSH ((uint) 11);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_12");
			Asm.CLI ();
			Asm.PUSH ((uint) 12);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_13");
			Asm.CLI ();
			Asm.PUSH ((uint) 13);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_14");
			Asm.CLI ();
			Asm.PUSH ((uint) 14);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_15");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 15);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_16");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 16);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_17");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 17);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_18");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 18);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_19");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 19);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_20");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 20);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_21");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 21);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_22");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 22);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_23");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 23);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_24");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 24);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_25");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 25);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_26");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 26);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_27");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 27);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_28");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 28);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_29");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 29);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_30");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 30);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_31");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 31);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_32");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 32);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_33");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 33);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_34");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 34);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_35");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 35);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_36");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 36);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_37");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 37);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_38");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 38);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_39");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 39);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_40");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 40);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_41");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 41);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_42");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 42);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_43");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 43);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_44");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 44);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_45");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 45);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_46");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 46);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_47");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 47);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_48");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 48);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_49");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 49);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_50");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 50);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_51");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 51);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_52");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 52);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_53");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 53);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_54");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 54);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_55");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 55);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_56");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 56);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_57");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 57);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_58");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 58);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_59");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 59);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_60");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 60);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_61");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 61);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_62");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 62);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_63");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 63);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_64");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 64);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_65");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 65);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_66");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 66);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_67");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 67);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_68");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 68);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_69");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 69);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_70");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 70);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_71");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 71);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_72");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 72);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_73");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 73);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_74");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 74);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_75");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 75);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_76");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 76);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_77");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 77);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_78");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 78);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_79");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 79);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_80");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 80);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_81");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 81);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_82");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 82);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_83");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 83);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_84");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 84);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_85");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 85);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_86");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 86);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_87");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 87);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_88");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 88);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_89");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 89);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_90");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 90);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_91");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 91);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_92");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 92);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_93");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 93);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_94");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 94);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_95");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 95);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_96");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 96);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_97");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 97);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_98");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 98);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_99");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 99);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_100");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 100);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_101");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 101);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_102");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 102);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_103");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 103);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_104");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 104);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_105");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 105);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_106");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 106);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_107");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 107);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_108");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 108);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_109");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 109);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_110");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 110);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_111");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 111);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_112");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 112);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_113");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 113);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_114");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 114);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_115");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 115);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_116");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 116);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_117");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 117);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_118");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 118);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_119");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 119);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_120");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 120);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_121");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 121);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_122");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 122);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_123");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 123);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_124");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 124);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_125");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 125);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_126");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 126);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_127");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 127);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_128");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 128);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_129");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 129);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_130");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 130);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_131");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 131);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_132");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 132);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_133");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 133);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_134");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 134);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_135");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 135);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_136");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 136);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_137");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 137);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_138");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 138);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_139");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 139);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_140");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 140);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_141");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 141);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_142");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 142);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_143");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 143);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_144");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 144);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_145");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 145);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_146");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 146);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_147");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 147);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_148");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 148);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_149");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 149);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_150");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 150);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_151");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 151);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_152");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 152);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_153");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 153);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_154");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 154);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_155");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 155);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_156");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 156);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_157");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 157);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_158");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 158);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_159");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 159);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_160");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 160);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_161");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 161);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_162");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 162);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_163");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 163);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_164");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 164);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_165");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 165);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_166");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 166);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_167");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 167);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_168");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 168);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_169");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 169);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_170");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 170);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_171");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 171);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_172");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 172);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_173");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 173);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_174");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 174);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_175");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 175);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_176");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 176);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_177");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 177);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_178");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 178);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_179");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 179);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_180");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 180);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_181");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 181);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_182");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 182);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_183");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 183);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_184");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 184);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_185");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 185);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_186");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 186);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_187");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 187);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_188");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 188);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_189");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 189);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_190");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 190);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_191");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 191);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_192");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 192);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_193");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 193);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_194");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 194);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_195");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 195);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_196");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 196);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_197");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 197);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_198");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 198);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_199");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 199);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_200");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 200);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_201");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 201);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_202");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 202);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_203");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 203);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_204");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 204);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_205");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 205);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_206");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 206);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_207");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 207);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_208");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 208);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_209");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 209);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_210");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 210);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_211");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 211);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_212");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 212);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_213");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 213);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_214");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 214);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_215");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 215);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_216");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 216);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_217");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 217);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_218");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 218);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_219");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 219);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_220");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 220);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_221");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 221);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_222");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 222);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_223");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 223);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_224");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 224);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_225");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 225);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_226");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 226);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_227");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 227);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_228");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 228);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_229");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 229);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_230");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 230);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_231");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 231);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_232");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 232);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_233");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 233);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_234");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 234);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_235");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 235);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_236");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 236);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_237");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 237);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_238");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 238);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_239");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 239);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_240");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 240);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_241");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 241);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_242");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 242);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_243");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 243);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_244");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 244);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_245");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 245);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_246");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 246);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_247");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 247);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_248");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 248);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_249");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 249);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_250");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 250);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_251");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 251);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_252");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 252);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_253");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 253);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_254");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 254);
			Asm.JMP ("ISRDispatcher");

			Asm.LABEL ("ISR_255");
			Asm.CLI ();
			Asm.PUSH ((byte) 0);
			Asm.PUSH ((uint) 255);
			Asm.JMP ("ISRDispatcher");
			#endregion

			Asm.LABEL ("ISRDispatcher");
			
			Asm.PUSH (Seg.DS);
			Asm.PUSH (Seg.ES);
			Asm.PUSH (Seg.GS);
			Asm.PUSH (Seg.FS);
			Asm.PUSH (Seg.SS);

			Asm.PUSHAD ();
			
			
			// If we where able to store ESP in a static field, we wouldn't have to use EBX,
			//	which makes it easier to put this before all the pushes (and not polute EBX)
			Asm.MOV (R32.EBX, R32.ESP);

			// Set new stack, it would be better if we could do this before all the pushes above!
			uint temp = IDT_Stack_Top; // hack!
			Asm.MOV (R32.ESP, &temp);
			
			// Push old stack pointer on new stack
			Asm.PUSH (R32.EBX);		// ESP / ISRData*


			// Not necessary yet but perhaps in the future
			Asm.MOV (R16.AX, GDT.DataSelector);
			Asm.MOV (Seg.DS, R16.AX);
			Asm.MOV (Seg.ES, R16.AX);
			Asm.MOV (Seg.FS, R16.AX);
			Asm.MOV (Seg.GS, R16.AX);
			

			// Push the fake stack frame data
			//	get EIP from old stack:
			Asm.PUSH (new DWordMemory (null, R32.EBX, null, 0, 15 * 4));	// FrameIP
			Asm.PUSH (R32.EBP);												// FrameBP
			Asm.MOV (R32.EBP, R32.ESP);

			// Get the index of the interrupt and read the address of the handler
			// 13 is the position on the old stack
			Asm.MOVZX (R32.EAX, new ByteMemory (null, R32.EBX, null, 0, 13 * 4));
			Asm.SHL (R32.EAX, 2);
			Asm.MOV (R32.EDX, IDT_TABLE);
			Asm.MOV (R32.EAX, new DWordMemory (null, R32.EAX, R32.EDX, 0, 0));
			Asm.CALL (R32.EAX);
			
			Asm.CALL (IRQ_CLEAN_UP);

			// Clean the fake stack frame data
			Asm.POP (R32.EAX);		// EIP
			Asm.POP (R32.EAX);		// EBP

			// Clean function parameter + set old stack back
			Asm.POP (R32.ESP);		// ESP / ISRData*


			Asm.POPAD ();
			
			Asm.POP (Seg.SS);
			Asm.POP (Seg.FS);
			Asm.POP (Seg.GS);
			Asm.POP (Seg.ES);
			Asm.POP (Seg.DS);

			Asm.ADD (R32.ESP, 0x08);
			Asm.STI ();
			Asm.IRETD ();
		}
		#endregion
	}
}
