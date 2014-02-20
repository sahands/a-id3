using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames.Parsers
{
	class OriginalReleaseYearTextFrameParser : TextFrameParser
	{
		protected override Frame ParseFrame(byte[] data)
		{
			return new OriginalReleaseYearTextFrame(ParseTextFrame(data));
		}
	}
}