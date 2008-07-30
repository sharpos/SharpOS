//
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using SharpOS.AOT.Attributes;
using SharpOS.Kernel.ADC;

namespace SharpOS.Kernel.Foundation {
	public class Testcase {
		/// <summary>
		/// When true, records testcases for viewing later.
		/// </summary>
		public static bool RecordTestcases = true;

		/// <summary>
		/// When true, prints testcases to the console when they are performed.
		/// </summary>
		public static bool PrintTestcases = true;

		/// <summary>
		/// A record of a test that was done previously.
		/// </summary>
		public unsafe struct TestRecord {
			public CString8 *Source;
			public CString8 *Name;
			public bool Result;

			/// <summary>
			/// Please do not use this directly, instead use
			/// TestCase.GetNextTest ().
			/// </summary>
			internal unsafe TestRecord *Next;
		}

		/// <summary>
		/// Tracks the previous tests when RecordTestcases is true.
		/// </summary>
		unsafe static TestRecord *Records = null;

		/// <summary>
		/// Clears all testcase records from memory.
		/// </summary>
		public unsafe static void Dispose ()
		{
			TestRecord *it = Records;

			while (it != null) {
				TestRecord *next = it->Next;
				MemoryManager.Free (it);
				it = next;
			}
		}

		/// <summary>
		/// Gets the amount of test records that arve currently held.
		/// </summary>
		public unsafe static int GetTestCount ()
		{
			int count = 0;
			TestRecord *it = Records;

			while (it != null) {
				it = it->Next;
				++count;
			}

			return count;
		}

		/// <summary>
		/// Gets a test by it's numerical index.
		/// </summary>
		public unsafe static TestRecord *GetTest (int index)
		{
			int x = 0;
			TestRecord *it = Records;

			while (it != null && x < index) {
				it = it->Next;
				++x;
			}

			if (x < index)
				return null;

			return it;
		}

		/// <summary>
		/// Gets the last test record that is held.
		/// </summary>
		public unsafe static TestRecord *GetLastTest ()
		{
			if (!RecordTestcases)
				return null;

			if (Records == null)
				return null;

			TestRecord *it = Records;

			while (it->Next != null)
				it = it->Next;

			return it;
		}

		/// <summary>
		/// Gets the first test record, which can be used to iterate over all entries.
		/// </summary>
		public unsafe static TestRecord *GetFirstTest ()
		{
			return Records;
		}

		/// <summary>
		/// Iterates over the record list.
		/// </summary>
		public unsafe static TestRecord *GetNextTest (TestRecord *rec)
		{
			Diagnostics.Assert (rec != null, "Testcase::GetNextTest(): argument `rec' is null");
			return rec->Next;
		}

		public unsafe static void TestFail (string source, string name)
		{
			Test (false, source, name);
		}

		public unsafe static void TestPass (string source, string name)
		{
			Test (true, source, name);
		}

		/// <summary>
		/// Performs a new test, optionally printing it and/or recording it in memory.
		/// </summary>
		public unsafe static void Test (bool result, string source, string name)
		{
			CString8 *sourceStr =  CString8.Copy (source);
			CString8 *nameStr = CString8.Copy (name);

			if (PrintTestcases) {
				Debug.COM1.Write (sourceStr);
				Debug.COM1.Write (": ");
				Debug.COM1.Write (nameStr);
				Debug.COM1.Write (" ... ");

				TextMode.SaveAttributes ();

				if (result) {
					Debug.COM1.WriteLine ("PASS");
				}
				else {
					Debug.COM1.WriteLine ("FAIL");
				}

				TextMode.RestoreAttributes ();
			}

			if (RecordTestcases) {
				TestRecord *rec = (TestRecord*) MemoryManager.Allocate ((uint)sizeof (TestRecord));

				rec->Source = sourceStr;
				rec->Name = nameStr;
				rec->Result = result;
				rec->Next = null;

				if (Records == null)
					Records = rec;
				else
					GetLastTest ()->Next = rec;
			} else {
				MemoryManager.Free (sourceStr);
				MemoryManager.Free (nameStr);
			}
		}
	}
}
