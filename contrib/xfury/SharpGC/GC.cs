/*
 * SharpGC -- A conservative, generational, compacting garbage collector written in C#
 *
 * (C) 2007 William Lahti. This software is licensed under the terms of the GNU Lesser
 * General Public License.
 *
 * GC.cs -- The bulk of the SharpGC code
 *
 */
 
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace SharpGC {
	public unsafe delegate void Finalizer(void *objState, void *gcState);
	public unsafe delegate GCField *ObjectWalk(void *objState);
	
	/**
		<summary>
			The GCHandle structure is used to track object state 
			which is important to the garbage collector. The life time
			of a GCHandle is tied to the life time of a managed object.
		</summary>
		<remarks>
			
		</remarks>
	*/
	public unsafe struct GCHandle {
		public int Generation;
		public bool Finalize;
		
		public void *ObjectState;
		public void *Finalizer;
		public GCData *Data;
		
		public GCHandle *Next;
		public GCHandle *Last;
		
		public static GCHandle *Add(GCHandle *list, void *objState)
		{
			GCHandle *hndl = Alloc(objState);
			GCHandle *last = list;
			
			if (last != null) {
				while (last->Next != null)
					last = last->Next;
					
				last->Next = hndl;
				hndl->Last = last;
			}
			
			return hndl;
		}
		
		public static GCHandle *Alloc(void *objState)
		{
			GCHandle *hndl = (GCHandle*)GCMem.Alloc(sizeof(GCHandle), "GCHandle");
			
			hndl->Generation = 0;
			hndl->Finalize = true;
			hndl->ObjectState = objState;
			hndl->Finalizer = null;
			hndl->Next = null;
			hndl->Last = null;
			
			return hndl;
		}
		
		/**
			<summary>
				Removes <paramref name="hndl" /> from <paramref name="list" />. 
				Returns the new address for the list.
			</summary>
		*/
		public static GCHandle *Remove(GCHandle *list, GCHandle *hndl)
		{
			GCHandle *ret = list;
			GCHandle *prev = hndl->Last, next = hndl->Next;
			
			if (list == hndl && prev != null)
				throw new Exception("Handle list is corrupted: first entry has a Last pointer");
				
			if (prev != null)
				prev->Next = next;
				
			if (next != null)
				next->Last = prev;
			
			hndl->Next = hndl->Last = null;
			
			if (list == hndl)
				ret = next;
				
			Dealloc(hndl);
			
			return ret;
		}
		
		public static void Dealloc(GCHandle *hndl)
		{
			GCMem.Dealloc(hndl, "GCHandle");
		}
	}
	
	[Flags]
	public enum GCFlags {
		None = 0,
		
		MemoryLock,		// storage is locked, may not move
		KeepAlive,		// object may not be collected
	}
	
	public enum GCStatus: int {
		Active,			// object is in use
		Collected,		// object has been collected by GC
		Finalized,		// object has been finalized by runtime
		Resurrected,		// object was resurrected at finalization
	}
	
	/**
		<summary>
			<para>This structure is passed to the runtime, and acts like an
			ongoing interface between the GC and the runtime for a 
			given object. The memory allocated from the managed heap
			is available here from Memory, and that pointer is also
			updated by the heap when compaction causes the data to
			be slid down the stack.</para>
			
			<para>The Size field holds the size of the data allocated
			on the managed heap for this object.</para>
		</summary>
	*/
	public unsafe struct GCData {
		public void *GCHandle;
		public void *Memory;
		public int Size;
		public GCFlags Flags;
		public GCStatus Status;
		
		public static GCData *Alloc(int initDataSize, GCHandle *hndl)
		{
			GCData *dat = Alloc();
			
			dat->GCHandle = hndl;
			dat->Memory = ManagedHeap.Allocate(initDataSize, hndl);
			dat->Size = initDataSize;
			dat->Flags = 0;
			dat->Status = GCStatus.Active;
			
			// TODO!!
			return dat;
		}
		
		public static GCData *Alloc()
		{
			GCData *dat = (GCData*)GCMem.Alloc(sizeof(GCData), "GCData");
			
			dat->Memory = null;
			dat->Size = 0;
			
			return dat;
		}
		
		public static void Dealloc(GCData *dat)
		{
			if (dat == null)
				throw new ArgumentNullException("dat");
				
			ManagedHeap.Deallocate(dat->Memory); dat->Memory = null;
			GCMem.Dealloc(dat, "GCData");
		}
	}
	
	public unsafe struct GCField {
		public void *Reference;	
		public GCField *Next;
		
		public static GCField *Alloc(void *reference)
		{
			GCField *field = (GCField*)GCMem.Alloc(sizeof(GCField), "GCField");
			
			field->Reference = reference;
			field->Next = null;
			
			return field;
		}
		
		public static GCField *Copy(GCField *field)
		{
			return Alloc(field->Reference);
		}
		
		public static GCField *CopyList(GCField *list)
		{
			GCField *nlist = null;
			GCField *iter = list;
			
			while (iter != null) {
				nlist = GCField.Add(nlist, iter->Reference);
				iter = iter->Next;
			}
			
			return nlist;
		}
		
		public static GCField *Add(GCField *list_start, void *reference)
		{
			GCField *field = Alloc(reference);
			
			return Add(list_start, field);
		}
		
		public static GCField *Add(GCField *list_start, GCField *field)
		{
			GCField *iter = list_start;
			GCField *ret = list_start;
			
			if (iter != null) {
				while (iter->Next != null)
					iter = iter->Next;
				
				iter->Next = field;
			} else {
				ret = field;
			}
			
			return ret;
		}
		
		public static void Dealloc(GCField *field)
		{
			GCMem.Dealloc(field, "GCField");
		}
		
		public static void DeallocList(GCField *list)
		{
			GCField *cur, last;
			
			cur = list;
			last = null;
			
			while (cur != null) {
			
				if (last != null)
					Dealloc(last);
					
				if (cur->Next == null) {
					Dealloc(cur);
					break;
				}
			
				last = cur;
				cur = cur->Next;
			}
		}
	}
	
	public unsafe struct GCHandleList {
		public GCHandle *Handle;
		public GCHandleList *Next;
		
		public static GCHandleList *Alloc()
		{
			GCHandleList *list = (GCHandleList*)GCMem.Alloc(sizeof(GCHandleList), "GCHandleList");
			
			list->Handle = null;
			list->Next = null;
			
			return list;
		}
		
		public static void Dealloc(GCHandleList *list)
		{
			GCHandleList *iter = list;
			GCHandleList *last = null;
			
			while (iter != null) {
				if (last != null) {
					GCMem.Dealloc(last, "GCHandleList");
				}
				
				if (iter->Next == null) {
					GCMem.Dealloc(iter, "GCHandleList");
					break;
				}
				
				last = iter;
				iter = iter->Next;
			}
		}
	}
	
	public unsafe struct GCFieldStackFrame {
		public GCField *Field;
		public GCFieldStackFrame *Next;
		
		public static GCFieldStackFrame *Alloc()
		{
			GCFieldStackFrame *frame = (GCFieldStackFrame*)GCMem.Alloc(sizeof(GCFieldStackFrame), 
									"GCFieldStackFrame");
			
			frame->Field = null;
			frame->Next = null;
			
			return frame;
		}
		
		public static void Dealloc(GCFieldStackFrame *frame)
		{
			GCMem.Dealloc(frame, "GCFieldStackFrame");
		}
	}
	
	public unsafe struct GCFieldStack {
		public GCFieldStackFrame *Top;
		
		public static GCFieldStack *Alloc()
		{
			GCFieldStack *stack = (GCFieldStack*)GCMem.Alloc(sizeof(GCFieldStack), "GCFieldStack");
			
			stack->Top = null;
			
			return stack;
		}
		
		public static int GetCount(GCFieldStack *stack)
		{
			int count = 0;
			GCFieldStackFrame *ent = stack->Top;
			
			while (ent != null) {
				++count;
				ent = ent->Next;
			}
			
			return count;
		}
		
		public static void Push(GCFieldStack *stack, GCField *field)
		{
			if (stack == null)
				throw new ArgumentNullException("stack");
				
			GCFieldStackFrame *frame = GCFieldStackFrame.Alloc();
			
			frame->Field = field;
			frame->Next = stack->Top;
			stack->Top = frame;
		}
		
		public static GCField *Pop(GCFieldStack *stack)
		{
			GCFieldStackFrame *frame = stack->Top;
			GCField *field = frame->Field;
			
			stack->Top = stack->Top->Next;
			
			GCFieldStackFrame.Dealloc(frame); frame = null;
			
			return field;
		}
		
		public static void Clear(GCFieldStack *stack)
		{
			while (stack->Top != null)
				Pop(stack);
		}
		
		public static void Dealloc(GCFieldStack *stack)
		{
			if (GetCount(stack) > 0)
				Clear(stack);
				
			GCMem.Dealloc(stack, "GCFieldStack");
		}
	}
	
	public unsafe struct GCMarkResult {
		public GCHandle *Subject;
		public GCMarkResult *Next;
		
		public static GCMarkResult *Alloc(GCHandle *subj)
		{
			GCMarkResult *result = (GCMarkResult*)GCMem.Alloc(sizeof(GCMarkResult), "GCMarkResult");
			
			result->Subject = subj;
			result->Next = null;
			
			return result;
		}
		
		public static void Dealloc(GCMarkResult *results)
		{
			GCMarkResult *result = results;
			GCMarkResult *last = null;
			
			while (result != null) {
				if (last != null) {
					GCMem.Dealloc(last, "GCMarkResult");
				}
				
				if (result->Next == null) {
					GCMem.Dealloc(result, "GCMarkResult");
					break;
				}
				last = result;
				result = result->Next;
			}
		}
		
		public static bool Add(GCMarkResult *list, GCHandle *subject)
		{
			if (list == null)
				throw new ArgumentNullException("list");
				
			GCMarkResult *res = list;
			
			while (res->Next != null) {
				if (res->Subject == subject)
					return false;
				
				res = res->Next;
			}
			
			res->Next = Alloc(subject);
			
			return true;
		}
	}
	
	public unsafe class GCMem {
		[DllImport("libc")]
			static extern void *malloc(int bytes);
		[DllImport("libc")]
			static extern void free(void *ptr);
		
		public static unsafe void Dealloc(void *ptr)
		{
			if (ptr == null)
				throw new ArgumentNullException("ptr");
				
			free(ptr);
		}
		
		public static unsafe void *Alloc(int bytes)
		{
			return malloc(bytes);
		}
		
		public static unsafe void *Alloc(int bytes, string reason)
		{
			return malloc(bytes);
		}
		
		public static unsafe void Dealloc(void *ptr, string reason)
		{
			if (ptr == null)
				throw new ArgumentNullException("ptr");
				
			free(ptr);
		}
	}
	
	public unsafe class GC {
		private GC()
		{ }
	
		// Private State
		
		static int _MaxGeneration = 0;
		static long _ManagedMemory = 0, _UnmanagedMemory = 0;
		static long _AvailableMemory = 0;
		static GCHandle *_Handles = null;
		static Finalizer _Finalizer = null;
		static ObjectWalk _ObjectWalk = null;
		// Private Utilities
		
		static GCHandle *_GetHandle(void *objHandle)
		{
			GCHandle *ptr = _Handles;
			
			while (ptr != null) {
				if (ptr->ObjectState == objHandle)
					return ptr;
				
				ptr = ptr->Next;
			}
			
			return null;
		}
		
		static GCHandle *_RegisterObject(void *objHandle, int initDataSize)
		{
			// TODO: add memory size checks, throw out of memory exceptions?
			
 			GCHandle *newhl = GCHandle.Add(_Handles, objHandle);
			
			if (_Handles == null)
				_Handles = newhl;
				
			newhl->ObjectState = objHandle;
			newhl->Data = GCData.Alloc(initDataSize, newhl);
			
			return newhl;
		}
		
		// Public Properties
		
		public static int MaxGeneration {
			get {
				return _MaxGeneration;
			}
		}
		
		// Public Methods
		
		public static void ClearGCState(void *gcState)
		{
			GCHandle *gobj = (GCHandle*)gcState;
			
			// delete object, remove GC handle
			GCData.Dealloc(gobj->Data); gobj->Data = null;
			_Handles = GCHandle.Remove(_Handles, gobj); gobj = null;
		}
		
		public static void Initialize(Finalizer fin, ObjectWalk walk, int heapSize)
		{
			_Finalizer = fin;
			_ObjectWalk = walk;
			
			ManagedHeap.Initialize(heapSize);
		}
		
		public static void AddMemoryPressure(long bytesAllocated)
		{
			_UnmanagedMemory += bytesAllocated;
		}
		
		public static void RemoveMemoryPressure(long bytesDeallocated)
		{
			_UnmanagedMemory -= bytesDeallocated;
		}
		
		public static unsafe GCData *Allocate(void *objHandle, int initDataSize)
		{
			return _RegisterObject(objHandle, initDataSize)->Data;
		}
		
		public static GCData *Allocate(void *objHandle, int initDataSize, Finalizer finalize)
		{
			GCHandle *hndl = _RegisterObject(objHandle, initDataSize);
			
			hndl->Finalizer = (void*)Marshal.GetFunctionPointerForDelegate(finalize);
			
			return hndl->Data;
		}
		
		public static void SetGlobalFinalizer(Finalizer f)
		{
			_Finalizer = f;
		}
		
		public static void SetObjectWalk(ObjectWalk walk)
		{
			_ObjectWalk = walk;
		}
		
		public static void Collect(GCField *root_list)
		{
			Collect(MaxGeneration+1, root_list);
		}
		
		/**
			<summary>
				Collects garbage throughout the generation named by
				<paramref name="generation" />. 
			</summary>
		*/
		public unsafe static void Collect(int generation, GCField *root_list)
		{
			if (generation < 0)
				throw new ArgumentOutOfRangeException("generation < 0");
			
			if (_ObjectWalk == null)
				throw new Exception("GC: Collect() without Initialize() first!");
			
			// mark 
			
			GCMarkResult *results = null;
			GCField *field = root_list, first_field = root_list;
			GCFieldStack *stack = GCFieldStack.Alloc(), header_stack = GCFieldStack.Alloc();
			
			int depth = 0;
			
			if (field != null) while (true) {
				// check if we've already walked this object
				
				GCMarkResult *prev_res = results;
				bool handled_prev = false;
				
				while (prev_res != null) {
					if (prev_res->Subject->ObjectState == field->Reference) {
						handled_prev = true;
						break;
					}
					
					prev_res = prev_res->Next;
				}
				
				if (!handled_prev) {
					GCHandle *mark_hndl = _GetHandle(field->Reference);
					
					if (mark_hndl == null) {
						Console.WriteLine("ptr: 0x{0}", ((int)field->Reference).ToString("x"));
						throw new Exception("field references object that has no GCHandle");
					}
					
					if (results == null) {
						results = GCMarkResult.Alloc(mark_hndl);
					} else {
						GCMarkResult.Add(results, mark_hndl);
					}
				
					// walk children if any
					
					GCField *child_list = _ObjectWalk(field->Reference);
					
					if (child_list != null) {
						GCFieldStack.Push(header_stack, first_field);
						GCFieldStack.Push(stack, field->Next);
						
						++depth;
						
						field = first_field = child_list;
						
						continue;
					}
				}
				
				// walk to next field 
				
				if (field->Next == null) {
					bool done = false;
					
					// dealloc field list for this level
					GCField.DeallocList(first_field);
					
					// go up to the next level that has fields to process
					do {
						if (GCFieldStack.GetCount(header_stack) == 0) {
							done = true;
							break;
						}
							
						first_field = GCFieldStack.Pop(header_stack);
						field = GCFieldStack.Pop(stack);
					} while (field == null);
					
					if (done) 
						break;
				} else {
					field = field->Next;
				}
				
			}
			
			// delete the field stacks used to track walking
			GCFieldStack.Dealloc(stack);
			GCFieldStack.Dealloc(header_stack);
			
			// generate a list of garbage
			GCHandleList *garbage_list = null;
			GCHandleList *garbage = null; 
			GCMarkResult *mark = null;
			GCHandle *hndl_iter = _Handles;
			
			while (hndl_iter != null) {
				bool found = false;
				
				mark = results;
				while (mark != null) {
					if (mark->Subject == null)
						throw new Exception("invalid mark result has no subject");
						
					if (mark->Subject->ObjectState == hndl_iter->ObjectState) {
						found = true;
						break;
					}
					
					mark = mark->Next;
				}
				
				if (!found) {
					// If the KeepAlive flag is not set, this object is garbage
					if ((hndl_iter->Data->Flags & GCFlags.KeepAlive) == 0) {
						GCHandleList *newent = GCHandleList.Alloc();
						
						if (garbage == null) {
							garbage_list = garbage = newent;
						} else {
							garbage->Next = newent;
							garbage = newent;
						}
						
						newent->Handle = hndl_iter;
					}
				}
				
				hndl_iter = hndl_iter->Next;
			}
			
			// delete the mark results
			
			GCMarkResult.Dealloc(results);
			
			garbage = garbage_list;
			
			while (garbage != null) {
				GCHandle *gobj = garbage->Handle;
				
				if (gobj->Finalize) {
					if (gobj->Finalizer != null) {
						Finalizer obj_fin = Marshal.GetDelegateForFunctionPointer(
									(IntPtr)gobj->Finalizer, typeof(Finalizer)) as Finalizer;
						obj_fin(gobj->ObjectState, null);
					}
					
					if (_Finalizer != null)
						_Finalizer(gobj->ObjectState, gobj);
				}
				
				garbage = garbage->Next;
			}
			
			// delete handle list
			garbage = null;
			GCHandleList.Dealloc(garbage_list); garbage_list = null;
			
			// compacts the heap, updates GCData pointers
			ManagedHeap.Compact();
		}
		
		public static int CollectionCount(int generation)
		{
			// TODO
			return 0;
		}
		
		public static int GetGeneration(void *objHandle)
		{
			return _GetHandle(objHandle)->Generation;
		}
		
		/**
			<summary>
				Gets the total amount of memory allocated,
				according to the GC. 
			</summary>
			<remarks>
				This method does not match the System.GC 
				interface, because the GC needs a list of
				application roots to collect garbage. The
				System.GC class should have the runtime
				initiate garbage collection if the 
				forceFullCollection parameter is true.
			</remarks>
		*/
		public static long GetTotalMemory()
		{
			return _ManagedMemory + _UnmanagedMemory;
		}
		
		public static void ReRegisterForFinalize(void *objHandle)
		{
			GCHandle *hndl = _GetHandle(objHandle);
			
			if (hndl != null) 
				hndl->Finalize = true;
		}
		
		public static void SuppressFinalize(void *objHandle)
		{
			GCHandle *hndl = _GetHandle(objHandle);
			
			if (hndl != null)
				hndl->Finalize = false;
		}
		
		private static int CountHandles()
		{
			GCHandle *hndl = _Handles;
			int count = 0;
			
			while (hndl != null) {
				++count;
				hndl = hndl->Next;	
			}
			
			return count;
		}
		
		public static void PrintStats()
		{
			Console.WriteLine("SharpGC Statistics");
			Console.WriteLine();
			Console.WriteLine("Handle List Address: 0x{0}", ((int)_Handles).ToString("x"));
			Console.WriteLine("Handle Count: {0}", CountHandles());
		}
		
		// WaitForPendingFinalizers
	}
}