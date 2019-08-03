using System.Collections.Generic;

namespace SocketRequest
{
    public class HttpRequest
    {
        public string MethodType { get; internal set; }
        public string Url { get; internal set; }
        public readonly string HttpVersion = "HTTP/1.1";
        public string Host { get => RequestMetadata["Host"]; }
        public string ContentLength { get => RequestMetadata["Content-Length"]; }
        public string UserAgent { get => RequestMetadata["User-Agent"]; }
        public string Accept { get => RequestMetadata["Accept"]; }
        public string AcceptLanguage { get => RequestMetadata["Accept-Language"]; }
        public string AcceptEncoding { get => RequestMetadata["Accept-Encoding"]; }

        public Dictionary<string, string> UrlParameters;
        internal Dictionary<string, string> RequestMetadata = new Dictionary<string, string>();
    }
}