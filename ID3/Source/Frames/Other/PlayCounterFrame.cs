using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames
{
	[global::System.Serializable]
	public class PlayCounterFrame
		: Frame
	{
		private int _counter;

		public int Counter
		{
			get { return _counter;}
			set { _counter = value;}
		}
	
		public PlayCounterFrame(int counter)
			: base()
		{
			this.Counter=counter;
		}

		public override Writers.FrameWriter CreateWriter(ID3v2MajorVersion version, EncodingScheme encoding)
		{
			switch(version)
			{
				case ID3v2MajorVersion.Version2:
					return new Writers.PlayCounterFrameWriter(this, Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), "CNT", encoding);
				case ID3v2MajorVersion.Version3:
				case ID3v2MajorVersion.Version4:
					return new Writers.PlayCounterFrameWriter(this, Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), "PCNT", encoding);
				default:
					return null;
			}
		}

		public static Parsers.FrameParser CreateParser(ID3v2MajorVersion version, string frameID)
		{
			if(version==ID3v2MajorVersion.Version2 && frameID=="CNT")
			{
				return new Parsers.PlayCounterFrameParser();
			}
			else if((version == ID3v2MajorVersion.Version3 || version==ID3v2MajorVersion.Version4) && frameID=="PCNT")
			{
				return new Parsers.PlayCounterFrameParser();
			}
			return null;
		}
	}
}
