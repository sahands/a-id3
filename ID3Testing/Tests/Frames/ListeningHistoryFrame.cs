using System;
using System.Collections.Generic;
using System.Text;
using Achamenes.ID3;
using Achamenes.ID3.Frames;
using Achamenes.ID3.Frames.Writers;
using Achamenes.ID3.Frames.Parsers;
using Achamenes.ID3.Fields;


namespace MyID3Application
{
	class LastPlayedOnFrameWriter : FrameWriter
	{
		public LastPlayedOnFrameWriter(
			LastPlayedOnFrame	frameToWrite, 
			FrameHeaderWriter	frameHeaderWriter, 
			string				frameID, 
			EncodingScheme		encoding) 
			: base(frameToWrite, frameHeaderWriter, frameID, encoding)
		{
		}

		public override void WriteToStream(System.IO.Stream stream)
		{
			LastPlayedOnFrame frame=(LastPlayedOnFrame)this.FrameToWrite;
			List<Field> fields=new List<Field>();

			// Declare the fields to write.
			fields.Add(new SingleByteField((byte)this.Encoding));  //Encoding identifier byte
			fields.Add(TextField.CreateTextField(frame.LastPlayedOn.ToShortDateString(), 
				EncodingScheme.Ascii));
			fields.Add(TextField.CreateTextField(frame.PlayCount.ToString(),
				EncodingScheme.Ascii));
			fields.Add(TextField.CreateTextField(frame.Memo,this.Encoding));

			// Calculate the length and write the header
			int length=0;
			foreach(Field f in fields)
			{
				length+=f.Length;
			}			
			HeaderWriter.WriteHeader(stream, new FrameHeader(this.FrameID, length));

			// Write the fields
			foreach(Field f in fields)
			{
				f.WriteToStream(stream);
			}
		}
	}

	class LastPlayedOnFrameParser : FrameParser
	{
		protected override Frame ParseFrame(byte[] data)
		{
			int place=0;

			SingleByteField encodingField=new SingleByteField();
			place+=encodingField.Parse(data, place);

			TextField lastPlayedOnField=TextField.CreateTextField(true, EncodingScheme.Ascii);
			place+=lastPlayedOnField.Parse(data, place);

			TextField playCountField=TextField.CreateTextField(true, EncodingScheme.Ascii);
			place+=playCountField.Parse(data, place);

			TextField memoField=TextField.CreateTextField(true, EncodingScheme.Ascii);
			place+=memoField.Parse(data, place);

			DateTime lastPlayed;
			if(!DateTime.TryParse(lastPlayedOnField.Text, out lastPlayed))
			{
				throw new FrameParsingException("Invalid value in date field of LastPlayedOn frame.");
			}
			int playCount;
			if(!int.TryParse(playCountField.Text, out playCount))
			{
				throw new FrameParsingException("Invalid value in play count field of LastPlayedOn frame.");
			}

			return new LastPlayedOnFrame(lastPlayed, playCount, memoField.Text);
		}
	}

	[global::System.Serializable]
	public class LastPlayedOnFrame : Frame
	{
		private DateTime _lastPlayed;	

		public DateTime LastPlayedOn
		{
			get { return _lastPlayed;}
			set { _lastPlayed = value;}
		}

		private int _playCount;

		public int PlayCount
		{
			get
			{
				return _playCount;
			}
			set
			{
				_playCount = value;
			}
		}

		private string _memo;

		public string Memo
		{
			get
			{
				return _memo;
			}
			set
			{
				_memo = value;
			}
		}

		public LastPlayedOnFrame(DateTime lastPlayed, int playCount, string memo)
			: base()
		{
			this.LastPlayedOn=lastPlayed;
			this.PlayCount=playCount;
			this.Memo=memo;			
		}

		public override FrameWriter CreateWriter(ID3v2MajorVersion version, EncodingScheme encoding)
		{
			switch(version)
			{
				case ID3v2MajorVersion.Version2:
					return new LastPlayedOnFrameWriter(this, 
						FrameHeaderWriter.CreateFrameHeaderWriter(version), "XLP", encoding);
				case ID3v2MajorVersion.Version3:
				case ID3v2MajorVersion.Version4:
					return new LastPlayedOnFrameWriter(this, 
						FrameHeaderWriter.CreateFrameHeaderWriter(version), "XLPO", encoding);
				default:
					return null;
			}
		}

		public static FrameParser CreateParser(ID3v2MajorVersion version, string frameID)
		{
			if(version==ID3v2MajorVersion.Version2 && frameID=="XLP")
			{
				return new LastPlayedOnFrameParser();
			}
			else if((version == ID3v2MajorVersion.Version3 || version==ID3v2MajorVersion.Version4) 
				&& frameID=="XLPO")
			{
				return new LastPlayedOnFrameParser();
			}
			return null;
		}
	}

	public class MyFrameParserFactory : FrameParserFactory
	{
		public override FrameParser CreateFrameParser(ID3v2MajorVersion version, string frameID)
		{
			FrameParser parser=LastPlayedOnFrame.CreateParser(version, frameID);
			if(parser==null)
			{
				return base.CreateFrameParser(version, frameID);
			}
			return parser;
		}
	}

	public class LastPlayedOnFrameTest
	{		
		public static void Main(string[] args)
		{
			TagBase tag=new TagBase();
			tag.Frames.Add(new LastPlayedOnFrame(DateTime.Now, 1, "Test!"));
			tag.WriteToFile("C:/Test.mp3", ID3v2MajorVersion.Version3, EncodingScheme.UnicodeWithBOM);

			TagBase tag2=new TagBase();
			tag2.ReadFromFile("C:/Test.mp3", new MyFrameParserFactory());
			System.Diagnostics.Debug.Assert(tag2.Frames.Count==1);
			System.Diagnostics.Debug.Assert(tag2.Frames[0] is LastPlayedOnFrame);
		}
	}
}
