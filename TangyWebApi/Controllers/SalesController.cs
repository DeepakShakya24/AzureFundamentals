using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TangyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly HttpClient httpClient=new HttpClient();
        [HttpPost]
        public async Task<IActionResult> PutSalesInQueue(SaleModel saleModel)
        {
            var JsonContent = JsonConvert.SerializeObject(saleModel);
            var content = new StringContent(JsonContent,System.Text.Encoding.UTF8,"application/json");
            HttpResponseMessage httpResponse = await httpClient.PostAsync("http://localhost:7125/api/QueueWrite", content);
            string res=httpResponse.Content.ReadAsStringAsync().Result;
            return Ok(res);
        }
    }
}
