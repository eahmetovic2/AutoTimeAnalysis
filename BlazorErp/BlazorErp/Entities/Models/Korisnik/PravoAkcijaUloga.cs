using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorErp.Entities.Models.Korisnik
{
    public class PravoAkcijaUloga
    {
        public int PravoAkcijaId { get; set; }

        [JsonIgnore]
        public virtual PravoAkcija PravoAkcija { get; set; }

        public int UlogaId { get; set; }

        [JsonIgnore]
        public virtual Uloga Uloga { get; set; }
    }
}
