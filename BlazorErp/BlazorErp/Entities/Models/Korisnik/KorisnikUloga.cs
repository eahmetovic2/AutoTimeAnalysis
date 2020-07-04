using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorErp.Entities.Models.Korisnik
{
    public class KorisnikUloga : IdentityUserRole<int>
    {
        public virtual IdentityKorisnik User { get; set; }
        public virtual Uloga Role { get; set; }
        [JsonIgnore]
        public virtual ICollection<KorisnikUlogaDodatnaInformacija> KorisnikUlogaDodatnaInformacija { get; set; }

    }
}
