using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorErp.Shared.Models.Request.Korisnik.Uloga
{
    public class AzurirajUloguRequestModel
    {
        [Required]
        [StringLength(100)]
        public string Naziv { get; set; }

        [Required]
        [StringLength(100)]
        public string Sifra { get; set; }

        [Required]
        public int FrontendModulId { get; set; }

        [Required]
        public List<int> DozvoljeneUlogeZaUpravljanje { get; set; }
    }
}
