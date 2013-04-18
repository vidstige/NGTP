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
            SendCommand("protocol_version\n\n");
            var response = ReadResponse();
            // verify
            return response.AsInt();
        }

        public string GetName()
        {
            SendCommand("name\n\n");
            var response = ReadResponse();
            // verify
            return response.AsString();
        }

        private Response ReadResponse()
        {
            var line = string.Empty;
            while (string.IsNullOrEmpty(line))
                line = _input.ReadLine();

            return Response.Parse(line);
        }

        private class Response
        {
            private readonly string _responseText;

            private Response(string responseText)
            {
                _responseText = responseText;
            }

            public static Response Parse(string line)
            {
                char first = line[0];
                if (first == '=')
                {
                    var firstSpaceIndex = line.IndexOf(" ", StringComparison.Ordinal);

                    if (firstSpaceIndex < 0) throw new NotImplementedException("abc");
                    
                    var id = line.Substring(1, firstSpaceIndex);
                    var responseText = line.Substring(firstSpaceIndex + 1, line.Length - firstSpaceIndex - 1);
                    return new Response(responseText);
                    
                }
                else if (first == '?')
                {
                    
                }
                else
                {
                    
                }
                throw new ArgumentException();
            }

            public int AsInt()
            {
                return int.Parse(_responseText);
            }

            public string AsString()
            {
                return _responseText;
            }
        }


        private void SendCommand(string commandName)
        {
            _output.Write(commandName);
            _output.Flush();
        }
    }
}
