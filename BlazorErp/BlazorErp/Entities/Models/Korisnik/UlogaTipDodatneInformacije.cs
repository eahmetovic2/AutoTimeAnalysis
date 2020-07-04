using BlazorErp.Entities.Models.Sifarnik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorErp.Entities.Models.Korisnik
{
    public class UlogaTipDodatneInformacije
    {
        public int KorisnikTipDodatneInformacijeId { get; set; }

        [JsonIgnore]
        public virtual KorisnikTipDodatneInformacije KorisnikTipDodatneInformacije { get; set; }


        public int UlogaId { get; set; }

        [JsonIgnore]
        public virtual Uloga Uloga { get; set; }
    }
}
