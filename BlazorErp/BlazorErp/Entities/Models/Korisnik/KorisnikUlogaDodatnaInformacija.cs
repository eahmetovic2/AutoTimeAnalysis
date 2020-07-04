using BlazorErp.Entities.Models.Sifarnik;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorErp.Entities.Models.Korisnik
{
    public class KorisnikUlogaDodatnaInformacija : BazniModel
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int KorisnikTipDodatneInformacijeId { get; set; }

        public int Vrijednost { get; set; }

        [JsonIgnore]
        public virtual KorisnikTipDodatneInformacije KorisnikTipDodatneInformacije { get; set; }


        public int KorisnikId { get; set; }

        public int UlogaId { get; set; }
        [JsonIgnore]
        public virtual KorisnikUloga KorisnikUloga { get; set; }
    }
}
