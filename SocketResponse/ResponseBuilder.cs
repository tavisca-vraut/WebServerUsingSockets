using System;

namespace SocketResponse
{
    public class ResponseBuilder
    {
        public const string HttpVersion = "HTTP/1.1";
        public const string Server = "Localhost (RV): Windows 10 64-bit";
        public object Date { get; private set; }

        public void SetDate(DateTime dateTime)
        {
            this.Date = dateTime;
        }
    }
}
