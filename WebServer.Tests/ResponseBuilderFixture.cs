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
            HttpResponse.HttpVersion.Should().Be("HTTP/1.1");
        }

        [Fact]
        public void Response_server_name_test()
        {
            HttpResponse.Server.Should().Be("Localhost (RV): Windows 10 64-bit");
        }

        [Fact]
        public void Response_set_date_test()
        {
            var response = new ResponseBuilder();

            var currentDateTime = DateTime.Now;

            response.httpResponse.Date = currentDateTime;
            response.httpResponse.Date.Should().Be(currentDateTime);
        }

        [Fact]
        public void Response_set_content_length_test()
        {
            var response = new ResponseBuilder();
            response.httpResponse.ContentLength = 100;

            response.httpResponse.ContentLength.Should().Be(100);
        }

        [Fact]
        public void Response_set_content_type_test()
        {
            var response = new ResponseBuilder();
            response.httpResponse.ContentType = "text/html";

            response.httpResponse.ContentType.Should().Be("text/html");
        }

        [Fact]
        public void Response_set_status_code_test()
        {
            var response = new ResponseBuilder();
            response.httpResponse.StatusCode = 200;

            response.httpResponse.StatusCode.Should().Be(200);
        }

        [Fact]
        public void Response_set_status_message_test()
        {
            var response = new ResponseBuilder();

            response.httpResponse.StatusMessage = "OK";
            response.httpResponse.StatusMessage.Should().Be("OK");
        }

        [Fact]
        public void Response_set_data_test()
        {
            var response = new ResponseBuilder();
            response.httpResponse.Data = Encoding.ASCII.GetBytes("OK");

            response.httpResponse.Data.Should().BeEquivalentTo(Encoding.ASCII.GetBytes("OK"));
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
            expectedResponseBuilder.AppendLine($"{HttpResponse.HttpVersion} {statusCode} {statusMessage}");
            expectedResponseBuilder.AppendLine($"Date: {dateTime.ToString("ddd, dd MMM yyyy hh:mm:ss IST")}");
            expectedResponseBuilder.AppendLine($"Server: {HttpResponse.Server}");
            expectedResponseBuilder.AppendLine($"Content-Length: {contentLength}");
            expectedResponseBuilder.AppendLine($"Content-Type: {contentType}");
            expectedResponseBuilder.AppendLine();

            var responseObject = new ResponseBuilder();

            responseObject.httpResponse.StatusCode = 200;
            responseObject.httpResponse.StatusMessage = "OK";
            responseObject.httpResponse.Data = Encoding.ASCII.GetBytes(data);
            responseObject.httpResponse.ContentType = contentType;
            responseObject.httpResponse.ContentLength = contentLength;
            responseObject.httpResponse.Date = dateTime;

            responseObject.BuildResponseHeaderString();
            responseObject.httpResponse.ResponseMessage.Should().BeEquivalentTo(expectedResponseBuilder);
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
            expectedResponseBuilder.AppendLine($"{HttpResponse.HttpVersion} {statusCode} {statusMessage}");
            expectedResponseBuilder.AppendLine($"Date: {dateTime.ToString("ddd, dd MMM yyyy hh:mm:ss IST")}");
            expectedResponseBuilder.AppendLine($"Server: {HttpResponse.Server}");
            expectedResponseBuilder.AppendLine($"Content-Length: {contentLength}");
            expectedResponseBuilder.AppendLine($"Content-Type: {contentType}");
            expectedResponseBuilder.AppendLine();

            var exptedResponse = new List<byte>();
            exptedResponse.AddRange(Encoding.ASCII.GetBytes(expectedResponseBuilder.ToString()));
            exptedResponse.AddRange(Encoding.ASCII.GetBytes(data));

            var responseObject = new ResponseBuilder();

            responseObject.httpResponse.StatusCode = 200;
            responseObject.httpResponse.StatusMessage = "OK";
            responseObject.httpResponse.Data = Encoding.ASCII.GetBytes(data);
            responseObject.httpResponse.ContentType = contentType;
            responseObject.httpResponse.ContentLength = contentLength;
            responseObject.httpResponse.Date = dateTime;

            responseObject.GetResponseAsBytes().ToString().Should().BeEquivalentTo(exptedResponse.ToArray().ToString());
        }
    }
}
