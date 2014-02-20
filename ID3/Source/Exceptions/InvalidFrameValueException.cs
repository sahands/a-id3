using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3
{
	/// <summary>
	/// InvalidFrameValueException is thrown when an invalid value is specified for a 
	/// given frame. For example, a value longer than 4 characters for the Year frame.
	/// </summary>
	[global::System.Serializable]
	public class InvalidFrameValueException : NonFatalException
	{
		private string _invalidValue;

		/// <summary>
		/// Gets the rejected string value.
		/// </summary>
		public string InvalidValue
		{
			get
			{
				return _invalidValue;
			}
		}

		/// <summary>
		/// Initializes a new instance of InvalidFrameValueException.
		/// </summary>
		/// <param name="invalidValue">The rejected string value.</param>
		public InvalidFrameValueException(string invalidValue)
		{
			this._invalidValue=invalidValue;
		}
		/// <summary>
		/// Initializes a new instance of InvalidFrameValueException.
		/// </summary>
		/// <param name="invalidValue">The rejected string value.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		public InvalidFrameValueException(string invalidValue,string message) : base(message)
		{
			this._invalidValue=invalidValue;
		}
		/// <summary>
		/// Initializes a new instance of InvalidFrameValueException.
		/// </summary>
		/// <param name="invalidValue">The rejected string value.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner"></param>
		public InvalidFrameValueException(string invalidValue, string message, Exception inner)
			: base(message, inner)
		{
			this._invalidValue=invalidValue;
		}

		/// <summary>
		/// Creates a new instance of InvalidFrameValueException.
		/// </summary>		
		/// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>		
		protected InvalidFrameValueException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
		}
	}
}
