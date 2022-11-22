using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SwaApi
{
    public class GetGeekJoke
    {
        private readonly HttpClient _client;

        private readonly string[] _blacklistFlags =
        {
            "nsfw",
            "religious",
            "political",
            "racist",
            "sexist",
            "explicit"
        };

        public GetGeekJoke(HttpClient client)
        {
            _client = client;
        }

        [FunctionName("GetGeekJoke")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
            HttpRequest request,
            ILogger log,
            [StaticWebAppsPrincipal] Task<ClaimsPrincipal> principalTask)
        {
            var principal = await principalTask;
            // var principal = await ClientPrincipal.ParseFromRequest(request);
            log.LogInformation("C# HTTP trigger function processed a request");

            if (principal is not { Identity.IsAuthenticated: true })
                return new UnauthorizedResult();

            if (!principal.IsInRole("admin"))
                return new ForbidResult(principal.Identity.AuthenticationType);

            var uri = $"https://v2.jokeapi.dev/joke/Any?format=txt&type=single&blacklistFlags={string.Join(",", _blacklistFlags)}";
            var response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
                return new OkObjectResult("Out of jokes for another hour! :(");

            var joke = await response.Content.ReadAsStringAsync();
            return new OkObjectResult(joke);
        }
    }
}
