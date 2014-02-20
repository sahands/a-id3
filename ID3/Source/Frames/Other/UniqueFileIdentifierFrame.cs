using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames
{
	[global::System.Serializable]
	public class UniqueFileIdentifierFrame
		: Frame
	{
		private byte[] _identifier;

		public byte[] UniqueIdentifier
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

		private string _owner;

		public string Owner
		{
			get
			{
				return _owner;
			}
			set
			{
				if(value==null)
				{
					throw new ArgumentNullException("value", "Can not set Owner to null.");
				}
				_owner = value;
			}
		}

		public UniqueFileIdentifierFrame(byte[] uniqueIdentifier, string owner)
			: base()
		{
			this.UniqueIdentifier=uniqueIdentifier;
			this.Owner=owner;
		}

		public override Achamenes.ID3.Frames.Writers.FrameWriter CreateWriter(ID3v2MajorVersion version, EncodingScheme encoding)
		{
			if(version==ID3v2MajorVersion.Version2)
			{
				return new Writers.UniqueFileIdentifierFrameWriter(this, Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), "UFI", encoding);
			}
			else if(version==ID3v2MajorVersion.Version3 || version==ID3v2MajorVersion.Version4)
			{
				return new Writers.UniqueFileIdentifierFrameWriter(this, Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), "UFID", encoding);
			}
			return null;
		}

		public static Parsers.FrameParser CreateParser(ID3v2MajorVersion version, string frameID)
		{
			if(version==ID3v2MajorVersion.Version2 && frameID=="UFI")
			{
				return new Parsers.UniqueFileIdentifierFrameParser();
			}
			else if((version == ID3v2MajorVersion.Version3 || version==ID3v2MajorVersion.Version4) && frameID=="UFID")
			{
				return new Parsers.UniqueFileIdentifierFrameParser();
			}
			return null;
		}
	}	
}
