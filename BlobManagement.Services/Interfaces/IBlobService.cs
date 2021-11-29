using BlobManagement.Services.Models.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlobManagement.Services.Interfaces
{
    public interface IBlobService
    {
        Task<BlobsResponse> GetAllBlobs();
        Task<BlobsResponse> GetBlobsByContainer(string containerName);
        Task<UploadBlobResponse> UploadBlobToContainer(IFormFile file, string containerName);
        Task<DeleteBlobResponse> DeleteBlob(string blobName, string containerName);
    }
}
