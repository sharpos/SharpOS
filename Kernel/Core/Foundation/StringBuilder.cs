// 
// (C) 2007 The SharpOS Project Team (http://sharpos.sourceforge.net)
//
// Authors:
//	William Lahti <xfurious@gmail.com>
//  Bruce Markham <illuminus86@gmail.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;

using SharpOS.Memory;

namespace SharpOS.Foundation {
	public unsafe struct StringBuilder {
        private CString8* buffer;
        private uint capacity;
        private uint caret;

        public const uint DefaultCapacity = 31;

        private void Write_INTERNAL(byte* ptr, uint count)
        {
            byte* oPtr = buffer->Pointer;
            for (int x = 0; x < count && ptr[x] != (byte)'\0'; x++)
            {
                oPtr[caret + x] = ptr[x];
            }
            caret += count;

            Tie();
        }
        private void Write_INTERNAL(byte ch)
        {
            buffer->Pointer[caret] = ch;
            caret = caret + 1;

            Tie();
        }

        public void AppendChar(byte ch)
		{
            if ((caret + 1) >= this.capacity)
                this.Expand(DefaultCapacity);

            Write_INTERNAL(ch);
		}

		public void Append (byte *str)
		{
			uint length = (uint)ByteString.Length (str);
            if ((caret + length) >= this.capacity)
                this.Expand((uint)(DefaultCapacity *
                    (((length - (caret + 1)) / DefaultCapacity) + 1)));

            Write_INTERNAL(str, length);
		}

		public void AppendNumber (int number)
		{
			AppendNumber (number, false);
		}
		
		public void AppendNumber (int number, bool hex)
		{
            byte* str = (byte*)Convert.ToString(number, hex);
            Append(str);
            ADC.MemoryManager.Free((void*)str);
		}

		public void AppendNumber (short number, bool hex)
		{
			AppendNumber ((int)number, hex);
		}
		
		public void AppendNumber (short number)
		{
			AppendNumber ((int)number, false);
		}

		public void AppendNumber (byte number, bool hex)
		{
			AppendNumber ((int)number, hex);
		}
		
		public void Clear ()
		{
            caret = 0;
			Tie ();
		}

        private void Tie()
        {
            buffer->Pointer[caret] = (byte)'\0';
        }

        private void Expand(uint amount)
        {
            StringBuilder* tempInstance = CREATE(this.buffer, amount);
            this.buffer = tempInstance->buffer;
            this.capacity = tempInstance->capacity;
            ADC.MemoryManager.Free(tempInstance);
        }
        
        public static StringBuilder* CREATE()
        {
            return CREATE(DefaultCapacity);
        }

        public static StringBuilder* CREATE(CString8* initialContents)
        {
            Diagnostics.Assert(initialContents != null, "CString8::Create(CString8*): Parameter 'initialContents' should not be null");
            return CREATE(initialContents, DefaultCapacity);
        }

        public static StringBuilder* CREATE(uint initialCapacity)
        {
            StringBuilder* instance = (StringBuilder*)ADC.MemoryManager.Allocate((uint)sizeof(StringBuilder));
            
            instance->buffer = (CString8*)ADC.MemoryManager.Allocate(initialCapacity + 1);
            instance->capacity = initialCapacity;

            byte* ptr = instance->buffer->Pointer;
            for (int x = 0; x < (initialCapacity + 1); x++)
                ptr[x] = (byte)'\0';

            return instance;
        }

        public static StringBuilder* CREATE(CString8* initialContents, uint additionalPadding)
        {
            Diagnostics.Assert(initialContents != null, "CString8::Create(CString8*,uint): Parameter 'initialContents' should not be null");

            StringBuilder* instance = (StringBuilder*)ADC.MemoryManager.Allocate((uint)sizeof(StringBuilder));
            uint initialCapacity = (uint)initialContents->Length + additionalPadding;

            instance->buffer = (CString8*)ADC.MemoryManager.Allocate(initialCapacity + 1);
            instance->capacity = initialCapacity;


            byte* oPtr = instance->buffer->Pointer;
            byte* iPtr = initialContents->Pointer;

            bool inFill = false;
            for (int x = 0; x < (instance->capacity); x++)
            {
                if (!inFill && iPtr[x] == (byte)'\0')
                    inFill = true;
                if (inFill)
                    oPtr[x] = (byte)'\0';
                else
                    oPtr[x] = iPtr[x];
            }
            oPtr[instance->capacity + 1] = (byte)'\0';

            return instance;
        }
	}
}
