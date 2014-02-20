using System;
using System.Collections.Generic;
using System.Text;
using Achamenes.ID3.Fields;

namespace Achamenes.ID3.Frames.Parsers
{
	class MusicCDIdentifierFrameParser : FrameParser
	{
		protected override Frame ParseFrame(byte[] data)
		{
			int place=0;

			BinaryField dataField=new BinaryField();
			place+=dataField.Parse(data, place);

			return new MusicCDIdentifierFrame(dataField.Data);
		}
	}
}

