using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using SocketRequest;

namespace WebServer.Tests
{
    public class RequestParserFixture
    {
        [Fact]
        public void Request_parser_initialization_test()
        {
            string rawMessage = Guid.NewGuid().ToString();

            var parser = new RequestParser(rawMessage);

            parser.RawMessage.Should().Be(rawMessage);
        }

        [Fact]
        public void Status_line_parsing_test()
        {
            string rawMessage = "GET /test/index.html HTTP/1.1";

            var parser = new RequestParser(rawMessage);
            parser.BeginParsing();

            parser.GetRequestObject().MethodType.Should().Be("GET");
            parser.GetRequestObject().Url.Should().Be("/test/index.html");
            parser.GetRequestObject().HttpVersion.Should().Be("HTTP/1.1");
        }
    }
}
