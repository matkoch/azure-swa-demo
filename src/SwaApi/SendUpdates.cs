// using System;
// using System.Threading.Tasks;
// using Microsoft.Azure.WebJobs;
// using Microsoft.Azure.WebJobs.Host;
// using Microsoft.Extensions.Logging;
//
// namespace SwaApi
// {
//     public static class SendUpdates
//     {
//         [FunctionName("SendUpdates")]
//         public static async Task RunAsync(
//             [TimerTrigger("0 */5 * * * *")]
//             TimerInfo myTimer,
//             ILogger log)
//         {
//             log.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow}");
//         }
//     }
// }