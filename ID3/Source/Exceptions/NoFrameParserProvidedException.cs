using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3
{
	/// <summary>
	/// NoFrameParserProvidedException is thrown when an ID3 Frame is to be parsed but
	/// no FrameParser object is registered with the given FrameParserFactory. That is, 
	/// parsing that type of frame is not supported in this implementation.
	/// </summary>
	[global::System.Serializable]
	public class NoFrameParserProvidedException : NonFatalException
	{
		private string _frameID;

		/// <summary>
		/// The ID of the unrecognized frame.
		/// </summary>
		public string FrameID
		{
			get
			{
				return _frameID;
			}
			set
			{
				_frameID = value;
			}
		}

		private ID3v2MajorVersion _version;

		/// <summary>
		/// The ID3 v2 major version of the tag in which the frame was to be parsed.
		/// </summary>
		public ID3v2MajorVersion ID3Version
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

		/// <summary>
		/// Initializes a new instance of NoFrameParserProvidedException.
		/// </summary>
		/// <param name="frameID">The ID of the unrecognized frame.</param>
		/// <param name="version">The ID3 v2 major version of the tag in which the frame was to be parsed.</param>
		public NoFrameParserProvidedException(string frameID, ID3v2MajorVersion version)
		{
			this._frameID=frameID;
			this._version=version;
		}

		/// <summary>
		/// Initializes a new instance of NoFrameParserProvidedException.
		/// </summary>
		/// <param name="frameID">The ID of the unrecognized frame.</param>
		/// <param name="version">The ID3 v2 major version of the tag in which the frame was to be parsed.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		public NoFrameParserProvidedException(string frameID, ID3v2MajorVersion version,string message) : base(message)
		{
			this._frameID=frameID;
			this._version=version;
		}

		/// <summary>
		/// Initializes a new instance of NoFrameParserProvidedException.
		/// </summary>
		/// <param name="frameID">The ID of the unrecognized frame.</param>
		/// <param name="version">The ID3 v2 major version of the tag in which the frame was to be parsed.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public NoFrameParserProvidedException(string frameID, ID3v2MajorVersion version, string message, Exception inner)
			: base(message, inner)
		{
			this._frameID=frameID;
			this._version=version;
		}

		/// <summary>
		/// Creates a new instance of FrameParsingException.
		/// </summary>		
		/// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
		protected NoFrameParserProvidedException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
		}
	}
}
