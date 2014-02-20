using System;
using System.Collections.Generic;
using System.Text;
using Achamenes.ID3.Fields;

namespace Achamenes.ID3.Frames.Writers
{
	class MusicCDIdentifierFrameWriter
		: Writers.FrameWriter
	{
		public MusicCDIdentifierFrameWriter(MusicCDIdentifierFrame frameToWrite, Writers.FrameHeaderWriter frameHeaderWriter, string frameID, EncodingScheme encoding)
			: base(frameToWrite, frameHeaderWriter, frameID, encoding)
		{
		}

		public override void WriteToStream(System.IO.Stream stream)
		{
			MusicCDIdentifierFrame frame=(MusicCDIdentifierFrame)this.FrameToWrite;
			List<Field> fields=new List<Field>();

			// Declare the fields to write.
			fields.Add(new BinaryField(frame.Identifier, 0, frame.Identifier.Length));

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

