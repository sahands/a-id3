using System;
using System.Collections.Generic;
using System.Text;
using Achamenes.ID3.Fields;

namespace Achamenes.ID3.Frames.Writers
{
	class PrivateFrameWriter
		: FrameWriter
	{
		public PrivateFrameWriter(PrivateFrame frameToWrite, Writers.FrameHeaderWriter frameHeaderWriter, string frameID, EncodingScheme encoding)
			: base(frameToWrite, frameHeaderWriter, frameID, encoding)
		{
		}

		public override void WriteToStream(System.IO.Stream stream)
		{
			PrivateFrame frame=(PrivateFrame)this.FrameToWrite;
			List<Field> fields=new List<Field>();

			// Declare the fields to write.
			fields.Add(TextField.CreateTextField(frame.OwnerIdentifier, EncodingScheme.Ascii));
			fields.Add(new BinaryField(frame.PrivateData, 0, frame.PrivateData.Length));

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
