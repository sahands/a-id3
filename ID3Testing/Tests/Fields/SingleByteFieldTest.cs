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
	[TestFixture(Description="Tests the SingleByteField class.")]
	public class SingleByteFieldTest
	{
		private Random _randomNumberGenerator=null;

		[SetUp]
		public void SetUp()
		{
			_randomNumberGenerator=new Random();
		}

		[Test(Description="Tests the Write method of the class.")]
		public void TestWrite()
		{
			for(int testCase=0;testCase<100;testCase++)
			{
				byte[] randomData=new byte[1];
				_randomNumberGenerator.NextBytes(randomData);
				SingleByteField field=new SingleByteField(randomData[0]);
				MemoryStream stream=new MemoryStream();
				field.WriteToStream(stream);
				Assert.AreEqual(stream.Length, 1);
				Assert.AreEqual(randomData[0], stream.GetBuffer()[0]);
			}
		}

		[Test(Description="Tests the Read method of the class.")]
		public void TestRead()
		{
			for(int testCase=0;testCase<100;testCase++)
			{
				byte[] randomData=new byte[_randomNumberGenerator.Next(1, 50000)];
				int offset=_randomNumberGenerator.Next(0, randomData.Length-1);
				_randomNumberGenerator.NextBytes(randomData);
				SingleByteField field=new SingleByteField();
				field.Parse(randomData, offset);
				Assert.AreEqual(randomData[offset], field.Value);				
			}
		}
	}
}
