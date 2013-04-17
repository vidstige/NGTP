using System.IO;
using NUnit.Framework;

namespace NGTP.Specs
{
    [TestFixture]
    public class Given_GtpClient
    {
        [Test]
        public void TestGetVersion()
        {
            var outstream = new MemoryStream();
            var instream = new MemoryStream();
            TextWriter output = new StreamWriter(outstream);
            TextReader input = new StreamReader(instream);

            var mock1 = new StreamWriter(instream);
            mock1.Write("= 44\n");
            mock1.Flush();
            mock1.BaseStream.Seek(0, SeekOrigin.Begin);

            var client = new GtpClient(input, output);
            var actual = client.GetVersion();
            Assert.That(actual, Is.EqualTo(44));
        }
    }
}
