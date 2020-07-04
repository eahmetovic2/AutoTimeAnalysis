using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Response.Korisnik.UlogaTipDodatneInformacije
{
    public class UlogaTipDodatneInformacijeListModelItem
    {
        public int UlogaId { get; set; }

        public int KorisnikTipDodatneInformacijeId { get; set; }

        public string Sifra { get; set; }
    }
}
