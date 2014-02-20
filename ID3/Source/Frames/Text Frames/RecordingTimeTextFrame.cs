using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames
{
	[global::System.Serializable]
	public class RecordingTimeTextFrame : TextFrame
	{
		public RecordingTimeTextFrame(string text)
			: base(text)
		{
		}

		public static Achamenes.ID3.Frames.Parsers.FrameParser CreateParser(ID3v2MajorVersion version, string frameID)
		{
			if(version==ID3v2MajorVersion.Version4 && frameID=="TDRC")
			{
				return new Parsers.RecordingTimeTextFrameParser();
			}
			return null;
		}

		public override Achamenes.ID3.Frames.Writers.FrameWriter CreateWriter(ID3v2MajorVersion version, EncodingScheme encoding)
		{
			if(version==ID3v2MajorVersion.Version4)
			{
				return new Writers.TextFrameWriter(this, "TDRC", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
			}
			return null;
		}
	}
}

