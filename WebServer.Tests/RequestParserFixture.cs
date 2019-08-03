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

            requestObject.Host.Should().Be("www.test.com");
        }

        [Fact]
        public void Request_message_parser()
        {
            string rawMessage = "GET /test/url.com HTTP/1.1\n" +
                                "Host: www.test.com\n" +
                                "Accept: image / gif, image / jpeg, */*\n" +
                                "Accept-Language: en-us\n" +
                                "Accept-Encoding: gzip, deflate\n" +
                                "User-Agent: Mozilla/4.0\n" +
                                "Content-Length: 35\n" +
                                "\n" +
                                "bookId=12345&author=Tan+Ah+Teck\n";

            var parser = new RequestParser(rawMessage);
            parser.ProcessEachLineOfRequest();

            var requestObject = parser.GetRequestObject();

            Assert.Equal("GET", requestObject.MethodType);

            requestObject.MethodType.Should().Be("GET");
            requestObject.Url.Should().Be("/test/url.com");
            requestObject.HttpVersion.Should().Be("HTTP/1.1");
            requestObject.Host.Should().Be("www.test.com");
            requestObject.Accept.Should().Be("image / gif, image / jpeg, */*");
            requestObject.AcceptLanguage.Should().Be("en-us");
            requestObject.AcceptEncoding.Should().Be("gzip, deflate");
            requestObject.UserAgent.Should().Be("Mozilla/4.0");
            requestObject.ContentLength.Should().Be("35");
            requestObject.UrlParameters["bookId"].Should().Be("12345");
            requestObject.UrlParameters["author"].Should().Be("Tan Ah Teck");
        }
    }
}
