//
// (C) 2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Ásgeir Halldórsson <asgeir.halldorsson@gmail.com>
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//  Cédric Rousseau <cedrou@gmail.com>
//  Phil Garcia (aka tgiphil) <phil@thinkedge.com>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

// Since thread support is not yet available this "Event Dispatch" class
// is used to handle async events, like serial io. 

using System;
using AOTAttr = SharpOS.AOT.Attributes;
using SharpOS.Kernel.Foundation;

namespace SharpOS.Kernel.ADC
{
	public static class SimpleEventDispatch
	{
		private struct Dispatch
		{
			public object obj;
			public uint address;

			public Dispatch (object obj, uint address)
			{
				this.obj = obj;
				this.address = address;
			}
		}

		private const uint MaxDispatches = 100;
		private static uint dispatchCount;
		private static Dispatch[] dispathTable;
		private static SpinLock spinLock;

		public static void Setup ()
		{
			dispathTable = new Dispatch[MaxDispatches];
			dispatchCount = 0;
		}

		private static bool Add (Dispatch dispatch)
		{
			if (dispatchCount + 1 == dispathTable.Length)
				return false;

			dispathTable[dispatchCount] = dispatch;
			return true;
		}

		public static EventRegisterStatus RegisterDataReceivedEvent (object obj, uint address)
		{
			try {
				spinLock.Enter ();
				for (int x = 0; x < dispatchCount; x++)
					if ((dispathTable[x].address == address) && (dispathTable[x].obj == obj))
						return EventRegisterStatus.AlreadySubscribed;

				if (Add (new Dispatch (obj, address)))
					return EventRegisterStatus.Success;
				else
					return EventRegisterStatus.CapacityExceeded;
			}
			finally {
				spinLock.Exit ();
			}
		}

		public static unsafe void OnEvent (object obj)
		{
			TextMode.WriteLine ("HIT");
			try {
				spinLock.Enter ();
				for (int x = 0; x < dispatchCount; ++x) {
					if (dispathTable[x].obj == obj)
						MemoryUtil.Call ((void*)dispathTable[x].address, (void*)0);
				}
			}
			finally {
				spinLock.Exit ();
			}
		}

	}
}