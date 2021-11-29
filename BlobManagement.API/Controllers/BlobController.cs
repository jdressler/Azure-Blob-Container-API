using BlobManagement.Services.Interfaces;
using BlobManagement.Services.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlobManagement.API.Controllers
{
    [ApiController]
    [Route("api/blobs")]
    public class BlobController : Controller
    {
        private readonly ILogger<BlobController> _logger;
        private readonly IBlobService _blobService;

        public BlobController(ILogger<BlobController> logger, IBlobService blobService)
        {
            _logger = logger;
            _blobService = blobService;
        }

        [HttpGet("all")]
        public async Task<BlobsResponse> GetAllBlobs()
        {
            return await _blobService.GetAllBlobs();
        }

        [HttpGet("container/{containerName}")]
        public async Task<BlobsResponse> GetBlobsByContainer(string containerName)
        {
            return await _blobService.GetBlobsByContainer(containerName);
        }

        [HttpPost("upload")]
        public async Task<UploadBlobResponse> UploadBlob(IFormFile file, string containerName)
        {
            _logger.LogInformation("Uploading blob: " + file.FileName);
            return await _blobService.UploadBlobToContainer(file, containerName);
        }

        [HttpDelete("{containerName}/{blobName}")]
        public async Task<DeleteBlobResponse> DeleteBlob(string containerName, string blobName)
        {
            _logger.LogInformation("Deleting blob: " + blobName);
            return await _blobService.DeleteBlob(blobName, containerName);
        }
    }
}
