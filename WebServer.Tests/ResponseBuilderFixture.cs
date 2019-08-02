using System;
using Xunit;
using FluentAssertions;
using SocketResponse;
using System.Text;
using System.Collections.Generic;

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

            response.Date = currentDateTime;
            response.Date.Should().Be(currentDateTime);
        }

        [Fact]
        public void Response_set_content_length_test()
        {
            var response = new ResponseBuilder
            {
                ContentLength = 100
            };

            response.ContentLength.Should().Be(100);
        }

        [Fact]
        public void Response_set_content_type_test()
        {
            var response = new ResponseBuilder
            {
                ContentType = "text/html"
            };

            response.ContentType.Should().Be("text/html");
        }

        [Fact]
        public void Response_set_status_code_test()
        {
            var response = new ResponseBuilder
            {
                StatusCode = 200
            };
            response.StatusCode.Should().Be(200);
        }

        [Fact]
        public void Response_set_status_message_test()
        {
            var response = new ResponseBuilder
            {
                StatusMessage = "OK"
            };
            response.StatusMessage.Should().Be("OK");
        }

        [Fact]
        public void Response_set_data_test()
        {
            var response = new ResponseBuilder
            {
                Data = Encoding.ASCII.GetBytes("OK")
            };
            response.Data.Should().BeEquivalentTo(Encoding.ASCII.GetBytes("OK"));
        }

        [Fact]
        public void Response_header_builder_test()
        {
            int statusCode = 200;
            string statusMessage = "OK";
            string data = "<h1>Hello</h1>";
            string contentType = "text/html";
            int contentLength = data.Length;
            var dateTime = DateTime.Now;

            var expectedResponseBuilder = new StringBuilder();
            expectedResponseBuilder.AppendLine($"{ResponseBuilder.HttpVersion} {statusCode} {statusMessage}");
            expectedResponseBuilder.AppendLine($"Date: {dateTime.ToString("ddd, dd MMM yyyy hh:mm:ss IST")}");
            expectedResponseBuilder.AppendLine($"Server: {ResponseBuilder.Server}");
            expectedResponseBuilder.AppendLine($"Content-Length: {contentLength}");
            expectedResponseBuilder.AppendLine($"Content-Type: {contentType}");
            expectedResponseBuilder.AppendLine();

            var responseObject = new ResponseBuilder
            {
                StatusCode = 200,
                StatusMessage = "OK",
                Data = Encoding.ASCII.GetBytes(data),
                ContentType = contentType,
                ContentLength = contentLength,
                Date = dateTime
            };

            responseObject.BuildResponseHeaderString();
            responseObject.ResponseMessage.Should().BeEquivalentTo(expectedResponseBuilder);
        }

        [Fact]
        public void Response_message_in_bytes_test()
        {
            int statusCode = 200;
            string statusMessage = "OK";
            string data = "<h1>Hello</h1>";
            string contentType = "text/html";
            int contentLength = data.Length;
            var dateTime = DateTime.Now;

            var expectedResponseBuilder = new StringBuilder();
            expectedResponseBuilder.AppendLine($"{ResponseBuilder.HttpVersion} {statusCode} {statusMessage}");
            expectedResponseBuilder.AppendLine($"Date: {dateTime.ToString("ddd, dd MMM yyyy hh:mm:ss IST")}");
            expectedResponseBuilder.AppendLine($"Server: {ResponseBuilder.Server}");
            expectedResponseBuilder.AppendLine($"Content-Length: {contentLength}");
            expectedResponseBuilder.AppendLine($"Content-Type: {contentType}");
            expectedResponseBuilder.AppendLine();

            var exptedResponse = new List<byte>();
            exptedResponse.AddRange(Encoding.ASCII.GetBytes(expectedResponseBuilder.ToString()));
            exptedResponse.AddRange(Encoding.ASCII.GetBytes(data));

            var responseObject = new ResponseBuilder
            {
                StatusCode = 200,
                StatusMessage = "OK",
                Data = Encoding.ASCII.GetBytes(data),
                ContentType = contentType,
                ContentLength = contentLength,
                Date = dateTime
            };

            responseObject.GetResponseAsBytes().ToString().Should().BeEquivalentTo(exptedResponse.ToArray().ToString());
        }
    }
}
