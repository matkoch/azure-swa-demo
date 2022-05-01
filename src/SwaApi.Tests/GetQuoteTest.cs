using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace SwaApi.Tests
{
    public class GetQuoteTest : TestBase
    {
        private readonly GetQuote _sut;

        public GetQuoteTest(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
            _sut = new GetQuote(HttpClient);
        }

        [Fact(Skip = "Principal binding issue")]
        public async Task Test1()
        {
            // Arrange
            var principal = CreateAuthedPrincipal(new Claim(ClaimTypes.Role, "admin"));
            var request = CreateRequest(new Dictionary<string, string>
            {
                ["name"] = "Matthias"
            });

            // Act
            var response = await _sut.RunAsync(request, TestLogger);

            // Assert
            var result = response.Should().BeOfType<OkObjectResult>().Subject;
            result.Value.Should().BeOfType<string>().Which
                .Should().NotBeEmpty();
        }
    }
}
