using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Response.Korisnik.Uloga
{
    public class GrupaPravaUlogeListModel
    {
        public ICollection<GrupaPravaUlogeListModelItem> Items { get; set; }
    }
}
