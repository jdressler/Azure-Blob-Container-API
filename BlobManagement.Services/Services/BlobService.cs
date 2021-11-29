using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlobManagement.Providers.Interfaces;
using BlobManagement.Services.Interfaces;
using BlobManagement.Services.Models;
using BlobManagement.Services.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlobManagement.Services.Services
{
    public class BlobService : IBlobService
    {
        private readonly IBlobClientProvider _blobClientProvider;
        private readonly IContainerProvider _containerProvider;
        private readonly ILogger<BlobService> _logger;
        private readonly BlobServiceClient _blobClient;
        private readonly string blobUri;

        public BlobService(IBlobClientProvider blobClientProvider, IContainerProvider containerProvider, ILogger<BlobService> logger)
        {
            _blobClientProvider = blobClientProvider;
            _containerProvider = containerProvider;
            _logger = logger;
            _blobClient = _blobClientProvider.GetBlobClient();
            blobUri = _blobClient.Uri.ToString();

        }

        public async Task<BlobsResponse> GetAllBlobs()
        {
            var containers = _containerProvider.GetAllContainers();
            List<BlobContainerClient> containerClients = new List<BlobContainerClient>();
            foreach (BlobContainerItem containerItem in containers)
            {
                containerClients.Add(_containerProvider.GetContainerClientByName(containerItem.Name));
            }

            List<Blob> blobs = new List<Blob>();
            foreach (BlobContainerClient containerClient in containerClients)
            {
                var containerName = containerClient.Name;
                await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
                {
                    blobs.Add(new Blob
                    {
                        FileName = blobItem.Name,
                        ContentType = blobItem.Properties.ContentType,
                        CreatedOn = blobItem.Properties.CreatedOn,
                        LastModified = blobItem.Properties.LastModified,
                        Uri = blobUri + containerName + "/" + blobItem.Name
                    });
                }
            }
            return new BlobsResponse
            {
                Blobs = blobs
            };
        }

        public async Task<BlobsResponse> GetBlobsByContainer(string containerName)
        {
            var containerClient = _containerProvider.GetContainerClientByName(containerName);
            List<Blob> blobs = new List<Blob>();
            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                blobs.Add(new Blob
                {
                    FileName = blobItem.Name,
                    ContentType = blobItem.Properties.ContentType,
                    CreatedOn = blobItem.Properties.CreatedOn,
                    LastModified = blobItem.Properties.LastModified,
                    Uri = blobUri + containerName + "/" + blobItem.Name
                });
            }

            return new BlobsResponse
            {
                Blobs = blobs
            };

        }

        public async Task<UploadBlobResponse> UploadBlobToContainer(IFormFile file, string containerName)
        {
            try
            {
                var containerClient = _containerProvider.GetContainerClientByName(containerName);
                _logger.LogInformation("Container client received: " + containerName);
                var blob = containerClient.GetBlobClient(file.FileName);
                _logger.LogInformation("Blob client received: " + file.FileName);
                await blob.UploadAsync(file.OpenReadStream());
                _logger.LogInformation("File uploaded: " + file.FileName);
                return new UploadBlobResponse
                {
                    IsError = false,
                    ErrorMessage = null
                };
            }
            catch(RequestFailedException ex)
            {
                _logger.LogError("Failed to upload file: " + file.FileName + " File Size(bytes): " + file.Length, ex);
                return new UploadBlobResponse
                {
                    IsError = true,
                    ErrorMessage = "Failed to upload file: " + file.FileName
                };
            }
           
        }

        public async Task<DeleteBlobResponse> DeleteBlob(string blobName, string containerName)
        {
            try
            {

                var blobClient = _containerProvider.GetContainerClientByName(containerName);
                _logger.LogInformation("Container Received: " + containerName);
                await blobClient.GetBlobClient(blobName).DeleteIfExistsAsync();
                _logger.LogInformation("Blob deleted: " + blobName);
                return new DeleteBlobResponse
                {
                    IsError = false,
                    ErrorMessage = null
                };
            } 
            catch(RequestFailedException ex){
                _logger.LogError("Failed to delete blob: " + blobName, ex);
                return new DeleteBlobResponse
                {
                    IsError = true,
                    ErrorMessage = "Failed to delete blob: " + blobName
                };

            }

        }

    }
}
