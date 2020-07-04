using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorErp.Shared.Models.Request.Korisnik.Korisnik
{
    public class AzurirajLicneDetaljeRequestModel
    {
        [Required]
        [StringLength(256)]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        [StringLength(128)]
        public String PunoIme { get; set; }


        public int? PolId { get; set; }
        public String Telefon { get; set; }
        public int? Jezik { get; set; }

        public string StaraLozinka { get; set; }
        public string NovaLozinka { get; set; }
        public bool Odjavi { get; set; }
    }
}
