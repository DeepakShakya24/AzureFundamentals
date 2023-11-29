namespace AzureBlobProject.Interface
{
    public interface IContainerService
    {
        Task<List<string>> GetAllContainers();
        Task CreateContainer(string containerName);
        Task DeleteContainer(string containerName);
    }
}
