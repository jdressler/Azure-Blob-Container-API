using System;
using System.Collections.Generic;
using System.Text;

namespace BlobManagement.Services.Models
{
    public class Blob
    {

        public string FileName { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? LastModified { get; set; }
        public string ContentType { get; set; }
        public string Uri { get; set; }
    }
}
