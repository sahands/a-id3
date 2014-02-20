using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames
{
	public class FrameHeader
	{
		private string _frameID;
		public string FrameID
		{
			get
			{
				return _frameID;
			}
			set
			{
				_frameID = value;
			}
		}

		private int _length;
		public int Length
		{
			get
			{
				return _length;
			}
			set
			{
				_length = value;
			}
		}

		private byte _flags1;

		public byte Flags1
		{
			get
			{
				return _flags1;
			}
			set
			{
				_flags1 = value;
			}
		}
		private byte _flags2;

		public byte Flags2
		{
			get
			{
				return _flags2;
			}
			set
			{
				_flags2 = value;
			}
		}

		public FrameHeader(string frameID, int length)
		{
			this.FrameID=frameID;
			this.Length=length;
		}

		public FrameHeader(string frameID, int length,byte flags1, byte flags2)
			:this(frameID,length)
		{
			this.Flags1=flags1;
			this.Flags2=flags2;
		}
	}
}


