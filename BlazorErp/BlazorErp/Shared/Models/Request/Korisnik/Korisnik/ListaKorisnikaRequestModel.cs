using BlazorErp.Shared.Models.Request.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Request.Korisnik
{
    public class ListaKorisnikaRequestModel: PagedRequestModel
    {
        public String Filter { get; set; }
        public String Username { get; set; }
        public int? UlogaId { get; set; }
    }
}
