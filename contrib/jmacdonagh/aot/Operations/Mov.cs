using System;
using System.IO;

namespace SharpOS.AOT
{
	public sealed class Mov : OperationBase
	{
		#region Private Fields
		
		private OperationSizes _Size;
		private MovOperand _Destination;
		private MovOperand _Source;
		
		#endregion
		
		#region Constructors
		
		private Mov(OperationSizes size)
		{
			_Size = size;
		}
		
		/// <summary>
		/// Instantiates a new Mov instance in the form of __________
		/// </summary>
		public Mov(OperationSizes size, Registers destinationIndirectRegister, int destinationOffset, Registers source) : this(size)
		{
			_Destination = new EffectiveAddress(destinationIndirectRegister, destinationOffset);
			_Source = new DirectRegister(source);
		}
		
		/// <summary>
		/// Instantiates a new Mov instance in the form of __________
		/// </summary>
		public Mov(OperationSizes size, Registers destinationIndirectRegister, int destinationOffset, int sourceConstant) : this(size)
		{
			_Destination = new EffectiveAddress(destinationIndirectRegister, destinationOffset);
			_Source = new Constant(sourceConstant);
		}
		
		/// <summary>
		/// Instantiates a new Mov instance in the form of __________
		/// </summary>
		public Mov(OperationSizes size, Registers destination, Registers sourceIndirectRegister, int sourceOffset) : this(size)
		{
			_Destination = new DirectRegister(destination);
			_Source = new EffectiveAddress(sourceIndirectRegister, sourceOffset);
		}
		
		#endregion
		
		#region Properties
		
		/// <summary>
		/// The size of the Mov operation.
		/// </summary>
		public OperationSizes Size
		{
			get { return _Size; }
			set { _Size = value; }
		}
		
		/// <summary>
		/// The location where the Source data will be moved.
		/// </summary>
		public MovOperand Destination
		{
			get { return _Destination; }
			set { _Destination = value; }
		}
		
		/// <summary>
		/// The location where the Source data is stored.
		/// </summary>
		public MovOperand Source
		{
			get { return _Source; }
			set { _Source = value; }
		}
		
		#endregion
		
		#region OperationBase Members
		
		public override uint OperationSize
		{
			get { throw new NotImplementedException(); }
		}
		
		public override void WriteBinary(Stream stream)
		{
			throw new NotImplementedException();
		}
		
		#endregion
		
		#region Internal Classes
		
		/// <summary>
		/// An abstract class that represents either a destination or source operand for the Mov operation.
		/// </summary>
		public abstract class MovOperand
		{
		}
		
		/// <summary>
		/// A location whose address is computed by taking the effective address of a register and adding an offset.
		/// </summary>
		public class EffectiveAddress : MovOperand
		{
			private Registers _Register;
			private int _Offset;

			public EffectiveAddress(Registers register, int offset)
			{
				_Register = register;
				_Offset = offset;
			}
			
			/// <summary>
			/// The register to calculate the effective address of.
			/// </summary>
			public Registers Register
			{
				get { return _Register; }
				set { _Register = value; }
			}
			
			/// <summary>
			/// The offset to add to the effective address of the register.
			/// </summary>
			public int Offset
			{
				get { return _Offset; }
				set { _Offset = value; }
			}
		}
		
		/// <summary>
		/// A register that is either a destination, or whose value provides a source.
		/// </summary>
		public class DirectRegister : MovOperand
		{
			private Registers _Register;
			
			public DirectRegister(Registers register)
			{
				_Register = register;
			}
			
			public Registers Register
			{
				get { return _Register; }
				set { _Register = value; }
			}
		}
		
		/// <summary>
		/// A constant that that is used as a source and moved into a destination.
		/// </summary>
		public class Constant : MovOperand
		{
			private int _Value;
			
			public Constant(int val)
			{
				_Value = val;
			}
			
			public int Value
			{
				get { return _Value; }
				set { _Value = value; }
			}
		}
		
		#endregion
	}
}
