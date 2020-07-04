using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorErp.Shared.Models.Request.Korisnik
{
    public class PostaviLozinkuRequestModel
    {
        [Required]
        [StringLength(256, MinimumLength = 6)]
        public String NovaLozinka { get; set; }
    }
}
