using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Achamenes.ID3.Fields
{
	public abstract class Field
	{
		/// <summary>
		/// When overridden in a derived class, writes 
		/// the field to a given stream.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <exception cref="System.ArgumentNullException">
		/// The passed stream was null.
		/// </exception>
		public virtual void WriteToStream(Stream stream)
		{
			if(stream==null)
				throw new ArgumentNullException("The parameter 'stream' can not be null.");
		}
		/// <summary>
		/// When overridden in a derived class, parses the field
		/// data from the given byte array starting at position 'offset'.
		/// </summary>
		/// <param name="data">Byte array to parse from.</param>
		/// <param name="offset">Index in the byte array at which parsing starts.</param>
		/// <returns>
		/// The number of bytes used from the byte array.
		/// </returns>
		public virtual int Parse(byte[] data, int offset)
		{
			if(data==null)
			{
				throw new ArgumentNullException("Passed data array was null.");
			}
			if(offset>data.Length || offset<0)
			{
				throw new FieldParsingException(this.GetType(), "Expected field was not found in frame.");
			}
			return 0;
		}

		/// <summary>
		/// When overridden in a derived class, returns the number
		/// of bytes required when writing the field to a stream.
		/// </summary>
		public abstract int Length
		{
			get;
		}
	}
}

