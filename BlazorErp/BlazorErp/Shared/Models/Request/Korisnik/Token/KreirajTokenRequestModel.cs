using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorErp.Shared.Models.Request.Korisnik.Token
{
    public class KreirajTokenRequestModel
    {
        [Required]
        [StringLength(256)]
        public String Lozinka { get; set; }

        [Required]
        public int UlogaId { get; set; }
    }
}
