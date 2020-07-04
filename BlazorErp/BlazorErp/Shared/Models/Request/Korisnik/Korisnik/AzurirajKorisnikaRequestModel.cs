
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorErp.Shared.Models.Request.Korisnik
{
    public class AzurirajKorisnikaRequestModel
    {
        [Required]
        [StringLength(256)]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        [StringLength(128)]
        public String PunoIme { get; set; }

        public string Lozinka { get; set; }
        public int? PolId { get; set; }

        [Required]
        public List<DodavanjeUlogeKorisnika> Uloge { get; set; }

    }
}
