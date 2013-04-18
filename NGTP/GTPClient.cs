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

        public int GetProtocolVersion()
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

        public string GetVersion()
        {
            SendCommand("version\n\n");
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

        private void SendCommand(string commandName)
        {
            _output.Write(commandName);
            _output.Flush();
        }
    }
}
