using System;
using System.Collections.Generic;
using System.Text;
using BlazorErp.Shared.Models.Response.Korisnik.PravoObjekt;

namespace BlazorErp.Shared.Models.Response.Korisnik.Uloga
{
    public class GrupaPravaUlogeListModelItem
    {
        public int Id { get; set; }

        public string Naziv { get; set; }

        public PravoObjektListModel PravoObjekti { get; set; }
    }
}
