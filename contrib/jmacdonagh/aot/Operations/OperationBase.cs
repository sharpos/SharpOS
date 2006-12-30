using System;
using System.IO;

namespace SharpOS.AOT
{
	public abstract class OperationBase
	{
		/// <summary>
		/// Gets the size, in bytes, that the operation will generate with its current set of operands.
		/// </summary>
		public abstract uint OperationSize { get; }
		
		/// <summary>
		/// Writes the operation in binary form to the supplied stream.
		/// </summary>
		public abstract void WriteBinary(Stream stream);
	}
}
