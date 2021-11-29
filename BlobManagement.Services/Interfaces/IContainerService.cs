using BlobManagement.Services.Models;
using BlobManagement.Services.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlobManagement.Services.Interfaces
{
    public interface IContainerService
    {
        GetContainerResponse GetContainers();
        Task<CreateContainerResponse> CreateContainer(CreateContainerRequest request);
        Task<DeleteContainerResponse> DeleteContainer(string containerName);
    }
}
