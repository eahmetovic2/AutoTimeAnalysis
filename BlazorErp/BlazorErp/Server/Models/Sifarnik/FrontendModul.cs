using BlazorErp.Server.Models.Korisnik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorErp.Server.Models.Sifarnik
{
    public class FrontendModul : BazniModel
    {
        public int Id { get; set; }

        public string Naziv { get; set; }

        public string Sifra { get; set; }

        public bool Onemogucen { get; set; }

        [JsonIgnore]
        public virtual ICollection<Uloga> Uloge { get; set; }
    }
}
