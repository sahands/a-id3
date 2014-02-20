using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames
{
	[global::System.Serializable]
	public class PartOfSetTextFrame : TextFrame
	{
		private int _partNumber;
		private int _totalParts;

		public PartOfSetTextFrame(string text)
			: base(text)
		{
		}

		public PartOfSetTextFrame(int partNumber)
			: base(partNumber.ToString())
		{
			this._partNumber=partNumber;
		}

		public PartOfSetTextFrame(int partNumber, int totalParts)
			: base(partNumber.ToString()+"/"+totalParts.ToString())
		{
			this._totalParts=totalParts;
			this._partNumber=partNumber;
		}

		public int PartNumber
		{
			get
			{
				return this._partNumber;
			}
		}

		public int TotalParts
		{
			get
			{
				return this._totalParts;
			}
		}

		protected override void Validate(string value)
		{
			if(value.Length<=0)
			{
				throw new InvalidFrameValueException(value,"Invalid value specified for PartOfSet frame.");
			}
			string[] parts=value.Split('/');
			if(parts.Length>2)
			{
				throw new InvalidFrameValueException(value, "Only one '/' character should be present in a PartOfSet frame.");
			}
			try
			{
				int.Parse(parts[0]);
				if(parts.Length==2)
				{
					int.Parse(parts[1]);
				}
			}
			catch(FormatException)
			{
				throw new InvalidFrameValueException(value, "Invalid value for PartOfSet frame.");
			}
		}

		protected override void Parse()
		{
			string[] parts=this.Text.Split('/');
			this._partNumber=int.Parse(parts[0]);
			if(parts.Length==2)
			{
				this._totalParts=int.Parse(parts[1]);
			}
		}

		public static Achamenes.ID3.Frames.Parsers.FrameParser CreateParser(ID3v2MajorVersion version, string frameID)
		{
			if(version==ID3v2MajorVersion.Version2 && frameID=="TPA")
			{
				return new Parsers.PartOfSetTextFrameParser();
			}
			if((version==ID3v2MajorVersion.Version3 || version==ID3v2MajorVersion.Version4) && frameID=="TPOS")
			{
				return new Parsers.PartOfSetTextFrameParser();
			}
			return null;
		}

		public override Achamenes.ID3.Frames.Writers.FrameWriter CreateWriter(ID3v2MajorVersion version, EncodingScheme encoding)
		{
			if(version==ID3v2MajorVersion.Version2)
			{
				return new Writers.TextFrameWriter(this, "TPA", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
			}
			if(version==ID3v2MajorVersion.Version3 || version==ID3v2MajorVersion.Version4)
			{
				return new Writers.TextFrameWriter(this, "TPOS", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
			}
			return null;
		}
	}
}
