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
	public class FixedLengthAsciiTextFieldTest
	{
		private static string[] _asciiTestCases=
			{
				"",
				"a short string",
				"a long long long long long long long long long long long long long long long long string"
			};

		[Test(Description="Tests the Parse and Write methods for ASCII strings.")]
		public void DoTest()
		{
			foreach(string testCase in _asciiTestCases)
			{
				FixedLengthAsciiTextField field=new FixedLengthAsciiTextField(testCase);
				MemoryStream stream=new MemoryStream();

				field.WriteToStream(stream);

				FixedLengthAsciiTextField field2=new FixedLengthAsciiTextField(testCase.Length);
				field2.Parse(stream.GetBuffer(), 0);

				Assert.AreEqual(field.Text, field2.Text);
			}
		}
	}
}
