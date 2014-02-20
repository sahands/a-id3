using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames
{
	/// <summary>
	/// Track frame holds the track number and total tracks information.
	/// </summary>
	[global::System.Serializable]
	public class TrackTextFrame : TextFrame
	{
		private int _trackNumber=-1;
		private int _totalTracks=-1;

		/// <summary>
		/// Initializes a new instance of TrackTextFrame
		/// </summary>
		/// <param name="text">
		/// Text string describing the frame. 
		/// </param>
		/// <exception cref="InvalidFrameValueException">
		/// The given text value was not in correct format.
		/// </exception>
		/// <remarks>
		/// The given value should be in the trackNumber[/totalTracks] format. 
		/// See examples below for more details.
		/// </remarks>
		/// <example>
		/// The value should begin with a numberic value and optionally followed by
		/// a forward slash and another numeric value. Examples: '5', '5/12', '53/123'.
		/// </example>
		public TrackTextFrame(string text)
			: base(text)
		{
		}

		/// <summary>
		/// Initializes a new instance of TrackTextFrame
		/// </summary>
		/// <param name="trackNumber">Track number.</param>
		public TrackTextFrame(int trackNumber)
			: base(trackNumber.ToString())
		{
			if(trackNumber<0)
			{
				throw new InvalidFrameValueException(trackNumber.ToString(), "The value specified for track number should be positive.");
			}
			this._trackNumber=trackNumber;
		}


		/// <summary>
		/// Initializes a new instance of TrackTextFrame
		/// </summary>
		/// <param name="trackNumber">Track number.</param>
		/// <param name="totalTracks">Total number of tracks.</param>
		public TrackTextFrame(int trackNumber, int totalTracks)
			: base(trackNumber.ToString()+"/"+totalTracks.ToString())
		{
			if(trackNumber<0)
			{
				throw new InvalidFrameValueException(trackNumber.ToString(), "The value specified for track number should be positive.");
			}
			if(totalTracks<0)
			{
				throw new InvalidFrameValueException(totalTracks.ToString(), "The value specified for total number of tracks should be positive.");
			}
			this._totalTracks=totalTracks;
			this._trackNumber=trackNumber;
		}

		/// <summary>
		/// Gets or sets the track number.
		/// </summary>
		public int TrackNumber
		{
			get
			{
				return this._trackNumber;
			}
			set
			{
				if(value<0)
				{
					throw new InvalidFrameValueException(value.ToString(), "The value specified for track number should be positive.");
				}
				this._trackNumber=value;
				this.Text=_trackNumber.ToString();
				if(this._totalTracks>=0)
				{
					this.Text+="/"+_totalTracks.ToString();
				}
			}
		}

		/// <summary>
		/// Gets or sets the total number of tracks.
		/// </summary>
		public int TotalTracks
		{
			get
			{
				return this._totalTracks;
			}
			set
			{
				if(value<0)
				{
					throw new InvalidFrameValueException(value.ToString(), "The value specified for total number of tracks should be positive.");
				}
				this._totalTracks=value;								
				if(this._trackNumber>=0)
				{
					this.Text=_trackNumber+"/"+_totalTracks.ToString();
				}
			}
		}

		/// <summary>
		/// Parses the given string value and throws exceptions if it is an invalid text value for 
		/// a track frame.
		/// </summary>
		/// <param name="value"></param>
		protected override void Validate(string value)
		{
			if(value.Length<=0)
			{
				throw new InvalidFrameValueException(value,"Invalid value specified for Track frame.");
			}
			string []parts=value.Split('/');
			if(parts.Length>2)			
			{
				throw new InvalidFrameValueException(value,"Only one '/' character should be present in a Track frame.");
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
				throw new InvalidFrameValueException(value,"Invalid value for Track frame.");
			}
		}

		/// <summary>
		/// Parses this.Text and extracts the TrackNumber and TotalTracks numbers out of it.
		/// </summary>
		protected override void Parse()
		{	
			string[] parts=this.Text.Split('/');
			this._trackNumber=int.Parse(parts[0]);
			if(parts.Length==2)
			{
				this._totalTracks=int.Parse(parts[1]);
			}
		}

		/// <summary>
		/// Creates a FrameParser class for the given version and frameID.
		/// </summary>
		/// <param name="version">ID3 v2 major version to create the parser for.</param>
		/// <param name="frameID">The frame ID of the frame to be parsed.</param>
		/// <returns>A FrameParser object to parse the frame. Null if the given frame ID is not a track frame.</returns>
		public static Achamenes.ID3.Frames.Parsers.FrameParser CreateParser(ID3v2MajorVersion version, string frameID)
		{
			if(version==ID3v2MajorVersion.Version2 && frameID=="TRK")
			{
				return new Parsers.TrackTextFrameParser();
			}
			if((version==ID3v2MajorVersion.Version3 || version==ID3v2MajorVersion.Version4) && frameID=="TRCK")
			{
				return new Parsers.TrackTextFrameParser();
			}
			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="version">ID3 v2 major version to create the writer for.</param>
		/// <param name="encoding">The encoding scheme to use for writing encoded text fields.</param>
		/// <returns>A FrameWriter object to write this frame, null if writing this frame is not supported for the given version or encoding.</returns>
		public override Achamenes.ID3.Frames.Writers.FrameWriter CreateWriter(ID3v2MajorVersion version, EncodingScheme encoding)
		{
			if(version==ID3v2MajorVersion.Version2)
			{
				return new Writers.TextFrameWriter(this, "TRK", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
			}
			if(version==ID3v2MajorVersion.Version3 || version==ID3v2MajorVersion.Version4)
			{
				return new Writers.TextFrameWriter(this, "TRCK", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
			}
			return null;
		}
	}
}
