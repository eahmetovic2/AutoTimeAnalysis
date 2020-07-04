using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace BlazorErp.Shared.Models.Response.Upload
{
    public class UploadResultModel
    {
        public IFormCollection Options { get; set; }
        public object Result { get; set; }
    }
}
