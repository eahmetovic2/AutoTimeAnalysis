using BlazorErp.Shared.Models.Response.Sifarnik;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Response.Sifarnik
{
    public class SifarnikIzvjestajModel : SifarnikModel
    {
        public int UlogaId { get; set; }
        public string Uloga { get; set; }
        public string Url { get; set; }
    }
}
