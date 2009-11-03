using System;
using System.Text;
using System.Xml;
using System.IO;
using System.IO.Compression;

using Google.GData.Client;

using NUnit.Framework;

namespace Google.GData.Client.UnitTests
{
	/// <summary>
	/// Summary description for gziptest.
	/// </summary>
    [TestFixture]
    public class GZipStreamTest : BaseTestClass
	{
        private const string    Data = "System.IO.Compression.GZipStream";
        private const string    Base64GZipData = "H4sIAEDbsEYAA3PPz0/PSdVzd0ksSdRzzslMzSvRc4/KLAguKUpNzAUAg1++Bx4AAAA=";

        private byte[]          compressedData;

		public GZipStreamTest()
		{
		}

        public override void InitTest()
        {
            base.InitTest();
            this.compressedData = Convert.FromBase64String(Base64GZipData);
        }

        [Test]
        public void TestInit()
        {
            Stream      baseStream = new MemoryStream();
            GZipStream  gzipStream = new GZipStream(baseStream, CompressionMode.Decompress);

            Assert.AreSame(baseStream, gzipStream.BaseStream);

            gzipStream.Close();
        }

        [Test]
        public void TestDecompress()
        {
            Stream      baseStream = new MemoryStream(this.compressedData);
            GZipStream  gzipStream = new GZipStream(baseStream, CompressionMode.Decompress);

            StreamReader reader = new StreamReader(gzipStream);

            string  data = reader.ReadToEnd();

            Assert.AreEqual(Data, data);

            gzipStream.Close();
        }

        [Test]
        public void TestDestructor()
        {
            using (GZipStream stream = new GZipStream(new MemoryStream(), CompressionMode.Decompress))
            {
            }
        }

        [Test]
        public void TestProperties()
        {
            Stream      baseStream = new MemoryStream(this.compressedData);
            GZipStream  gzipStream = new GZipStream(baseStream, CompressionMode.Decompress);

            Assert.IsTrue(gzipStream.CanRead);
            Assert.IsFalse(gzipStream.CanWrite);
            Assert.IsFalse(gzipStream.CanSeek);

            try { long i = gzipStream.Length; }
            catch (NotSupportedException) {}

            try { long i = gzipStream.Position; }
            catch (NotSupportedException) {}

            try { gzipStream.Position = 0; }
            catch (NotSupportedException) {}
        }

        [Test]
        public void TestNotSupported()
        {
            Stream      baseStream = new MemoryStream(this.compressedData);
            GZipStream  gzipStream = new GZipStream(baseStream, CompressionMode.Decompress);

            try { gzipStream.Write(null, 0, 0); }
            catch (NotSupportedException) {}

            try { gzipStream.Flush(); }
            catch (NotSupportedException) {}

            try { gzipStream.Seek(0, SeekOrigin.Begin); }
            catch (NotSupportedException) {}

            try { gzipStream.SetLength(0); }
            catch (NotSupportedException) {}
        }
	}
}
