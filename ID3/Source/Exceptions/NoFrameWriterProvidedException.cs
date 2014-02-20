using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3
{
	/// <summary>
	/// NoFrameWriterProvidedException is thrown when an ID3 Frame is to be written but
	/// it does not support write feature for the given version and/or encoding.
	/// </summary>
	[global::System.Serializable]
	public class NoFrameWriterProvidedException : NonFatalException
	{
		private Frames.Frame _frame;

		/// <summary>
		/// The Frame that did not support the write operation.
		/// </summary>
		public Frames.Frame Frame
		{
			get
			{
				return _frame;
			}
			set
			{
				_frame = value;
			}
		}

		private ID3v2MajorVersion _version;

		/// <summary>
		/// The ID3 v2 major version of the tag to be written.
		/// </summary>
		public ID3v2MajorVersion Version
		{
			get
			{
				return _version;
			}
			set
			{
				_version = value;
			}
		}

		
		/// <param name="frameID">The ID of the unrecognized frame.</param>
		

		/// <summary>
		/// Initializes a new instance of NoFrameWriterProvidedException.
		/// </summary>
		/// <param name="frame">The frame that did not support the write operation.</param>
		/// <param name="version">The ID3 v2 major version of the tag in which the frame was to be written.</param>
		public NoFrameWriterProvidedException(Frames.Frame frame, ID3v2MajorVersion version)
		{
			this._frame=frame;
			this._version=version;
		}

		/// <summary>
		/// Initializes a new instance of NoFrameWriterProvidedException.
		/// </summary>
		/// <param name="frame">The frame that did not support the write operation.</param>
		/// <param name="version">The ID3 v2 major version of the tag in which the frame was to be written.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		public NoFrameWriterProvidedException(Frames.Frame frame, ID3v2MajorVersion version,string message) : base(message)
		{
			this._frame=frame;
			this._version=version;
		}

		/// <summary>
		/// Initializes a new instance of NoFrameWriterProvidedException.
		/// </summary>
		/// <param name="frame">The frame that did not support the write operation.</param>
		/// <param name="version">The ID3 v2 major version of the tag in which the frame was to be written.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public NoFrameWriterProvidedException(Frames.Frame frame, ID3v2MajorVersion version,string message, Exception inner) : base(message, inner)
		{
			this._frame=frame;
			this._version=version;
		}

		/// <summary>
		/// Creates a new instance of FrameParsingException.
		/// </summary>		
		/// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
		protected NoFrameWriterProvidedException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
		}
	}
}
