using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames
{
	[global::System.Serializable]
	public class PrivateFrame
		: Frame
	{
		private string _owner;

		public string OwnerIdentifier
		{
			get
			{
				return _owner;
			}
			set
			{
				_owner = value;
			}
		}

		private byte[] _data;

		public byte[] PrivateData
		{
			get
			{
				return _data;
			}
			set
			{
				_data = value;
			}
		}

		public PrivateFrame(string owner, byte[] data)
          : base()
        {
			this._data=data;
			this._owner=owner;
        }

		public override Writers.FrameWriter CreateWriter(ID3v2MajorVersion version, EncodingScheme encoding)
		{
			switch(version)
			{
				case ID3v2MajorVersion.Version2:				
					return new Writers.PrivateFrameWriter(this,Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version),"PRI",encoding);
				case ID3v2MajorVersion.Version3:					
				case ID3v2MajorVersion.Version4:
					return new Writers.PrivateFrameWriter(this, Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), "PRIV", encoding);
				default:
					return null;
			}
		}

		public static Parsers.FrameParser CreateParser(ID3v2MajorVersion version, string frameID)
		{
			if(version==ID3v2MajorVersion.Version2 && frameID=="PRI")
			{
				return new Parsers.PrivateFrameParser();
			}
			else if((version == ID3v2MajorVersion.Version3 || version==ID3v2MajorVersion.Version4) && frameID=="PRIV")
			{
				return new Parsers.PrivateFrameParser();
			}
			return null;
		}
	}
}
