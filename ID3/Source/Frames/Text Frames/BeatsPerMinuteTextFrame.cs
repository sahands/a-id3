using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames
{
	[global::System.Serializable]
	public class BeatsPerMinuteTextFrame : TextFrame
	{
		public BeatsPerMinuteTextFrame(int bpm) 
			: base(bpm.ToString())
		{
		}

		public BeatsPerMinuteTextFrame(string bpm)
			:base(bpm)
		{
		}

		protected override void Validate(string value)
		{
			int r;
			if(!int.TryParse(value,out r) || r<=0)
			{
				throw new InvalidFrameValueException(value, "Invalid value for Beats Per Minute frame. Should be numeric and positive.");
			}
		}

		public static Achamenes.ID3.Frames.Parsers.FrameParser CreateParser(ID3v2MajorVersion version, string frameID)
		{
			if(version==ID3v2MajorVersion.Version2 && frameID=="TBP")
			{
				return new Parsers.BeatsPerMinuteTextFrameParser();
			}
			if((version==ID3v2MajorVersion.Version3 || version==ID3v2MajorVersion.Version4) && frameID=="TBPM")
			{
				return new Parsers.BeatsPerMinuteTextFrameParser();
			}
			return null;
		}

		public override Achamenes.ID3.Frames.Writers.FrameWriter CreateWriter(ID3v2MajorVersion version, EncodingScheme encoding)
		{
			if(version==ID3v2MajorVersion.Version2)
			{
				return new Writers.TextFrameWriter(this, "TBP", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
			}
			if(version==ID3v2MajorVersion.Version3 || version==ID3v2MajorVersion.Version4)
			{
				return new Writers.TextFrameWriter(this, "TBPM", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
			}
			return null;
		}
	}
}
