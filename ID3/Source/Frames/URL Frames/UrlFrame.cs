using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames
{
	[global::System.Serializable]
	public abstract class UrlFrame : Frame
	{
		private string _url;

		public string Url
		{
			get
			{
				return _url;
			}
			set
			{
				if(value==null)
				{
					throw new ArgumentNullException("The set value can not be null.");
				}
				_url = value;
			}
		}

		public UrlFrame(string url)
		{
			if(url==null)
			{
				throw new ArgumentNullException("The passed url parameter can not be null.");
			}
			this._url=url;
		}
	}
}
