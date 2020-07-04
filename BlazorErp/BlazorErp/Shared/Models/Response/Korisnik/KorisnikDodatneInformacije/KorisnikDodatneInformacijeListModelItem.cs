using System;
using System.Collections.Generic;
using System.Text;
using BlazorErp.Shared.Models.Response.Korisnik.KorisnikTipDodatneInformacije;

namespace BlazorErp.Shared.Models.Response.Korisnik.KorisnikDodatneInformacije
{
    public class KorisnikDodatneInformacijeListModelItem
    {
        public int KorisnikId { get; set; }

        public KorisnikTipDodatneInformacijeModel TipDodatneInformacije { get; set; }

        public int Vrijednost { get; set; }
    }
}
