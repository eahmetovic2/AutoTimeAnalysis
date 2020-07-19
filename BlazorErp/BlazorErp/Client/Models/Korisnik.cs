using BlazorErp.Shared.Prava;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorErp.Client.Models
{
    public class Korisnik
    {
        public string KorisnickoIme { get; set; }
        public string PunoIme { get; set; }
        public string Uloga { get; set; }
        public int UlogaId { get; set; }
        public List<PravoAkcije> PravoAkcije { get; set; }
    }
}
