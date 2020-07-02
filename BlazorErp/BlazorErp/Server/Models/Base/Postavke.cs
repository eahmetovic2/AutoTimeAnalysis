using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorErp.Server.Models.Base
{
    /// <summary>
    /// Postavke sistema
    /// </summary>
    public class Postavke : BazniModel
    {
        /// <summary>
        /// Konstruktor sa defaultnim vrijednostima postavki
        /// </summary>
        public Postavke()
        {
            NaslovSistema = "BlazorErp";
            TrajanjeSesije = 5;
        }

        /// <summary>
        /// Id postavki
        /// </summary>
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Naslov sistema (koristi se u frontendu)
        /// </summary>
        [Required]
        [StringLength(64)]
        public String NaslovSistema { get; set; }

        /// <summary>
        /// Trajanje sesije korisnika u danima
        /// </summary>
        [Range(1, 10)]
        public int TrajanjeSesije { get; set; }
    }
}
