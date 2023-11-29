using AzureBlobProject.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly IContainerService _containerService;
        public ContainerController(IContainerService containerService)
        {
            _containerService = containerService;
        }

        [HttpGet("GetContainer")]
        public async Task<IActionResult> GetContainers()
        {
            var containers = await _containerService.GetAllContainers();
            return Ok(containers);
        }

        [HttpPost("CreateContainer")]
        public async Task<IActionResult> CreateContainer(string containerName)
        {
             await _containerService.CreateContainer(containerName);
            return StatusCode(200, "Container is created");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContainer(string containerName)
        {
            await _containerService.DeleteContainer(containerName);
            return StatusCode(200, "Container is Deleted");
        }
    }
}
