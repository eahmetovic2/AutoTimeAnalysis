using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorErp.Entities.Models.Korisnik
{
    /// <summary>
    /// Korisnika aplikacije
    /// </summary>
    public class IdentityKorisnik : IdentityUser<int>
    {
        /// <summary>
        /// Puno ime korisnika
        /// </summary>
        [Required]
        [StringLength(128)]
        public String PunoIme { get; set; }

        [JsonIgnore]
        public virtual ICollection<Base.LogAkcija> LogAkcije { get; set; }

        [NotMapped]
        public int TrenutnaUlogaId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public Uloga TrenutnaUloga { get; set; }
        public virtual ICollection<KorisnikUloga> Roles { get; set; } = new List<KorisnikUloga>();
    }
}
