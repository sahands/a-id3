using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames
{
	[global::System.Serializable]
	public class OriginalReleaseYearTextFrame : TextFrame
	{
		public OriginalReleaseYearTextFrame(string text)
			: base(text)
		{
		}

		public static Achamenes.ID3.Frames.Parsers.FrameParser CreateParser(ID3v2MajorVersion version, string frameID)
		{
			if(version==ID3v2MajorVersion.Version2 && frameID=="TOR")
			{
				return new Parsers.OriginalReleaseYearTextFrameParser();
			}
			if(version==ID3v2MajorVersion.Version3  && frameID=="TORY")
			{
				return new Parsers.OriginalReleaseYearTextFrameParser();
			}
			return null;
		}

		public override Achamenes.ID3.Frames.Writers.FrameWriter CreateWriter(ID3v2MajorVersion version, EncodingScheme encoding)
		{
			if(version==ID3v2MajorVersion.Version2)
			{
				return new Writers.TextFrameWriter(this, "TOR", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
			}
			if(version==ID3v2MajorVersion.Version3)
			{
				return new Writers.TextFrameWriter(this, "TORY", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
			}
			return null;
		}
	}
}