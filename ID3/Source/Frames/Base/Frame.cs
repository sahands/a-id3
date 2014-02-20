using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames
{
	[global::System.Serializable]
	public abstract class Frame
	{
		//public abstract Parsers.FrameParser CreateParser(ID3v2MajorVersion version, string frameID);
		public abstract Writers.FrameWriter CreateWriter(ID3v2MajorVersion version, EncodingScheme encoding);
	}
}
