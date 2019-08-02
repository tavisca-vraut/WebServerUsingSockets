using System;

namespace SocketRequest
{
    public class RequestParser
    {
        public string RawMessage { get; private set; }
        private HttpRequest RequestObject { get; set; }

        public RequestParser(string requestMessage)
        {
            this.RawMessage = requestMessage;
        }

        public void BeginParsing()
        {
            this.RequestObject = new HttpRequest();

            var split = this.RawMessage.Split(' ');

            this.RequestObject.MethodType = split[0];
            this.RequestObject.Url = split[1];
            this.RequestObject.HttpVersion = split[2];
        }

        public HttpRequest GetRequestObject()
        {
            return RequestObject;
        }
    }
}
