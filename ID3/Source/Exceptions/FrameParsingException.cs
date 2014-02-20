using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3
{
	/// <summary>
	/// FrameParsingException is thrown when an error is found while trying to parse a frame.
	/// </summary>
	[global::System.Serializable]
	public class FrameParsingException : NonFatalException
	{
		/// <summary>
		/// Creates a new instance of FrameParsingException.
		/// </summary>
		public FrameParsingException()
		{
		}
		/// <summary>
		/// Creates a new instance of FrameParsingException.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		public FrameParsingException(string message)
			: base(message)
		{
		}
		/// <summary>
		/// Creates a new instance of FrameParsingException.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public FrameParsingException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>
		/// Creates a new instance of FrameParsingException.
		/// </summary>		
		/// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
		protected FrameParsingException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
		}
	}
}
