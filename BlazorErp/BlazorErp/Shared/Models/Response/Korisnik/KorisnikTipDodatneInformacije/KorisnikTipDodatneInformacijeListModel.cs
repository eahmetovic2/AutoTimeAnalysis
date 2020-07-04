using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Response.Korisnik.KorisnikTipDodatneInformacije
{
    public class KorisnikTipDodatneInformacijeListModel
    {
        public ICollection<KorisnikTipDodatneInformacijeListModelItem> Items { get; set; }
    }
}
