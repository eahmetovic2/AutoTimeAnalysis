using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Response.Korisnik.Korisnik
{
    public class KorisnikModel : BazniResponseModel
    {
        public int Id { get; set; }
        public String KorisnickoIme { get; set; }

        public String Email { get; set; }

        public String PunoIme { get; set; }

        public DateTime? DatumKreiranja { get; set; }

        public bool Onemogucen { get; set; }

        public List<KorisnikUlogaModel> Uloge { get; set; }
        public int? PolId { get; set; }
        public int? Jezik { get; set; }
    }
}
