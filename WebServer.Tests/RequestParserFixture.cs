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
            parser.ProcessEachLineOfRequest();
            var requestObject = parser.GetRequestObject();

            requestObject.MethodType.Should().Be("GET");
            requestObject.Url.Should().Be("/test/index.html");
            requestObject.HttpVersion.Should().Be("HTTP/1.1");
        }

        [Fact]
        public void Singel_url_parameter_line_parsing_test()
        {
            string rawMessage = "bookId=12345";

            var parser = new RequestParser(rawMessage);
            parser.ProcessEachLineOfRequest();

            var requestObject = parser.GetRequestObject();

            var urlParameters = new Dictionary<string, string>()
            {
                { "bookId", "12345" }
            };

            requestObject.UrlParameters.Keys.Should().BeEquivalentTo(urlParameters.Keys);
        }

        [Fact]
        public void Multiple_url_parameters_line_parsing_test()
        {
            string rawMessage = "bookId=12345&author=Tan+Ah+Teck";

            var parser = new RequestParser(rawMessage);
            parser.ProcessEachLineOfRequest();

            var requestObject = parser.GetRequestObject();

            var urlParameters = new Dictionary<string, string>()
            {
                { "bookId", "12345" },
                { "author", "Tan Ah Teck" }
            };

            requestObject.UrlParameters.Keys.Should().BeEquivalentTo(urlParameters.Keys);
        }

        [Fact]
        public void Request_metadata_line_parsing_test()
        {
            string rawMessage = "Host: www.test.com";

            var parser = new RequestParser(rawMessage);
            parser.ProcessEachLineOfRequest();

            var requestObject = parser.GetRequestObject();

        }
    }
}
