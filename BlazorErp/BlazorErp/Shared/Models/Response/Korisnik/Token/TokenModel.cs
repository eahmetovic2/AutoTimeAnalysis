using System;
using System.Collections.Generic;
using System.Text;
using BlazorErp.Shared.Models.Response.Korisnik.Korisnik;

namespace BlazorErp.Shared.Models.Response.Token
{
    public class TokenModel
    {
        public Guid Id { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public DateTime DatumIsteka { get; set; }
        public String PosljednjiIp { get; set; }
        public String PosljedniKlijent { get; set; }
        public DateTime DatumZadnjeAkcije { get; set; }

        public KorisnikTokenModel Vlasnik { get; set; }
        public BlazorErp.Core.Constants.TokenTip Tip { get; set; }
        public int UlogaId { get; set; }
        public string Uloga { get; set; }



    }
}
