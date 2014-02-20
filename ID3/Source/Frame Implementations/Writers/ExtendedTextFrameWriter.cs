using System;
using System.Collections.Generic;
using System.Text;
using Achamenes.ID3.Fields;

namespace Achamenes.ID3.Frames.Writers
{
	public class ExtendedTextFrameWriter : FrameWriter
	{
		private string _frameID;		

		public ExtendedTextFrameWriter(ExtendedTextFrame frameToWrite, string frameID, FrameHeaderWriter headerWriter, EncodingScheme encoding)
			: base(frameToWrite, headerWriter,frameID,encoding)
		{
			this._frameID=frameID;
		}

		public override void WriteToStream(System.IO.Stream stream)
		{
			ExtendedTextFrame frame=(ExtendedTextFrame)this.FrameToWrite;
			List<Field> fields=new List<Field>();

			// Declare the fields to write.
			fields.Add(new SingleByteField((byte)this.Encoding));
			if(frame.Language==LanguageCode.Unknown)
			{
				fields.Add(new FixedLengthAsciiTextField("XXX"));
			}
			else
			{
				fields.Add(new FixedLengthAsciiTextField(frame.Language.ToString()));				
			}
			fields.Add(TextField.CreateTextField(frame.Description, this.Encoding));
			fields.Add(TextField.CreateTextField(frame.Text, this.Encoding));

			// Write the header
			int length=0;
			foreach(Field f in fields)
			{
				length+=f.Length;
			}
			HeaderWriter.WriteHeader(stream, new FrameHeader(this._frameID, length));

			// Write the fields
			foreach(Field f in fields)
			{
				f.WriteToStream(stream);
			}
		}		
	}
}

