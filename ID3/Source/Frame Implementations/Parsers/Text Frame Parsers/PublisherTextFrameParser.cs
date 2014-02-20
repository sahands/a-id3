using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames.Parsers
{
	class PublisherTextFrameParser : TextFrameParser
	{
		protected override Frame ParseFrame(byte[] data)
		{
			return new PublisherTextFrame(ParseTextFrame(data));
		}
	}
}

