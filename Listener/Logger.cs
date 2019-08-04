using System;
using ClientRequestHandler;
using SocketResponse;

namespace Listener
{
    public static class ServerLogger
    {
        private delegate void LoggerType(string s);
        private static LoggerType Log = Console.WriteLine;

        public static void LogResponseDetails(HttpResponse responseObject)
        {
            Log($"Response => Status: {responseObject.StatusCode}, Message: {responseObject.StatusMessage}\n");
        }
        public static void LogRequestDetails(RequestHandler requestHandler)
        {
            Log($"Request-Type: {requestHandler.RequestParser.GetRequestObject().MethodType}");
            Log($"Request-Url{requestHandler.RequestParser.GetRequestObject().Url}");
        }

        public static void SimpleLog(string s)
        {
            Log(s);
        }
    }
}
