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
    public class GetQuote
    {
        private readonly HttpClient _client;

        public GetQuote(HttpClient client)
        {
            _client = client;
        }

        [FunctionName("GetQuote")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
            HttpRequest request,
            ILogger log,
            [StaticWebAppsPrincipal] ClaimsPrincipal principal)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (principal is not { Identity.IsAuthenticated: true })
                return new UnauthorizedResult();

            if (!principal.IsInRole("admin"))
                return new ForbidResult(principal.Identity.AuthenticationType);

            var response = await _client.GetAsync("https://quotes.rest/qod");
            if (!response.IsSuccessStatusCode)
                return new OkObjectResult("Out of quotes for another hour! :(");

            var content = await response.Content.ReadAsStringAsync();
            dynamic json = JsonConvert.DeserializeObject(content);
            string quote = json.contents.quotes[0].quote;
            return new OkObjectResult(quote);
        }
    }
}
