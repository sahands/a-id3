using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3
{
	/// <summary>
	/// Specifies what major version of ID3 v2 was used, for example Version3 for ID3 v2.3.
	/// </summary>
	public enum ID3v2MajorVersion : byte
	{
		/// <summary>
		/// ID3 v2.2
		/// </summary>
		Version2=2,
		/// <summary>
		/// ID3 v2.3
		/// </summary>
		Version3=3,
		/// <summary>
		/// ID3 v2.4
		/// </summary>
		Version4=4
	}
}

