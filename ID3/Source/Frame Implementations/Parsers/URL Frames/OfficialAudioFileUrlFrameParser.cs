using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames.Parsers
{
	class OfficialAudioFileUrlFrameParser : UrlFrameParser
	{
		protected override Frame ParseFrame(byte[] data)
		{
			return new OfficialAudioFileUrlFrame(ParseUrlFrame(data));
		}
	}
}