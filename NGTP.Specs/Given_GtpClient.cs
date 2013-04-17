using System.IO;
using NUnit.Framework;

namespace NGTP.Specs
{
    [TestFixture]
    public class Given_GtpClient
    {
        private GtpClient _gtpClient;
        private MemoryStream _outstream;
        private MemoryStream _instream;

        [SetUp]
        public void SetUp()
        {
            _outstream = new MemoryStream();
            _instream = new MemoryStream();
            TextWriter output = new StreamWriter(_outstream);
            TextReader input = new StreamReader(_instream);

            _gtpClient = new GtpClient(input, output);
        }

        [Test]
        public void TestGetVersion()
        {
            AnswerWith("= 44\n");

            Assert.That(_gtpClient.GetVersion(), Is.EqualTo(44));
        }

        private void AnswerWith(string format)
        {
            var mock1 = new StreamWriter(_instream);
            var previous = mock1.BaseStream.Position;
            mock1.Write(format);
            mock1.Flush();
            mock1.BaseStream.Seek(previous, SeekOrigin.Begin);
        }
    }
}
