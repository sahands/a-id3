using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames.Parsers
{
	public class FrameParserFactory
	{
		private delegate FrameParser CreateParserDelegate(ID3v2MajorVersion version, string frameID);
		public virtual FrameParser CreateFrameParser(ID3v2MajorVersion version, string frameID)
		{
			List<CreateParserDelegate> parsers=new List<CreateParserDelegate>();

			parsers.Add(new CreateParserDelegate(Frames.AlbumTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.ArtistTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.BeatsPerMinuteTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.ComposerTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.CopyrightTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.CustomUserTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.DateTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.EncodedByTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.EncodingTimeTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.FileTypeTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.GenreTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.GroupingTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.InitialKeyTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.LanguageTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.LengthTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.MediaTypeTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.OrchestraTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.OriginalAlbumTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.OriginalArtistTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.OriginalReleaseTimeTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.OriginalReleaseYearTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.PartOfSetTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.PublisherTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.RecordingTimeTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.ReleaseTimeTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.SoftwareSettingsTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.TaggingTimeTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.TimeTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.TitleTextFrame.CreateParser));		
			parsers.Add(new CreateParserDelegate(Frames.TrackTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.YearTextFrame.CreateParser));

			parsers.Add(new CreateParserDelegate(Frames.CommentExtendedTextFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.LyricsExtendedTextFrame.CreateParser));

			parsers.Add(new CreateParserDelegate(Frames.CommercialUrlFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.CustomUserUrlFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.OfficialArtistUrlFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.OfficialAudioFileUrlFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.OfficialAudioSourceUrlFrame.CreateParser));

			parsers.Add(new CreateParserDelegate(Frames.GeneralEncapsulatedObjectFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.MusicCDIdentifierFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.PictureFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.PlayCounterFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.PrivateFrame.CreateParser));
			parsers.Add(new CreateParserDelegate(Frames.UniqueFileIdentifierFrame.CreateParser));

			foreach(CreateParserDelegate p in parsers)
			{
				FrameParser parser=p(version, frameID);
				if(parser!=null)
					return parser;
			}
			return null;
		}
	}

}


