using System;
using System.Text;
using Xunit;
using FluentAssertions;
using ClientRequestHandler;

namespace WebServer.Tests
{
    public class RequestHandlerFixture
    {
        [Fact]
        public void Raw_request_message_input_as_string_test()
        {
            var rawMessage = "TEST 1234";

            var requestHandler = new RequestHandler(rawMessage);

            requestHandler.RawRequestMessage.Should().Be(rawMessage);
        }

        [Fact]
        public void Raw_request_message_input_as_byte_array_test()
        {
            var rawMessage = Encoding.ASCII.GetBytes("TEST 1234");

            var requestHandler = new RequestHandler(rawMessage);

            requestHandler.RawRequestMessage.Should().Be(Encoding.ASCII.GetString(rawMessage));
        }

        [Fact]
        public void Request_message_parsing_successful_test()
        {
            var rawMessage = Encoding.ASCII.GetBytes("GET /test/index.html HTTP/1.1");

            var requestHandler = new RequestHandler(rawMessage);

            requestHandler.TryParseRequestMessage().Should().Be(true);
        }

        [Fact]
        public void Request_message_parsing_unsuccessful_test()
        {
            var rawMessage = Encoding.ASCII.GetBytes("GET/test/index.html HTTP/1.1");

            var requestHandler = new RequestHandler(rawMessage);

            requestHandler.TryParseRequestMessage().Should().Be(false);
        }

        [Fact]
        public void Request_processing_successful_test()
        {
            var rawMessage = Encoding.ASCII.GetBytes("GET /test/index.html HTTP/1.1");
            var requestHandler = new RequestHandler(rawMessage);

            requestHandler.TryParseRequestMessage();

            requestHandler.TryProcessRequest().Should().Be(true);
        }

        [Fact]
        public void Request_processing_unsuccessful_test()
        {
            var rawMessage = Encoding.ASCII.GetBytes("POST /test/index.html HTTP/1.1");
            var requestHandler = new RequestHandler(rawMessage);

            requestHandler.TryParseRequestMessage();

            requestHandler.TryProcessRequest().Should().Be(false);
        }

        //[Fact]
        //public void Get_request_correct_file_search_test()
        //{
        //    var rawMessage = Encoding.ASCII.GetBytes("GET /test/index.html HTTP/1.1");
        //    var requestHandler = new RequestHandler(rawMessage);

        //    requestHandler.TryParseRequestMessage();
        //    requestHandler.TryProcessRequest();
        //}
    }
}
