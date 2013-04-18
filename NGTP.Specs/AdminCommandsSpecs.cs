using NUnit.Framework;

namespace NGTP.Specs
{
    [TestFixture]
    public class AdminCommandsSpecs : Given_GtpClient
    {
        private GtpClient _gtpClient;

        [SetUp]
        public void SetUp()
        {
            _gtpClient = CreateGtpClient();
        }

        [Test]
        public void TestGetProtocolVersion()
        {
            AnswerWith("= 44\n");

            var actual = _gtpClient.GetProtocolVersion();
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

        [Test]
        public void TestGetVersion()
        {
            AnswerWith("= 10.10.20\n\n");

            var actual = _gtpClient.GetVersion();
            Assert.That(Output, Is.EqualTo("version\n\n"));
            Assert.That(actual, Is.EqualTo("10.10.20"));
        }
    }
}