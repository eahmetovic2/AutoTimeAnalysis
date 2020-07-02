using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorErp.Server.Models.Korisnik
{
    /// <summary>
    /// Token korisnika
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Id tokena
        /// </summary>
        [Key, Column(Order = 0)]
        public Guid Id { get; set; }

        [Required]
        public int VlasnikId { get; set; }

        /// <summary>
        /// Datum kreiranja tokena
        /// </summary>
        public DateTime DatumKreiranja { get; set; }

        /// <summary>
        /// Datum isteka tokena
        /// </summary>
        public DateTime DatumIsteka { get; set; }

        /// <summary>
        /// Ip adresa posljenjeg klijenta koji je koristio token
        /// </summary>
        [StringLength(256)]
        public String PosljednjiIp { get; set; }

        /// <summary>
        /// Posljenji klijent koji je koristio token
        /// </summary>
        [StringLength(256)]
        public String PosljednjiKlijent { get; set; }

        /// <summary>
        /// Datum kada je token koristen posljednji put
        /// </summary>
        public DateTime DatumPosljednjeAkcije { get; set; }

        /// <summary>
        /// Vlasnik tokena
        /// </summary>
        [JsonIgnore]
        public virtual IdentityKorisnik Vlasnik { get; set; }

        public Core.Constants.TokenTip Tip { get; set; }

        [NotMapped]
        public string FrontendModul { get; set; }

        [NotMapped]
        public string FrontendModulNaslov { get; set; }

        public int UlogaId { get; set; }

        [JsonIgnore]
        public virtual Uloga Uloga { get; set; }
    }
}
