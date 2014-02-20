using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames.Parsers
{
	class OriginalAlbumTextFrameParser : TextFrameParser
	{
		protected override Frame ParseFrame(byte[] data)
		{
			return new OriginalAlbumTextFrame(ParseTextFrame(data));
		}
	}
}


