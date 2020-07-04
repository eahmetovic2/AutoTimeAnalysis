using BlazorErp.Shared.Models.Response.Sifarnik;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Response.Sifarnik
{
    public class SifarnikList : List<SifarnikModel>
    {
        public DateTime DatumResponsa { get; set; }
    }
}
