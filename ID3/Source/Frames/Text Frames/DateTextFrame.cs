using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames
{
	[global::System.Serializable]
	public class DateTextFrame : TextFrame
	{
		public DateTextFrame(string text)
			: base(text)
		{
		}

		public static Achamenes.ID3.Frames.Parsers.FrameParser CreateParser(ID3v2MajorVersion version, string frameID)
		{
			if(version==ID3v2MajorVersion.Version2 && frameID=="TDA")
			{
				return new Parsers.DateTextFrameParser();
			}
			if(version==ID3v2MajorVersion.Version3 && frameID=="TDAT")
			{
				return new Parsers.DateTextFrameParser();
			}
			return null;
		}

		public override Achamenes.ID3.Frames.Writers.FrameWriter CreateWriter(ID3v2MajorVersion version, EncodingScheme encoding)
		{
			if(version==ID3v2MajorVersion.Version2)
			{
				return new Writers.TextFrameWriter(this, "TDA", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
			}
			if(version==ID3v2MajorVersion.Version3)
			{
				return new Writers.TextFrameWriter(this, "TDAT", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
			}
			return null;
		}
	}
}