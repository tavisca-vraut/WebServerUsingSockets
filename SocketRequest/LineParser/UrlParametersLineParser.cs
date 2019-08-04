using System;
using System.Collections.Generic;

namespace SocketRequest
{
    public class UrlParametersLineParser : ILineParser
    {
        public bool TryParse(string line, ref HttpRequest requestObject)
        {
            if (IsUrlParametersLine(line) == false)
                return false;

            Process(line, ref requestObject);
            return true;
        }

        public void Process(string line, ref HttpRequest requestObject)
        {
            InitializeUrlParametersStore(ref requestObject);
            ParseUrlParameters(line, requestObject);
        }
        private void InitializeUrlParametersStore(ref HttpRequest requestObject)
        {
            requestObject.UrlParameters = new Dictionary<string, string>();
        }

        private static void ParseUrlParameters(string line, HttpRequest requestObject)
        {
            var parameters = line.Split('&');

            foreach (var parameter in parameters)
            {
                var keyValue = parameter.Split('=');
                var key = keyValue[0].Trim();
                var value = keyValue[1].Trim().Replace("+", " ");

                requestObject.UrlParameters[key] = value;
            }
        }

        private bool IsUrlParametersLine(string line)
        {
            return string.IsNullOrWhiteSpace(line) == false &&
                   (line.Contains('&') || line.Contains('='));
        }
    }
}
