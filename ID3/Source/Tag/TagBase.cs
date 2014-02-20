#define ACHAMENES_ID3_BACKUP_FILES_BEFORE_MODIFICATION

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Achamenes.ID3.Frames;
using Achamenes.ID3.Frames.Parsers;
using Achamenes.ID3.Frames.Writers;


namespace Achamenes.ID3
{
	public class ReadingWarningEventArgs
		: EventArgs
	{
		private string _frameID;
		public string FrameID
		{
			get
			{
				return _frameID;
			}
		}

		private NonFatalException _exception;

		public NonFatalException Exception
		{
			get
			{
				return _exception;
			}
		}

		public ReadingWarningEventArgs(NonFatalException exception, string frameID)
		{
			this._frameID=frameID;
			this._exception=exception;
		}
	}

	public class WritingWarningEventArgs
		:EventArgs
	{
		private Frame _frame;
		public Frame Frame
		{
			get
			{
				return _frame;
			}
		}

		private NonFatalException _exception;

		public NonFatalException Exception
		{
			get
			{
				return _exception;
			}
		}

		public WritingWarningEventArgs(NonFatalException exception, Frame frame)
		{
			this._frame=frame;
			this._exception=exception;
		}
	}

	public delegate void ReadingWarningEventHandler(object sender, ReadingWarningEventArgs args);
	public delegate void WritingWarningEventHandler(object sender, WritingWarningEventArgs args);

	public class TagBase
	{
		private static Random _rand=new Random();
		public event ReadingWarningEventHandler ReadingWarning;
		public event WritingWarningEventHandler WritingWarning;

		private List<Frame> _frames;

		public TagBase()
		{
			_frames=new List<Frame>();
		}

		public List<Frames.Frame> Frames
		{
			get
			{
				return _frames;
			}
		}

		public Frame SearchForFrame(Type frameType)
		{
			foreach(Frame frame in Frames)
			{
				if(typeof(Frame)==frameType)
				{
					return frame;
				}
			}
			return null;
		}

		public virtual ID3v2MajorVersion ReadFromFile(string fileName)
		{
			return ReadFromFile(fileName, new FrameParserFactory());
		}

		public virtual ID3v2MajorVersion ReadFromFile(string fileName, FrameParserFactory frameParserFactory)
		{
			FileStream stream=File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			try
			{
				return Read(stream,frameParserFactory);
			}
			finally
			{
				if(stream!=null)
				{
					stream.Close();
				}
			}
		}

		private void RaiseReadingWarning(NonFatalException exception,string frameID)
		{
			if(ReadingWarning!=null)
			{
				ReadingWarning(this, new ReadingWarningEventArgs(exception,frameID));
			}
		}

		private void RaiseWritingWarning(NonFatalException exception, Frame frameBeingWritten)
		{
			if(WritingWarning!=null)
			{
				WritingWarning(this, new WritingWarningEventArgs(exception, frameBeingWritten));
			}
		}

		private static string GetTempIntermediateFileName(string fileName)
		{
			string tempFileName=fileName+".x"+_rand.Next().ToString();
			// Try at most ten times to get a file name that does not exist in the
			// directory.
			for(int i=0;File.Exists(tempFileName) && i<10;i++)
			{
				tempFileName+=fileName+".x"+_rand.Next().ToString();
			}
			if(File.Exists(tempFileName))
				throw new FatalException("Could not create a temporary file in the directory.");
			return tempFileName;
		}

		private static string GetBackupFileName(string fileName)
		{			
			// Try a maximum of 100 times.
			int revision=0;
			for(revision=1;File.Exists(fileName+"."+revision.ToString("##")+".old") && revision<100;revision++)
			{
			}
			//TODO assuming that it will eventually succeed here!
			return fileName+"."+revision.ToString("##")+".old";
		}

		private static void CopyFromStreamToStream(Stream sourceStream, Stream destStream)
		{
			byte[] buffer=new byte[256];
			while(sourceStream.Position<sourceStream.Length)
			{
				int read=sourceStream.Read(buffer, 0, 256);
				destStream.Write(buffer, 0, read);
				if(read<256)
				{
					break;
				}
			}
		}

		private void WriteFrame(Frame frame, Stream stream, ID3v2MajorVersion version, EncodingScheme encoding)
		{
			FrameWriter writer=frame.CreateWriter(version, encoding);
			if(writer==null)
			{
				throw new NoFrameWriterProvidedException(frame, version);
			}
			writer.WriteToStream(stream);
		}

		private void WriteFrames(Stream stream, ID3v2MajorVersion version, EncodingScheme encoding)
		{
			foreach(Frame f in this.Frames)
			{
				try
				{
					WriteFrame(f,stream,version,encoding);					
				}
				catch(NonFatalException exception)
				{
					RaiseWritingWarning(exception, f);
				}
			}
		}

		public virtual void WriteToFile(string fileName, ID3v2MajorVersion version, EncodingScheme encoding)
		{
			FileStream targetFileStream=null;
			try
			{
				// First write all frame data to a memory buffer
				// This way we catch all fatal exceptions before 
				// touching the file, so there's not a even a slight 
				// chance of corrupting the file.

				MemoryStream memoryBuffer=new MemoryStream();
				WriteFrames(memoryBuffer, version, encoding);

				int newSize=(int)memoryBuffer.Length;

				if(newSize==0)
				{
					throw new FatalException("No data to write in the tag.");
				}

				targetFileStream=File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
				TagHeader oldHeader=TagHeader.FromStream(targetFileStream);

				bool useIntermediate=false;

				if(oldHeader==null)
				{
					// There is no attached tag in the file. We need to use an intermediate temporary file.					
					useIntermediate=true;
					// Rewind the place in stream to the very beginning, so that when
					// we copy the data from the original file to the intermediate file,
					// we won't lose any data.
					targetFileStream.Seek(0, SeekOrigin.Begin);
				}
				else
				{
					// File already has an ID3 v2 tag attached.
					if(oldHeader.TagSize<newSize)
					{
						useIntermediate=true;
						// Allow for 4KB of padding.
						newSize+=4*1024;
						// Move to the correct place in stream so when we copy data to 
						// the intermediate file, we won't lose any data.
						// The +10 is to go skip the tag header, since it's not included
						// in TagHeader.TagSize 
						targetFileStream.Seek(oldHeader.TagSize+10, SeekOrigin.Begin);
					}
					else
					{
						// We should write exactly the same number of bytes back to the file.
						// When writing the padding, compare memoryBuffer.Length to newSize to
						// calculate the number of padding bytes required to write.
						newSize=oldHeader.TagSize;
						// Seek the beginning of the file. The tag header and frame information
						// will be overwritten.
						targetFileStream.Seek(0, SeekOrigin.Begin);
					}
				}

				TagHeader newHeader=new TagHeader((byte)version, 0, TagHeaderFlags.None, newSize);

				if(useIntermediate)
				{
					string intermediateFileName=GetTempIntermediateFileName(fileName);
					FileStream intermediateStream=null;
					try
					{
						intermediateStream=File.Create(intermediateFileName);
						newHeader.WriteToStream(intermediateStream);

						// Write the frame data residing in memory buffer.
						intermediateStream.Write(memoryBuffer.GetBuffer(), 0, (int)memoryBuffer.Length);

						//Write any required paddings.
						for(int i=(int)memoryBuffer.Length;i<newSize;i++)
						{
							intermediateStream.WriteByte(0);
						}

						// Copy the data from the original file to the intermediate file.
						CopyFromStreamToStream(targetFileStream, intermediateStream);

						// Close the stream of original and intermediate file streams.
						targetFileStream.Close();
						intermediateStream.Close();

						// If control reaches this point, then everything went file, so
						// should normally delete the original file and rename the intermediate file.
						// But as a safety measure, for pre-release, alpha, and beta version, 
						// instead of removing the file, I decided to rename the old file to
						// fileName+".old."+revisionNumber instead. The user can manually delete
						// the these files after ensuring the integrity of the new files.
#if ACHAMENES_ID3_BACKUP_FILES_BEFORE_MODIFICATION
						File.Move(fileName, GetBackupFileName(fileName));
#else
						File.Delete(fileName);
#endif
						File.Move(intermediateFileName, fileName);
					}
					finally
					{
						if(intermediateStream!=null)
						{
							intermediateStream.Close();
						}
					}
				}
				else // If using an intermediate file is not necessary.
				{
					// Similarly, we would normally just start writing to @stream here,
					// but instead, we close it, make a backup copy of it, and then 
					// open it again, and write the tag information just to ensure nothing
					// will be lost.

					targetFileStream.Close();
#if ACHAMENES_ID3_BACKUP_FILES_BEFORE_MODIFICATION
					File.Copy(fileName, GetBackupFileName(fileName));
#endif
					targetFileStream=File.Open(fileName, FileMode.Open, FileAccess.Write, FileShare.Write);
					
					// Write the header.
					newHeader.WriteToStream(targetFileStream);
					
					// Write frame data from memory buffer.
					targetFileStream.Write(memoryBuffer.GetBuffer(), 0, (int)memoryBuffer.Length);
					
					// Write any required padding.
					for(int i=(int)memoryBuffer.Length;i<newSize;i++)
					{
						targetFileStream.WriteByte(0);
					}

					// And finally close the stream.
					targetFileStream.Close();
				}
			}
			finally
			{
				if(targetFileStream!=null)
				{
					targetFileStream.Close();
				}
			}
		}

		private ID3v2MajorVersion Read(Stream stream, FrameParserFactory frameParserFactory)
		{
			TagHeader header=TagHeader.FromStream(stream);
			if(header==null)
			{
				throw new FatalException("No ID3 v2 tag is attached to this file.");
			}
			if(!Enum.IsDefined(typeof(ID3v2MajorVersion),header.MajorVersion) )
			{
				throw new FatalException("Reading this major version of ID3 v2 is not supported.");
			}
			if(header.Flags!=TagHeaderFlags.None)
			{
				throw new FatalException("Reading tags with with any set flags are not supported in this version.");
			}
			long startingPosition=stream.Position;
			while(stream.Position-startingPosition <header.TagSize)
			{
				long beginPosition=stream.Position;
				Frame frame=null;
				string frameID="";
				try
				{					
					frame=FrameParser.Parse(stream, (ID3v2MajorVersion)header.MajorVersion, frameParserFactory, out frameID);
					if(frame==null)
					{
						break;
					}
				}
				catch(NonFatalException ex)
				{
					RaiseReadingWarning(ex,frameID);
				}
				if(beginPosition==stream.Position) // Probably stuck in an infinite loop because of corrupt data in file. Exit the loop.
				{
					break;
				}
				if(frame!=null)
				{
					this.Frames.Add(frame);
				}				
			}
			return (ID3v2MajorVersion)header.MajorVersion;
		}

		/// <summary>
		/// Removes any attached ID3v2 tags attached to the given file.
		/// Does not change the ID3v1 tags.
		/// </summary>
		/// <param name="fileName">Full path of the file whose tags are to be removed.</param>
		/// <returns>True if tags were found and successfully removed. False otherwise.</returns>
		public static bool RemoveTag(string fileName)
		{
			FileStream stream=File.Open(fileName,FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
			try
			{
				TagHeader header=TagHeader.FromStream(stream);
				if(header==null)
				{
					return false;
				}
				// An intermediate file needs to be used.
				string intermediateFile=GetTempIntermediateFileName(fileName);
				FileStream intermedStream=File.Create(intermediateFile);
				stream.Seek(header.TagSize+10, SeekOrigin.Begin);
				
				//Copy the data after the tag to the intermediate file.
				CopyFromStreamToStream(stream, intermedStream);
				intermedStream.Close();
				stream.Close();

				// If control reaches this point, then everything went file, so
				// should normally delete the original file and rename the intermediate file.
				// But as a safety measure, for pre-release, alpha, and beta version, 
				// instead of removing the file, I decided to rename the old file to
				// fileName+".old."+revisionNumber instead. The user can manually delete
				// the these files after ensuring the integrity of the new files.
#if ACHAMENES_ID3_BACKUP
				File.Move(fileName, GetBackupFileName(fileName));
#else
						File.Delete(fileName);
#endif
				File.Move(intermediateFile, fileName);

				return true;
			}
			finally
			{
				if(stream!=null)
				{
					stream.Close();
				}
			}
		}
	}
}