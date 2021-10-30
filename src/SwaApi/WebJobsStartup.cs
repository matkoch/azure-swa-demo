using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SwaApi;

[assembly: WebJobsStartup(typeof(WebJobsStartup))]

namespace SwaApi
{
    public class WebJobsStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddExtension<StaticWebAppsPrincipalConfigProvider>();

            builder.Services
                .AddTransient<StaticWebAppsPrincipalBinding>()
                .AddTransient<StaticWebAppsPrincipalBindingProvider>()
                .AddTransient<StaticWebAppsPrincipalValueProvider>();
        }
    }
}