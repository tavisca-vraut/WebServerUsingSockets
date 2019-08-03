using System;

namespace SocketRequest
{
    public interface ILineParser
    {
        bool TryParse(string line, ref HttpRequest requestObject);
        void Process(string line, ref HttpRequest requestObject);
    }

    public class RequestMetaDataParser : ILineParser
    {
        public bool TryParse(string line, ref HttpRequest requestObject)
        {
            if (IsRequestMetaDataLine(line) == false)
                return false;

            Process(line, ref requestObject);
            return true;
        }

        private bool IsRequestMetaDataLine(string line)
        {
            return line.Contains(':') == true;
        }

        public void Process(string line, ref HttpRequest requestObject)
        {
            throw new System.NotImplementedException();
        }
    }
}
