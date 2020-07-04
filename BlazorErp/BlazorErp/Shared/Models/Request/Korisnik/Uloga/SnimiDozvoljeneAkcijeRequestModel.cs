using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorErp.Shared.Models.Request.Korisnik.Uloga
{
    public class SnimiDozvoljeneAkcijeRequestModel
    {
        public int UlogaId { get; set; }

        public List<int> Akcije { get; set; }
    }
}
