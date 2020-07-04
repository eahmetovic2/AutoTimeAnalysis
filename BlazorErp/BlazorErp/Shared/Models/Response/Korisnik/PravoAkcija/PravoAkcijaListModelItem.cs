using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Response.Korisnik.PravoAkcija
{
    public class PravoAkcijaListModelItem
    {
        public int Id { get; set; }

        public string Naziv { get; set; }

        public string Opis { get; set; }

        public bool Dozvoljeno { get; set; }

        public string Sifra { get; set; }
    }
}
