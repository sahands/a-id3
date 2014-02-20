using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Achamenes.ID3.V1
{
	public class ID3v1Tag
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

		private byte _trackNumber=0;

		public byte TrackNumber
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

		private byte _genre=0;

		public byte GenreCode
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

		private string _year;

		public string Year
		{
			get
			{
				return _year;
			}
			set
			{
				if(value.Length>4)
				{
					throw new ArgumentException("The value for Year can not be longer than 4 characters.", "value");
				}
				_year = value;
			}
		}


		public ID3v1Tag()
		{
		}

		/// <summary>
		/// Reads the ID3 v1.0 or v1.1 tag attached to a file from a given FileStream.
		/// </summary>
		/// <param name="stream">The FileStream reading the file. It should support both seeking and reading.</param>
		/// <returns>True if ID3 v1.0 or v1.1 tag information was read successfully, false otherwise.</returns>
		private bool ReadFromFile(FileStream stream)
		{
			if(!stream.CanSeek || !stream.CanRead)
				throw (new ArgumentException("The passed Stream object must support seeking and reading.","stream"));

			if(stream.Length<128)
				return false;

			stream.Seek(stream.Length-128, SeekOrigin.Begin);
			byte[] data=new byte[128];
			if(stream.Read(data, 0, 128)<128)
				return false;

			if(Encoding.GetEncoding("ISO-8859-1").GetString(data, 0, 3).ToLower()!="tag")
				return false;

			Title=Encoding.UTF8.GetString(data, 3, 30).Trim();
			Artist=Encoding.UTF8.GetString(data, 33, 30).Trim();
			Album=Encoding.UTF8.GetString(data, 63, 30).Trim();
			Year=Encoding.UTF8.GetString(data, 93, 4);
			Comment=Encoding.UTF8.GetString(data, 97, 30).Trim();
			GenreCode=data[127];
			if(data[125]==0)
			{
				TrackNumber=data[126];
			}
			return true;
		}

		/// <summary>
		/// Writes the information provided in the TagInformation object
		/// to the given FileStream in ID3v1.1 format.
		/// </summary>
		/// <param name="stream">The FileStream that can read and write to the file. Must also support seeking.</param>
		/// <param name="tag">The TagInformation object containing the tag information. </param>
		private void WriteToFile(FileStream stream)
		{
			if(!(stream.CanRead && stream.CanSeek && stream.CanSeek))
				throw (new ArgumentException("The passed Stream object must support seeking, reading and writing."));

			ID3v1Tag tagInformation=new ID3v1Tag();
			if(!tagInformation.ReadFromFile(stream))
				stream.Seek(stream.Length, SeekOrigin.Begin);
			else
				stream.Seek(stream.Length-128, SeekOrigin.Begin);
			
			stream.Write(Encoding.GetEncoding("ISO-8859-1").GetBytes("TAG"), 0, 3);
			byte[] b=new byte[0];
			if(Title!="")
				b=Encoding.GetEncoding("ISO-8859-1").GetBytes(Title);
			for(int i=0;i<30;i++)
			{
				if(i<b.Length)
					stream.WriteByte(b[i]);
				else
					stream.WriteByte(0);
			}
			b=new byte[0];
			if(Artist!="")
				b=Encoding.GetEncoding("ISO-8859-1").GetBytes(Artist);
			for(int i=0;i<30;i++)
			{
				if(i<b.Length)
					stream.WriteByte(b[i]);
				else
					stream.WriteByte(0);
			}
			b=new byte[0];
			if(Album!="")
				b=Encoding.GetEncoding("ISO-8859-1").GetBytes(Album);
			for(int i=0;i<30;i++)
			{
				if(i<b.Length)
					stream.WriteByte(b[i]);
				else
					stream.WriteByte(0);
			}
			b=new byte[0];
			if(Year!="")
				b=Encoding.GetEncoding("ISO-8859-1").GetBytes(Year);
			for(int i=0;i<4;i++)
			{
				if(i<b.Length)
					stream.WriteByte(b[i]);
				else
					stream.WriteByte(0);
			}
			b=new byte[0];
			if(Comment!="")
				b=Encoding.GetEncoding("ISO-8859-1").GetBytes(Comment);
			for(int i=0;i<28;i++)
			{
				if(i<b.Length)
					stream.WriteByte(b[i]);
				else
					stream.WriteByte(0);
			}
			stream.WriteByte(0);
			stream.WriteByte(TrackNumber);
			stream.WriteByte(GenreCode);
		}

		/// <summary>
		/// Reads the ID3 v1.0 or v1.1 tag information
		/// attached to a given file.
		/// </summary>
		/// <param name="fileName">Name of the target file to read from.</param>
		/// <returns>
		/// A TagInformation object containing the information in the tag.
		/// null is returned if no tag is attached to the file.
		/// </returns>
		public bool ReadFromFile(string fileName)
		{
			FileStream file=File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			try
			{
				return ReadFromFile(file);
			}
			finally
			{
				file.Close();
			}
		}

		/// <summary>
		/// Writes the information provided in the TagInformation object
		/// to the given file in ID3v1.1 format.
		/// </summary>
		/// <param name="filename">The name of the target file.</param>
		/// <param name="tag">The TagInformation object containing the tag information.</param>
		public void WriteToFile(string filename)
		{
			FileStream stream=File.Open(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
			try
			{
				WriteToFile(stream);
			}
			finally
			{
				stream.Close();
			}
		}

		public static ID3v1Tag FromV2Tag(TagBase tag, IGenreManager genreManager)
		{
			if(tag==null)
			{
				throw new ArgumentNullException("tag", "Argument 'tag' can not be null.");
			}

			ID3v1Tag information=new ID3v1Tag();
			Frames.AlbumTextFrame album=tag.SearchForFrame(typeof(Frames.AlbumTextFrame)) as Frames.AlbumTextFrame;
			Frames.ArtistTextFrame artist=tag.SearchForFrame(typeof(Frames.ArtistTextFrame)) as Frames.ArtistTextFrame;
			Frames.CommentExtendedTextFrame comment=tag.SearchForFrame(typeof(Frames.CommentExtendedTextFrame)) as Frames.CommentExtendedTextFrame;
			Frames.GenreTextFrame genre=tag.SearchForFrame(typeof(Frames.GenreTextFrame)) as Frames.GenreTextFrame;
			Frames.ReleaseTimeTextFrame releaseTime=tag.SearchForFrame(typeof(Frames.ReleaseTimeTextFrame)) as Frames.ReleaseTimeTextFrame;
			Frames.TitleTextFrame title=tag.SearchForFrame(typeof(Frames.TitleTextFrame)) as Frames.TitleTextFrame;
			Frames.TrackTextFrame track=tag.SearchForFrame(typeof(Frames.TrackTextFrame)) as Frames.TrackTextFrame;
			Frames.YearTextFrame year=tag.SearchForFrame(typeof(Frames.YearTextFrame)) as Frames.YearTextFrame;

			if(album!=null)
				information.Album=album.Text;
			if(artist!=null)
				information.Artist=album.Text;
			if(comment!=null)
				information.Comment=album.Text;
			if(genre!=null)
				information.GenreCode=genreManager.GetGenreCode(genre.Text);
			if(releaseTime!=null)
				information.Year=releaseTime.Text.Substring(0, 4);
			else if(year!=null)
				information.Year=year.Text;
			if(track!=null)
				information.TrackNumber=(byte)track.TrackNumber;

			return information;
		}
	}
}

