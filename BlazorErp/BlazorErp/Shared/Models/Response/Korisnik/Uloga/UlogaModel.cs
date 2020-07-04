using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Response.Korisnik.Uloga
{
    public class UlogaModel
    {
        public int Id { get; set; }

        public string Naziv { get; set; }

        public string Sifra { get; set; }

        public List<int> DozvoljeneUlogeZaUpravljanje { get; set; }
    }
}
