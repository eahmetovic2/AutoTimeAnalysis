using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorErp.Entities.Models.Korisnik
{
    public class Uloga : IdentityRole<int>
    {
        public int? VrijednostUAplikaciji { get; set; }

        public int Poredak { get; set; }

        public bool IsDeleted { get; set; }

        [JsonIgnore]
        public virtual ICollection<PravoAkcijaUloga> PravoAkcijaUloge { get; set; }

        [JsonIgnore]
        public virtual ICollection<UlogaTipDodatneInformacije> TipoviDodatneInformacije { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(PravoUpravljanjaKorisnikom.UlogaUpravitelja))]
        public virtual ICollection<PravoUpravljanjaKorisnikom> PravaUpravljanja { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(PravoUpravljanjaKorisnikom.UlogaUpravljanog))]
        public virtual ICollection<PravoUpravljanjaKorisnikom> PravaUpravljanosti { get; set; }

        public virtual ICollection<KorisnikUloga> Users { get; set; } = new List<KorisnikUloga>();

        //[JsonIgnore]
        //public virtual ICollection<Token> Tokeni { get; set; }
    }
}
