using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3
{
	public class SimpleTag
	{
		private string _title="";

		public string Title
		{
			get
			{
				return _title;
			}
			set
			{
				_title = value;
			}
		}
		private string _artist="";

		public string Artist
		{
			get
			{
				return _artist;
			}
			set
			{
				_artist = value;
			}
		}

		private string _album="";

		public string Album
		{
			get
			{
				return _album;
			}
			set
			{
				_album = value;
			}
		}

		private string _comment="";

		public string Comment
		{
			get
			{
				return _comment;
			}
			set
			{
				_comment = value;
			}
		}

		private string _lyrics="";

		public string Lyrics
		{
			get
			{
				return _lyrics;
			}
			set
			{
				_lyrics = value;
			}
		}

		private string _encodedBy="";

		public string EncodedBy
		{
			get
			{
				return _encodedBy;
			}
			set
			{
				_encodedBy = value;
			}
		}

		private string _copyright="";

		public string Copyright
		{
			get
			{
				return _copyright;
			}
			set
			{
				_copyright = value;
			}
		}

		private string _genre="";

		public string Genre
		{
			get
			{
				return _genre;
			}
			set
			{
				_genre = value;
			}
		}

		private string _releaseYear="";

		public string Year
		{
			get
			{
				return _releaseYear;
			}
			set
			{
				_releaseYear = value;
			}
		}

		private int _trackNumber;

		public int TrackNumber
		{
			get
			{
				return _trackNumber;
			}
			set
			{
				_trackNumber = value;
			}
		}

		private int _totalTracks;

		public int TotalTracks
		{
			get
			{
				return _totalTracks;
			}
			set
			{
				_totalTracks = value;
			}
		}
		private int _partNumber;

		public int PartNumber
		{
			get
			{
				return _partNumber;
			}
			set
			{
				_partNumber = value;
			}
		}
		private int _totalParts;

		public int TotalParts
		{
			get
			{
				return _totalParts;
			}
			set
			{
				_totalParts = value;
			}
		}

		private string _originalArtist="";

		public string OriginalArtist
		{
			get
			{
				return _originalArtist;
			}
			set
			{
				_originalArtist = value;
			}
		}

		private string _composer="";

		public string Composer
		{
			get
			{
				return _composer;
			}
			set
			{
				_composer = value;
			}
		}

		private string _userUrl="";

		public string UserUrl
		{
			get
			{
				return _userUrl;
			}
			set
			{
				_userUrl = value;
			}
		}

		private string _artistUrl="";

		public string ArtistUrl
		{
			get
			{
				return _artistUrl;
			}
			set
			{
				_artistUrl = value;
			}
		}

		private string _grouping="";

		public string Grouping
		{
			get
			{
				return _grouping;
			}
			set
			{
				_grouping = value;
			}
		}

		private System.Drawing.Image _coverArt;

		public System.Drawing.Image CoverArt
		{
			get
			{
				return _coverArt;
			}
			set
			{
				_coverArt = value;
			}
		}

		private int _bpm;

		public int BPM
		{
			get
			{
				return _bpm;
			}
			set
			{
				_bpm = value;
			}
		}

		public static SimpleTag FromFile(string fileName)
		{
			SimpleTag result=new SimpleTag();
			TagBase tag=new TagBase();
			tag.ReadFromFile(fileName, new Achamenes.ID3.Frames.Parsers.FrameParserFactory());
			foreach(Frames.Frame frame in tag.Frames)
			{
				if(frame is Frames.PictureFrame)
				{
					if(((Frames.PictureFrame)frame).PictureType==PictureType.CoverFront)
						result.CoverArt=((Frames.PictureFrame)frame).Picture;
					else if(result.CoverArt==null)
						result.CoverArt=((Frames.PictureFrame)frame).Picture;
				}
				else if(frame is Frames.ArtistTextFrame)
					result.Artist=((Frames.ArtistTextFrame)frame).Text;
				else if(frame is Frames.AlbumTextFrame)
					result.Album=((Frames.AlbumTextFrame)frame).Text;
				else if(frame is Frames.CommentExtendedTextFrame)
					result.Comment=((Frames.CommentExtendedTextFrame)frame).Text;
				else if(frame is Frames.ComposerTextFrame)
					result.Composer=((Frames.ComposerTextFrame)frame).Text;
				else if(frame is Frames.CopyrightTextFrame)
					result.Copyright=((Frames.CopyrightTextFrame)frame).Text;
				else if(frame is Frames.EncodedByTextFrame)
					result.EncodedBy=((Frames.EncodedByTextFrame)frame).Text;
				else if(frame is Frames.GenreTextFrame)
				{
					StandardGenreManager manager=new StandardGenreManager();
					result.Genre=manager.TranslateToUserFriendly(((Frames.GenreTextFrame)frame).Text);
				}
				else if(frame is Frames.GroupingTextFrame)
					result.Grouping=((Frames.GroupingTextFrame)frame).Text;
				else if(frame is Frames.LyricsExtendedTextFrame)
					result.Lyrics=((Frames.LyricsExtendedTextFrame)frame).Text;
				else if(frame is Frames.OriginalArtistTextFrame)
					result.OriginalArtist=((Frames.OriginalArtistTextFrame)frame).Text;
				else if(frame is Frames.TitleTextFrame)
					result.Title=((Frames.TitleTextFrame)frame).Text;
				else if(frame is Frames.CustomUserUrlFrame)
					result.UserUrl=((Frames.CustomUserUrlFrame)frame).Url;
				else if(frame is Frames.OfficialArtistUrlFrame)
					result.ArtistUrl=((Frames.OfficialArtistUrlFrame)frame).Url;
				else if(frame is Frames.TrackTextFrame)
				{
					result.TrackNumber=((Frames.TrackTextFrame)frame).TrackNumber;
					result.TotalTracks=((Frames.TrackTextFrame)frame).TotalTracks;
				}
				else if(frame is Frames.PartOfSetTextFrame)
				{
					result.PartNumber=((Frames.PartOfSetTextFrame)frame).PartNumber;
					result.TotalParts=((Frames.PartOfSetTextFrame)frame).TotalParts;
				}
				else if(frame is Frames.YearTextFrame)
				{
					string year=((Frames.YearTextFrame)frame).Text;
					if(year!="")
						result.Year=year.Substring(0, 4);
				}
				else if(frame is Frames.ReleaseTimeTextFrame)
				{
					string year=((Frames.ReleaseTimeTextFrame)frame).Text;
					if(year!="")
						result.Year=year.Substring(0, 4);
				}
				else if(frame is Frames.BeatsPerMinuteTextFrame)
				{
					result.BPM=int.Parse((((Frames.BeatsPerMinuteTextFrame)frame).Text));
				}
			}

			return result;
		}

		public void WriteToFile(string fileName)
		{
			TagBase tag=new TagBase();
			if(this.Artist!="")			
				tag.Frames.Add(new Frames.ArtistTextFrame(this.Artist));			
			if(this.Album!="")
				tag.Frames.Add(new Frames.AlbumTextFrame(this.Album));
			if(this.ArtistUrl!="")
				tag.Frames.Add(new Frames.OfficialArtistUrlFrame(this.ArtistUrl));
			if(this.Comment!="")
				tag.Frames.Add(new Frames.CommentExtendedTextFrame(this.Comment,"", LanguageCode.eng));
			if(this.Composer!="")
				tag.Frames.Add(new Frames.ComposerTextFrame(this.Composer));
			if(this.Copyright!="")
				tag.Frames.Add(new Frames.CopyrightTextFrame(this.Copyright));
			if(this.CoverArt!=null)
				tag.Frames.Add(new Frames.PictureFrame(this.CoverArt,"",PictureType.CoverFront));
			if(this.EncodedBy!="")
				tag.Frames.Add(new Frames.EncodedByTextFrame(this.EncodedBy));
			if(this.Genre!="")
				tag.Frames.Add(new Frames.GenreTextFrame(this.Genre));
			if(this.Grouping!="")
				tag.Frames.Add(new Frames.GroupingTextFrame(this.Grouping));
			if(this.Lyrics!="")
				tag.Frames.Add(new Frames.LyricsExtendedTextFrame(this.Lyrics,"",LanguageCode.eng));
			if(this.OriginalArtist!="")
				tag.Frames.Add(new Frames.OriginalArtistTextFrame(this.OriginalArtist));
			if(this.Year!=null)
				tag.Frames.Add(new Frames.YearTextFrame(this.Year));
			if(this.Title!="")
				tag.Frames.Add(new Frames.TitleTextFrame(this.Title));
			if(this.UserUrl!="")
				tag.Frames.Add(new Frames.CustomUserTextFrame(this.UserUrl));
			if(this.OriginalArtist!="")
				tag.Frames.Add(new Frames.OriginalArtistTextFrame(this.OriginalArtist));
			
			if(this.BPM>0)
				tag.Frames.Add(new Frames.BeatsPerMinuteTextFrame(this.BPM));

			if(this.TrackNumber>0 && this.TotalTracks>0)
				tag.Frames.Add(new Frames.TrackTextFrame(this.TrackNumber,this.TotalTracks));
			else if(this.TrackNumber>0)
				tag.Frames.Add(new Frames.TrackTextFrame(this.TrackNumber));

			if(this.PartNumber>0 && this.TotalParts>0)
				tag.Frames.Add(new Frames.PartOfSetTextFrame(this.PartNumber, this.TotalParts));
			else if(this.PartNumber>0)
				tag.Frames.Add(new Frames.PartOfSetTextFrame(this.PartNumber));

			tag.WriteToFile(fileName, ID3v2MajorVersion.Version3, EncodingScheme.UnicodeWithBOM);
		}
	}
}