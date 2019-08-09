using System;
using Listener;

namespace WebServer.App
{
    class Program
    {
        static void Main(string[] _)
        {

            HttpListener listener = new HttpListener(8000);

            listener.Listen(10);

            Console.ReadKey(true);
        }
    }
}
