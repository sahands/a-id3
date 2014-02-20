using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames
{
	public abstract class ExtendedTextFrame : Frame
	{
		private string _description;

		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
			}
		}

		private LanguageCode _language;

		public LanguageCode Language
		{
			get
			{
				return _language;
			}
			set
			{
				_language = value;
			}
		}

		private string _text;

		public string Text
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value;
			}
		}

		protected ExtendedTextFrame(string text, string description, LanguageCode language)
			:base()
		{
			this.Text=text;
			this.Description=description;
			this.Language=language;			
		}
	}
}
