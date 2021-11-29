using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlobManagement.Providers.Interfaces;
using BlobManagement.Services.Interfaces;
using BlobManagement.Services.Models;
using BlobManagement.Services.Models.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlobManagement.Services.Services
{
    public class ContainerService : IContainerService
    {

        private readonly IBlobClientProvider _blobClientProvider;
        private readonly ILogger<ContainerService> _logger;
        private readonly BlobServiceClient _blobClient;
        private readonly IContainerProvider _containerProvider;
        public ContainerService(IBlobClientProvider blobClientProvider, ILogger<ContainerService> logger, IContainerProvider containerProvider)
        {
            _blobClientProvider = blobClientProvider;
            _containerProvider = containerProvider;
            _logger = logger;
            _blobClient = _blobClientProvider.GetBlobClient();

        }

        public GetContainerResponse GetContainers()
        {
            Pageable<BlobContainerItem> containers;
            try
            {
                containers = _containerProvider.GetAllContainers();

            }catch(RequestFailedException ex)
            {
                _logger.LogError("Get Containers Failed", ex);
                return new GetContainerResponse
                {
                    IsError = true,
                    ErrorMessage = "Failed to get Containers"
                };
            }
            var response = new GetContainerResponse
            {
                BlobContainers = new BlobContainers
                {
                    Containers = new List<BlobContainer>()
                }
             };
         
            foreach(BlobContainerItem container in containers)
            {
                response.BlobContainers.Containers.Add(new BlobContainer
                {
                    Name = container.Name,
                    LastModified = container.Properties.LastModified
                });
              
            }

            response.IsError = false;

            return response;
        }

        public async Task<CreateContainerResponse> CreateContainer(CreateContainerRequest request)
        {
            try
            {
                await _blobClient.CreateBlobContainerAsync(request.ContainerName);
                _logger.LogInformation("Created Container: " + request.ContainerName);

                return new CreateContainerResponse {
                    ErrorMessage = null,
                    IsError = false
                };

            } catch(RequestFailedException exception)
            {
                _logger.LogError("Create Container Failed: " + request.ContainerName, exception);
                return new CreateContainerResponse
                {
                    IsError = true,
                    ErrorMessage = "Error creating container: " + request.ContainerName
                }; ;

            }
        }

        public async Task<DeleteContainerResponse> DeleteContainer(string containerName)
        {
            BlobContainerClient containerClient;
            try
            {
                _logger.LogInformation("Getting Container Client: " + containerName);
                containerClient = _containerProvider.GetContainerClientByName(containerName);
                
                _logger.LogInformation("Container Client created");
                await containerClient.DeleteAsync();
                _logger.LogInformation("Container " + containerName + " deleted");
                return new DeleteContainerResponse
                {
                    IsError = false,
                    ErrorMessage = null
                };

            }
            catch (RequestFailedException ex)
            {
                _logger.LogError("Error receiving Blob Container Client. Container name: " + containerName);
                return new DeleteContainerResponse
                {
                    IsError = true,
                    ErrorMessage = "Error connecting to container: " + containerName
                };
            }


        }


    }
}
