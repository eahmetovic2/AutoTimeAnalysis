using System;
using System.Collections.Generic;
using System.Text;
using BlazorErp.Shared.Models.Response.Korisnik.PravoAkcija;

namespace BlazorErp.Shared.Models.Response.Korisnik.PravoObjekt
{
    public class PravoObjektListModelItem
    {
        public int Id { get; set; }

        public string Naziv { get; set; }

        public PravoAkcijaListModel PravoAkcije { get; set; }
    }
}
