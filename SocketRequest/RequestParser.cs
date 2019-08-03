using System.Collections.Generic;

namespace SocketRequest
{
    public class RequestParser
    {
        public string RawMessage { get; private set; }
        private HttpRequest RequestObject;

        private List<ILineParser> lineParsersFactory = new List<ILineParser>()
        {
            new RequestMetaDataParser(),
            new UrlParametersLineParser(),
            new StatusLineParser()
        };

        public RequestParser(string requestMessage)
        {
            this.RawMessage = requestMessage;
            this.RequestObject = new HttpRequest();
        }

        public void ProcessEachLineOfRequest()
        {
            foreach (var line in RawMessage.Split('\n'))
            {
                Parse(line);
            }
        }

        private void Parse(string line)
        {
            foreach (var parser in lineParsersFactory)
            {
                if (parser.TryParse(line, ref RequestObject) == true)
                    return;
            }
        }

        public HttpRequest GetRequestObject()
        {
            return RequestObject;
        }
    }
}
