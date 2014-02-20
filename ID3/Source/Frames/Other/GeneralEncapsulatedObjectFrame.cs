using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames
{
	public class GeneralEncapsulatedObjectFrame
		: Frame
	{
		private string _mimeType;

		public string MimeType
		{
			get
			{
				return _mimeType;
			}
			set
			{
				_mimeType = value;
			}
		}

		private string _filename;

		public string Filename
		{
			get
			{
				return _filename;
			}
			set
			{
				_filename = value;
			}
		}

		private string _contentDescription;

		public string ContentDescription
		{
			get
			{
				return _contentDescription;
			}
			set
			{
				_contentDescription = value;
			}
		}

		private byte[] _data;

		public byte[] BinaryData
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

		public GeneralEncapsulatedObjectFrame(string filename, string contentDescription, string mimeType, byte[] data)
			: base()
		{
			this._contentDescription=contentDescription;
			this._data=data;
			this._filename=filename;
			this._mimeType=mimeType;
		}

		public static Achamenes.ID3.Frames.Parsers.FrameParser CreateParser(ID3v2MajorVersion version, string frameID)
		{
			if(version==ID3v2MajorVersion.Version2 && frameID=="GEO")
			{
				return new Parsers.GeneralEncapsulatedObjectFrameParser();
			}
			if((version==ID3v2MajorVersion.Version3 || version==ID3v2MajorVersion.Version4) && frameID=="GEOB")
			{
				return new Parsers.GeneralEncapsulatedObjectFrameParser();
			}
			return null;
		}

		public override Achamenes.ID3.Frames.Writers.FrameWriter CreateWriter(ID3v2MajorVersion version, EncodingScheme encoding)
		{
			switch(version)
			{
				case ID3v2MajorVersion.Version2:
					return new Writers.GeneralEncapsulatedObjectFrameWriter(this, Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version),
						"GEO", encoding);
				case ID3v2MajorVersion.Version3:					
				case ID3v2MajorVersion.Version4:
					return new Writers.GeneralEncapsulatedObjectFrameWriter(this, Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version),
						"GEOB", encoding);
				default:
					return null;
			}
		}
	}
}
