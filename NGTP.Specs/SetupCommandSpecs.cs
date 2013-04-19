using NUnit.Framework;

namespace NGTP.Specs
{
    [TestFixture]
    public class SetupCommandSpecs: Given_GtpClient
    {
        private GtpClient _gtpClient;

        [SetUp]
        public void SetUp()
        {
            _gtpClient = CreateGtpClient();
        }

        [Test]  
        public void SetValidBoardSize()
        {
            AnswerWith("=\n\n");

            _gtpClient.SetBoardSize(19);
            Assert.That(Output, Is.EqualTo("boardsize 19\n\n"));
        }

        [Test]
        [ExpectedException(typeof(CommandException), ExpectedMessage = "unacceptable size")]
        public void SetUnacceptableBoardSize()
        {
            AnswerWith("? unacceptable size\n\n");

            _gtpClient.SetBoardSize(1024);
            Assert.That(Output, Is.EqualTo("boardsize 1024\n\n"));
        }

        [Test]
        public void ClearBoard()
        {
            AnswerWith("=\n\n");

            _gtpClient.ClearBoard();
            Assert.That(Output, Is.EqualTo("clear_board\n\n"));
        }
    } 
}
