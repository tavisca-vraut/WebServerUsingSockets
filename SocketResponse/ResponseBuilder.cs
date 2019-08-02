using System;
using System.Collections.Generic;
using System.Text;

namespace SocketResponse
{
    public class ResponseBuilder
    {
        private byte[] Header { get; set; }
        public StringBuilder ResponseMessage { get; private set; }
        public const string HttpVersion = "HTTP/1.1";
        public const string Server = "Localhost (RV): Windows 10 64-bit";
        public DateTime? Date { get; set; } = null;
        public int? ContentLength { get; set; } = null;
        public string ContentType { get; set; } = null;
        public int StatusCode { get; set; } = 200;
        public string StatusMessage { get; set; } = "OK";
        public byte[] Data { get; set; } = null;

        public byte[] GetResponseAsBytes()
        {
            BuildResponseHeaderString();
            ConvertHeaderToBytes();
            return GenerateResponseBytes();
        }

        private byte[] GenerateResponseBytes()
        {
            List<byte> response = new List<byte>();

            response.AddRange(Header);
            response.AddRange(Data);

            return response.ToArray();
        }

        private void ConvertHeaderToBytes()
        {
            Header = Encoding.ASCII.GetBytes(ResponseMessage.ToString());
        }

        public void BuildResponseHeaderString()
        {
            ResponseMessage = new StringBuilder();
            AddStatusLineToResponse();

            if (IsSetDate(Date))
                AddDateToResponseMessage();

            AddServerToResponse();

            if (IsSetContentLength(ContentLength))
                AddContentLengthToResponseMessage();

            if (IsSetContentType(ContentType))
                AddContentTypeToResponseMessage();

            ResponseMessage.AppendLine();
        }

        private void AddContentTypeToResponseMessage()
        {
            ResponseMessage.AppendLine($"Content-Type: {ContentType}");
        }

        private void AddContentLengthToResponseMessage()
        {
            ResponseMessage.AppendLine($"Content-Length: {ContentLength}");
        }

        private void AddServerToResponse() => ResponseMessage.AppendLine($"Server: {Server}");

        private void AddStatusLineToResponse()
        {
            ResponseMessage.AppendLine($"{HttpVersion} {StatusCode} {StatusMessage}");
        }

        private void AddDateToResponseMessage()
        {
            ResponseMessage.AppendLine($"Date: {Date.Value.ToString("ddd, dd MMM yyyy hh:mm:ss IST")}");
        }

        private bool IsSetContentLength(int? contentLength)
        {
            if (contentLength == null)
                throw new ArgumentNullException("Content Length is NULL.");
            return true;
        }
        private bool IsSetContentType(string contentType)
        {
            if (ContentType == null)
                throw new ArgumentNullException("Content Type is NULL.");
            return true;
        }
        private bool IsSetData(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("Data is NULL.");
            return true;
        }
        public bool IsSetDate(DateTime? date)
        {
            return date != null;
        }
    }
}
