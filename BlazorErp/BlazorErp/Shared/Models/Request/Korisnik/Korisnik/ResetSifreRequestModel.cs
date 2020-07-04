using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Request.Korisnik
{
    public class ResetSifreRequestModel
    {
		public string Token { get; set; }

        public string NovaLozinka { get; set; }
    }
}
