using System;

namespace SocketRequest
{
    public class StatusLineParser : ILineParser
    {
        public bool TryParse(string line, ref HttpRequest requestObject)
        {
            if (IsStatusLine(line, ref requestObject) == false)
                return false;

            Process(line, ref requestObject);
            return true;
        }

        public void Process(string line, ref HttpRequest requestObject)
        {
            var statusData = line.Split(' ');
            requestObject.MethodType = statusData[0];
            requestObject.Url = statusData[1];
        }
        private bool IsStatusLine(string line, ref HttpRequest requestObject)
        {
            return string.IsNullOrWhiteSpace(line) == false &&
                   line.Contains(requestObject.HttpVersion) == true;
        }
    }
}
