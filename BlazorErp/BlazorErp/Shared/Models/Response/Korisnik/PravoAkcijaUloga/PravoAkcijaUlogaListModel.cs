using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Response.Korisnik.PravoAkcijaUloga
{
    public class PravoAkcijaUlogaListModel
    {
        public ICollection<PravoAkcijaUlogaListModelItem> Items { get; set; }
    }
}
