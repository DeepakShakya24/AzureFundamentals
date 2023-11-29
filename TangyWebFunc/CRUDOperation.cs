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
using TangyWebFunc.Data;
using System.Linq;

namespace TangyWebFunc
{
    public class CRUDOperation
    {
        private readonly TangyFuncDbContext _db;
        public CRUDOperation(TangyFuncDbContext db)
        {
            _db = db;
        }
        
        [FunctionName("CreateGrocery")]
        public async Task<IActionResult> CreateGrocery(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "createGrocery")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            SalesModel data = JsonConvert.DeserializeObject<SalesModel>(requestBody);
            
            _db.SalesModels.Add(data);
            _db.SaveChanges();

            return new OkObjectResult(data);
        }


        [FunctionName("GetGrocery")]
        public async Task<IActionResult> GetGrocery(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "getGrocery")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return new OkObjectResult(_db.SalesModels.ToList());
        }

        [FunctionName("GetGroceryById")]
        public async Task<IActionResult> GetGroceryById(
         [HttpTrigger(AuthorizationLevel.Function, "get", Route = "getGrocery/{id}")] HttpRequest req,
         ILogger log,string id)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var item = _db.SalesModels.FirstOrDefault(u=>u.Id== id);
            return new OkObjectResult(item);
        }

        [FunctionName("UpdateGrocery")]
        public async Task<IActionResult> UodateGrocery(
         [HttpTrigger(AuthorizationLevel.Function, "put", Route = "updateGrocery/{id}")] HttpRequest req,
         ILogger log, string id)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var item = _db.SalesModels.FirstOrDefault(u => u.Id == id);
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            SalesModel data = JsonConvert.DeserializeObject<SalesModel>(requestBody);
            if(!String.IsNullOrEmpty(data.Name))
            {
                item.Name= data.Name;
            }
            _db.SalesModels.Update(item);
            _db.SaveChanges();
            return new OkObjectResult(item);
        }
    }
}
