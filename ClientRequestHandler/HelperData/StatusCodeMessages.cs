using System.Collections.Generic;

namespace ClientRequestHandler
{
    public class StatusCodeMessages
    {
        public static Dictionary<int, string> availableStatusCodeMessages { get; } = new Dictionary<int, string>()
        {
            { 200, "OK" },
            { 404, "Not Found" }
        };

        public static string GetStatusMessage(int statusCode)
        {
            return availableStatusCodeMessages[statusCode];
        }
    }
}
