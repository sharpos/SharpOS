// 
// (C) 2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;

using SharpOS.Memory;

namespace SharpOS.Foundation {
	public unsafe struct StringBuilder {
		public StringBuilder (byte *buffer, int bufferSize)
		{
			Buffer = buffer;
			BufferSize = bufferSize;
			Caret = 0;
		}

		public byte *Buffer;
		public int BufferSize;
		public int Caret;

		void Write (byte b)
		{
			Buffer [Caret++] = b;
		}

		void Write (byte *str)
		{
			int length = ByteString.Length (str);

			for (int x = 0; x < length; ++x) {
				Buffer [Caret++] = str [x];
			}
		}

		void Tie ()
		{
			Buffer [Caret] = 0;
		}

		public void Add (byte *str, int value)
		{
			Add (str);
			Add (value);
		}
		
		public void Add (byte *str, int value, byte *str2)
		{
			Add (str);
			Add (value);
			Add (str2);
		}

		public void Add (byte *str, byte value)
		{
			Add (str);
			Add (value);
		}
		
		public void Add (byte *str, byte value, byte *str2)
		{
			Add (str);
			Add (value);
			Add (str2);
		}
		
		public void Add (byte b)
		{
			Write (b);
			Tie ();
		}

		public void Add (byte *str)
		{
			Write (str);
			Tie ();
		}

		public void Add (int number)
		{
			Add (number, false);
		}
		
		public void Add (int number, bool hex)
		{
			Caret += Convert.ToString (number, hex, Buffer, BufferSize, Caret);
			Tie ();
		}

		public void Add (short number, bool hex)
		{
			Add ((int)number, hex);
		}
		
		public void Add (short number)
		{
			Add ((int)number, false);
		}

		public void Add (byte number, bool hex)
		{
			Add ((int)number, hex);
		}
		
		public void Clear ()
		{
			Caret = 0;
			Tie ();
		}
	}
}
