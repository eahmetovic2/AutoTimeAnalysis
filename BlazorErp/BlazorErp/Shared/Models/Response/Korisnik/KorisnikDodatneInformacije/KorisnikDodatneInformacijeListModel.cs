using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Response.Korisnik.KorisnikDodatneInformacije
{
    public class KorisnikDodatneInformacijeListModel
    {
        public ICollection<KorisnikDodatneInformacijeListModelItem> Items { get; set; }
    }
}
