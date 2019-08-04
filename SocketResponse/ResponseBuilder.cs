using System;
using System.Collections.Generic;
using System.Text;

namespace SocketResponse
{
    public class ResponseBuilder
    {
        public HttpResponse httpResponse;

        public ResponseBuilder() => httpResponse = new HttpResponse();

        public byte[] GetResponseAsBytes()
        {
            BuildResponseHeaderString();
            ConvertHeaderToBytes();
            return GenerateResponseBytes();
        }

        public string GetResponseAsString()
        {
            return Encoding.ASCII.GetString(GetResponseAsBytes());
        }

        private byte[] GenerateResponseBytes()
        {
            List<byte> response = new List<byte>();

            response.AddRange(httpResponse.Header);
            response.AddRange(httpResponse.Data);

            return response.ToArray();
        }

        private void ConvertHeaderToBytes()
        {
            httpResponse.Header = Encoding.ASCII.GetBytes(httpResponse.ResponseMessage.ToString());
        }

        public void BuildResponseHeaderString()
        {
            httpResponse.ResponseMessage = new StringBuilder();

            AddStatusLineToResponse();

            if (HttpResponse.IsSetDate(httpResponse.Date)) AddDateToResponseMessage();

            AddServerToResponse();

            if (httpResponse.IsSetContentLength()) AddContentLengthToResponseMessage();

            if (httpResponse.IsSetContentType()) AddContentTypeToResponseMessage();

            httpResponse.ResponseMessage.AppendLine();
        }

        private void AddContentTypeToResponseMessage() 
            => httpResponse.ResponseMessage.AppendLine($"Content-Type: {httpResponse.ContentType}");

        private void AddContentLengthToResponseMessage()
            => httpResponse.ResponseMessage.AppendLine($"Content-Length: {httpResponse.ContentLength}");

        private void AddServerToResponse() => httpResponse.ResponseMessage.AppendLine($"Server: {HttpResponse.Server}");

        private void AddStatusLineToResponse() 
            => httpResponse.ResponseMessage
            .AppendLine($"{HttpResponse.HttpVersion} {httpResponse.StatusCode} {httpResponse.StatusMessage}");

        private void AddDateToResponseMessage() 
            => httpResponse.ResponseMessage
            .AppendLine($"Date: {httpResponse.Date.Value.ToString("ddd, dd MMM yyyy hh:mm:ss IST")}");
    }
}
