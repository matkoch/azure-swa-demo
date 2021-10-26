using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace SwaApi.Tests
{
    // SOURCE: https://docs.microsoft.com/en-us/azure/azure-functions/functions-test-a-function
    public class TestBase
    {
        protected static HttpClient HttpClient = new HttpClient();
        protected HttpContext Context;
        protected ILogger ListLogger;
        protected ILogger TestLogger;

        protected TestBase(ITestOutputHelper outputHelper)
        {
            Context = new DefaultHttpContext();
            ListLogger = new ListLogger();
            TestLogger = new TestLogger(outputHelper);

        }

        protected HttpRequest EmptyRequest => Context.Request;
        protected ClaimsPrincipal EmptyPrincipal => new ClaimsPrincipal();

        public HttpRequest CreateRequest<T>(T obj)
        {
            var request = Context.Request;
            var bodyString = JsonConvert.SerializeObject(obj);
            request.Body = GenerateStreamFromString(bodyString);
            return request;
        }

        public HttpRequest CreateRequest(Dictionary<string, string> dictionary)
        {
            var request = Context.Request;
            request.Query = new QueryCollection(dictionary.ToDictionary(x => x.Key, x => (StringValues)x.Value));
            return request;
        }

        protected static ClaimsPrincipal CreateAuthedPrincipal(params Claim[] claims)
        {
            return new ClaimsPrincipal(new ClaimsIdentity(claims, "authenticated"));
        }

        private static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}