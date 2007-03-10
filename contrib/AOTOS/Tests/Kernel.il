.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 1:0:0:0
}
.assembly SharpOS
{
  .ver 1:0:0:0
}
.module SharpOS.dll

.class public auto ansi beforefieldinit SharpOS.Kernel
       extends [mscorlib]System.Object
{
  .field public static uint8* BootMessage
  .method public hidebysig static uint8* 
          String(string 'value') cil managed
  {
    .maxstack  1
    .locals init ([0] uint8* CS$1$0000)
    IL_0000:  nop
    IL_0001:  ldc.i4.0
    IL_0002:  conv.u
    IL_0003:  stloc.0
    IL_0004:  br.s       IL_0006

    IL_0006:  ldloc.0
    IL_0007:  ret
  } 

  .method public hidebysig static void  BootEntry() cil managed
  {
    .maxstack  8
    
	call void Wrapper::Run()

    ret
  } 

  .method public hidebysig static void  WriteMessage(uint8* message) cil managed
  {
    .maxstack  5
    .locals init ([0] uint8* video,
             [1] int32 i,
             [2] bool CS$4$0000)
    IL_0000:  nop
    IL_0001:  ldc.i4     0xb8000
    IL_0006:  conv.i
    IL_0007:  stloc.0
    IL_0008:  ldc.i4.0
    IL_0009:  stloc.1
    IL_000a:  br.s       IL_0025

    IL_000c:  nop
    IL_000d:  ldloc.0
    IL_000e:  dup
    IL_000f:  ldc.i4.1
    IL_0010:  conv.i
    IL_0011:  add
    IL_0012:  stloc.0
    IL_0013:  ldarg.0
    IL_0014:  ldloc.1
    IL_0015:  dup
    IL_0016:  ldc.i4.1
    IL_0017:  add
    IL_0018:  stloc.1
    IL_0019:  add
    IL_001a:  ldind.u1
    IL_001b:  stind.i1
    IL_001c:  ldloc.0
    IL_001d:  dup
    IL_001e:  ldc.i4.1
    IL_001f:  conv.i
    IL_0020:  add
    IL_0021:  stloc.0
    IL_0022:  ldc.i4.7
    IL_0023:  stind.i1
    IL_0024:  nop
    IL_0025:  ldarg.0
    IL_0026:  ldloc.1
    IL_0027:  add
    IL_0028:  ldind.u1
    IL_0029:  ldc.i4.0
    IL_002a:  ceq
    IL_002c:  ldc.i4.0
    IL_002d:  ceq
    IL_002f:  stloc.2
    IL_0030:  ldloc.2
    IL_0031:  brtrue.s   IL_000c

    IL_0033:  ret
  } 

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  ret
  } 

  .method private hidebysig specialname rtspecialname static 
          void  .cctor() cil managed
  {
    // Code size       16 (0x10)
    .maxstack  8
    IL_0000:  ldstr      bytearray (47 00 72 00 75 00 62 00 20 00 2D 00 3E 00 20 00   // G.r.u.b. .-.>. .
                                    42 00 6F 00 6F 00 74 00 69 00 6E 00 67 00 20 00   // B.o.o.t.i.n.g. .
                                    5B 00 53 00 68 00 61 00 72 00 70 00 4F 00 53 00   // [.S.h.a.r.p.O.S.
                                    5D 00 20 00 32 00 30 00 30 00 37 00 20 00 62 00   // ]. .2.0.0.7. .b.
                                    79 00 20 00 4D 00 69 00 72 00 63 00 65 00 61 00   // y. .M.i.r.c.e.a.
                                    2D 00 43 00 72 00 69 00 73 00 74 00 69 00 61 00   // -.C.r.i.s.t.i.a.
                                    6E 00 20 00 52 00 61 00 63 00 61 00 73 00 61 00   // n. .R.a.c.a.s.a.
                                    6E 00 20 00 28 00 01 00 29 00 )                   // n. .(...).
    IL_0005:  call       uint8* SharpOS.Kernel::String(string)
    IL_000a:  stsfld     uint8* SharpOS.Kernel::BootMessage
    IL_000f:  ret
  } 

} 
