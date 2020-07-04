using System;
using System.Collections.Generic;
using System.Text;
using BlazorErp.Shared.Models.Response.Common;

namespace BlazorErp.Shared.Models.Response.Korisnik.Korisnik
{
    public class KorisnikListModel : PagedListModel<KorisnikListModelItem>
    {
    }

    public class KorisnikListModelItem : BazniResponseModel
    {
        public int Id { get; set; }
        public String KorisnickoIme { get; set; }
        public String Email { get; set; }
        public string Uloga { get; set; }
        public String PunoIme { get; set; }
        public DateTime? DatumKreiranja { get; set; }
        public bool Onemogucen { get; set; }
    }
}
