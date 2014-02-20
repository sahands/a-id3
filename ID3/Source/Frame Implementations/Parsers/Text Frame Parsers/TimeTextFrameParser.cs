using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames.Parsers
{
	class TimeTextFrameParser : TextFrameParser
	{
		protected override Frame ParseFrame(byte[] data)
		{
			return new TimeTextFrame(ParseTextFrame(data));
		}
	}
}