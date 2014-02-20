using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames.Parsers
{
	class TitleTextFrameParser : TextFrameParser
	{
		protected override Frame ParseFrame(byte[] data)
		{
			return new TitleTextFrame(ParseTextFrame(data));
		}
	}
}


