using SocketRequest;
using SocketResponse;

namespace ClientRequestHandler
{
    public interface IParticularRequestTypeHandler
    {
        bool TryProcess(HttpRequest request, out ResponseBuilder responseBuilder);
        void Process(HttpRequest request, out ResponseBuilder responseBuilder);
    }
}
