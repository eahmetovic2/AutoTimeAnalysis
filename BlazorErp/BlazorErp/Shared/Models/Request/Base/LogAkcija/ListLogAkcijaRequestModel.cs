using BlazorErp.Shared.Models.Request.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Request.LogAkcija
{
    public class ListLogAkcijaRequestModel : PagedRequestModel
    {
        public int Akcija { get; set; }
        public int Level { get; set; }
        public int Kategorija { get; set; }
        public string KorisnickoIme { get; set; }
        public DateTime? Od { get; set; }
        public DateTime? Do { get; set; }
    }
}
