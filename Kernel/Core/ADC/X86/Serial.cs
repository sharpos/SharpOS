//
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Ásgeir Halldórsson <asgeir.halldorsson@gmail.com>
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//  Cédric Rousseau <cedrou@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

//#define LOG_IO

using SharpOS.Kernel.Foundation;
using System;
namespace SharpOS.Kernel.ADC.X86 {

	public class Serial
	{
		static ADC.SerialPort com1;
		static ADC.SerialPort com2;
		static ADC.SerialPort com3;
		static ADC.SerialPort com4;

		public static void Setup ()
		{
			com1 = new SerialPort (1);
			IDT.RegisterIRQ (IDT.Interrupt.COM1Default, Stubs.GetFunctionPointer (COM1_HANDLER));

			com2 = new SerialPort (2);
			IDT.RegisterIRQ (IDT.Interrupt.COM2Default, Stubs.GetFunctionPointer (COM2_HANDLER));

			com3 = new SerialPort (3);
			IDT.RegisterIRQ (IDT.Interrupt.COM3User, Stubs.GetFunctionPointer (COM3_HANDLER));

			com4 = new SerialPort (4);
			IDT.RegisterIRQ (IDT.Interrupt.COM4User, Stubs.GetFunctionPointer (COM4_HANDLER));

			initialized = true;
		}

		private static bool initialized = false;
		public static bool Initialized { get { return initialized; } }

		public static ADC.SerialPort COM1 { get { if (initialized) return com1; else return null; } }
		public static ADC.SerialPort COM2 { get { if (initialized) return com2; else return null; } }
		public static ADC.SerialPort COM3 { get { if (initialized) return com3; else return null; } }
		public static ADC.SerialPort COM4 { get { if (initialized) return com4; else return null; } }

		#region Interrupts handlers

		private const string COM1_HANDLER = "COM1_HANDLER";
		private const string COM2_HANDLER = "COM2_HANDLER";
		private const string COM3_HANDLER = "COM3_HANDLER";
		private const string COM4_HANDLER = "COM4_HANDLER";

		[SharpOS.AOT.Attributes.Label (COM1_HANDLER)]
		unsafe static void COM1Handler (IDT.ISRData data)
		{
			if (initialized)
				com1.OnDataReceived ();
		}

		[SharpOS.AOT.Attributes.Label (COM2_HANDLER)]
		unsafe static void COM2Handler (IDT.ISRData data)
		{
			if (initialized)
				com2.OnDataReceived ();
		}

		[SharpOS.AOT.Attributes.Label (COM3_HANDLER)]
		unsafe static void COM3Handler (IDT.ISRData data)
		{
			if (initialized)
				com3.OnDataReceived ();
		}

		[SharpOS.AOT.Attributes.Label (COM4_HANDLER)]
		unsafe static void COM4Handler (IDT.ISRData data)
		{
			if (initialized)
				com4.OnDataReceived ();
		}

		#endregion
	}

	public unsafe class SerialPort : ADC.SerialPort
	{
		#region Private fields

		private static ushort* PortBase = (ushort*)Stubs.StaticAlloc (5 * sizeof (ushort));

		uint _port;

		#endregion

		#region Helpers

		// In the following helpers, port is 1-based

		// Receive Buffer Register (read only)
		private IO.Port RBRBase { get { return (IO.Port)(PortBase [_port]); } }
		// Transmitter Holding Register (write only)
		private IO.Port THRBase { get { return (IO.Port)(PortBase [_port]); } }
		// Interrupt Enable Register
		private IO.Port IERBase { get { return (IO.Port)(PortBase [_port] + 1); } }
		// Divisor Latch (LSB and MSB)
		private IO.Port DLLBase { get { return (IO.Port)(PortBase [_port]); } }
		private IO.Port DLMBase { get { return (IO.Port)(PortBase [_port] + 1); } }
		// Interrupt Identification Register (read only)
		private IO.Port IIRBase { get { return (IO.Port)(PortBase [_port] + 2); } }
		// FIFO Control Register (write only, 16550+ only)
		private IO.Port FCRBase { get  { return (IO.Port)(PortBase [_port] + 2); } }
		// Line Control Register
		private IO.Port LCRBase { get { return (IO.Port)(PortBase [_port] + 3); } }
		// Modem Control Register
		private IO.Port MCRBase { get { return (IO.Port)(PortBase [_port] + 4); } }
		// Line Status Register
		private IO.Port LSRBase { get { return (IO.Port)(PortBase [_port] + 5); } }
		// Modem Status Register
		private IO.Port MSRBase { get { return (IO.Port)(PortBase [_port] + 6); } }
		// Scratch Register (16450+ and some 8250s, special use with some boards)
		private IO.Port SCRBase { get { return (IO.Port)(PortBase [_port] + 7); } }

		[Flags]
		private enum IER : byte
		{
			DR = 0x01, // Data ready, it is generated if data waits to be read by the CPU.
			THRE = 0x02, // THR Empty, this interrupt tells the CPU to write characters to the THR.
			SI = 0x04, // Status interrupt. It informs the CPU of occurred transmission errors during reception.
			MSI = 0x08 // Modem status interrupt. It is triggered whenever one of the delta-bits is set (see MSR).
		}

		[Flags]
		private enum FCR : byte
		{
			Enabled = 0x01, // FIFO enable.
			CLR_RCVR = 0x02, // Clear receiver FIFO. This bit is self-clearing.
			CLR_XMIT = 0x04, // Clear transmitter FIFO. This bit is self-clearing.
			DMA = 0x08, // DMA mode
			// Receiver FIFO trigger level
			TL1 = 0x00,
			TL4 = 0x40,
			TL8 = 0x80,
			TL14 = 0xC0,
		}

		[Flags]
		private enum LCR : byte
		{
			// Word length
			CS5 = 0x00, // 5bits
			CS6 = 0x01, // 6bits
			CS7 = 0x02, // 7bits
			CS8 = 0x03, // 8bits
			// Stop bit
			ST1 = 0x00, // 1
			ST2 = 0x04, // 2
			// Parity
			PNO = 0x00, // None
			POD = 0x08, // Odd
			PEV = 0x18, // Even
			PMK = 0x28, // Mark 
			PSP = 0x38, // Space

			BRK = 0x40,
			DLAB = 0x80,
		}

		[Flags]
		private enum MCR : byte
		{
			DTR = 0x01,
			RTS = 0x02,
			OUT1 = 0x04,
			OUT2 = 0x08,
			LOOP = 0x10,
		}

		[Flags]
		private enum LSR : byte
		{
			DR = 0x01, // Data Ready. Reset by reading RBR (but only if the RX FIFO is empty, 16550+).
			OE = 0x02, // Overrun Error. Reset by reading LSR. Indicates loss of data.
			PE = 0x04, // Parity Error. Indicates transmission error. Reset by LSR.
			FE = 0x08, // Framing Error. Indicates missing stop bit. Reset by LSR.
			BI = 0x10, // Break Indicator. Set if RxD is 'space' for more than 1 word ('break'). Reset by reading LSR.
			THRE = 0x20, // Transmitter Holding Register Empty. Indicates that a new word can be written to THR. Reset by writing THR.
			TEMT = 0x40, // Transmitter Empty. Indicates that no transmission is running. Reset by reading LSR.
		}

		[Flags]
		private enum MSR : byte
		{
			DCTS = 0x01,
			DDSR = 0x02,
			DRI = 0x04,
			DDCD = 0x08,
			CTS = 0x10,
			DSR = 0x20,
			RI = 0x40,
			DCD = 0x80
		}

		#endregion

		#region ADC implementation

		public SerialPort (uint oneBasedPort)
		{
			PortBase[1] = 0x03F8;
			PortBase[2] = 0x02F8;
			PortBase[3] = 0x03E8;
			PortBase[4] = 0x02E8;

			_port = oneBasedPort;

			// Disable all UART interrupts
			IO.WriteByte (IERBase, 0x00);          

			// Enable DLAB (set baud rate divisor)
			IO.WriteByte (LCRBase, (byte)LCR.DLAB);           
   
			// Set Baud rate
			int baudRate = 115200;
			int divisor = 115200 / baudRate;
			IO.WriteByte (DLLBase, (byte)(divisor & 0xFF));
			IO.WriteByte (DLMBase, (byte)(divisor>>8 & 0xFF));

			// Reset DLAB, Set 8 bits, no parity, one stop bit
			IO.WriteByte (LCRBase, (byte)(LCR.CS8 | LCR.ST1 | LCR.PNO));

			// Enable FIFO, clear them, with 14-byte threshold
			IO.WriteByte (FCRBase, (byte)(FCR.Enabled |FCR.CLR_RCVR|FCR.CLR_XMIT|FCR.TL14));

			// IRQs enabled, RTS/DSR set
			IO.WriteByte (MCRBase, (byte)(MCR.DTR | MCR.RTS | MCR.OUT2));     
      
			// Interrupt when data received
			IO.WriteByte (IERBase, (byte)IER.DR);
		}

		#region Write

		private bool CanTransmit ()
		{
			return (IO.ReadByte (LSRBase) & (byte)LSR.THRE) != 0;
		}

		public override void Write (byte ch)
		{
			while (!CanTransmit ())
				;

#if LOG_IO
			ADC.TextMode.Write ("<- 0x");
			ADC.TextMode.WriteByte (ch);
			ADC.TextMode.Write (" (");
			ADC.TextMode.WriteChar (ch);
			ADC.TextMode.WriteLine (")");
#endif

			IO.WriteByte (THRBase, ch);
		}

		#endregion

		#region Read

		private bool CanRead ()
		{
			return (IO.ReadByte (LSRBase) & (byte)LSR.DR) != 0;
		}

		public override byte Read ()
		{
			while (!CanRead ())
				;

			byte b = IO.ReadByte (RBRBase);
#if LOG_IO
			ADC.TextMode.Write ("-> 0x");
			ADC.TextMode.WriteByte (b);
			ADC.TextMode.Write (" (");
			ADC.TextMode.WriteChar (b);
			ADC.TextMode.WriteLine (")");
#endif

			return b;
		}

		#endregion

		#region Events

		public override void DisableDataReceivedInterrupt ()
		{
			IER ier = (IER)IO.ReadByte (IERBase);
			ier &= ~IER.DR;
			IO.WriteByte (IERBase, (byte)ier);
		}

		public override void EnableDataReceivedInterrupt ()
		{
			byte ier = IO.ReadByte (IERBase);
			ier |= (byte)IER.DR;
			IO.WriteByte (IERBase, ier);
		}

		#endregion

		#endregion
	}
}
