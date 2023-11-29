using System;
using System.IO;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using TangyWebFunc.Data;
using TangyWebFunc.Model;

namespace TangyWebFunc
{
    public class DbUpdateonBlobUpload
    {
        private readonly TangyFuncDbContext tangyFuncDbContext;
        public DbUpdateonBlobUpload(TangyFuncDbContext tangyFuncDbContext)
        {
            this.tangyFuncDbContext = tangyFuncDbContext;
        }

        [FunctionName("DbUpdateonBlobUpload")]
        public void Run([BlobTrigger("mynewcontainer-sm/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob, 
            string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            BlobDetails blobDetails = new BlobDetails()
            {
                Name= name,
                Size= myBlob.Length
            };
            tangyFuncDbContext.BlobDetails.Add(blobDetails);
            tangyFuncDbContext.SaveChanges();
        }
    }
}
