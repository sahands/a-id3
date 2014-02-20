using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3
{
	/// <summary>
	/// NonFatalException is the base class for all the exceptions that should
	/// not interrupt the process of reading or writing an ID3 tag.
	/// </summary>
	[global::System.Serializable]
	public class NonFatalException : ApplicationException
	{
		/// <summary>
		/// Creates a new instance of NonFatalException.
		/// </summary>
		public NonFatalException()
		{
		}
		/// <summary>
		/// Creates a new instance of NonFatalException.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		public NonFatalException(string message)
			: base(message)
		{
		}
		/// <summary>
		/// Creates a new instance of NonFatalException.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public NonFatalException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>
		/// Creates a new instance of NonFatalException.
		/// </summary>		
		/// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
		protected NonFatalException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
		}
	}
}
