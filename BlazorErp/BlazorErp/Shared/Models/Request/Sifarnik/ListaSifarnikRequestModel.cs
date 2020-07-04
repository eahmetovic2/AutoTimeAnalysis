using BlazorErp.Shared.Models.Request.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Request.Sifarnik
{
    public class ListaSifarnikRequestModel : PagedRequestModel
    {
        public String Filter { get; set; }
        public bool? Zavrseno { get; set; }
    }
}
