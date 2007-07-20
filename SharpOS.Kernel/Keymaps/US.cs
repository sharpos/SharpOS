//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL License version 2.
//

using System;
using System.Runtime.InteropServices;
using SharpOS;
using SharpOS.AOT.X86;
using SharpOS.AOT.IR;

namespace SharpOS.ADC.X86 {
	public unsafe partial class KeyboardLayouts {
		public static void US (byte *buffer)
		{
			buffer [0] = 0;
			buffer [1] = 27;
			buffer [2] = (byte) '1';
			buffer [3] = (byte) '2';
			buffer [4] = (byte) '3';
			buffer [5] = (byte) '4';
			buffer [6] = (byte) '5';
			buffer [7] = (byte) '6';
			buffer [8] = (byte) '7';
			buffer [9] = (byte) '8';
			buffer [10] = (byte) '9';
			buffer [11] = (byte) '0';
			buffer [12] = (byte) '-';
			buffer [13] = (byte) '=';
			buffer [14] = (byte) '\b';
			buffer [15] = (byte) '\t';
			buffer [16] = (byte) 'q';
			buffer [17] = (byte) 'w';
			buffer [18] = (byte) 'e';
			buffer [19] = (byte) 'r';
			buffer [20] = (byte) 't';
			buffer [21] = (byte) 'y';
			buffer [22] = (byte) 'u';
			buffer [23] = (byte) 'i';
			buffer [24] = (byte) 'o';
			buffer [25] = (byte) 'p';
			buffer [26] = (byte) '[';
			buffer [27] = (byte) ']';
			buffer [28] = (byte) '\n';
			buffer [29] = (byte) 0;		// control
			buffer [30] = (byte) 'a';
			buffer [31] = (byte) 's';
			buffer [32] = (byte) 'd';
			buffer [33] = (byte) 'f';
			buffer [34] = (byte) 'g';
			buffer [35] = (byte) 'h';
			buffer [36] = (byte) 'j';
			buffer [37] = (byte) 'k';
			buffer [38] = (byte) 'l';
			buffer [39] = (byte) ';';
			buffer [40] = (byte) '\'';
			buffer [41] = (byte) '`';
			buffer [42] = (byte) 0;		// left shift
			buffer [43] = (byte) '\\';
			buffer [44] = (byte) 'z';
			buffer [45] = (byte) 'x';
			buffer [46] = (byte) 'c';
			buffer [47] = (byte) 'v';
			buffer [48] = (byte) 'b';
			buffer [49] = (byte) 'n';
			buffer [50] = (byte) 'm';
			buffer [51] = (byte) ',';
			buffer [52] = (byte) '.';
			buffer [53] = (byte) '/';
			buffer [54] = (byte) 0;		// right shift
			buffer [55] = (byte) '*';
			buffer [56] = (byte) 0;		// alt
			buffer [57] = (byte) ' ';
			buffer [58] = (byte) 0;		// caps lock
			buffer [59] = (byte) 0;		// F1
			buffer [60] = (byte) 0;
			buffer [61] = (byte) 0;
			buffer [62] = (byte) 0;
			buffer [63] = (byte) 0;
			buffer [64] = (byte) 0;
			buffer [65] = (byte) 0;
			buffer [66] = (byte) 0;
			buffer [67] = (byte) 0;
			buffer [68] = (byte) 0;		// F10
			buffer [69] = (byte) 0;		// num lock
			buffer [70] = (byte) 0;		// scroll lock
			buffer [71] = (byte) 0;		// home key
			buffer [72] = (byte) 0;		// up arrow
			buffer [73] = (byte) 0;		// page up
			buffer [74] = (byte) '-';	
			buffer [75] = (byte) 0;		// left arrow
			buffer [76] = (byte) 0;	
			buffer [77] = (byte) 0;		// right arrow
			buffer [78] = (byte) '+';
			buffer [79] = (byte) 0;		// end
			buffer [80] = (byte) 0;		// down arrow
			buffer [81] = (byte) 0;		// page down
			buffer [82] = (byte) 0;		// insert
			buffer [83] = (byte) 0;		// delete
			buffer [84] = (byte) 0;	
			buffer [85] = (byte) 0;	
			buffer [86] = (byte) 0;	
			buffer [87] = (byte) 0;		// f11
			buffer [88] = (byte) 0;		// f12
			buffer [89] = (byte) 0;
		}
	}
}
