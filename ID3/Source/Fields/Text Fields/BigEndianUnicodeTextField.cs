using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Fields
{
	class BigEndianUnicodeTextField : TextField
	{
		public BigEndianUnicodeTextField(bool isNullTerminated)
			: base(isNullTerminated)
		{

		}

		public BigEndianUnicodeTextField(string text)
			: base(text)
		{

		}

		public override int Length
		{
			get
			{
				return Encoding.Unicode.GetByteCount(this.Text)
				+2	// For the NULL terminators
				;
			}
		}

		public override int Parse(byte[] data, int offset)
		{
			base.Parse(data, offset);
			if(this.IsNullTerminated)
			{
				int nullTerminator=-1;
				for(int i = offset;i < data.Length-1;i+=2)
				{
					if(data[i]==0 && data[i+1]==0)
					{
						nullTerminator=i;
						break;
					}
				}
				if(nullTerminator==-1 || nullTerminator>=data.Length) // null-terminator was not found.
				{
					throw new FieldParsingException(this.GetType(), "Invalid Unicode text field: not null terminated.");
				}

				this.Text=System.Text.Encoding.BigEndianUnicode.GetString(data, offset, nullTerminator-offset);
				return nullTerminator-offset+2;
			}
			else
			{
				this.Text=System.Text.Encoding.BigEndianUnicode.GetString(data, offset, data.Length-offset);
				int nullTerminator=this.Text.IndexOf('\0');
				if(nullTerminator!=-1)
				{
					this.Text=this.Text.Substring(0, nullTerminator);
				}
				return data.Length-offset;
			}
		}

		public override void WriteToStream(System.IO.Stream stream)
		{
			byte[] buffer=Encoding.BigEndianUnicode.GetBytes(this.Text);
			stream.Write(buffer, 0, buffer.Length);	// char data
			stream.WriteByte(0);					// null terminator
			stream.WriteByte(0);					// null terminator
		}

		public override EncodingScheme EncodingScheme
		{
			get
			{
				return EncodingScheme.BigEndianUnicode;
			}
		}
	}
}

