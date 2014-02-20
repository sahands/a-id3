using System;
using System.Collections.Generic;
using System.Text;
using Achamenes.ID3.Fields;

namespace Achamenes.ID3.Frames.Writers
{
	class TextFrameWriter : FrameWriter
	{
		public TextFrameWriter(TextFrame frameToWrite, string frameID, FrameHeaderWriter headerWriter, EncodingScheme encoding)
			: base(frameToWrite, headerWriter,frameID, encoding)
		{
		}

		public override void WriteToStream(System.IO.Stream stream)
		{
			TextFrame frame=(TextFrame)this.FrameToWrite;
			List<Field> fields=new List<Field>();

			// Declare the fields to write.
			fields.Add(new SingleByteField((byte)this.Encoding));
			fields.Add(TextField.CreateTextField(frame.Text, this.Encoding));

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

