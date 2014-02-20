using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames.Parsers
{
	class OfficialArtistUrlFrameParser : UrlFrameParser
	{
		protected override Frame ParseFrame(byte[] data)
		{
			return new OfficialArtistUrlFrame(ParseUrlFrame(data));
		}
	}
}