// 
// (C) 2006-2007 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Mircea-Cristian Racasan <darx_kies@gmx.net>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;
using System.Text;
using SharpOS.AOT.IR;
using SharpOS.AOT.Metadata;

namespace SharpOS.AOT.IR {
	public class ExceptionHandlingClause {
		private Block tryBegin = null;

		/// <summary>
		/// Gets or sets the try begin.
		/// </summary>
		/// <value>The try begin.</value>
		public Block TryBegin
		{
			get
			{
				return tryBegin;
			}
			set
			{
				tryBegin = value;
			}
		}
		private Block tryEnd = null;

		/// <summary>
		/// Gets or sets the try end.
		/// </summary>
		/// <value>The try end.</value>
		public Block TryEnd
		{
			get
			{
				return tryEnd;
			}
			set
			{
				tryEnd = value;
			}
		}
		private Block filterBegin = null;

		/// <summary>
		/// Gets or sets the filter begin.
		/// </summary>
		/// <value>The filter begin.</value>
		public Block FilterBegin
		{
			get
			{
				return filterBegin;
			}
			set
			{
				filterBegin = value;
			}
		}
		private Block filterEnd = null;

		/// <summary>
		/// Gets or sets the filter end.
		/// </summary>
		/// <value>The filter end.</value>
		public Block FilterEnd
		{
			get
			{
				return filterEnd;
			}
			set
			{
				filterEnd = value;
			}
		}
		private Block handlerBegin = null;

		/// <summary>
		/// Gets or sets the handler begin.
		/// </summary>
		/// <value>The handler begin.</value>
		public Block HandlerBegin
		{
			get
			{
				return handlerBegin;
			}
			set
			{
				handlerBegin = value;
			}
		}
		private Block handlerEnd = null;

		/// <summary>
		/// Gets or sets the handler end.
		/// </summary>
		/// <value>The handler end.</value>
		public Block HandlerEnd
		{
			get
			{
				return handlerEnd;
			}
			set
			{
				handlerEnd = value;
			}
		}
		private Class _class = null;

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>The type.</value>
		public Class Class
		{
			get
			{
				return _class;
			}
			set
			{
				_class = value;
			}
		}

		private ExceptionHandlerType type;

		public ExceptionHandlerType Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
			}
		}
	}
}
