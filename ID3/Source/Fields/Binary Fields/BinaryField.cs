using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Achamenes.ID3.Fields
{
	/// <summary>
	/// Represents a field containing raw binary data.
	/// </summary>
	/// <remarks>
	/// Note that this field must always be the last field in a frame as 
	/// there is no way to distinquish the end of a binary field. 
	/// That is to say, the binary field parses all the data from the start of 
	/// the field to the end of the frame.
	/// </remarks>
	public class BinaryField: Field
	{
		private byte[] _data=null;

		/// <summary>
		/// Returns the raw binary data in the field.
		/// </summary>
		public byte[] Data
		{
			get
			{
				return this._data;
			}
		}

		/// <summary>
		/// Creates a new binary data field and initializes the data to null.
		/// </summary>
		public BinaryField()
		{
			this._data=null;
		}

		/// <summary>
		/// Create a new binary field that contains the give binary data.
		/// </summary>
		/// <param name="data">The data to be contained in the field.</param>
		public BinaryField(byte[] data, int offset, int length)
		{
			this._data=new byte[length];
			Array.Copy(data, offset, this._data, 0, length);
		}

		/// <summary>
		/// Returns the length of the binary data in bytes.
		/// </summary>
		public override int Length
		{
			get
			{
				if(_data==null)
				{
					throw new InvalidOperationException("This BinaryField object has not been initialized yet.");
				}
				return _data.Length;
			}
		}

		/// <summary>
		/// Parses the binary data from a given byte array.
		/// </summary>
		/// <param name="data">The byte array to parse from.</param>
		/// <param name="offset">The index at which to start parsing.</param>
		/// <returns></returns>
		public override int Parse(byte[] data, int offset)
		{
			this._data=new byte[data.Length-offset];
			Array.Copy(data, offset, this._data, 0, this._data.Length);
			return this._data.Length;
		}

		/// <summary>
		/// Writes the binary data field to a stream.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <exception cref="ArgumentNullException">
		/// The passed stream was null.
		/// </exception>
		/// <exception cref="System.IO.IOException">
		/// There was an IO exception while trying to write the field.
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// The stream does not support writing.
		/// </exception>
		/// <exception cref="ObjectDisposedException">
		/// The passed stream was closed before the method was called.
		/// </exception>
		public override void WriteToStream(Stream stream)
		{
			base.WriteToStream(stream);
			stream.Write(this._data, 0, this._data.Length);
		}
	}
}

