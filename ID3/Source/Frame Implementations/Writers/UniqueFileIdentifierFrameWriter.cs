using System;
using System.Collections.Generic;
using System.Text;
using Achamenes.ID3.Fields;

namespace Achamenes.ID3.Frames.Writers
{
	class UniqueFileIdentifierFrameWriter
		: Writers.FrameWriter
	{
		public UniqueFileIdentifierFrameWriter(UniqueFileIdentifierFrame frameToWrite, Writers.FrameHeaderWriter frameHeaderWriter, string frameID, EncodingScheme encoding)
			: base(frameToWrite, frameHeaderWriter, frameID, encoding)
		{
		}

		public override void WriteToStream(System.IO.Stream stream)
		{
			//Owner identifier        <text string> $00
			//Identifier              <up to 64 bytes binary data>

			UniqueFileIdentifierFrame frame=(UniqueFileIdentifierFrame)this.FrameToWrite;
			List<Field> fields=new List<Field>();

			// Declare the fields to write.
			fields.Add(TextField.CreateTextField(frame.Owner, EncodingScheme.Ascii));
			fields.Add(new BinaryField(frame.UniqueIdentifier, 0, frame.UniqueIdentifier.Length));

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


