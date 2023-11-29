using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TangyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        private readonly BlobServiceClient _blobServiceClient;
        public ImageUploadController(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient= blobServiceClient;
        }

        [HttpPost]
        public async Task<string> UploadImage(IFormFile file)
        {
            MemoryStream ms = new MemoryStream();
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient("mynewcontainer");
            BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);
            file.CopyTo(ms);
            ms.Position=0;
            await blobClient.UploadAsync(ms);
            return blobClient.Uri.AbsoluteUri;
        }

    }
}
