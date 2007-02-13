/*
 * SharpGC -- A conservative, generational, compacting garbage collector written in C#
 *
 * (C) 2007 William Lahti. This software is licensed under the terms of the GNU Lesser
 * General Public License.
 *
 * ManagedHeap.cs -- A managed heap implementation. Controlled by the GC.
 *
 */
 
using SharpGC;

public unsafe struct RuntimeObjHandle {
	public int ID;
	public GCData *Data;
	public GCField *Fields;
	
	public RuntimeObjHandle *Next;
}

public class GCTest {
	public unsafe static void Finalize(void *objHandle, void *gcState)
	{
		RuntimeObjHandle *hndl = (RuntimeObjHandle*)objHandle;
		
		// clean up runtime structures
		System.Console.WriteLine("Finalizing object #{0}", hndl->ID);
		CleanUp(hndl);
		GC.ClearGCState(gcState);
	}
	
	public unsafe static void CleanUp(RuntimeObjHandle *hndl)
	{
		RuntimeObjHandle *last = Objects;
		
		// remove reference to GCData structure, which
		// will be deleted by the GC sometime after this call.
		
		hndl->Data = null;
		
		System.Console.WriteLine("Deallocating object #{0}", hndl->ID);
		if (hndl == Objects) {
			Objects = hndl->Next;
			GCField.DeallocList(hndl->Fields);
			GCMem.Dealloc(hndl);
			
			if (Objects == null)
				LastObject = null;
			return;
		}
		
		while (last->Next != null) {
			if (last->Next == hndl)
				break;
				
			last = last->Next;
		}
		
		if (last == Objects)
			Objects->Next = hndl->Next;
		else
			last->Next = hndl->Next;
		
		System.Console.WriteLine("Deallocating object {0}", hndl->ID);
		GCField.DeallocList(hndl->Fields);
		GCMem.Dealloc(hndl);
	}
	
	public unsafe static GCField *ObjectWalk(void *objHandle)
	{
		RuntimeObjHandle *hndl = (RuntimeObjHandle*)objHandle;
		
		return GCField.CopyList(hndl->Fields);
	}
	
	public unsafe static RuntimeObjHandle *New(int id, int dataSize)
	{
		RuntimeObjHandle *hndl = (RuntimeObjHandle*)GCMem.Alloc(sizeof(RuntimeObjHandle));
		
		hndl->ID = id;
		hndl->Data = GC.Allocate(hndl, dataSize);
		hndl->Next = null;
		hndl->Fields = null;
		
		if (LastObject == null) {
			Objects = LastObject = hndl;
		} else {
			LastObject->Next = hndl;
			LastObject = hndl;
		}
		
		return hndl;
	}
	
	public unsafe static RuntimeObjHandle *GetHandle(int id)
	{
		RuntimeObjHandle *hndl = Objects;
			
		while (hndl != null) {
			if (hndl->ID == id) {
				return hndl;
			}
			
			hndl = hndl->Next;
		}
		
		return null;
	}
	
	public unsafe static void Connect(int hostID, int objID)
	{
		RuntimeObjHandle *host, obj;
		GCField *field = null;
		
		host = GetHandle(hostID);
		obj = GetHandle(objID);
		
		if (host == null)
			throw new System.Exception("host " + hostID + " doesn't exist");
		if (obj == null)
			throw new System.Exception("obj " + objID + " doesn't exist");
		
		host->Fields = GCField.Add(host->Fields, obj);
	}
	
	unsafe static RuntimeObjHandle *Objects = null;
	unsafe static RuntimeObjHandle *LastObject = null;
	
	public enum TestType: int {
		// test: do nothing (makes sure non-GC allocation is cleaned up properly)
		// expected: nothing
		Nothing,
		
		// test: create 5 objects, link them all together, have first object in chain be root
		// expected: no collection
		Linear5Root1,
		
		// test: create 5 objects, link all but one together, have first object in chain be root
		// expected: one object collected
		Linear5Root1_Collect1,
		
		// test: create 5 objects, link them together in cyclic chain, have first object be root.
		// expected: no collection 
		Cyclic5Root1,
		
		// test: create 5 objects, link all but one together in cyclic chain, have first object be root.
		// expected: one object collected
		Cyclic5Root1_Collect1,
		
		Complex13Root2,
		KeepAliveTest,
		
		TestTest,
		
		Last
	}
	
	public unsafe static void PerformTest(TestType testType)
	{
		RuntimeObjHandle *list = null;
		GCField *root_list = null;
		int startMem = ManagedHeap.GetAllocatedMemory();
		bool suppressGC = false;
		
		switch (testType) {
			case TestType.Nothing:
				System.Console.WriteLine("GC has allocated {0} bytes (should be 0)", 
								ManagedHeap.GetAllocatedMemory() - startMem);
			break;
			case TestType.TestTest:
				New(1, 8); 
				New(2, 16);
				New(3, 16);
				New(4, 16);
				New(5, 8);
			
				CleanUp(GetHandle(1));
				CleanUp(GetHandle(2));
				CleanUp(GetHandle(3));
				CleanUp(GetHandle(4));
				CleanUp(GetHandle(5));
				
				suppressGC = true;
			break;
			case TestType.Linear5Root1:
				
				New(1, 8); 
				New(2, 16);
				New(3, 16);
				New(4, 16);
				New(5, 8);
			
				System.Console.WriteLine("GC has allocated {0} bytes (should be 64)", 
							ManagedHeap.GetAllocatedMemory() - startMem);
							
				Connect(1, 2);
				Connect(2, 3);
				Connect(3, 4);
				Connect(4, 5);
				
				root_list = GCField.Alloc(GetHandle(1));
					GC.Collect(root_list);
				GCField.Dealloc(root_list); root_list = null;
			break;
			case TestType.Linear5Root1_Collect1:
				New(1, 8);
				New(2, 16);
				New(3, 16);
				New(4, 16);
				New(5, 8);
			
				System.Console.WriteLine("GC has allocated {0} bytes (should be 64)", 
							ManagedHeap.GetAllocatedMemory() - startMem);
				Connect(1, 2);
				Connect(2, 3);
				Connect(3, 5);
				
				root_list = GCField.Alloc(GetHandle(1));
					GC.Collect(root_list);
				GCField.Dealloc(root_list); root_list = null;
			break;
			case TestType.Cyclic5Root1:
				New(1, 8);
				New(2, 16);
				New(3, 16);
				New(4, 16);
				New(5, 8);
				
				System.Console.WriteLine("GC has allocated {0} bytes (should be 64)", 
							ManagedHeap.GetAllocatedMemory() - startMem);
							
				Connect(1, 2);
				Connect(2, 3);
				Connect(3, 4);
				Connect(4, 5);
				Connect(5, 1);
				
				root_list = GCField.Alloc(GetHandle(1));
					GC.Collect(root_list);
				GCField.Dealloc(root_list); root_list = null;
			break;
			case TestType.Cyclic5Root1_Collect1:
				New(1, 8);
				New(2, 16);
				New(3, 16);
				New(4, 16);
				New(5, 8);
				
				System.Console.WriteLine("GC has allocated {0} bytes (should be 64)", 
							ManagedHeap.GetAllocatedMemory() - startMem);
							
				Connect(1, 2);
				Connect(2, 3);
				Connect(3, 5);
				Connect(5, 1);
				
				root_list = GCField.Alloc(GetHandle(1));
					GC.Collect(root_list);
				GCField.Dealloc(root_list); root_list = null;
			break;
			case TestType.Complex13Root2:
				New(1, 32);
				New(2, 2);
				
				New(3, 4);
				New(4, 10);
				New(5, 21);
				New(6, 21);
				New(7, 20);
				New(8, 43);
				New(9, 21);
				New(10, 18);
				New(11, 18);
				New(12, 18);
				New(13, 18);
				
				Connect(1, 11);
				Connect(2, 3);
				Connect(2, 5);
				Connect(3, 4);
				Connect(11, 6);
				Connect(9, 12);
				Connect(6, 13);
				
				root_list = GCField.Alloc(GetHandle(1));
				root_list = GCField.Add(root_list, GetHandle(1));
					GC.Collect(root_list);
				GCField.DeallocList(root_list); root_list = null;
			break;
			case TestType.KeepAliveTest:
				New(1, 8);
				New(2, 8);
				New(3, 8);
				New(4, 8);
				New(5, 8);
				
				Connect(1, 2);
				Connect(1, 4);
				Connect(1, 5);
				
				GetHandle(3)->Data->Flags |= GCFlags.KeepAlive;
				
				root_list = GCField.Alloc(GetHandle(1));
				root_list = GCField.Add(root_list, GetHandle(1));
					GC.Collect(root_list);
				GCField.DeallocList(root_list); root_list = null;
			break;
		}
		
		if (!suppressGC)
			GC.Collect(null);
		
		System.Console.WriteLine("Finished test {0}", testType);
		GC.PrintStats();
	}
	
	public unsafe static int Main(string[] args)
	{
		GC.Initialize(Finalize, ObjectWalk, 1024 * 64);
		int startMem = ManagedHeap.GetAllocatedMemory();
		bool doAllTests = false;
		TestType test = TestType.Nothing;
		
		if (args.Length == 0)
			doAllTests = true;
		else if (args.Length == 1) {
			try {
				test = (TestType)System.Enum.Parse(typeof(TestType), args[0]);
			} catch {
				System.Console.WriteLine("Usage: Test [test-type]");
				return 1;
			}
		} else {
			System.Console.WriteLine("Usage: Test [test-type]");
			return 1;
		}
		
		if (doAllTests) {
			for (int x = 1; x < (int)TestType.Last; ++x) {
				TestType testType = (TestType)x;
				
				System.Console.WriteLine("\n:: Running test {0}", testType);
				PerformTest((TestType)x);
			}
		} else {
			System.Console.WriteLine("\n:: Running test {0}", test);
			PerformTest(test);
		}
		
		if (Objects != null)
			throw new System.Exception("Some objects remain allocated, all should've been collected and freed");
		
		System.Console.WriteLine("All tests completed.");
		ManagedHeap.PrintStats();
		GC.PrintStats();
		
		return 0;
	}
}