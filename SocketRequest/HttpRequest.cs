using System.Collections.Generic;

namespace SocketRequest
{
    public class HttpRequest
    {
        public string MethodType { get; internal set; }
        public string Url { get; internal set; }
        public string Host
        {
            get
            {
                return RequestMetadata["Host"];
            }
        }

        public readonly string HttpVersion = "HTTP/1.1";
        public Dictionary<string, string> UrlParameters;
        internal Dictionary<string, string> RequestMetadata;
    }
}