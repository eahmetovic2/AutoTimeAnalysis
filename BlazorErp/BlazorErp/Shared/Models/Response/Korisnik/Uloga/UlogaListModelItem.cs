using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Response.Korisnik.Uloga
{
    public class UlogaListModelItem
    {
        public int Id { get; set; }

        public string Naziv { get; set; }

        public int? VrijednostUAplikaciji { get; set; }
    }
}
