using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames
{	
	[global::System.Serializable]
	public class YearTextFrame : TextFrame
	{
		public YearTextFrame(string text)
			: base(text)
		{
		}

		protected override void Validate(string value)
		{
			if(value.Length>4)
			{
				throw new InvalidFrameValueException(value,"Invalid value supplied for the Year frame. Maximum length is 4.");
			}
		}

		public static Achamenes.ID3.Frames.Parsers.FrameParser CreateParser(ID3v2MajorVersion version, string frameID)
		{
			// ID3 v2.4 does not support Year text frame
			if(version==ID3v2MajorVersion.Version2 && frameID=="TYE")
			{
				return new Parsers.YearTextFrameParser();
			}
			if(version==ID3v2MajorVersion.Version3 && frameID=="TYER")
			{
				return new Parsers.YearTextFrameParser();
			}
			return null;
		}

		public override Achamenes.ID3.Frames.Writers.FrameWriter CreateWriter(ID3v2MajorVersion version, EncodingScheme encoding)
		{
			if(version==ID3v2MajorVersion.Version2)
			{
				return new Writers.TextFrameWriter(this, "TYE", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
			}
			if(version==ID3v2MajorVersion.Version3)
			{
				return new Writers.TextFrameWriter(this, "TYER", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
			}
			return null;
		}
	}
}