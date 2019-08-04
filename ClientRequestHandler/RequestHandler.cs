using System;
using System.Collections.Generic;
using System.Text;
using SocketRequest;
using SocketResponse;

namespace ClientRequestHandler
{
    public class RequestHandler
    {
        public string RawRequestMessage;
        public RequestParser RequestParser;
        public ResponseBuilder responseBuilder;

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

        public bool TryProcessRequestType()
        {
            foreach (var requestTypeHandler in availableRequestTypeHandlers)
            {
                if (requestTypeHandler.TryProcess(RequestParser.GetRequestObject(), out responseBuilder) == true)
                    return true;
            }

            return false;
        }

        public byte[] GetResponseAsBytes()
        {
            return responseBuilder.GetResponseAsBytes();
        }

        public string GetResponseAsString()
        {
            return responseBuilder.GetResponseAsString();
        }
    }
}
