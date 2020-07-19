using BlazorErp.Client.Models;
using BlazorErp.Client.Services.Definition;
using BlazorErp.Shared.Prava;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorErp.Client.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private AuthenticationStateProvider authenticationStateProvider;
        private Korisnik trenutni = new Korisnik();

        public AuthService(AuthenticationStateProvider authenticationStateProvider)
        {
            this.authenticationStateProvider = authenticationStateProvider;
            DajTrenutnogKorisnika();
        }

        public Korisnik TrenutniKorisnik()
        {
            return trenutni;
        }

        private async Task DajTrenutnogKorisnika()
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            var claims = user.Claims;

            var korisnik = new Korisnik
            {
                PravoAkcije = new List<PravoAkcije>(),
                KorisnickoIme = "",
                PunoIme = "",
                UlogaId = 0,
                Uloga = ""
            };

            korisnik.KorisnickoIme = claims.Where(x => x.Type == "name").FirstOrDefault().Value;

            korisnik.UlogaId = Convert.ToInt32(claims.Where(x => x.Type == "ulogaId").FirstOrDefault().Value);
            korisnik.Uloga = claims.Where(x => x.Type == "role").FirstOrDefault().Value;

            korisnik.PunoIme = claims.Where(x => x.Type == "punoIme").FirstOrDefault().Value;

            var pravoAkcijeJson = claims.Where(x => x.Type == "PravoAkcija").Select(x => x.Value).FirstOrDefault();
            if (pravoAkcijeJson != null && pravoAkcijeJson.Length > 0)
            {
                var pravoAkcije = JsonConvert.DeserializeObject<List<string>>(pravoAkcijeJson);

                korisnik.PravoAkcije = pravoAkcije.Select(x => (PravoAkcije)Convert.ToInt32(x)).ToList();
            }



            trenutni = korisnik;
        }
    }
}
