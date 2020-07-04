using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Response.Korisnik.PravoObjekt
{
    public class PravoObjektListModel
    {
        public ICollection<PravoObjektListModelItem> Items { get; set; }
    }
}
