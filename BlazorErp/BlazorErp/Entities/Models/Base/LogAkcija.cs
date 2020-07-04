using BlazorErp.Core.Constants;
using BlazorErp.Entities.Models.Korisnik;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorErp.Entities.Models.Base
{
    public class LogAkcija
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public LogLevel Level { get; set; }

        public LogKategorija Kategorija { get; set; }
        //public int Akcija { get; set; }
        public Core.Constants.LogAkcija Akcija { get; set; }

        public string Opis { get; set; }

        [Required]
        public DateTime DatumAkcije { get; set; }


        public int KorisnikId { get; set; }

        [ForeignKey("KorisnikId")]
        [JsonIgnore]
        public virtual IdentityKorisnik Korisnik { get; set; }
    }
}
