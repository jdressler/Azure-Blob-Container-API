using BlobManagement.Services.Interfaces;
using BlobManagement.Services.Models;
using BlobManagement.Services.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlobManagement.API.Controllers
{
    [ApiController]
    [Route("api/containers")]
    public class ContainerController : Controller
    {

        private readonly IContainerService _containerService;
        private readonly ILogger<ContainerController> _logger;

        public ContainerController(IContainerService containerService, ILogger<ContainerController> logger)
        {
            _containerService = containerService;
            _logger = logger;

        }

        [HttpGet("list")]
        public GetContainerResponse GetContainers()
        {
            _logger.LogInformation("Getting containers");
            return _containerService.GetContainers();
        }

        [HttpPost("create")]
        public async Task<CreateContainerResponse> CreateContainer(CreateContainerRequest request)
        {
            _logger.LogInformation("Creating Container: " + request.ContainerName);
            return await _containerService.CreateContainer(request);
        }

        [HttpDelete("{name}")]
        public async Task<DeleteContainerResponse> DeleteContainer([FromRoute] string name)
        {
            _logger.LogInformation("Deleting container: " + name);
            return await _containerService.DeleteContainer(name);
        }


    }
}
