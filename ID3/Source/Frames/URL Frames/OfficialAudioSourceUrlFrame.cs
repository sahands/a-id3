using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames
{
	[global::System.Serializable]
	public class OfficialAudioSourceUrlFrame : UrlFrame
	{
		public OfficialAudioSourceUrlFrame(string url)
			: base(url)
		{
		}

		public static Achamenes.ID3.Frames.Parsers.FrameParser CreateParser(ID3v2MajorVersion version, string frameID)
		{
			if(version==ID3v2MajorVersion.Version2 && frameID=="WAS")
			{
				return new Parsers.OfficialAudioSourceUrlFrameParser();
			}
			if((version==ID3v2MajorVersion.Version3 || version==ID3v2MajorVersion.Version4) && frameID=="WOAS")
			{
				return new Parsers.OfficialAudioSourceUrlFrameParser();
			}
			return null;
		}

		public override Achamenes.ID3.Frames.Writers.FrameWriter CreateWriter(ID3v2MajorVersion version, EncodingScheme encoding)
		{
			if(version==ID3v2MajorVersion.Version2)
			{
				return new Writers.UrlFrameWriter(this, "WAS", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
			}
			if(version==ID3v2MajorVersion.Version3 || version==ID3v2MajorVersion.Version4)
			{
				return new Writers.UrlFrameWriter(this, "WOAS", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
			}
			return null;
		}
	}
}