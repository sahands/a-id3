using System;
using System.Collections.Generic;
using System.Text;
using Achamenes.ID3.Fields;

namespace Achamenes.ID3.Frames.Parsers
{
	/// <summary>
	/// Base class for all URL frame parsers.
	/// Provides a helper function to parse URL frames.
	/// </summary>
	public abstract class UrlFrameParser : FrameParser
	{
		/// <summary>
		/// Returns the URL string encoded in a URL frame.
		/// </summary>
		/// <param name="data">Byte array to parse the text frame out of.</param>
		/// <returns>The string encoded in the text frame.</returns>
		protected static string ParseUrlFrame(byte[] data)
		{
			TextField textField=TextField.CreateTextField(false, EncodingScheme.Ascii);
			textField.Parse(data, 0);
			return textField.Text;
		}
	}
}


