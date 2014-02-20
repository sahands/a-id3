using System;
using System.Collections.Generic;
using System.Text;
using Achamenes.ID3.Fields;

namespace Achamenes.ID3.Frames.Writers
{
	public abstract class FrameWriter
	{
		private EncodingScheme _encoding;
		private string _frameID;
		private FrameHeaderWriter _headerWriter;
		private Frame _frame;

		protected FrameHeaderWriter HeaderWriter
		{
			get
			{
				return _headerWriter;
			}
		}

		protected Frame FrameToWrite
		{
			get
			{
				return _frame;
			}
		}

		protected string FrameID
		{
			get
			{
				return _frameID;
			}
		}

		protected EncodingScheme Encoding
		{
			get
			{
				return this._encoding;
			}
		}

		protected FrameWriter(Frame frameToWriter, FrameHeaderWriter headerWriter, string frameID, EncodingScheme encoding)
		{
			if(frameToWriter==null)
			{
				throw new ArgumentNullException("The Frame object passed can not be null.");
			}
			if(headerWriter==null)
			{
				throw new ArgumentNullException("The FrameHeaderWriter object can not be null.");
			}
			this._frameID=frameID;
			this._frame=frameToWriter;
			this._headerWriter=headerWriter;
			this._encoding=encoding;
		}

		public abstract void WriteToStream(System.IO.Stream stream);
	}
}

