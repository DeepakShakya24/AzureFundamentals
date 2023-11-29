using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using TangyWebFunc.Data;
using TangyWebFunc.Model;

namespace TangyWebFunc
{
    public class DatabaseWrite
    {
        private readonly TangyFuncDbContext _dbContext;
        public DatabaseWrite(TangyFuncDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [FunctionName("DatabaseWrite")]
        public void Run([QueueTrigger("queueinbound", Connection = "AzureWebJobsStorage")]SalesModel myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            _dbContext.SalesModels.Add(myQueueItem);
            _dbContext.SaveChanges();
        }
    }
}
