using System;
using System.Collections.Generic;
using System.Text;
using Achamenes.ID3.Fields;

namespace Achamenes.ID3.Frames.Writers
{
	class PlayCounterFrameWriter
		: FrameWriter
	{
		public PlayCounterFrameWriter(PlayCounterFrame frameToWrite, Writers.FrameHeaderWriter frameHeaderWriter, string frameID, EncodingScheme encoding)
			: base(frameToWrite, frameHeaderWriter, frameID, encoding)
		{
		}

		public override void WriteToStream(System.IO.Stream stream)
		{
			PlayCounterFrame frame=(PlayCounterFrame)this.FrameToWrite;
			List<Field> fields=new List<Field>();

			// Declare the fields to write.
			byte[] data=new byte[4];
			int c=frame.Counter;
			data[3]=(byte)(c%0x100);
			c>>=8;
			data[2]=(byte)(c%0x100);
			c>>=8;
			data[1]=(byte)(c%0x100);
			c>>=8;
			data[0]=(byte)(c%0x100);
			fields.Add(new BinaryField(data, 0, data.Length));

			// Write the header
			int length=0;
			foreach(Field f in fields)
			{
				length+=f.Length;
			}
			HeaderWriter.WriteHeader(stream, new FrameHeader(this.FrameID, length));

			// Write the fields
			foreach(Field f in fields)
			{
				f.WriteToStream(stream);
			}
		}
	}
}

