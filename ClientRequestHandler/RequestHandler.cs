using System;
using System.Collections.Generic;
using System.Text;
using SocketRequest;

namespace ClientRequestHandler
{
    public interface IParticularRequestTypeHandler
    {
        bool TryProcess(HttpRequest request);
        void Process(HttpRequest request);
    }

    public class GetRequestHandler : IParticularRequestTypeHandler
    {
        public bool TryProcess(HttpRequest request)
        {
            if (request.MethodType.ToUpper() != "GET")
                return false;

            Process(request);
            return true;
        }
        public void Process(HttpRequest request)
        {
            //throw new NotImplementedException();
        }
    }

    public class RequestHandler
    {
        public string RawRequestMessage;
        public RequestParser RequestParser;

        public readonly List<IParticularRequestTypeHandler> availableRequestTypeHandlers = new List<IParticularRequestTypeHandler>()
        {
            new GetRequestHandler(),
        };

        public RequestHandler(string requestMessage)
        {
            RawRequestMessage = requestMessage;
        }
        public RequestHandler(byte[] requestMessage) : this(Encoding.ASCII.GetString(requestMessage)) { }

        public bool TryParseRequestMessage()
        {
            this.RequestParser = new RequestParser(this.RawRequestMessage);
            try
            {
                this.RequestParser.ProcessEachLineOfRequest();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                return false;
            }
            return true;
        }

        public bool TryProcessRequest()
        {
            foreach (var requestTypeHandler in availableRequestTypeHandlers)
            {
                if (requestTypeHandler.TryProcess(RequestParser.GetRequestObject()) == true)
                    return true;
            }

            return false;
        }
    }
}
