// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//	Sander van Rossen <sander.vanrossen@gmail.com>
//	Bruce Markham <illuminus86@gmail.com>
//  Phil Garcia (aka tgiphil) <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using SharpOS.Kernel;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;

namespace SharpOS.Kernel.ADC.X86 {
	public class IO {
		#region Ports
		public enum Port : ushort {
			#region 0000-001F - Primary Direct Memory Access (DMA) Controller
			DMA_Channel0Address						= 0x0000, // read/write
			DMA_Channel0Count						= 0x0001, // read/write
            
			DMA_Channel1Address						= 0x0002, // read/write
			DMA_Channel1Count						= 0x0003, // read/write
            
			DMA_Channel2Address						= 0x0004, // read/write
			DMA_Channel2Count						= 0x0005, // read/write
			
			DMA_Channel3Address						= 0x0006, // read/write
			DMA_Channel3Count						= 0x0007, // read/write
			
			/// <summary>
			/// 0008 r DMA channel 0-3 status register
			///		bit 7 = 1 channel 3 request
			///		bit 6 = 1 channel 2 request
			///		bit 5 = 1 channel 1 request
			///		bit 4 = 1 channel 0 request
			///		bit 3 = 1 channel terminal count on channel 3
			///		bit 2 = 1 channel terminal count on channel 2
			///		bit 1 = 1 channel terminal count on channel 1
			///		bit 0 = 1 channel terminal count on channel 0
			///	</summary>
			DMA_StatusRegister						= 0x0008, // read
			/// <summary>
			/// 0008 w DMA channel 0-3 command register
			///		bit 7	= 1 DACK sense active high
			///				= 0 DACK sense active low
			///		bit 6	= 1 DREQ sense active high
			///				= 0 DREQ sense active low
			///		bit 5	= 1 extended write selection
			///				= 0 late write selection
			///		bit 4	= 1 rotating priority
			///				= 0 fixed priority
			///		bit 3	= 1 compressed timing
			///				= 0 normal timing
			///		bit 2	= 1 enable controller
			///				= 0 enable memory-to-memory
			///	</summary>
			DMA_CommandRegister						= 0x0008, // write
			DMA_WriteRequestRegister				= 0x0009, // write
			///	<summary>
			///	000A r/w DMA channel 0-3 mask register
			///		bit 7-3	= 0 reserved
			///		bit 2	= 0 clear mask bit
			///				= 1 set mask bit
			///		bit 1-0	= 00 channel 0 select
			///				= 01 channel 1 select
			///				= 10 channel 2 select
			///				= 11 channel 3 select
			/// </summary>
			DMA_ChannelMaskRegister					= 0x000A,
            ///	<summary>
			///	000B w DMA channel 0-3 mode register
			///		bit 7-6	= 00 demand mode
			///				= 01 single mode
			///				= 10 block mode
			///				= 11 cascade mode
			///		bit 5	= 0 address increment select
			///				= 1 address decrement select
			///		bit 3-2	= 00 verify operation
			///				= 01 write to memory
			///				= 10 read from memory
			///				= 11 reserved
			///		bit 1-0	= 00 channel 0 select
			///				= 01 channel 1 select
			///				= 10 channel 2 select
			///				= 11 channel 3 select
			/// </summary>
			DMA_ModeRegister						= 0x000B,
			DMA_ClearBytePointerFlipFlop			= 0x000C, // write
			DMA_ReadTemporaryRegister				= 0x000D, // read
			DMA_MasterClear							= 0x000D, // write
			DMA_ClearMaskRegister					= 0x000E, // write
			DMA_WriteMaskRegister					= 0x000F, // write
			#endregion

			#region 0020-0021 - Programmable Interrupt (PIC) Controller
			Master_PIC_CommandPort					= 0x0020,
			Master_PIC_DataPort						= 0x0021,
			#endregion

			//0022-002B - Intel 82355, part of chipset for 386sx
			//0022-0023 - Chip Set Data
			//0022-0023 - Cyrix Cx486SLC/DLC processor Cache Configuration Registers
			//0026-0027 - Power Management

			#region 0040-005F - Primary Programmable Interrupt Timer (PIT) Controller
			PIT_counter_0_counter_divisor		= 0x0040, // read/write
			PIT_counter_1_RAM_refresh_counter	= 0x0041, // read/write
			PIT_counter_2_cassette_and_speaker	= 0x0042, // read/write
			#region 0x0043 PIT_mode_control_port
			/// <summary>
			/// 0043 r/w PIT mode port, control word register for counters 0-2
			/// <list>
			///		<item>bit 7-6	= 00 counter 0 select
			///						= 01 counter 1 select (not PS/2)
			///						= 10 counter 2 select</item>
			///		<item>bit 5-4	= 00 counter latch command
			///						= 01 read/write counter bits 0-7 only
			///						= 10 read/write counter bits 8-15 only
			///						= 11 read/write counter bits 0-7 first, then 8-15</item>
			///		<item>bit 3-1	= 000 mode 0 select
			///						= 001 mode 1 select - programmable one shot
			///						= x10 mode 2 select - rate generator
			///						= x11 mode 3 select - square wave generator
			///						= 100 mode 4 select - software triggered strobe
			///						= 101 mode 5 select - hardware triggered strobe</item>
			///		<item>bit 0		= 0 binary counter 16 bits
			///						= 1 BCD counter</item>
			/// </list>
			/// </summary>
			PIT_mode_control_port				= 0x0043, // read/write
			#endregion
			PIT_counter_3						= 0x0044, // read/write
			PIT_counter_3_control_port			= 0x0047, // read/write
			#endregion

			#region 0060-006F - Keyboard Controller

			#region 0x0060 KB_data_port (read/write)
			/// <summary>
			/// KB controller data port or keyboard input buffer (ISA, EISA)
			///		should only be read from after status port bit0 = 1
			///		should only be written to if status port bit1 = 0
			///		keyboard commands (data also goes to port 0060):
			///	<list>
			///		<item>E6 sngl set mouse scaling to 1:1</item>
			///		<item>E7 sngl set mouse scaling to 2:1</item>
			///		<item>E8 dbl set mouse resolution 
			///			(00h = 1/mm,01h = 2/mm,02h = 4/mm,03h = 8/mm)</item>
			///		<item>E9 sngl get mouse information
			///			read two status bytes:
			///			<list>
			///				<item>byte 0
			///					<list>
			///						<item>bit 7 unused</item>
			///						<item>bit 6 remote rather than stream mode</item>
			///						<item>bit 5 mouse enabled</item>
			///						<item>bit 4 scaling set to 2:1</item>
			///						<item>bit 3 unused</item>
			///						<item>bit 2 left button pressed</item>
			///						<item>bit 1 unused</item>
			///						<item>bit 0 right button pressed</item>
			///					</list></item>
			///				<item>byte 1: resolution</item>
			///			</list>
			///		</item>
			///		<item>ED dbl set/reset mode indicators Caps Num Scrl
			///			bit 2 = CapsLk, bit 1 = NumLk, bit 0 = ScrlLk</item>
			///		<item>EE sngl diagnostic echo. returns EE.</item>
			///		<item>EF sngl NOP (No OPeration). reserved for future use</item>
			///		<item>F0 dbl get/set scan code set
			///			<list>
			///				<item>00h get current set</item>
			///				<item>01h scancode set 1 (except Type 2 ctrlr)</item>
			///				<item>02h scancode set 2 (default)</item>
			///				<item>03h scancode set 3</item>
			///			</list>
			///		</item>
			///		<item>F2 sngl read keyboard ID (read two ID bytes)</item>
			///		<item>F2 sngl read mouse ID (read two ID bytes)</item>
			///		<item>F3 dbl set typematic rate/delay</item>
			///		<item>F3 dbl set mouse sample rate in reports per second</item>
			///		<item>F4 sngl enable keyboard</item>
			///		<item>F4 sngl enable mouse</item>
			///		<item>F5 sngl disable keyboard. set default parameters</item>
			///		<item>F5 sngl disable mouse, set default parameters</item>
			///		<item>F6 sngl set default parameters</item>
			///		<item>F7 sngl [MCA] set all keys to typematic (scancode set 3)</item>
			///		<item>F8 sngl [MCA] set all keys to make/release</item>
			///		<item>F9 sngl [MCA] set all keys to make only</item>
			///		<item>FA sngl [MCA] set all keys to typematic/make/release</item>
			///		<item>FB sngl [MCA] set al keys to typematic</item>
			///		<item>FC dbl [MCA] set specific key to make/release</item>
			///		<item>FD dbl [MCA] set specific key to make only</item>
			///		<item>FE sngl resend last scancode</item>
			///		<item>FF sngl perform internal power-on reset function</item>
			///		<item>FF sngl reset mouse</item>
			///		</list>
			///		Note: must issue command D4h to port 64h first to access
			///		mouse functions
			/// </summary>
			KB_data_port						= 0x0060,
			#endregion

			//0060 r KeyBoard or KB controller data output buffer (via PPI on XT)
			//0061 w KB controller port B (ISA, EISA) (PS/2 port A is at 0092)
			//0061 r KB controller port B control register (ISA, EISA)
			//0061 w PPI Programmable Peripheral Interface 8255 (XT only)
			//0062 r/w PPI (XT only)
			//0063 r/w PPI (XT only) command mode register (read dipswitches)
			
			#region 0x0064 KB_controller_read_status (read)
			///<summary>
			///KB controller read status (ISA, EISA)
			/// <list>
			///		<item>bit 7 = 1 parity error on transmission from keyboard</item>
			///		<item>bit 6 = 1 receive timeout</item>
			///		<item>bit 5 = 1 transmit timeout</item>
			///		<item>bit 4 = 0 keyboard inhibit</item>
			///		<item>bit 3 = 1 data in input register is command
			///					  0 data in input register is data</item>
			///		<item>bit 2 system flag status: 0=power up or reset 1=selftest OK</item>
			///		<item>bit 1 = 1 input buffer full (input 60/64 has data for 8042)</item>
			///		<item>bit 0 = 1 output buffer full (output 60 has data for system)</item>
			/// </list>
			/// KB controller read status (MCA)
			/// <list>
			///		<item>bit 7 = 1 parity error on transmission from keyboard</item>
			///		<item>bit 6 = 1 general timeout</item>
			///		<item>bit 5 = 1 mouse output buffer full</item>
			///		<item>bit 4 = 0 keyboard inhibit</item>
			///		<item>bit 3 = 1 data in input register is command
			///					  0 data in input register is data</item>
			///		<item>bit 2 system flag status: 0=power up or reset 1=selftest OK</item>
			///		<item>bit 1 = 1 input buffer full (input 60/64 has data for 804x)</item>
			///		<item>bit 0 = 1 output buffer full (output 60 has data for system)</item>
			/// </list>
			///	KB controller read status by Compaq
			/// <list>
			///		<item>bit 7 = 1 parity error detected (11-bit format only). If an
			///						error is detected, a Resend command is sent to the
			///						keyboard once only, as an attempt to recover.</item>
			///		<item>bit 6 = 1 receive timeout. transmission didnt finish in 2mS.</item>
			///		<item>bit 5 = 1 transmission timeout error</item>
			///		<item>bit 5,6,7 cause
			///				  1 0 0 No clock
			///				  1 1 0 Clock OK, no response
			///				  1 0 1 Clock OK, parity error</item>
			///		<item>bit 4 = 0 security lock engaged</item>
			///		<item>bit 3 = 1 data in OUTPUT register is command
			///					  0 data in OUTPUT register is data</item>
			///		<item>bit 2 system flag status: 0=power up or reset 1=soft</item>
			/// </list>
			///</summary>
			KB_controller_read_status			= 0x0064,
			#endregion

			#region 0x0064 KB_controller_commands (write)
			/// <summary>
			/// 0064 w KB controller input buffer (ISA, EISA)
			/// KB controller commands (data goes to port 0060):
			/// <list>
			///		<item>20		read	read byte zero of internal RAM, this is the
			///						last KB command send to 804x
			///						Compaq	Put current command byte on port 0060
			///						command structure:
			///						<list>
			///							<item>bit 7 reserved</item>
			///							<item>bit 6 = 1 convert KB codes to 8086 scan codes</item>
			///							<item>bit 5 = 0 use 11-bit codes, 1=use 8086 codes</item>
			///							<item>bit 4 = 0 enable keyboard, 1=disable keyboard</item>
			///							<item>bit 3 = 1 ignore security lock state</item>
			///							<item>bit 2 this bit goes into bit2 status reg.</item>
			///							<item>bit 1 = 0 reserved</item>
			///							<item>bit 0 = 1 generate int. when output buffer full</item>
			///						</list>
			///		</item>
			///		<item>21-3F	read	reads the byte specified in the lower 5 bits of
			///						the command in the 804x's internal RAM</item>
			///		<item>60-7F	dbl		writes the data byte to the address specified in
			///						the 5 lower bits of the command.
			///						Alternate description KB IO command 60 summary:
			///						<list>
			///							<item>bit7 = 0 reserved</item>
			///							<item>bit6 = IBM PC compatibility mode</item>
			///							<item>bit5 = IBM PC mode</item>
			///							<item>bit4 = disable kb</item>
			///							<item>bit3 = inhibit override</item>
			///							<item>bit2 = system flag</item>
			///							<item>bit1 = 0 reserved</item>
			///							<item>bit0 = enableoutput buffer full interrupt</item>
			///						</list>
			///		</item>
			///		<item>60		Compaq	Load new command (60 to [64], command to [60])</item>
			///		<item>A1		Compaq	unknown speedfunction ??</item>
			///		<item>A2		Compaq	unknown speedfunction ??</item>
			///		<item>A3		Compaq	Enable system speed control</item>
			///		<item>A4		MCA		check if password installed</item>
			///		<item>A4		Compaq	Toggle speed</item>
			///		<item>A5		MCA		load password</item>
			///		<item>A5		Compaq	Special reed. the 8042 places the real values
			///						of port 2 except for bits 4 and 5 wich are given
			///						a new definition in the output buffer. No output
			///						buffer full is generated.
			///						<list>
			///							<item>if bit 5 = 0, a 9-bit keyboard is in use</item>
			///							<item>if bit 5 = 1, an 11-bit keyboard is in use</item>
			///							<item>if bit 4 = 0, outp-buff-full interrupt disabled</item>
			///							<item>if bit 4 = 1, output-buffer-full int. enabled</item>
			///						</list>
			///		</item>
			///		<item>A6		MCA		check password</item>
			///		<item>A6		Compaq	unknown speedfunction ??</item>
			///		<item>A7		MCA		disable mouse port</item>
			///		<item>A8		MCA		enable mouse port</item>
			///		<item>A9		MCA		test mouse port</item>
			///		<item>AA		sngl	initiate self-test. will return 55 to data port
			///						Compaq	Initializes ports 1 and 2, disables the keyboard
			///						and clears the buffer pointers. It then places
			///						55 in the output buffer.</item>
			/// </list>
			/// </summary>
			KB_controller_commands				= 0x0064,
			#endregion

			#endregion

            #region 03F2-03F5 - Primary Floppy Disk Controller
            FDC_DORPort                         = 0x03F2,
            FDC_StatusPort                      = 0x03F4,
            FDC_CommandPort                     = 0x03F4,
            FDC_DataPort                        = 0x03F5,
			FDC_Config							= 0x03F7,
            #endregion

			#region 0372-0375 - Secondary Floppy Disk Controller
			SDC_DORPort = 0x0372,
			SDC_StatusPort = 0x0374,
			SDC_CommandPort = 0x0374,
			SDC_DataPort = 0x0375,
			#endregion

            #region 0070-007F - CMOS RAM / Real Time Clock
            RTC_CommandPort						= 0x0070,
			RTC_DataPort						= 0x0071,
			#endregion

			#region 0080-008F - DMA page registers
			DMA_TemporaryStorage				= 0x0080, // read/write extra page register (temporary storage)
			DMA_Channel2Page					= 0x0081, // read/write
			DMA_Channel3Page					= 0x0082, // read/write
			DMA_Channel1Page					= 0x0083, // read/write
			//0084 r/w extra page register
			//0085 r/w extra page register
			//0086 r/w extra page register
			DMA_Channel0Page					= 0x0087, // read/write
			//0088 r/w extra page register
			DMA_Channel6Page					= 0x0089, // read/write
			DMA_Channel7Page					= 0x008A, // read/write
			DMA_Channel5Page					= 0x008B, // read/write
			//008C r/w extra page register
			//008D r/w extra page register
			//008E r/w extra page register
			DMA_RefreshPageRegister				= 0x008F, // read/write
			#endregion

			#region 00A0-00AF - Secondary Programmable Interrupt Controller (PIC) Controller
			Slave_PIC_CommandPort				= 0x00A0,
			Slave_PIC_DataPort					= 0x00A1,
			#endregion
			//00C0-00DF - Secondary Direct Memory Access (DMA) Controller
			//00F0-00FF - coprocessor (8087..80387)
			//0130-0133 - Adaptec 154xB/154xC SCSI adapter
			//0134-0137 - Adaptec 154xB/154xC SCSI adapter.
			//0140-014F - SCSI (alternate Small Computer System Interface) adapter
			//0178-0179 - Power Management
			//0200-020F - Game port reserved I/O address space
			//0200-0207 - Game port, eight identical addresses on some boards
			//0220-0223 - Sound Blaster / Adlib port
			//0220-0227 - Soundblaster PRO and SSB 16 ASP
			//0220-022F - Soundblaster PRO 2.0
			//0220-022F - Soundblaster PRO 4.0
			//0230-0233 - Adaptec 154xB/154xC SCSI adapter.
			//0234-0237 - Adaptec 154xB/154xC SCSI adapter.
			//0278-027E - parallel printer port
			//02B0-02DF - alternate EGA, primary EGA at 03C0
			//02E8-02EF - serial port, same as 02F8, 03E8 and 03F8
			//02E8-02EF - 8514/A and compatible video cards (e.g. ATI Graphics Ultra)
			//02F8-02FF - serial port, same as 02E8, 03E8 and 03F8
			//0300-0301 - Soundblaster 16 ASP MPU-Midi
			//0300-031F - prototype cards
			//0330-0331 - MIDI interface
			//0330-0333 - Adaptec 154xB/154xC SCSI adapter
			//0334-0337 - Adaptec 154xB/154xC SCSI adapter
			//0370-0377 - Secondary Floppy Disk Controller
			//0378-037A - parallel printer port, same as 0278 and 03BC
			//0388-0389 - Sound Blaster / Adlib port
			//0388-0389 - Soundblaster PRO FM-Chip
			//0388-038B - Soundblaster 16 ASP FM-Chip

			#region 03B0-03BF - Monochrome Display Adapter (MDA)
			//03B0 same as 03B4
			//03B1 same as 03B5
			//03B2 same as 03B4
			//03B3 same as 03B5

			///<summary>
			///03B4 w MDA CRT index register (EGA/VGA)
			///		selects which register (0-11h) is to be accessed through 3B5
			///</summary>
			MDA_CRT_index_register				= 0x03B4,

			///<summary>
			///03B5 r/w MDA CRT data register (EGA/VGA)
			///		selected by port 3B4. registers C-F may be read
			/// <list>
			///		<item>00 horizontal total</item>
			///		<item>01 horizontal displayed</item>
			///		<item>02 horizontal sync position</item>
			///		<item>03 horizontal sync pulse width</item>
			///		<item>04 vertical total</item>
			///		<item>05 vertical displayed</item>
			///		<item>06 vertical sync position</item>
			///		<item>07 vertical sunc pulse width</item>
			///		<item>08 interlace mode</item>
			///		<item>09 maximum scan lines</item>
			///		<item>0A cursor start</item>
			///		<item>0B cursor end</item>
			///		<item>0C start address high</item>
			///		<item>0D start address low</item>
			///		<item>0E cursor location high</item>
			///		<item>0F cursor location low</item>
			///		<item>10 light pen high</item>
			///		<item>11 light pen low</item>
			/// </list>
			///</summary>
			MDA_CRT_data_register				= 0x03B5,

			//03B6 same as 03B4
			//03B7 same as 03B5

			///<summary>
			///03B8 r/w MDA mode control register
			/// <list>
			///		<item>bit 7 not used</item>
			///		<item>bit 6 not used</item>
			///		<item>bit 5 enable blink</item>
			///		<item>bit 4 not used</item>
			///		<item>bit 3 video enable</item>
			///		<item>bit 2 not used</item>
			///		<item>bit 1 not used</item>
			///		<item>bit 0 high resolution mode</item>
			/// </list>
			///</summary>
			MDA_mode_control_register			= 0x03B8,
			
			//03B9 reserved for color select register on color adapter
			
			///<summary>
			///03BA r CRT status register EGA/VGA: input status 1 register
			/// <list>
			///		<item>bit 7 (MSD says) if this bit changes within 8000h reads then</item>
			///		<item>bit 6-4 = 000 = adapter is Hercules or compatible</item>
			///		<item>001 = adapter is Hercules+</item>
			///		<item>101 = adapter is Hercules InColor</item>
			///		<item>else: adapter is unknown</item>
			///		<item>bit 3 black/white video</item>
			///		<item>bit 2-1 reserved</item>
			///		<item>bit 0 horizontal drive</item>
			/// </list>
			///</summary>
			MDA_CRT_status_register				= 0x03B8,
			
			//03BA w EGA/VGA feature control register
			//03BB reserved for light pen strobe reset
			#endregion

			//03BC-03BF - parallel printer port, same as 0278 and 0378
			#region 03C0-03CF - Primary Enhanced Graphics Adapter
			//03C0	(r)/w	EGA VGA ATC index/data register
			//03C1	r		VGA other attribute register
			//03C2	r		EGA VGA input status 0 register
			//		w		VGA miscellaneous output register
			EGA_input_status_0_register			= 0x03C2,
			VGA_input_status_0_register			= 0x03C2,
			//03C3	r/w		VGA video subsystem enable (see also port 46E8h)
			//				for IBM, motherboard VGA only
			//03C4	w		EGA TS index register
			//		r/w		VGA sequencer index register
			//03C5	w		EGA TS data register
			//		r/w		VGA other sequencer register
			//03C6	r/w		VGA PEL mask register
			//03C7	r/w		VGA PEL address read mode
			//		r		VGA DAC state register
			//03C8	r/w		VGA PEL address write mode
			//03C9	r/w		VGA PEL data register
			//03CA	w		EGA graphics 2 position register
			//		r		VGA feature control register
			//03CC	w		EGA graphics 1 position register
			//		r		VGA miscellaneous output register
			EGA_graphics_1_position_register	= 0x03CC,
			VGA_miscellaneous_output_register	= 0x03CC,
			//03CE	w		EGA GDC index register
			//		r/w		VGA graphics address register
			//03CF	w		EGA GDC data register
			//		r/w		VGA other graphics register
			#endregion
			
			#region 03D0-03DF - CGA (Color Graphics Adapter)
			//03D0 same as 03D4
			//03D1 same as 03D5
			//03D2 same as 03D4
			//03D3 same as 03D5
			//03D4 w CRT (6845) index register (EGA/VGA)
			//	selects which register (0-11h) is to be accessed through 3B5
			CGA_CRT_index_register				= 0x03D4,
			//03D5 w CRT (6845) data register (EGA/VGA)
			//	selected by port 3B4. registers C-F may be read
			//	(for registers see at 3B5)
			CGA_CRT_data_register				= 0x03D5,
			//03D6 same as 03D4
			//03D7 same as 03D5
			//03D8 r/w CGA mode control register (except PCjr)
			//		bit 7-6 not used
			//		bit 5	= 1 blink enabled
			//		bit 4	= 1 640*200 graphics mode
			//		bit 3	= 1 video enabled
			//		bit 2	= 1 monochrome signal
			//		bit 1	= 0 text mode
			//				= 1 320*200 graphics mode
			//		bit 0	= 0 40*25 text mode
			//				= 1 80*25 text mode
			CGA_CRT_mode_control_register		= 0x03D8,
			//03D9 r/w CGA palette register
			//		bit 7-6 not used
			//		bit 5	= 0 active color set: red, green brown
			//				= 1 active color set: cyan, magenta, white
			//		bit 4	intense colors in graphics, background colors text
			//		bit 3	intense border in 40*25, intense background in
			//				320*200, intense foreground in 640*200
			//		bit 2	red border in 40*25, red background in 320*200,
			//				red foreground in 640*200
			//		bit 1	green border in 40*25, green background in
			//				320*200, green foreground in 640*200
			//		bit 0	blue border in 40*25, blue background in 320*200,
			//		blue foreground in 640*200
			CGA_CRT_palette_register			= 0x03D9,
			//03DA r CGA status register EGA/VGA: input status 1 register
			//		bit 7-4 not used
			//		bit 3	= 1 in vertical retrace
			//		bit 2	= 1 light pen switch is off
			//		bit 1	= 1 positive edge from light pen has set trigger
			//		bit 0	= 0 do not use memory
			//				= 1 memory access without interfering with display
			CGA_CRT_status_register				= 0x03DA,
			//03DA w EGA/VGA feature control register
			CGA_feature_control_register		= 0x03DA,
			//03DB w clear light pen latch
			//03DC r/w preset light pen latch
			//03DF CRT/CPU page register (PCjr only)
			#endregion

            #region 03F8-03FF - serial port, same as 02E8, 02F8 and 03F8
            /// <summary>
            /// W: Transmitter holding buffer
						/// R: Receiver buffer
            /// </summary>
            UART_Transmit_Receive_Buffer = 0x03F8,
            /// <summary>
            /// R/W: Interrupt enable buffer
            /// </summary>
            UART_Interrupt_Enable_Register = 0x03F9,
            /// <summary>
            /// R: Interrupt identification register
						/// W: FIFO control register
            /// </summary>
            UART_Interrupt_Identification_Register = 0x03FA,
            /// <summary>
            /// R/W: Line control register
            /// </summary>
            UART_Line_Control_Register = 0x03FB,
            /// <summary>
            /// R/W: Modem control register
            /// </summary>
            UART_Modem_Control_Register = 0x03FC,
            /// <summary>
            /// R: Line status register
            /// </summary>
            UART_Line_Status_Register = 0x03FD,
            /// <summary>
            /// R: Modem status register
            /// </summary>
            UART_Modem_Status_Register = 0x03FE,
            /// <summary>
            /// R/W: Scratch register
            /// </summary>
            UART_Scratch_Register = 0x03FF,
            #endregion

			#region 03F0-03F7 - Primary Floppy Disk Controller (PFC)
			PFC_status_A						= 0x03F0,	// r	diskette controller status A (PS/2 model 30)
			//				 			 			 			 		bit 7	 interrupt pending
			//				 			 			 			 		bit 6	 -DRV2	second drive installed
			//				 			 			 			 		bit 5	 step
			//				 			 			 			 		bit 4	 -track 0
			//				 			 			 			 		bit 3	 head 1 select
			//				 			 			 			 		bit 2	 -index
			//				 			 			 			 		bit 1	 -write protect
			//				 			 			 			 		bit 0	 +direction
			PFC_status_B						= 0x03F1,	// r	diskette controller status B (PS/2)
			//															bit 7-6 =1 reserved
			//															bit 5 drive select (0=A:, 1=B:)
			//															bit 4 write data
			//															bit 3 read data
			//															bit 2 write enable
			//															bit 1 motor enable 1
			//															bit 0 motor enable 0
			PFC_control_port					= 0x03F2,	// w	diskette controller DOR (Digital Output Register)
			//															bit 7-6 reserved on PS/2
			//															bit 7 = 1 drive 3 motor enable
			//															bit 6 = 1 drive 2 motor enable
			//															bit 5 = 1 drive 1 motor enable
			//															bit 4 = 1 drive 0 motor enable
			//															bit 3 = 1 diskette DMA enable (reserved PS/2)
			//															bit 2 = 1 FDC enable (controller reset)
			//																  = 0 hold FDC at reset
			//															bit 1-0 drive select (0=A 1=B ..)
			PFC_tape_drive						= 0x03F3,
			//															bit 7-2 reserved, tri-state
			//															bit 1-0 tape select
			//																= 00 none, drive 0 cannot be a tape drive.
			//																= 01 drive1
			//																= 10 drive2
			//																= 11 drive3
			PFC_status_register					= 0x03F4,	// r	diskette controller main status register
			//															bit 7 = 1 RQM data register is ready
			//																	0 no access is permitted
			//															bit 6 = 1 transfer is from controller to system
			//																	0 transfer is from system to controller
			//															bit 5 = 1 non-DMA mode
			//															bit 4 = 1 diskette controller is busy
			//															bit 3 = 1 drive 3 busy (reserved on PS/2)
			//															bit 2 = 1 drive 2 busy (reserved on PS/2)
			//															bit 1 = 1 drive 1 busy (= drive is in seek mode)
			//															bit 0 = 1 drive 0 busy (= drive is in seek mode)
			//															Note:	in non-DMA mode, all data transfers occur through
			//																	port 03F5h and the status registers (bit 5 here
			//																	indicates data read/write rather than than
			//																	command/status read/write)
			PFC_data_rate_Select_register		= 0x03F4,	// w	diskette controller data rate select register
			//															bit 7 = 1 S/W reset
			//															bit 6 = 1 power down
			//															bit 5 = 0 reserved
			//															bit 4-2 write precompensation, 000 default
			//															bit 1-0 data rate select
			//																= 00 500 Kb/s (MFM)
			//																= 01 300 Kb/s (MFM)
			//																= 10 250 Kb/s (MFM)
			//																= 11 1 Mb/s (MFM)
			PFC_data_FIFO						= 0x03F5,	// r	diskette command/data register 0 (ST0)
			//															bit 7-6 last command status
			//																= 00 command terminated successfully
			//																= 01 command terminated abnormally
			//																= 10 invalid command
			//																= 11 terminated abnormally by change in ready signal
			//															bit 5 = 1 seek completed
			//															bit 4 = 1 equipment check occurred after error
			//															bit 3 = 1 not ready
			//															bit 2 = 1 head number at interrupt
			//															bit 1-0 = 1 unit select (0=A 1=B .. )
			//																(on PS/2 01=A 10=B)
			//
			//														status register 1 (ST1)
			//															bit 7 end of cylinder; sector# greater then sectors/track
			//															bit 6 = 0
			//															bit 5 = 1 CRC error in ID or data field
			//															bit 4 = 1 overrun
			//															bit 3 = 0
			//															bit 2 = 1 sector ID not found
			//															bit 1 = 1 write protect detected during write
			//															bit 0 = 1 ID address mark not found
			//
			//														status register 2 (ST2)
			//															bit 7 = 0
			//															bit 6 = 1 deleted Data Address Mark detected
			//															bit 5 = 1 CRC error in data
			//															bit 4 = 1 wrong cylinder detected
			//															bit 3 = 1 scan command equal condition satisfied
			//															bit 2 = 1 scan command failed, sector not found
			//															bit 1 = 1 bad cylinder, ID not found
			//															bit 0 = 1 missing Data Address Mark
			//
			//														status register 3 (ST3)
			//															bit 7 fault status signal
			//															bit 6 write protect status
			//															bit 5 ready status
			//															bit 4 track zero status
			//															bit 3 two sided status signal
			//															bit 2 side select (head select)
			//															bit 1-0 unit select (0=A 1=B .. )
			PFC_controller_data					= 0x03F6,	// w	diskette command register..
			PFC_digital_input					= 0x03F7,	// read-only
			PFC_configuration_control_register			= 0x03F7,	// write-only
			#endregion

			#region 03F0-03F7 - Secondary Floppy Disk Controller (SFC)
			SFC_status_A = 0x0370,	// r	diskette controller status A (PS/2 model 30)
			//				 			 			 			 		bit 7	 interrupt pending
			//				 			 			 			 		bit 6	 -DRV2	second drive installed
			//				 			 			 			 		bit 5	 step
			//				 			 			 			 		bit 4	 -track 0
			//				 			 			 			 		bit 3	 head 1 select
			//				 			 			 			 		bit 2	 -index
			//				 			 			 			 		bit 1	 -write protect
			//				 			 			 			 		bit 0	 +direction
			SFC_status_B = 0x0371,	// r	diskette controller status B (PS/2)
			//															bit 7-6 =1 reserved
			//															bit 5 drive select (0=A:, 1=B:)
			//															bit 4 write data
			//															bit 3 read data
			//															bit 2 write enable
			//															bit 1 motor enable 1
			//															bit 0 motor enable 0
			SFC_control_port = 0x0372,	// w	diskette controller DOR (Digital Output Register)
			//															bit 7-6 reserved on PS/2
			//															bit 7 = 1 drive 3 motor enable
			//															bit 6 = 1 drive 2 motor enable
			//															bit 5 = 1 drive 1 motor enable
			//															bit 4 = 1 drive 0 motor enable
			//															bit 3 = 1 diskette DMA enable (reserved PS/2)
			//															bit 2 = 1 FDC enable (controller reset)
			//																  = 0 hold FDC at reset
			//															bit 1-0 drive select (0=A 1=B ..)
			SFC_tape_drive = 0x0373,
			//															bit 7-2 reserved, tri-state
			//															bit 1-0 tape select
			//																= 00 none, drive 0 cannot be a tape drive.
			//																= 01 drive1
			//																= 10 drive2
			//																= 11 drive3
			SFC_status_register = 0x0374,	// r	diskette controller main status register
			//															bit 7 = 1 RQM data register is ready
			//																	0 no access is permitted
			//															bit 6 = 1 transfer is from controller to system
			//																	0 transfer is from system to controller
			//															bit 5 = 1 non-DMA mode
			//															bit 4 = 1 diskette controller is busy
			//															bit 3 = 1 drive 3 busy (reserved on PS/2)
			//															bit 2 = 1 drive 2 busy (reserved on PS/2)
			//															bit 1 = 1 drive 1 busy (= drive is in seek mode)
			//															bit 0 = 1 drive 0 busy (= drive is in seek mode)
			//															Note:	in non-DMA mode, all data transfers occur through
			//																	port 03F5h and the status registers (bit 5 here
			//																	indicates data read/write rather than than
			//																	command/status read/write)
			SFC_data_rate_Select_register = 0x0374,	// w	diskette controller data rate select register
			//															bit 7 = 1 S/W reset
			//															bit 6 = 1 power down
			//															bit 5 = 0 reserved
			//															bit 4-2 write precompensation, 000 default
			//															bit 1-0 data rate select
			//																= 00 500 Kb/s (MFM)
			//																= 01 300 Kb/s (MFM)
			//																= 10 250 Kb/s (MFM)
			//																= 11 1 Mb/s (MFM)
			SFC_data_FIFO = 0x0375,	// r	diskette command/data register 0 (ST0)
			//															bit 7-6 last command status
			//																= 00 command terminated successfully
			//																= 01 command terminated abnormally
			//																= 10 invalid command
			//																= 11 terminated abnormally by change in ready signal
			//															bit 5 = 1 seek completed
			//															bit 4 = 1 equipment check occurred after error
			//															bit 3 = 1 not ready
			//															bit 2 = 1 head number at interrupt
			//															bit 1-0 = 1 unit select (0=A 1=B .. )
			//																(on PS/2 01=A 10=B)
			//
			//														status register 1 (ST1)
			//															bit 7 end of cylinder; sector# greater then sectors/track
			//															bit 6 = 0
			//															bit 5 = 1 CRC error in ID or data field
			//															bit 4 = 1 overrun
			//															bit 3 = 0
			//															bit 2 = 1 sector ID not found
			//															bit 1 = 1 write protect detected during write
			//															bit 0 = 1 ID address mark not found
			//
			//														status register 2 (ST2)
			//															bit 7 = 0
			//															bit 6 = 1 deleted Data Address Mark detected
			//															bit 5 = 1 CRC error in data
			//															bit 4 = 1 wrong cylinder detected
			//															bit 3 = 1 scan command equal condition satisfied
			//															bit 2 = 1 scan command failed, sector not found
			//															bit 1 = 1 bad cylinder, ID not found
			//															bit 0 = 1 missing Data Address Mark
			//
			//														status register 3 (ST3)
			//															bit 7 fault status signal
			//															bit 6 write protect status
			//															bit 5 ready status
			//															bit 4 track zero status
			//															bit 3 two sided status signal
			//															bit 2 side select (head select)
			//															bit 1-0 unit select (0=A 1=B .. )
			SFC_controller_data = 0x0376,	// w	diskette command register..
			SFC_digital_input = 0x0377,	// read-only
			SFC_configuration_control_register = 0x0377,	// write-only
			#endregion

            //03F8-03FF - serial port (8250,8251,16450,16550)

            #region PCI Config
            PCI_Config_Address = 0x0CF8,
            PCI_Config_Data = 0x0CFC,
            #endregion

			SpeakerPort = 0x61,
        };
		#endregion

		#region ReadByte
		public unsafe static byte ReadByte (Port port)
		{
			return Read8 ((uint)port);
		}
		#endregion

		#region WriteByte
		public unsafe static void WriteByte (Port port, byte value)
		{
			Write8 ((uint)port, value);
		}
		#endregion

		#region ReadByte
		public unsafe static byte Read8 (uint port)
		{
			ushort uport = (ushort)port;
			byte value = 0;

			Asm.XOR (R32.EAX, R32.EAX);
			Asm.MOV (R16.DX, (ushort*)&uport);
			Asm.IN_AL__DX ();
			Asm.MOV (&value, R8.AL);

			return value;
		}
		#endregion

		#region ReadUInt16
		public unsafe static UInt16 Read16 (uint port)
		{
			ushort uport = (ushort)port;
			ushort value = 0;

			Asm.XOR (R32.EAX, R32.EAX);
			Asm.MOV (R16.DX, (ushort*)&uport);
			Asm.IN_AX__DX ();
			Asm.MOV (&value, R16.AX);

			return value;
		}
		#endregion

		#region ReadUInt32
		public unsafe static UInt32 Read32 (uint port)
		{
			ushort uport = (ushort)port;
			uint value = 0;

			Asm.XOR (R32.EAX, R32.EAX);
			Asm.MOV (R16.DX, (ushort*)&uport);
			Asm.IN_EAX__DX ();
			Asm.MOV (&value, R32.EAX);

			return value;
		}
		#endregion

		#region WriteByte
		public unsafe static void Write8 (uint port, byte value)
		{
			ushort uport = (ushort)port;
			Asm.MOV (R16.DX, (ushort*)&uport);
			Asm.MOV (R8.AL, &value);
			Asm.OUT_DX__AL ();
		}
		#endregion

		#region WriteUInt16
		public unsafe static void Write16 (uint port, UInt16 value)
		{
			ushort uport = (ushort)port;
			Asm.MOV (R16.DX, (ushort*)&uport);
			Asm.MOV (R16.AX, &value);
			Asm.OUT_DX__AX ();
		}
		#endregion

		#region Write32
		public unsafe static void Write32 (uint port, UInt32 value)
		{
			ushort uport = (ushort)port;
			Asm.MOV (R16.DX, (ushort*)&uport);
			Asm.MOV (R32.EAX, &value);
			Asm.OUT_DX__EAX ();
		}
		#endregion

		#region Delay
		public unsafe static void Delay ()
		{
			Asm.IN_AL (0x80);
			Asm.OUT__AL (0x80);
		}
		#endregion

	}
}
