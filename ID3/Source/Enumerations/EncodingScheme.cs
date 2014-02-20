using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3
{
	/// <summary>
	/// This enumeration specifies the text _encoding scheme to use when writing text fields.
	/// </summary>
	/// <remarks>
	/// ID3 versions prior to ID3v2.4 do not support the BigEndianUnicode and UTF8 _encoding schemes.
	/// An exception of type NotSupportedException will be thrown if you try to use an _encoding scheme
	/// that is not supported by the ID3 version in that context.
	/// </remarks>
	public enum EncodingScheme : byte
	{
		/// <summary>
		/// ISO-8859-1. Null terminated with 0x00.     
		/// </summary>
	    Ascii=0,
		/// <summary>
		/// UTF-16 encoded Unicode with BOM. Null terminated with 0x00 0x00.     
		/// </summary>
		UnicodeWithBOM=1,
		/// <summary>
		/// UTF-16BE (Big-Endian) encoded Unicode without BOM. Null terminated with 0x00 0x00.     
		/// </summary>
		BigEndianUnicode=2,
		/// <summary>
		/// UTF-8 encoded Unicode. Null terminated with 0x00.
		/// </summary>
		UTF8=3
	}	
}

