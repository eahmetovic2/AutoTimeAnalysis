using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Response.Common
{
    public class FileResponse
    {
        public string Naziv { get; set; }
        public string ContentType { get; set; }
        public byte[] Bytes { get; set; }
    }
}
