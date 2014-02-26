using System;
using System.Drawing.Imaging;

namespace Achamenes.ID3.Utils
{
	public class ImagingHelpers
	{
		public static string ImageFormatToMimeType(ImageFormat imageFormat)
		{
			foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageDecoders()) 
			{
				if (codec.FormatID == imageFormat.Guid) 
				{
					return codec.MimeType;
				}
			}
			return "application/octet-stream";
		}

		// Taken from http://www.java2s.com/Code/CSharp/File-Stream/ImageFormattoExtension.htm
		// And modified.
		public static string ImageFormatToExtension(ImageFormat imageFormat)
		{
			if (imageFormat.Equals(ImageFormat.Bmp))
			{
				return "BMP";
			}
			else if (imageFormat.Equals(ImageFormat.MemoryBmp))
			{
				return "BMP";
			}
			else if (imageFormat.Equals(ImageFormat.Wmf))
			{
				return "EMF";
			}
			else if (imageFormat.Equals(ImageFormat.Wmf))
			{
				return "WMF";
			}
			else if (imageFormat.Equals(ImageFormat.Gif))
			{
				return "GIF";
			}
			else if (imageFormat.Equals(ImageFormat.Jpeg))
			{
				return "JPG";
			}
			else if (imageFormat.Equals(ImageFormat.Png))
			{
				return "PNG";
			}
			else if (imageFormat.Equals(ImageFormat.Tiff))
			{
				return "TIF";
			}
			else if (imageFormat.Equals(ImageFormat.Exif))
			{
				return "EXF";
			}
			else if (imageFormat.Equals(ImageFormat.Icon))
			{
				return "ICO";
			}
			return "";
		}
	}
}

