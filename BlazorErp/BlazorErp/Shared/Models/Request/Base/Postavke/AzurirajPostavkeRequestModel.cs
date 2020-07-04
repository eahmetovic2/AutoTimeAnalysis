using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorErp.Shared.Models.Request.Postavke
{
    public class AzurirajPostavkeRequestModel
    {
        [Required]
        [StringLength(64)]
        public String NaslovSistema { get; set; }

        [Range(1, 10)]
        public int TrajanjeSesije { get; set; }

        [Required]
        [StringLength(128)]
        public String UrlKarte { get; set; }

        [Required]
        [StringLength(256)]
        public String AutorskaPravaKarte { get; set; }

        [Required]
        public bool OmogucenoKreiranjeRazredaUProsleGodine { get; set; }

        [Required]
        public bool RetroaktivniUnosOcjenaSkola { get; set; }
    }
}
