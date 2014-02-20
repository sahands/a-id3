using System;
using System.Collections.Generic;
using System.Text;
using Achamenes.ID3.Fields;

namespace Achamenes.ID3.Frames.Parsers
{
	class UniqueFileIdentifierFrameParser : FrameParser
	{
		protected override Frame ParseFrame(byte[] data)
		{
			int place=0;

			TextField ownerField=TextField.CreateTextField(true, EncodingScheme.Ascii);
			place+=ownerField.Parse(data, place);
			
			BinaryField dataField=new BinaryField();
			place+=dataField.Parse(data, place);

			return new UniqueFileIdentifierFrame(dataField.Data,ownerField.Text);
		}
	}	
}

