using System;
using System.Collections.Generic;
using System.Text;

namespace BlobManagement.Services.Models
{
    public class BlobContainer
    {
        
        public string Name { get; set; }
        public DateTimeOffset LastModified { get; set; }

    }
}
