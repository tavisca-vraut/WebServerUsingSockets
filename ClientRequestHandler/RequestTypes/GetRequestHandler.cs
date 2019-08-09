using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SocketRequest;
using SocketResponse;

namespace ClientRequestHandler
{
    public class GetRequestHandler : IParticularRequestTypeHandler
    {
        public ResponseBuilder responseObject;
        private int statusCode = 200;

        public bool TryProcess(HttpRequest request, out ResponseBuilder responseBuilder)
        {
            responseBuilder = null;

            try
            {
                if (request.MethodType.ToUpper() != "GET")
                    return false;
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("No request object found");
            }
            

            Process(request, out responseBuilder);
            return true;
        }

        public void Process(HttpRequest request, out ResponseBuilder responseBuilder)
        {
            GetRequestedData(out string filePath, request.Url, out byte[] contents);
            //FilterUsingUrlParameters(request.UrlParameters);

            BuildResponse(filePath, contents);
            responseBuilder = responseObject;
        }

        private void GetRequestedData(out string filePath, string url, out byte[] contents)
        {
            filePath = ServerFileHandler.GetRequestedFilePath(url);
            ServerFileHandler.GetFileData(ref filePath, out contents, out bool fileFound);

            if (fileFound == false)
                statusCode = 404;
        }

        private void BuildResponse(string filePath, byte[] contents)
        {
            responseObject = new ResponseBuilder();

            responseObject.httpResponse.StatusCode = statusCode;
            responseObject.httpResponse.StatusMessage = StatusCodeMessages.GetStatusMessage(statusCode);
            responseObject.httpResponse.Data = contents;
            responseObject.httpResponse.ContentType = MimeTypes.GetMimeType(Path.GetExtension(filePath));
            responseObject.httpResponse.ContentLength = Encoding.ASCII.GetString(contents).Length;
            responseObject.httpResponse.Date = DateTime.Now;
        }

        private void FilterUsingUrlParameters(Dictionary<string, string> urlParameters)
        {
            throw new NotImplementedException();
        }
    }
}
