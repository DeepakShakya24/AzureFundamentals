using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureBlobProject.Interface;
using AzureBlobProject.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AzureBlobProject.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient= blobServiceClient;
        }
        public Task<bool> DeleteBlobs(string name, string containerName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> GetAllBlobs(string containerName)
        {
            var blobsString = new List<string>();
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobs = blobContainerClient.GetBlobsAsync();
            await foreach (var blob in blobs)
            {
                blobsString.Add(blob.Name);
            }
            return blobsString;
        }

        public async Task<List<string>> GetAllBlobsWithUri(string containerName)
        {
            var blobsString = new List<string>();
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobs = blobContainerClient.GetBlobsAsync();
            await foreach (var blob in blobs)
            {
                var blobClient = blobContainerClient.GetBlobClient(blob.Name);
                var blobsUri=blobClient.Uri.AbsoluteUri;
                blobsString.Add(blobsUri);
            }
            return blobsString;
        }

        public async Task<string> GetBlob(string name, string containerName)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainerClient.GetBlobClient(name);
            return blobClient.Uri.AbsoluteUri;
        }

        public async Task<bool> UploadBlobs(IFormFile file, string containerName,BlobMetaData blobMetaData)
        {
            MemoryStream ms = new MemoryStream();
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainerClient.GetBlobClient(file.FileName);
            file.CopyTo(ms);
            ms.Position = 0;
            var headers = new BlobHttpHeaders() { ContentType = file.ContentType };
            IDictionary<string, string> metaData = new Dictionary<string, string>
            {
                { "title", blobMetaData.Title }
            };
            var result = blobClient.UploadAsync(ms,headers,metaData);
            if (result!=null)
            {
                return true;
            }
            return false;
        }

        public async Task<Stream> BlobStream(string fileName, string containerName)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);
            var fileStream = await blobClient.DownloadAsync();
            return fileStream.Value.Content;
        }

        public async Task<DownloadModel> DownloadBlobs(string blobName, string containerName)
        {
            var result = new DownloadModel();
            //MemoryStream ms = new MemoryStream();
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = blobContainerClient.GetBlobClient(blobName);
            var fileStream = await blobClient.DownloadContentAsync();
           
            var blobProperties = await blobClient.GetPropertiesAsync();
            if (blobClient.Exists())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    await blobClient.DownloadToAsync(ms);
                    ms.Position = 0;
                    result.contentType = blobProperties.Value.ContentType;
                    result.MemoryStream = ms.ToArray();
                }
                
            }
            return result;

        }
    }
}
