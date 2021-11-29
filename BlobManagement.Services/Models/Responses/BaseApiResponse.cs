using System;
using System.Collections.Generic;
using System.Text;

namespace BlobManagement.Services.Models.Responses
{
    public class BaseApiResponse
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
