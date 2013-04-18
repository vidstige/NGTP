using System.IO;
using System.Text;

namespace NGTP.Specs
{
    public class Given_GtpClient
    {
        private MemoryStream _outstream;
        private MemoryStream _instream;
        private readonly Encoding _encoding = Encoding.UTF8;

        public GtpClient CreateGtpClient()
        {
            _outstream = new MemoryStream();
            _instream = new MemoryStream();
            TextWriter output = new StreamWriter(_outstream, _encoding);
            TextReader input = new StreamReader(_instream, _encoding);

            return new GtpClient(input, output);
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

        protected void AnswerWith(string format)
        {
            var mock1 = new StreamWriter(_instream);
            var previous = mock1.BaseStream.Position;
            mock1.Write(format);
            mock1.Flush();
            mock1.BaseStream.Seek(previous, SeekOrigin.Begin);
        }
    }
}
