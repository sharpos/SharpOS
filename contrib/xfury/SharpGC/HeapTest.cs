/*
 * SharpGC -- A conservative, generational, compacting garbage collector written in C#
 *
 * (C) 2007 William Lahti. This software is licensed under the terms of the GNU Lesser
 * General Public License.
 *
 * HeapTest.cs -- Performs a number of tests on the managed heap implementation
 *
 */
 
using System;
using SharpGC;

public unsafe class HeapTest {
	public static void SmallFillTest()
	{
		void *obj;
		GCHandle *hndl;
		int object_size = 4;
		int count = 1024 * 32;
		
		for (int x = 0; x < count; ++x) {
			hndl = GCHandle.Alloc((void*)1);
			hndl->Data = GCData.Alloc();
			obj = ManagedHeap.Allocate(object_size, hndl);
			hndl->Data->Memory = obj;
			*(byte*)hndl->Data->Memory = 200;
		}
		
		Console.WriteLine("Finished small fill test");
	}
	
	public static void LargeFillTest()
	{
		void *obj;
		GCHandle *hndl;
		int object_size = 16;
		int count = 1024 * 8;
		
		for (int x = 0; x < count; ++x) {
			hndl = GCHandle.Alloc((void*)1);
			hndl->Data = GCData.Alloc();
			obj = ManagedHeap.Allocate(object_size, hndl);
			hndl->Data->Memory = obj;
			*(byte*)hndl->Data->Memory = 200;
		}
		
		Console.WriteLine("Finished large fill test");
	}
	
	public static void StressTest()
	{
		void *obj1, obj2, obj3, obj4;
		GCHandle *hndl1, hndl2, hndl3, hndl4;
		int iterations = 200000;
		DateTime lastfps = DateTime.Now;
		int tests_second = 0;
		
		for (int x = 0; x < iterations; ++x) {
			double perc = (double)x / (double)iterations;
			
			hndl1 = GCHandle.Alloc((void*)1);
			hndl2 = GCHandle.Alloc((void*)2);
			hndl3 = GCHandle.Alloc((void*)3);
			hndl4 = GCHandle.Alloc((void*)4);
			
			hndl1->Data = GCData.Alloc();
			hndl2->Data = GCData.Alloc();
			hndl3->Data = GCData.Alloc();
			hndl4->Data = GCData.Alloc();
			
			obj1 = ManagedHeap.Allocate(16, hndl1);
			obj2 = ManagedHeap.Allocate(24, hndl2);
			obj3 = ManagedHeap.Allocate(32, hndl3);
			obj4 = ManagedHeap.Allocate(24, hndl4);
			
			hndl1->Data->Memory = obj1;
			hndl2->Data->Memory = obj2;
			hndl3->Data->Memory = obj3;
			hndl4->Data->Memory = obj4;
			
			// set some values
			*(byte*)hndl1->Data->Memory = 1;
			*(byte*)hndl2->Data->Memory = 2;
			*(byte*)hndl3->Data->Memory = 3;
			*(byte*)hndl4->Data->Memory = 4;
			
			if (perc >= 0.75) {
				ManagedHeap.Deallocate(obj1); obj1 = null;
				ManagedHeap.Compact();
				ManagedHeap.Deallocate(obj3); obj3 = null;
				ManagedHeap.Compact();
				ManagedHeap.Deallocate(obj2); obj2 = null;
				ManagedHeap.Compact();
				ManagedHeap.Deallocate(obj4); obj4 = null;
				ManagedHeap.Compact();
			} else if (perc >= 0.50) {
				ManagedHeap.Deallocate(obj4); obj4 = null;
				ManagedHeap.Compact();
			
				ManagedHeap.Deallocate(obj1); obj1 = null;
				ManagedHeap.Deallocate(obj2); obj2 = null;
				ManagedHeap.Compact();
				ManagedHeap.Deallocate(obj3); obj3 = null;
			} else if (perc >= 0.25) {
				ManagedHeap.Deallocate(obj4); obj4 = null;
				ManagedHeap.Deallocate(obj3); obj3 = null;
				ManagedHeap.Deallocate(obj2); obj2 = null;
				ManagedHeap.Deallocate(obj1); obj1 = null;
			} else {
				ManagedHeap.Deallocate(obj1); obj1 = null;
				ManagedHeap.Deallocate(obj2); obj2 = null;
				ManagedHeap.Deallocate(obj3); obj3 = null;
				ManagedHeap.Deallocate(obj4); obj4 = null;
			}
			
			GCHandle.Dealloc(hndl1);
			GCHandle.Dealloc(hndl2);
			GCHandle.Dealloc(hndl3);
			GCHandle.Dealloc(hndl4);
			
			tests_second++;
			
			if (lastfps + new TimeSpan(0, 0, 1) <= DateTime.Now) {
				Console.WriteLine("{0} tests per second", tests_second);
				tests_second = 0;
				lastfps = DateTime.Now;
			}
		}
		Console.WriteLine("Finished stress test. ran {0} iterations", iterations);
		ManagedHeap.PrintStats();
	}
	
	public static void SimpleTest()
	{
		void *obj1, obj2, obj3, obj4;
		GCHandle *hndl1, hndl2, hndl3, hndl4;
		
		hndl1 = GCHandle.Alloc((void*)1);
		hndl2 = GCHandle.Alloc((void*)2);
		hndl3 = GCHandle.Alloc((void*)3);
		hndl4 = GCHandle.Alloc((void*)4);
		
		hndl1->Data = GCData.Alloc();
		hndl2->Data = GCData.Alloc();
		hndl3->Data = GCData.Alloc();
		hndl4->Data = GCData.Alloc();
		
		obj1 = ManagedHeap.Allocate(16, hndl1);
		obj2 = ManagedHeap.Allocate(24, hndl2);
		obj3 = ManagedHeap.Allocate(32, hndl3);
		obj4 = ManagedHeap.Allocate(24, hndl4);
		
		hndl1->Data->Memory = obj1;
		hndl2->Data->Memory = obj2;
		hndl3->Data->Memory = obj3;
		hndl4->Data->Memory = obj4;
		
		*(byte*)hndl1->Data->Memory = 1;
		*(byte*)hndl2->Data->Memory = 2;
		*(byte*)hndl3->Data->Memory = 3;
		*(byte*)hndl4->Data->Memory = 4;
		
		ManagedHeap.Deallocate(obj2); obj2 = null;
		GCHandle.Dealloc(hndl2); hndl2 = null;
		
		ManagedHeap.PrintStats();
		
		Console.WriteLine("Compacting managed heap");
		
		ManagedHeap.Compact();
		
		if (*(byte*)hndl1->Data->Memory != 1)
			throw new Exception("Data in obj1 changed");
		if (*(byte*)hndl3->Data->Memory != 3)
			throw new Exception("Data in obj3 changed");
		if (*(byte*)hndl4->Data->Memory != 4)
			throw new Exception("Data in obj4 changed");
			
		ManagedHeap.PrintStats();
		
	}
	
	public static int Main()
	{
		
		ManagedHeap.Initialize(1024 * 64);
		
		StressTest();
		
		return 0;
	}
}