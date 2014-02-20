using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3
{
	/// <summary>
	/// Enumeration of possible types of an attached picture frame.
	/// </summary>
	public enum PictureType : byte
	{
		Other=0x00,
		FileIconPNGOnly=0x01,
		OtherFileIcon=0x02,
		CoverFront=0x03,
		CoverBack=0x04,
		LeafletPage=0x05,
		LabelSideOfCD=0x06,
		LeadArtist=0x07,
		ArtistPerformer=0x08,
		Conductor=0x09,
		BandOrchestra=0x0A,
		Composer=0x0B,
		Lyricist=0x0C,
		RecordingLocation=0x0D,
		DuringRecording=0x0E,
		DuringPerformance=0x0F,
		MovieVideoScreenCapture=0x10,
		BrightColouredFish=0x11,
		Illustration=0x12,
		BandartistLogotype=0x13,
		PublisherStudioLogotype=0x14
	}	
}


