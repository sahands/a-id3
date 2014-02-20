using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Fields
{
	public abstract class TextField : Field			
	{
		private string _text=null;

		protected void CheckStatus()
		{
			if(_text==null)
			{
				throw new InvalidOperationException("The TextField object is not initialized yet.");
			}
		}

		private bool _isNullTerminated;

		protected bool IsNullTerminated
		{
			get
			{
				return _isNullTerminated;
			}
		}


		public string Text
		{
			get
			{
				CheckStatus();
				return _text;
			}
			set
			{
				if(value==null)
				{
					throw new ArgumentNullException("value","Text can not be null.");
				}
				_text = value;
			}
		}

		public TextField(bool isNullTerminated)
		{
			this._isNullTerminated=isNullTerminated;
		}

		public TextField(string text)
		{
			this._text=text;
			this._isNullTerminated=true;
		}

		public TextField(string text, bool isNullTerminated)
			:this(isNullTerminated)
		{
			this.Text=text;			
		}

		public abstract EncodingScheme EncodingScheme
		{
			get;
		}

		public static TextField CreateTextField(string text,EncodingScheme encoding)
		{
			switch(encoding)
			{
				case EncodingScheme.Ascii:
					return new AsciiTextField(text);
				case EncodingScheme.UnicodeWithBOM:
					return new UnicodeTextField(text);
				case EncodingScheme.BigEndianUnicode:
					return new BigEndianUnicodeTextField(text);
				case EncodingScheme.UTF8:
					return new UTF8TextField(text);
				default:
					throw new FeatureNotSupportedException("The provided encoding scheme is not supported in this version.");
			}
		}

		public static TextField CreateTextField(bool isNullTerminated, EncodingScheme encoding)
		{
			switch(encoding)
			{
				case EncodingScheme.Ascii:
					return new AsciiTextField(isNullTerminated);
				case EncodingScheme.UnicodeWithBOM:
					return new UnicodeTextField(isNullTerminated);
				case EncodingScheme.BigEndianUnicode:
					return new BigEndianUnicodeTextField(isNullTerminated);
				case EncodingScheme.UTF8:
					return new UTF8TextField(isNullTerminated);
				default:
					throw new FeatureNotSupportedException("The provided encoding scheme is not supported in this version.");
			}
		}
	}
}

