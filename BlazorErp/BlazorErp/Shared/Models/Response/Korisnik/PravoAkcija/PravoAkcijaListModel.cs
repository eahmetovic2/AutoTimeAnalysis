using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Response.Korisnik.PravoAkcija
{
    public class PravoAkcijaListModel
    {
        public ICollection<PravoAkcijaListModelItem> Items { get; set; }
    }
}
