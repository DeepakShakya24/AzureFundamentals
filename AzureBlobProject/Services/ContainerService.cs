using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureBlobProject.Interface;

namespace AzureBlobProject.Services
{
    public class ContainerService : IContainerService
    {
        public readonly BlobServiceClient _blobServiceClient;
        public ContainerService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }
        public async Task<List<string>> GetAllContainers()
        {
            List<string> containerName= new List<string>();
            await foreach (BlobContainerItem item in _blobServiceClient.GetBlobContainersAsync())
            {
                containerName.Add(item.Name);
            }
            return containerName;
        }

        public async Task CreateContainer(string containerName)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await blobContainerClient.CreateIfNotExistsAsync();
        }

        public async Task DeleteContainer(string containerName)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await blobContainerClient.DeleteIfExistsAsync();
        }

    }
}
