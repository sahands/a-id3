using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames.Parsers
{
	class ReleaseTimeTextFrameParser : TextFrameParser
	{
		protected override Frame ParseFrame(byte[] data)
		{
			return new ReleaseTimeTextFrame(ParseTextFrame(data));
		}
	}
}