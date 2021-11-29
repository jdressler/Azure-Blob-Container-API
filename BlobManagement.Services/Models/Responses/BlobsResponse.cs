using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlobManagement.Services.Models.Responses
{
    public class BlobsResponse : BaseApiResponse
    {
        public List<Blob> Blobs { get; set; }
    }
}
