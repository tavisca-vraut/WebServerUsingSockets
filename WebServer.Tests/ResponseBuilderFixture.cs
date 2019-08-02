using System;
using Xunit;
using FluentAssertions;
using SocketResponse;

namespace WebServer.Tests
{
    public class ResponseBuilderFixture
    {
        [Fact]
        public void Response_http_version_test()
        {
            ResponseBuilder.HttpVersion.Should().Be("HTTP/1.1");
        }

        [Fact]
        public void Response_server_name_test()
        {
            ResponseBuilder.Server.Should().Be("Localhost (RV): Windows 10 64-bit");
        }

        [Fact]
        public void Response_set_date_test()
        {
            var response = new ResponseBuilder();

            var currentDateTime = DateTime.Now;

            response.SetDate(currentDateTime);
            response.Date.Should().Be(currentDateTime);
        }
    }
}
