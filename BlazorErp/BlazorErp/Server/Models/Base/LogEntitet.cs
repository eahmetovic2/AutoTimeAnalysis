using BlazorErp.Server.Models.Korisnik;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorErp.Server.Models.Base
{
    public class LogEntitet
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int EntitetId { get; set; }

        public Core.Constants.LogEntitet Entitet { get; set; }

        public string Vrijednost { get; set; }

        [Required]
        public DateTime DatumAkcije { get; set; }


        public int KorisnikId { get; set; }

        [ForeignKey("KorisnikId")]
        [JsonIgnore]
        public virtual IdentityKorisnik Korisnik { get; set; }
    }
}
