using System;
using System.Text;

namespace SocketResponse
{
    public class HttpResponse
    {
        public byte[] Header { get; set; }
        public StringBuilder ResponseMessage { get; set; }
        public const string HttpVersion = "HTTP/1.1";
        public const string Server = "Localhost (RV): Windows 10 64-bit";
        public DateTime? Date { get; set; } = null;
        public int? ContentLength { get; set; } = null;
        public string ContentType { get; set; } = null;
        public int StatusCode { get; set; } = 200;
        public string StatusMessage { get; set; } = "OK";
        public byte[] Data { get; set; } = null;

        public  bool IsSetContentLength()
        {
            if (ContentLength == null)
                throw new ArgumentNullException("Content Length is NULL.");
            return true;
        }
        public bool IsSetContentType()
        {
            if (ContentType == null)
                throw new ArgumentNullException("Content Type is NULL.");
            return true;
        }
        public static bool IsSetDate(DateTime? date)
        {
            return date != null;
        }
    }
}
