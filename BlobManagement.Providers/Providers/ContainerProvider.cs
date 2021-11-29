using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlobManagement.Providers.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlobManagement.Providers.Providers
{
    public class ContainerProvider : IContainerProvider 
    {

        private readonly IBlobClientProvider _blobClientProvider;
        private readonly ILogger<ContainerProvider> _logger;
        private readonly BlobServiceClient _blobClient;

        public ContainerProvider(IBlobClientProvider blobClientProvider, ILogger<ContainerProvider> logger)
        {
            _blobClientProvider = blobClientProvider;
            _logger = logger;
            _blobClient = _blobClientProvider.GetBlobClient();

        }

        public Pageable<BlobContainerItem> GetAllContainers()
        {
            return _blobClient.GetBlobContainers();

        }

        public BlobContainerClient GetContainerClientByName(string containerName)
        {
            return _blobClient.GetBlobContainerClient(containerName); 
        }

     

    }
}
