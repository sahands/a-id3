using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3
{
	/// <summary>
	/// Encapsulates the 10 byte long header of an ID3 v2 tag.
	/// </summary>
	class TagHeader
	{
		private byte majorVersion;
		private byte minorVersion;
		private TagHeaderFlags flags;
		private int tagSize;

		/// <summary>
		/// Gets the major version of the tag.
		/// </summary>
		public byte MajorVersion
		{
			get
			{
				return majorVersion;
			}
		}

		/// <summary>
		/// Gets the minor version of the tag.
		/// </summary>
		public byte MinorVersion
		{
			get
			{
				return minorVersion;
			}
		}

		/// <summary>
		/// Gets the tag size as specified in the header.
		/// </summary>
		public int TagSize
		{
			get
			{
				return tagSize;
			}
		}


		/// <summary>
		/// Gets the flags in the header.
		/// </summary>
		public TagHeaderFlags Flags
		{
			get
			{
				return flags;
			}
		}

		/// <summary>
		/// Constructs a new TagHeader object representing an ID3 v2 header.
		/// </summary>
		/// <param name="majorVersion">The major version of the ID3 tag (e.g. 3 for ID3v2.3)</param>
		/// <param name="minorVersion">The minor version of the ID3 tag</param>
		/// <param name="flags">The flag bits</param>
		/// <param name="tagSize">The total size of the tag (excluding the header itself)</param>
		/// <exception cref="System.ArgumentException">
		/// The given tagSize was too large.
		/// </exception>
		public TagHeader(byte majorVersion, byte minorVersion, TagHeaderFlags flags, int tagSize)
		{
			if(tagSize>= (1<<22))
				throw (new ArgumentException("The passed tag size is too large."));
			this.majorVersion=majorVersion;
			this.minorVersion=minorVersion;
			this.tagSize=tagSize;
			this.flags=flags;
		}


		/// <summary>
		/// Writes the ID3 v2 header to a given stream.
		/// </summary>
		/// <param name="stream">The stream to write the header to.</param>
		/// <exception cref="ArgumentNullException">
		/// The passed stream was null.
		/// </exception>
		/// <exception cref="System.IO.IOException">
		/// There was an IO exception while trying to write the header.
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// The stream does not support writing.
		/// </exception>
		/// <exception cref="ObjectDisposedException">
		/// The passed stream was closed before the method was called.
		/// </exception>
		public void WriteToStream(System.IO.Stream stream)
		{
			stream.Write(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes("ID3"), 0, 3);

			stream.WriteByte(this.majorVersion);
			stream.WriteByte(this.minorVersion);
			stream.WriteByte((byte)this.flags);

			int size=this.tagSize;
			byte[] sizeData=new byte[4];
			sizeData[3]=(byte)(size%(1<<7));
			size>>=7;
			sizeData[2]=(byte)(size%(1<<7));
			size>>=7;
			sizeData[1]=(byte)(size%(1<<7));
			size>>=7;
			sizeData[0]=(byte)(size%(1<<7));
			stream.Write(sizeData, 0, 4);
		}

		/// <summary>
		/// Reads the ID3 v2 header from a stream.
		/// </summary>
		/// <param name="stream">The stream to read the header from.</param>
		/// <returns>
		/// Returns a TagHeader object with the information 
		/// in the header, null if no ID3 v2 tag was available.
		/// </returns>
		/// <remarks>
		/// The method returns null if it can not find a valid ID3 v2
		/// tag in the file. This means that in case of a corrupt
		/// ID3 v2 header, (e.g. invalid tag size), null is returned.
		/// 
		/// This method can therefore be used to check a file
		/// for the existence of a valid ID3 v2 tag.
		/// 
		/// NOTE: Valid in this context does not mean of compatible version
		/// or having compatible flags.
		/// </remarks>
		/// <exception cref="ArgumentNullException">
		/// The passed stream was null.
		/// </exception>
		/// <exception cref="System.IO.IOException">
		/// There was an IO exception while trying to read the header.
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// The stream does not support reading.
		/// </exception>
		/// <exception cref="ObjectDisposedException">
		/// The passed stream was closed before the method was called.
		/// </exception>
		public static TagHeader FromStream(System.IO.Stream stream)
		{
			if(stream==null)
				throw new ArgumentNullException("The parameter 'stream' can not be null.");

			byte[] data=new byte[10];
			int read=stream.Read(data, 0, 10);

			if(read!=10)
				return null;

			if(System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(data, 0, 3)!="ID3")
				return null;

			int size=0;

			if(data[6]>127 || data[7] > 127 || data[8]>127 || data[9]> 127) // invalid size
				return null;

			checked
			{
				try
				{
					size=data[6]*(1<<21)+data[7]*(1<<14)+data[8]*(1<<7)+data[9];
				}
				catch(OverflowException) // Invalid size in stream
				{
					return null;
				}
			}

			TagHeader header=new TagHeader(data[3], data[4], (TagHeaderFlags)data[5], size);

			return header;
		}
	}
}

