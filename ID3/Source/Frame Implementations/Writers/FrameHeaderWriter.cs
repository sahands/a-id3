using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames.Writers
{
	public abstract class FrameHeaderWriter
	{
		public abstract void WriteHeader(System.IO.Stream stream, FrameHeader frameHeader);

		public static FrameHeaderWriter CreateFrameHeaderWriter(ID3v2MajorVersion version)
		{
			if(version==ID3v2MajorVersion.Version2)
			{
				return new FrameHeaderWriterM2();
			}
			else if(version==ID3v2MajorVersion.Version3)
			{
				return new FrameHeaderWriterM3();
			}
			else if(version==ID3v2MajorVersion.Version4)
			{
				return new FrameHeaderWriterM4();
			}

			throw new FeatureNotSupportedException("Writing ID3 tags in the provided version is not supported by this implementation.");
		}
	}

	class FrameHeaderWriterM2 : FrameHeaderWriter
	{
		public override void WriteHeader(System.IO.Stream stream, FrameHeader frameHeader)
		{
			string frameID=frameHeader.FrameID;			
			stream.Write(Encoding.GetEncoding("ISO-8859-1").GetBytes(frameID), 0, 3);

			int size=frameHeader.Length;
			byte[] sizeData=new byte[3];
			sizeData[2]=(byte)(size%0x100);
			size/=0x100;
			sizeData[1]=(byte)(size%0x100);
			size/=0x100;
			sizeData[0]=(byte)(size%0x100);
			stream.Write(sizeData, 0, 3);
		}
	}

	class FrameHeaderWriterM3 : FrameHeaderWriter
	{
		public override void WriteHeader(System.IO.Stream stream, FrameHeader frameHeader)
		{
			// Frame ID   $xx xx xx xx  (four characters)
			// Size       $xx xx xx xx
			// Flags      $xx xx
			string frameID=frameHeader.FrameID;
			stream.Write(Encoding.GetEncoding("ISO-8859-1").GetBytes(frameID), 0, 4);

			int size=frameHeader.Length;
			byte[] sizeData=new byte[4];
			sizeData[3]=(byte)(size%0x100);
			size/=0x100;
			sizeData[2]=(byte)(size%0x100);
			size/=0x100;
			sizeData[1]=(byte)(size%0x100);
			size/=0x100;
			sizeData[0]=(byte)(size%0x100);
			stream.Write(sizeData, 0, 4);

			stream.WriteByte(0); // Flags
			stream.WriteByte(0); // Flags
		}
	}

	class FrameHeaderWriterM4 : FrameHeaderWriter
	{
		public override void WriteHeader(System.IO.Stream stream, FrameHeader frameHeader)
		{
			// Frame ID      $xx xx xx xx  (four characters)
			// Size      4 * %0xxxxxxx
			// Flags         $xx xx
			string frameID=frameHeader.FrameID;
			stream.Write(Encoding.GetEncoding("ISO-8859-1").GetBytes(frameID), 0, 4);

			int size=frameHeader.Length;
			byte[] sizeData=new byte[4];
			sizeData[3]=(byte)(size%0x80);
			size/=0x80;
			sizeData[2]=(byte)(size%0x80);
			size/=0x80;
			sizeData[1]=(byte)(size%0x80);
			size/=0x80;
			sizeData[0]=(byte)(size%0x80);
			stream.Write(sizeData, 0, 4);

			stream.WriteByte(0); // Flags
			stream.WriteByte(0); // Flags
		}
	}
}

