using System;
using System.Collections.Generic;
using System.Text;

namespace BlobManagement.Services.Models.Responses
{
    public class GetContainerResponse : BaseApiResponse
    {
        public BlobContainers BlobContainers { get; set; }

    }
}
