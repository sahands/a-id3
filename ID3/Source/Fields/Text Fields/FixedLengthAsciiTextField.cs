using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Fields
{
	public class FixedLengthAsciiTextField : TextField
	{
		private int _length;

		public override int Length
		{
			get
			{
				return _length;
			}
		}
	
		public FixedLengthAsciiTextField(int length)
			: base(false)
		{
			_length=length;
		}

		public FixedLengthAsciiTextField(string text)
			: base(text)
		{
			if(text==null)
			{
				throw new ArgumentNullException("text","Parameter can not be null.");
			}

			_length=text.Length;
		}

		public override int Parse(byte[] data, int offset)
		{
			int read=base.Parse(data, offset);
			if(data.Length<offset+this.Length)
			{
				throw new FieldParsingException(this.GetType(), "Expected fixed length text field was not found in frame.");
			}

			this.Text=Encoding.GetEncoding("ISO-8859-1").GetString(data, offset, this.Length);
			return this.Length;
		}

		public override void WriteToStream(System.IO.Stream stream)
		{
			byte[] buffer=Encoding.GetEncoding("ISO-8859-1").GetBytes(this.Text);
			stream.Write(buffer, 0, buffer.Length);	// char data
		}

		public override EncodingScheme EncodingScheme
		{
			get
			{
				return EncodingScheme.Ascii;
			}
		}
	}
}

