using System;

namespace NGTP
{
    internal class Response
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
}