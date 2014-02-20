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
	[TestFixture(Description="Tests the BinaryField class.")]
	public class BinaryFieldTest
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
				byte[] randomData=new byte[_randomNumberGenerator.Next(1, 50000)];
				_randomNumberGenerator.NextBytes(randomData);
				BinaryField field=new BinaryField(randomData, 0, randomData.Length);
				MemoryStream stream=new MemoryStream();
				field.WriteToStream(stream);
				Assert.AreEqual(stream.Length, randomData.Length);
				for(int i = 0;i < randomData.Length;i++)
				{
					Assert.AreEqual(randomData[i], stream.GetBuffer()[i]);
				}
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
				BinaryField field=new BinaryField();
				field.Parse(randomData, offset);
				Assert.AreEqual(field.Data.Length, randomData.Length-offset);
				Assert.AreEqual(field.Length, randomData.Length-offset);
				Assert.AreEqual(field.Length, field.Data.Length);
				for(int i = offset;i < randomData.Length;i++)
				{
					Assert.AreEqual(randomData[i], field.Data[i-offset]);
				}
			}
		}
	}
}
