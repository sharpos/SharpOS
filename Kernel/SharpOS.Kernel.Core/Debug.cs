using SharpOS.AOT.X86;
using SharpOS.ADC;
using SharpOS.Memory;

namespace SharpOS {
	public unsafe partial class Kernel {
		public static void Panic (byte *msg, KernelStage stage, KernelError code)
		{
			TextMode.SetAttributes (TextColor.Red, TextColor.Black);

			TextMode.Write (String ("Panic! -- "));
			TextMode.WriteLine (msg);
			
			TextMode.Write (String ("  Stage: "));
			TextMode.WriteNumber ((int)stage, false);
			TextMode.WriteLine ();
			
			TextMode.Write (String ("  Error: "));
			TextMode.WriteNumber ((int)code, false);
			TextMode.WriteLine ();
			
			TextMode.RestoreAttributes ();
			
			Halt ();
		}
		
		public static void Panic (byte *msg)
		{
			Panic (msg, KernelStage.Unknown, KernelError.Unknown);
		}
		
		public static void Assert (bool cond, byte *msg)
		{
			if (!cond) {
				TextMode.Write (String ("Assertion Failed: "));
				TextMode.WriteLine (msg);
				
				Halt ();
			}
		}

		public static void AssertSuccess (uint err, byte *msg)
		{
			if (err != 0) {
				TextMode.Write (String ("Error: "));
				TextMode.WriteNumber ((int)err, false);

				Assert (false, msg);
			}
		}
		
		public static void Warning (byte *msg)
		{
			TextMode.Write (String ("Warning: "));
			TextMode.WriteLine (msg);
			TextMode.WriteLine ();
		}
		
		public static void Message (byte *msg)
		{
			TextMode.WriteLine (msg);
		}
	}
}