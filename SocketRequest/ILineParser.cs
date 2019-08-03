using System;

namespace SocketRequest
{
    public interface ILineParser
    {
        bool TryParse(string line, ref HttpRequest requestObject);
        void Process(string line, ref HttpRequest requestObject);
    }
}
