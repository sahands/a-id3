using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames
{
	[global::System.Serializable]
	public class OfficialArtistUrlFrame : UrlFrame
	{
		public OfficialArtistUrlFrame(string url)
			: base(url)
		{
		}

		public static Achamenes.ID3.Frames.Parsers.FrameParser CreateParser(ID3v2MajorVersion version, string frameID)
		{
			if(version==ID3v2MajorVersion.Version2 && frameID=="WAR")
			{
				return new Parsers.OfficialArtistUrlFrameParser();
			}
			if((version==ID3v2MajorVersion.Version3 || version==ID3v2MajorVersion.Version4) && frameID=="WOAR")
			{
				return new Parsers.OfficialArtistUrlFrameParser();
			}
			return null;
		}

		public override Achamenes.ID3.Frames.Writers.FrameWriter CreateWriter(ID3v2MajorVersion version, EncodingScheme encoding)
		{
			if(version==ID3v2MajorVersion.Version2)
			{
				return new Writers.UrlFrameWriter(this, "WAR", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
			}
			if(version==ID3v2MajorVersion.Version3 || version==ID3v2MajorVersion.Version4)
			{
				return new Writers.UrlFrameWriter(this, "WOAR", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
			}
			return null;
		}
	}
}