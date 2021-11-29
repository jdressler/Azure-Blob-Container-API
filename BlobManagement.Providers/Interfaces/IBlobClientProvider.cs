using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlobManagement.Providers.Interfaces
{
    public interface IBlobClientProvider
    {
        BlobServiceClient GetBlobClient();

    }
}
