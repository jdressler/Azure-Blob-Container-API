using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlobManagement.Providers.Interfaces
{
    public interface IContainerProvider
    {
        Pageable<BlobContainerItem> GetAllContainers();
        BlobContainerClient GetContainerClientByName(string containerName);
    }
}
