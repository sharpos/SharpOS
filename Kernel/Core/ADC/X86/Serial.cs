//
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Ásgeir Halldórsson <asgeir.halldorsson@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.Kernel.Foundation;
namespace SharpOS.Kernel.ADC.X86 {
	public class Serial {
		public static void Setup ()
		{
			IO.Out8 (IO.Port.UART_Interrupt_Enable_Register, 0x00);				// Disable all interrupts
			IO.Out8 (IO.Port.UART_Line_Control_Register, 0x80);					// Enable DLAB (set baud rate divisor)
			IO.Out8 (IO.Port.UART_Transmit_Receive_Buffer, 0x03);				// Set divisor to 3 (lo byte) 38400 baud
			IO.Out8 (IO.Port.UART_Interrupt_Enable_Register, 0x00);				//                  (hi byte)
			IO.Out8 (IO.Port.UART_Line_Control_Register, 0x03);					// 8 bits, no parity, one stop bit
			IO.Out8 (IO.Port.UART_Interrupt_Identification_Register, 0xC7);		// Enable FIFO, clear them, with 14-byte threshold
			IO.Out8 (IO.Port.UART_Modem_Control_Register, 0x0B);					// IRQs enabled, RTS/DSR set
		}

		private static bool CanTransmit ()
		{
			return (IO.In8 (IO.Port.UART_Modem_Status_Register) & 0x20) != 0;
		}


		public static void WriteChar (byte ch)
		{
			while (!CanTransmit ())
				continue;

			IO.Out8 (IO.Port.UART_Transmit_Receive_Buffer, ch);
		}

		public unsafe static void Write (byte* str)
		{
			int strLength = ByteString.Length (str);
			for (int x = 0; x < strLength; x++) {
				while (!CanTransmit ())
					continue;
				IO.Out8 (IO.Port.UART_Transmit_Receive_Buffer, str [x]);
			}
		}

		public static void Write (string str)
		{
			int strLength = str.Length;
			for (int x = 0; x < strLength; x++) {
				while (!CanTransmit ())
					continue;
				IO.Out8 (IO.Port.UART_Transmit_Receive_Buffer, (byte) str [x]);
			}
		}
	}
}