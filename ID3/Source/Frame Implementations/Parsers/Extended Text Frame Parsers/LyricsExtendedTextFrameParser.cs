using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames.Parsers
{
	class LyricsExtendedTextFrameParser : ExtendedTextFrameParser
	{
		protected override Frame ParseFrame(byte[] data)
		{
			string text, description;
			LanguageCode lang;
			text=ParseExtendedTextFrame(data, out description, out lang);
			return new LyricsExtendedTextFrame(text, description, lang);
		}
	}
}


