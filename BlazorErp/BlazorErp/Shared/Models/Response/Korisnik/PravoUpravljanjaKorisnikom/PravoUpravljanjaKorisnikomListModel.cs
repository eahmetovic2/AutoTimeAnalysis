using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Response.Korisnik.PravoUpravljanjaKorisnikom
{
    public class PravoUpravljanjaKorisnikomListModel
    {
        public ICollection<PravoUpravljanjaKorisnikomListModelItem> Items { get; set; }
    }
}
