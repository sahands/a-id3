using System;
using System.Collections.Generic;
using System.Text;
using Achamenes.ID3.Fields;

namespace Achamenes.ID3.Frames.Writers
{
	class PictureFrameWriterM2 : FrameWriter
	{
		public PictureFrameWriterM2(PictureFrame frameToWrite, string frameID, Writers.FrameHeaderWriter headerWriter, EncodingScheme encoding)
			: base(frameToWrite, headerWriter, frameID, encoding)
		{
		}

		public override void WriteToStream(System.IO.Stream stream)
		{
			PictureFrame frame=(PictureFrame)this.FrameToWrite;
			List<Field> fields=new List<Field>();

			// Declare the fields to write.
			fields.Add(new SingleByteField((byte)this.Encoding));
			fields.Add(new FixedLengthAsciiTextField("JPG"));
			fields.Add(new SingleByteField((byte)frame.PictureType));
			fields.Add(TextField.CreateTextField(frame.Description, this.Encoding));

			System.IO.MemoryStream memoryBuffer=new System.IO.MemoryStream();
			frame.Picture.Save(memoryBuffer, System.Drawing.Imaging.ImageFormat.Jpeg);
			fields.Add(new BinaryField(memoryBuffer.GetBuffer(), 0, (int)memoryBuffer.Length));
			memoryBuffer.Close();

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

	class PictureFrameWriterM3And4 : FrameWriter
	{
		public PictureFrameWriterM3And4(PictureFrame frameToWrite, string frameID, Writers.FrameHeaderWriter headerWriter, EncodingScheme encoding)
			: base(frameToWrite, headerWriter, frameID, encoding)
		{
		}

		public override void WriteToStream(System.IO.Stream stream)
		{
			PictureFrame frame=(PictureFrame)this.FrameToWrite;
			List<Field> fields=new List<Field>();

			// Declare the fields to write.
			fields.Add(new SingleByteField((byte)this.Encoding));
			fields.Add(TextField.CreateTextField("image/jpeg", EncodingScheme.Ascii));
			fields.Add(new SingleByteField((byte)frame.PictureType));
			fields.Add(TextField.CreateTextField(frame.Description, this.Encoding));

			System.IO.MemoryStream memoryBuffer=new System.IO.MemoryStream();
			frame.Picture.Save(memoryBuffer, System.Drawing.Imaging.ImageFormat.Jpeg);
			fields.Add(new BinaryField(memoryBuffer.GetBuffer(), 0, (int)memoryBuffer.Length));
			memoryBuffer.Close();

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

