using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames
{
	public class MusicCDIdentifierFrame
		: Frame
	{
		private byte[] _identifier;

		public byte[] Identifier
		{
			get
			{
				return _identifier;
			}
			set
			{
				if(value==null)
				{
					throw new ArgumentNullException("value", "Can not set UniqueIdentifier to null.");
				}
				_identifier = value;
			}
		}

		public MusicCDIdentifierFrame(byte[] identifier)
			: base()
		{
			this.Identifier=identifier;
		}

		public override Achamenes.ID3.Frames.Writers.FrameWriter CreateWriter(ID3v2MajorVersion version, EncodingScheme encoding)
		{
			if(version==ID3v2MajorVersion.Version2)
			{
				return new Writers.MusicCDIdentifierFrameWriter(this, Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), "MCI", encoding);
			}
			else if(version==ID3v2MajorVersion.Version3 || version==ID3v2MajorVersion.Version4)
			{
				return new Writers.MusicCDIdentifierFrameWriter(this, Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), "MCDI", encoding);
			}
			return null;
		}

		public static Parsers.FrameParser CreateParser(ID3v2MajorVersion version, string frameID)
		{
			if(version==ID3v2MajorVersion.Version2 && frameID=="MCI")
			{
				return new Parsers.MusicCDIdentifierFrameParser();
			}
			else if((version == ID3v2MajorVersion.Version3 || version==ID3v2MajorVersion.Version4) && frameID=="MCDI")
			{
				return new Parsers.MusicCDIdentifierFrameParser();
			}
			return null;
		}
	}
}

