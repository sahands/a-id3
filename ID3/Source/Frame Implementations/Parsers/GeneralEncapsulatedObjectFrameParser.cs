using System;
using System.Collections.Generic;
using System.Text;
using Achamenes.ID3.Fields;

namespace Achamenes.ID3.Frames.Parsers
{
	class GeneralEncapsulatedObjectFrameParser 
		: FrameParser
	{
		protected override Frame ParseFrame(byte[] data)
		{
			//Text encoding          $xx
			//MIME type              <text string> $00
			//Filename               <text string according to encoding> $00 (00)
			//Content description    <text string according to encóding> $00 (00)
			//Encapsulated object    <binary data>
			
			int place=0;

			SingleByteField encodingField=new SingleByteField();
			place+=encodingField.Parse(data, place);

			TextField mimeTypeField=TextField.CreateTextField(true,EncodingScheme.Ascii);
			place+=mimeTypeField.Parse(data, place);

			TextField fileNameField=TextField.CreateTextField(true,(EncodingScheme)encodingField.Value);
			place+=fileNameField.Parse(data, place);

			TextField contentDescriptionField=TextField.CreateTextField(true, (EncodingScheme)encodingField.Value);
			place+=contentDescriptionField.Parse(data, place);

			BinaryField dataField=new BinaryField();
			place+=dataField.Parse(data, place);

			return new GeneralEncapsulatedObjectFrame(
				fileNameField.Text,
				contentDescriptionField.Text,
				mimeTypeField.Text,
				dataField.Data);
		}
	}
}


