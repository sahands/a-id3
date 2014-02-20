using System;
using System.Collections.Generic;
using System.Text;
using Achamenes.ID3.Fields;

namespace Achamenes.ID3.Frames.Parsers
{
	/// <summary>
	/// Base class for all frame parsers.
	/// </summary>
	public abstract class FrameParser
	{	
		/// <summary>
		/// When implemented in a derived class, parses a Frame out of the given 
		/// byte array.
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		protected abstract Frame ParseFrame(byte[] data);

		/// <summary>
		/// This function parses the next frame from the stream and returns it.
		/// </summary>
		/// <param name="stream">The stream to parse the Frame from.</param>
		/// <param name="version">The ID3 v2 Major version of the ID3 tag that the stream is reading.</param>
		/// <param name="parserFactory">A FrameParserFactory to use to create FrameParsers based on Frame IDs.</param>
		/// <param name="frameID">Output: outputs the frameID of the frame just parsed.</param>
		/// <returns>The parsed Frame object.</returns>
		/// <exception cref="Achamenes.ID3.NoFrameParserProvidedException">
		/// Thrown if the FrameParserFactory object passed did not recognize the Frame ID for the given version.
		/// </exception>
		public static Frame Parse(System.IO.Stream stream, ID3v2MajorVersion version, FrameParserFactory parserFactory, out string frameID)
		{
			FrameHeaderParser headerParser=FrameHeaderParser.CreateFrameHeaderParser(version);
			FrameHeader header=headerParser.Parse(stream);
			frameID="";
			if(header==null) // have reached the padding, no more frames.
			{				
				return null;
			}
			frameID=header.FrameID;

			if(header.Length > 128*128*128*128)
			{
				throw new FatalException("Invalid frame length for frame with frame ID '"+frameID+"'.");
			}

			byte[] frameData=new byte[header.Length];
			stream.Read(frameData, 0, frameData.Length);
			FrameParser parser=parserFactory.CreateFrameParser(version,header.FrameID);
			if(parser==null)
			{
				throw new NoFrameParserProvidedException(frameID,version, "No frame parser object is provided to parse this type of frame in this implementation.");
			}
			return parser.ParseFrame(frameData);
		}
	}
}

