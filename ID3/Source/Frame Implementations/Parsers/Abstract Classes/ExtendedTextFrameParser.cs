using System;
using System.Collections.Generic;
using System.Text;
using Achamenes.ID3.Fields;

namespace Achamenes.ID3.Frames.Parsers
{
	abstract class ExtendedTextFrameParser : FrameParser
	{
		protected static string ParseExtendedTextFrame(byte[] data, out string description, out LanguageCode language)
		{
			int place=0;

			SingleByteField encodingField=new SingleByteField();
			place+=encodingField.Parse(data, place);

			FixedLengthAsciiTextField langField=new FixedLengthAsciiTextField(3);
			place+=langField.Parse(data, place);

			TextField descriptinoField=TextField.CreateTextField(true,(EncodingScheme)encodingField.Value);
			place+=descriptinoField.Parse(data, place);

			TextField textField=TextField.CreateTextField(false,(EncodingScheme)encodingField.Value);
			place+=textField.Parse(data, place);

			if(!Enum.IsDefined(typeof(LanguageCode), langField.Text))
			{
				langField.Text="Unknown";
			}
			language=(LanguageCode)Enum.Parse(typeof(LanguageCode), langField.Text);
			description=descriptinoField.Text;
			return textField.Text;
		}
	}
}


