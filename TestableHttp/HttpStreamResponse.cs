using System.IO;

namespace TestableHttp
{
    public class HttpStreamResponse
    {
        public HttpStreamResponse(int statusCode, HttpHeaders headers, Stream content)
        {
            StatusCode = statusCode;
            Headers = headers;
            Content = content;
        }

        public int StatusCode { get; }
        public HttpHeaders Headers { get; }
        public Stream Content { get; }
    }
}