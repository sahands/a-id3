using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using Achamenes.ID3;
using Achamenes.ID3.Fields;
using Achamenes.ID3.Frames;
using Achamenes.ID3.Frames.Parsers;
using Achamenes.ID3.Frames.Writers;
using Achamenes.ID3.V1;

namespace Achamenes.ID3Tests
{
	[TestFixture(Description="Tests reading and writing a variety of field types class.")]
	public class TagTest
	{
		private string _originalFile;
		private string _taggedFile1;
		private string _taggedFile2;
		private Random _random=new Random();

		private byte[] GetRandomByteArray(int minSize, int maxSize)
		{
			byte[] data=new byte[_random.Next(minSize, maxSize)];
			_random.NextBytes(data);
			return data;
		}

		[SetUp]
		public void SetUp()
		{
			_originalFile=Path.GetTempFileName();
			_taggedFile1=Path.GetTempFileName();
			_taggedFile2=Path.GetTempFileName();

			File.Delete(_originalFile);
			File.Delete(_taggedFile1);
			File.Delete(_taggedFile2);

			FileStream stream=File.Open(_originalFile, FileMode.CreateNew, FileAccess.Write, FileShare.Write);
			byte[] data=GetRandomByteArray(1, 1);
			stream.Write(data, 0, data.Length);
			stream.Close();

			File.Copy(_originalFile, _taggedFile1);
			File.Copy(_originalFile, _taggedFile2);
		}

		private bool CompareFiles(string file1, string file2)
		{
			System.Console.WriteLine (file1);
			System.Console.WriteLine (file2);
			FileStream stream1=File.OpenRead(file1);
			FileStream stream2=File.OpenRead(file2);
			try
			{
				if(stream1.Length!=stream2.Length)
				{
					return false;
				}

				while(stream1.Position<stream1.Length)
				{
					int b1 = stream1.ReadByte();
					int b2 = stream2.ReadByte();
					if(b1!=b2)
					{
						return false;
					}
					if(b1==-1)
					{
						break;
					}
				}
				return true;
			}
			finally
			{
				if(stream1!=null)
				{
					stream1.Close();
				}
				if(stream2!=null)
				{
					stream2.Close();
				}
			}
		}

		private void RunTest(TagBase tag, ID3v2MajorVersion version, EncodingScheme encoding)
		{
			File.Delete(_taggedFile1);
			File.Delete(_taggedFile2);
			File.Copy(_originalFile, _taggedFile1);
			File.Copy(_originalFile, _taggedFile2);

			tag.WriteToFile(_taggedFile1, version, encoding);

			TagBase tag2=new TagBase();
			tag2.ReadFromFile(_taggedFile1);

			tag2.WriteToFile(_taggedFile2, version, encoding);
			Assert.IsTrue(CompareFiles(_taggedFile1, _taggedFile2));

			TagBase.RemoveTag(_taggedFile1);
			Assert.IsTrue(CompareFiles(_taggedFile1, _originalFile));

			TagBase.RemoveTag(_taggedFile2);
			Assert.IsTrue(CompareFiles(_taggedFile2, _originalFile));
		}	

		[Test]
		public void TestReadAndWrite()
		{
			TagBase tag=new TagBase();

			//Add text frames common to all versions
			tag.Frames.Add(new AlbumTextFrame("Album"));
			tag.Frames.Add(new ArtistTextFrame("Artist"));
			tag.Frames.Add(new ComposerTextFrame("Composer"));
			tag.Frames.Add(new CopyrightTextFrame("Copyright"));
			tag.Frames.Add(new CustomUserTextFrame("CustomUser"));
			tag.Frames.Add(new DateTextFrame("Date"));
			tag.Frames.Add(new EncodedByTextFrame("EncodedBy"));			
			tag.Frames.Add(new FileTypeTextFrame("FileType"));
			tag.Frames.Add(new GenreTextFrame("Genre"));
			tag.Frames.Add(new GroupingTextFrame("Grouping"));
			tag.Frames.Add(new InitialKeyTextFrame("InitialKey"));
			tag.Frames.Add(new LanguageTextFrame("Language"));
			tag.Frames.Add(new LengthTextFrame("Length"));
			tag.Frames.Add(new MediaTypeTextFrame("MediaType"));
			tag.Frames.Add(new OrchestraTextFrame("Orchestra"));
			tag.Frames.Add(new OriginalAlbumTextFrame("OriginalAlbum"));
			tag.Frames.Add(new OriginalArtistTextFrame("OriginalArtist"));			
			tag.Frames.Add(new OriginalReleaseYearTextFrame("OriginalReleaseYear"));
			tag.Frames.Add(new PublisherTextFrame("Publisher"));
			tag.Frames.Add(new SoftwareSettingsTextFrame("SoftwareSettings"));
			tag.Frames.Add(new TitleTextFrame("Title"));
			tag.Frames.Add(new EncodingTimeTextFrame("EncodingTime"));
			tag.Frames.Add(new RecordingTimeTextFrame("RecordingTime"));
			tag.Frames.Add(new ReleaseTimeTextFrame("ReleaseTime"));
			tag.Frames.Add(new TaggingTimeTextFrame("TaggingTime"));
			tag.Frames.Add(new OriginalReleaseTimeTextFrame("OriginalReleaseTime"));

			// Picture frames
			System.Drawing.Image img = System.Drawing.Image.FromFile ("Resources/photo.jpg");
			tag.Frames.Add(new PictureFrame(img, "Description!", PictureType.CoverBack));
			tag.Frames.Add(new PictureFrame(img, "Yet another attached picture!", PictureType.CoverFront));

			// General encapsulated object frame
			tag.Frames.Add(new GeneralEncapsulatedObjectFrame("filename.dat", "some descriptoin!", "image/jpeg", GetRandomByteArray(1000, 1000)));
	
			// Music CD Identifier frame
			tag.Frames.Add(new MusicCDIdentifierFrame(GetRandomByteArray(10, 10)));

			// Play Counter Frame
			tag.Frames.Add(new PlayCounterFrame(11245));

			// Private Frame
			tag.Frames.Add(new PrivateFrame("owner", GetRandomByteArray(100,100)));

			// Unique File ID Frame
			tag.Frames.Add(new UniqueFileIdentifierFrame(GetRandomByteArray(100, 100), "owner"));

			// URL frames
			tag.Frames.Add(new CommercialUrlFrame("http://www.google.com/"));
			tag.Frames.Add(new CustomUserUrlFrame("http://www.google.com/"));
			tag.Frames.Add(new OfficialArtistUrlFrame("http://www.google.com/"));
			tag.Frames.Add(new OfficialAudioFileUrlFrame("http://www.google.com/"));
			tag.Frames.Add(new OfficialAudioSourceUrlFrame("http://www.google.com/"));

			// Part and Track frames
			tag.Frames.Add(new PartOfSetTextFrame(4));
			tag.Frames.Add(new PartOfSetTextFrame(45, 123));
			tag.Frames.Add(new TrackTextFrame(415));
			tag.Frames.Add(new TrackTextFrame(15, 1234));

			// Year frame
			tag.Frames.Add(new YearTextFrame(""));
			tag.Frames.Add(new YearTextFrame("14"));
			tag.Frames.Add(new YearTextFrame("144"));
			tag.Frames.Add(new YearTextFrame("1234"));
			
			//Time text frame
			tag.Frames.Add(new TimeTextFrame("134"));

			//Beats per minute frame
			tag.Frames.Add(new BeatsPerMinuteTextFrame(134));

			RunTest(tag, ID3v2MajorVersion.Version4, EncodingScheme.Ascii);
			RunTest(tag, ID3v2MajorVersion.Version4, EncodingScheme.BigEndianUnicode);
			RunTest(tag, ID3v2MajorVersion.Version4, EncodingScheme.UnicodeWithBOM);
			RunTest(tag, ID3v2MajorVersion.Version4, EncodingScheme.UTF8);

			RunTest(tag, ID3v2MajorVersion.Version2, EncodingScheme.Ascii);
			RunTest(tag, ID3v2MajorVersion.Version2, EncodingScheme.BigEndianUnicode);
			RunTest(tag, ID3v2MajorVersion.Version2, EncodingScheme.UnicodeWithBOM);
			RunTest(tag, ID3v2MajorVersion.Version2, EncodingScheme.UTF8);

			RunTest(tag, ID3v2MajorVersion.Version3, EncodingScheme.Ascii);
			RunTest(tag, ID3v2MajorVersion.Version3, EncodingScheme.BigEndianUnicode);
			RunTest(tag, ID3v2MajorVersion.Version3, EncodingScheme.UnicodeWithBOM);
			RunTest(tag, ID3v2MajorVersion.Version3, EncodingScheme.UTF8);			
		}
	}
}
