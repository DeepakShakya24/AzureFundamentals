using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TangyWebFunc.Model;

namespace TangyWebFunc
{
    public static class QueueWrite
    {
        [FunctionName("QueueWrite")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [Queue("queueInbound",Connection ="AzureWebJobsStorage")] IAsyncCollector<SalesModel> salesqueue,
            ILogger log)
        {
            log.LogInformation($"Recieved request {req}");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            SalesModel data = JsonConvert.DeserializeObject<SalesModel>(requestBody);

            await salesqueue.AddAsync(data);

            string responseMessage = $"Data recived for. {data.Name}";

            return new OkObjectResult(responseMessage);
        }
    }
}
