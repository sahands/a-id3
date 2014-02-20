using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames
{
	[global::System.Serializable]
	public class PictureFrame : Frame
	{
		private string _description;
		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
			}
		}

		private System.Drawing.Image _image;
		public System.Drawing.Image Picture
		{
			get
			{
				return _image;
			}
			set
			{
				_image = value;
			}
		}

		private PictureType _pictureType;
		public PictureType PictureType
		{
			get
			{
				return _pictureType;
			}
			set
			{
				_pictureType = value;
			}
		}

		protected PictureFrame(string description, PictureType pictureType)
			: base()
		{
			this.Description=description;
			this.PictureType=pictureType;
		}

		public PictureFrame(string fileName, string description, PictureType pictureType)
			: this(description, pictureType)
		{
			this._image=System.Drawing.Image.FromFile(fileName);
		}

		public PictureFrame(System.Drawing.Image image, string description, PictureType pictureType)
			: this(description, pictureType)
		{
			if(image==null)
			{
				throw new ArgumentNullException("The passed image object can not be null.");
			}
			this._image=image;
		}

		public static Achamenes.ID3.Frames.Parsers.FrameParser CreateParser(ID3v2MajorVersion version, string frameID)
		{
			if(version==ID3v2MajorVersion.Version2)
			{
				if(frameID=="PIC")
				{
					return new Parsers.PictureFrameParserM2();
				}
			}
			else if(version==ID3v2MajorVersion.Version3 || version==ID3v2MajorVersion.Version4)
			{
				if(frameID=="APIC")
				{
					return new Parsers.PictureFrameParserM3and4();
				}
			}
			return null;
		}

		public override Achamenes.ID3.Frames.Writers.FrameWriter CreateWriter(ID3v2MajorVersion version, EncodingScheme encoding)
		{
			if(version==ID3v2MajorVersion.Version2)
			{
				return new Writers.PictureFrameWriterM2(this, "PIC", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
			}
			else if(version==ID3v2MajorVersion.Version3 || version==ID3v2MajorVersion.Version4)
			{
				return new Writers.PictureFrameWriterM3And4(this, "APIC", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
			}
			else
			{
				return null;
			}
		}
	}
}
