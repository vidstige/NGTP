using System.IO;
using System.Text;
using NUnit.Framework;

namespace NGTP.Specs
{
    [TestFixture]
    public class Given_GtpClient
    {
        private GtpClient _gtpClient;
        private MemoryStream _outstream;
        private MemoryStream _instream;
        private readonly Encoding _encoding = Encoding.UTF8;

        [SetUp]
        public void SetUp()
        {
            _outstream = new MemoryStream();
            _instream = new MemoryStream();
            TextWriter output = new StreamWriter(_outstream, _encoding);
            TextReader input = new StreamReader(_instream, _encoding);

            _gtpClient = new GtpClient(input, output);
        }

        [Test]
        public void TestGetVersion()
        {
            AnswerWith("= 44\n");

            var actual = _gtpClient.GetVersion();
            Assert.That(Output, Is.EqualTo("protocol_version\n\n"));
            Assert.That(actual, Is.EqualTo(44));
        }

        [Test]
        public void TestGetName()
        {
            AnswerWith("= mockengine\n\n");

            var actual = _gtpClient.GetName();
            Assert.That(Output, Is.EqualTo("name\n\n"));
            Assert.That(actual, Is.EqualTo("mockengine"));
        }

        protected string Output
        {
            get
            {
                _outstream.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(_outstream))
                {
                    return reader.ReadToEnd();
                }
            }
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
