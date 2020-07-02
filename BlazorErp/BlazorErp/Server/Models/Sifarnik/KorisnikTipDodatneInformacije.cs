using BlazorErp.Server.Models.Korisnik;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorErp.Server.Models.Sifarnik
{
    public class KorisnikTipDodatneInformacije : Sifarnik
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [StringLength(45)]
        public string Sifra { get; set; }

        [StringLength(200)]
        public string Opis { get; set; }

        [StringLength(200)]
        public string Naziv { get; set; }

        public int Poredak { get; set; }

        public bool Onemogucen { get; set; }

        [JsonIgnore]
        public virtual ICollection<UlogaTipDodatneInformacije> TipoviDodatneInformacije { get; set; }
    }
}
