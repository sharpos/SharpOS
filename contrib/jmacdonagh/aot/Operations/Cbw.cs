using System;
using System.IO;

namespace SharpOS.AOT
{
	public class Cbw : OperationBase
	{
		public override uint OperationSize
		{
			get { return 1; }
		}
		
		public override void WriteBinary(Stream stream)
		{
		}
	}
}
