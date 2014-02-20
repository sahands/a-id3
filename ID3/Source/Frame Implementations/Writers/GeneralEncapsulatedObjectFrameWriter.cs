using System;
using System.Collections.Generic;
using System.Text;
using Achamenes.ID3.Fields;

namespace Achamenes.ID3.Frames.Writers
{
	class GeneralEncapsulatedObjectFrameWriter
		: Writers.FrameWriter
	{
		public GeneralEncapsulatedObjectFrameWriter(GeneralEncapsulatedObjectFrame frameToWrite, Writers.FrameHeaderWriter frameHeaderWriter, string frameID, EncodingScheme encoding)
			: base(frameToWrite, frameHeaderWriter, frameID, encoding)
		{
		}

		public override void WriteToStream(System.IO.Stream stream)
		{
			//Text encoding          $xx
			//MIME type              <text string> $00
			//Filename               <text string according to encoding> $00 (00)
			//Content description    <text string according to encóding> $00 (00)
			//Encapsulated object    <binary data>

			GeneralEncapsulatedObjectFrame frame=(GeneralEncapsulatedObjectFrame)this.FrameToWrite;
			List<Field> fields=new List<Field>();

			// Declare the fields to write.
			fields.Add(new SingleByteField((byte)this.Encoding));
			fields.Add(TextField.CreateTextField(frame.MimeType, EncodingScheme.Ascii));
			fields.Add(TextField.CreateTextField(frame.Filename, this.Encoding));
			fields.Add(TextField.CreateTextField(frame.ContentDescription, this.Encoding));
			fields.Add(new BinaryField(frame.BinaryData, 0, frame.BinaryData.Length));

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


