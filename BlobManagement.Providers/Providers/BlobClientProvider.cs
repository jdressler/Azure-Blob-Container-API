using Azure.Storage.Blobs;
using BlobManagement.Providers.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlobManagement.Providers.Providers
{
    public class BlobClientProvider : IBlobClientProvider
    {
        private readonly IConfiguration _config;


        public BlobClientProvider(IConfiguration config)
        {
            _config = config;
        }

        public BlobServiceClient GetBlobClient()
        {
            var connectionString = _config["Azure:AccessString"];
            return new BlobServiceClient(connectionString);
        }

    }
}
