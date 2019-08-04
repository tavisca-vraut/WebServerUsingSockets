using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ClientRequestHandler;
using SocketResponse;

namespace Listener
{
    public class HttpListener
    {
        private string HostName;

        private IPAddress IPAddress;
        private IPHostEntry IPHostEntry;
        private IPEndPoint IPEndPoint;
        private Socket serverSocket;

        private void GenerateHostName() => this.HostName = Dns.GetHostName();
        private void GenerateHostAddressInformation() => this.IPHostEntry = Dns.GetHostEntry(this.HostName);
        private void GenerateIpv4Address() => this.IPAddress = IPAddress.Parse("127.0.0.1"); // this.IPHostEntry.AddressList[0];
        private void GenerateEndPoint(int port) => this.IPEndPoint = new IPEndPoint(this.IPAddress, port);
        private void InitializeAndBindSocketToHostEntry()
        {
            this.serverSocket = new Socket(this.IPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this.serverSocket.Bind(this.IPEndPoint);
        }

        public HttpListener(int port)
        {
            GenerateHostName();
            GenerateHostAddressInformation();
            GenerateIpv4Address();
            GenerateEndPoint(port);
            InitializeAndBindSocketToHostEntry();
        }

        public void Listen(int backLog)
        {
            this.serverSocket.Listen(backLog);
            Console.WriteLine($"Listening at port: {this.IPEndPoint.Port}");
            ThreadPool.QueueUserWorkItem(KeepAcceptingClients);
        }

        private void KeepAcceptingClients(object state)
        {
            while (true)
            {
                ThreadPool.QueueUserWorkItem(HandleClientRequest, this.serverSocket.Accept());
            }
        }

        private static void HandleClientRequest(object obj)
        {
            Socket clientSocket = (Socket)obj;
            RequestHandler requestHandler = ProcessRequest(GetRequestMessage(clientSocket));
            GetResponse(requestHandler, out byte[] responseBytes, out HttpResponse responseObject);
            LogDetails(requestHandler, responseObject);
            SendMessageAndCloseSocket(clientSocket, responseBytes);
        }

        private static string GetRequestMessage(Socket clientSocket)
        {
            byte[] buffer = new byte[1024];
            int numberOfBytesReceived = clientSocket.Receive(buffer);

            return Encoding.ASCII.GetString(buffer, 0, numberOfBytesReceived);
        }

        private static RequestHandler ProcessRequest(string requestMessage)
        {
            RequestHandler requestHandler = new RequestHandler(requestMessage);
            requestHandler.TryParseRequestMessage();
            requestHandler.TryProcessRequestType();
            return requestHandler;
        }

        private static void GetResponse(RequestHandler requestHandler, out byte[] responseBytes, out SocketResponse.HttpResponse responseObject)
        {
            responseBytes = requestHandler.GetResponseAsBytes();
            responseObject = requestHandler.responseBuilder.httpResponse;
        }

        private static void SendMessageAndCloseSocket(Socket clientSocket, byte[] responseBytes)
        {
            clientSocket.Send(responseBytes);
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }

        private static void LogDetails(RequestHandler requestHandler, HttpResponse responseObject)
        {
            ServerLogger.SimpleLog("New Client Request");
            ServerLogger.LogRequestDetails(requestHandler);
            ServerLogger.LogResponseDetails(responseObject);
        }
    }
}
