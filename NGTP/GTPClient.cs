using System;
using System.IO;

namespace NGTP
{
    public class GtpClient
    {
        private readonly TextReader _input;
        private readonly TextWriter _output;

        public GtpClient(TextReader input, TextWriter output)
        {
            _input = input;
            _output = output;
        }

        public int GetVersion()
        {
            _output.WriteLine("protocol_version");
            var answer = _input.ReadLine();
            if (answer != null)
            {
                var firstSpaceIndex = answer.IndexOf(" ", StringComparison.Ordinal);
                if (firstSpaceIndex >= 0)
                {
                    var a = answer.Substring(0, firstSpaceIndex);
                    var b = answer.Substring(firstSpaceIndex + 1, answer.Length - firstSpaceIndex - 1);
                    return int.Parse(b);
                }
            }
            return -1;
        }
    }
}
