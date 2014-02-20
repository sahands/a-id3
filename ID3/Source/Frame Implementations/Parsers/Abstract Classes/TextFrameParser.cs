using System;
using System.Collections.Generic;
using System.Text;
using Achamenes.ID3.Fields;

namespace Achamenes.ID3.Frames.Parsers
{
	/// <summary>
	/// Base class for all text frame parsers.
	/// Provides a helper function to parse text frames.
	/// </summary>
	public abstract class TextFrameParser : FrameParser
	{
		/// <summary>
		/// Returns the text encoded in a text frame.
		/// </summary>
		/// <param name="data">Byte array to parse the text frame out of.</param>
		/// <returns>The string encoded in the text frame.</returns>
		protected static string ParseTextFrame(byte[] data)
		{
			//Text encoding                $xx
			//Information                  <text string according to encoding>
			int place=0;

			SingleByteField encodingField=new SingleByteField();
			place+=encodingField.Parse(data, place);

			TextField textField=TextField.CreateTextField(false, (EncodingScheme)encodingField.Value);
			place+=textField.Parse(data, place);

			return textField.Text;
		}		
	}	
}


