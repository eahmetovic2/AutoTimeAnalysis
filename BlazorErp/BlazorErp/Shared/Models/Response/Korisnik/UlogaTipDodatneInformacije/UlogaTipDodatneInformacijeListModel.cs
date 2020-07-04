using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Response.Korisnik.UlogaTipDodatneInformacije
{
    public class UlogaTipDodatneInformacijeListModel
    {
        public ICollection<UlogaTipDodatneInformacijeListModelItem> Items { get; set; }
    }
}
