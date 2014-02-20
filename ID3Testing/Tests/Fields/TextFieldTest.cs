using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using Achamenes.ID3;
using Achamenes.ID3.Fields;
using Achamenes.ID3.Frames;
using Achamenes.ID3.V1;

namespace Achamenes.ID3Tests
{
	[TestFixture(Description="Tests the AsciiTextField class.")]
	public class TextFieldTest
	{
		private static string[] _asciiTestCases=
			{
				"",
				"a short string",
				"a long long long long long long long long long long long long long long long long string"
			};
		private static string[] _unicodeTestCases=
			{
				"",
				"a string with unicode text in it: εν αρχη ην...",
				"Even more Unicode characters: سلام",
				" تو آنی که گفتی که رویین تنم     بلند آسمان بر زمین بر زنم",
				"και η ζωη ην το φως των ανθρωπων."
			};
		
		[Test(Description="Tests the Parse and Write methods for ASCII strings.")]
		public void TestAscii()
		{
			foreach(string testCase in _asciiTestCases)
			{
				TextField field=TextField.CreateTextField(testCase, EncodingScheme.Ascii);
				MemoryStream stream=new MemoryStream();

				field.WriteToStream(stream);

				TextField field2=TextField.CreateTextField(true, EncodingScheme.Ascii);
				field2.Parse(stream.GetBuffer(), 0);

				Assert.AreEqual(field.Text, field2.Text);
			}
		}

		[Test(Description="Tests the Parse and Write methods for UTF8 strings.")]
		public void TestUTF8()
		{
			foreach(string testCase in _unicodeTestCases)
			{
				TextField field=TextField.CreateTextField(testCase, EncodingScheme.UTF8);
				MemoryStream stream=new MemoryStream();

				field.WriteToStream(stream);

				TextField field2=TextField.CreateTextField(true, EncodingScheme.UTF8);
				field2.Parse(stream.GetBuffer(), 0);

				Assert.AreEqual(field.Text, field2.Text);
			}
		}

		[Test(Description="Tests the Parse and Write methods for UTF16 with BOM strings.")]
		public void TestUTF16BOM()
		{
			foreach(string testCase in _unicodeTestCases)
			{
				TextField field=TextField.CreateTextField(testCase, EncodingScheme.UnicodeWithBOM);
				MemoryStream stream=new MemoryStream();

				field.WriteToStream(stream);

				TextField field2=TextField.CreateTextField(true, EncodingScheme.UnicodeWithBOM);
				field2.Parse(stream.GetBuffer(), 0);

				Assert.AreEqual(field.Text, field2.Text);
			}
		}

		[Test(Description="Tests the Parse and Write methods for UTF16 without BOM strings.")]
		public void TestUTF16WithoutBOM()
		{
			foreach(string testCase in _unicodeTestCases)
			{
				TextField field=TextField.CreateTextField(testCase, EncodingScheme.BigEndianUnicode);
				MemoryStream stream=new MemoryStream();

				field.WriteToStream(stream);

				TextField field2=TextField.CreateTextField(true, EncodingScheme.BigEndianUnicode);
				field2.Parse(stream.GetBuffer(), 0);

				Assert.AreEqual(field.Text, field2.Text);
			}
		}
	}
}
