using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Fields
{
	class UnicodeTextField : TextField
	{
		public UnicodeTextField(bool isNullTerminated)
			: base(isNullTerminated)
		{

		}

		public UnicodeTextField(string text)
			: base(text)
		{

		}

		public override int Length
		{
			get
			{
				return Encoding.Unicode.GetByteCount(this.Text)
				+2	// For the BOM
				+2	// For the NULL terminators
				;
			}
		}

		public override int Parse(byte[] data, int offset)
		{
			int read=base.Parse(data, offset);
			Encoding encoder=null;
			if(data[offset]==0xff && data[offset+1]==0xfe)
			{
				encoder=Encoding.Unicode;
			}
			else if(data[offset]==0xfe && data[offset+1]==0xff)
			{
				encoder=Encoding.BigEndianUnicode;
			}
			else
			{
				throw new FieldParsingException(this.GetType(), "Invalid Unicode text field: incorrect BOM character.");
			}

			if(this.IsNullTerminated)
			{
				int nullTerminator=-1;
				for(int i = offset+2;i < data.Length-1;i+=2)
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
				this.Text=encoder.GetString(data, offset+2, nullTerminator-offset-2);
				return nullTerminator-offset+2;
			}
			else
			{
				this.Text=encoder.GetString(data, offset+2, data.Length-offset-2);
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
			byte[] buffer=Encoding.Unicode.GetBytes(this.Text);
			stream.WriteByte(0xff);					// first byte of BOM 
			stream.WriteByte(0xfe);					// second byte of BOM
			stream.Write(buffer, 0, buffer.Length);	// char data
			stream.WriteByte(0);					// null terminator
			stream.WriteByte(0);					// null terminator
		}

		public override EncodingScheme EncodingScheme
		{
			get
			{
				return EncodingScheme.UnicodeWithBOM;
			}
		}
	}
}

