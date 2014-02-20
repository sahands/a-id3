using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames.Parsers
{
	public abstract class FrameHeaderParser
	{
		public abstract FrameHeader Parse(System.IO.Stream stream);

		public static FrameHeaderParser CreateFrameHeaderParser(ID3v2MajorVersion version)
		{
			switch(version)
			{
				case ID3v2MajorVersion.Version2:
					return new FrameHeaderParserM2();
				case ID3v2MajorVersion.Version3:
					return new FrameHeaderParserM3();
				case ID3v2MajorVersion.Version4:
					return new FrameHeaderParserM4();
				default:
					throw new FeatureNotSupportedException("Reading this version of ID3 tags is not supported.");
			}
		}
	}
	class FrameHeaderParserM2 : FrameHeaderParser
	{
		public override FrameHeader Parse(System.IO.Stream stream)
		{
			byte[] header=new byte[6];
			if(stream.Read(header, 0, 6)!=6)
			{
				//TODO Create a new exception class for frame parsing errors.
				throw new FrameParsingException("Could not read frame's header.");
			}

			if(header[0]==0)//reached the padding
			{
				return null;
			}

			int frameSize=(int)header[3]*(1<<16)+(int)header[4]*(1<<8)+(int)header[5];
			string frameID=Encoding.GetEncoding("ISO-8859-1").GetString(header, 0, 3);
			return new FrameHeader(frameID, frameSize);
		}
	}

	class FrameHeaderParserM3 : FrameHeaderParser
	{
		public override FrameHeader Parse(System.IO.Stream stream)
		{
			byte[] header=new byte[10];
			if(stream.Read(header, 0, 10)!=10)
			{
				//TODO Create a new exception class for frame parsing errors.
				throw new FrameParsingException("Could not read frame's header.");
			}

			if(header[0]==0)//reached the padding
			{
				return null;
			}
			int frameSize=(int)header[4]*(1<<24)+(int)header[5]*(1<<16)+(int)header[6]*(1<<8)+(int)header[7];
			string frameID=Encoding.GetEncoding("ISO-8859-1").GetString(header, 0, 4);
			return new FrameHeader(frameID, frameSize,header[8],header[9]);
		}
	}

	class FrameHeaderParserM4 : FrameHeaderParser
	{
		public override FrameHeader Parse(System.IO.Stream stream)
		{
			byte[] header=new byte[10];
			if(stream.Read(header, 0, 10)!=10)
			{
				//TODO Create a new exception class for frame parsing errors.
				throw new FrameParsingException("Could not read frame's header.");
			}

			if(header[0]==0)//reached the padding
			{
				return null;
			}
			int frameSize=0;
			
			// This is to be compatible with programs that do not compoletely conform 
			// to the v2.4 standards. 
			if(header[4]>=128 || header[5]>=128 || header[6]>=128 || header[7]>=128)
			{
				frameSize=(int)header[4]*(1<<24)+(int)header[5]*(1<<16)+(int)header[6]*(1<<8)+(int)header[7];
			}
			else
			{
				frameSize=(int)header[4]*(1<<21)+(int)header[5]*(1<<14)+(int)header[6]*(1<<7)+(int)header[7];
			}
			string frameID=Encoding.GetEncoding("ISO-8859-1").GetString(header, 0, 4);
			return new FrameHeader(frameID, frameSize, header[8], header[9]);
		}
	}
}