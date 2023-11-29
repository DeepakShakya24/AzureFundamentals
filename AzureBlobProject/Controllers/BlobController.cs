using AzureBlobProject.Interface;
using AzureBlobProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private readonly IBlobService _blobService;
        public BlobController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpGet]
        public Task<List<string>> GetAllBlobs(string containerName)
        {
            var blobs=_blobService.GetAllBlobs(containerName);
            return blobs;
        }

        [HttpPost]
        public async Task<IActionResult> UploadBlobs(IFormFile file, string containerName)
        {
            BlobMetaData blobMetaData = new BlobMetaData();
            blobMetaData.Title = "dwdqw";
            var result = await _blobService.UploadBlobs(file, containerName,blobMetaData);
            if (result)
            
                return StatusCode(200, "The blob has been uploaded");
            return StatusCode(400, "Bad Request");
        }

        [HttpGet("GetImages")]
        public async Task<IActionResult> GetImages(string containerName)
        {
            var res=await _blobService.GetAllBlobsWithUri(containerName);
            return StatusCode(200, res);
        }

        [HttpGet("getBlobStream")]
        public async Task<IActionResult> GetBlobStream(string blobName,string containerName)
        {
            var fileStream=await _blobService.BlobStream(blobName, containerName);
            return File(fileStream, "image/jpeg");
        }

        [HttpPost("DownloadBlobs")]
        public async Task<IActionResult> DownlodBlobs(string blobName,string containerName)
        {
            var res = await _blobService.DownloadBlobs(blobName,containerName);
            return File(res.MemoryStream, res.contentType,"blobFile.pdf");
        }

    }
}
