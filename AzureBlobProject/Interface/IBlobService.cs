using AzureBlobProject.Models;
using AzureBlobProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobProject.Interface
{
    public interface IBlobService
    {
        Task<string> GetBlob(string name, string containerName);
        Task<List<string>> GetAllBlobs(string containerName);
        Task<List<string>> GetAllBlobsWithUri(string containerName);
        Task<bool> UploadBlobs(IFormFile file, string containerName, BlobMetaData blobMetaData);
        Task<DownloadModel> DownloadBlobs(string blobName, string containerName);
        Task<bool> DeleteBlobs(string name, string containerName);

      Task<Stream> BlobStream(string blobName,string containerName);
    }
}
