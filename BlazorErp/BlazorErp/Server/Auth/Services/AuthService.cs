using Autofac;
using BlazorErp.Entities;
using BlazorErp.Entities.Models.Korisnik;
using BlazorErp.Services.Definition.Korisnik;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorErp.Server.Auth.Services
{
    public class AuthService : IAuthService
    {
        /// <summary>
        /// Entity framework db kontekst 
        /// </summary>
        private Context context;

        /// <summary>
        /// Dozvoljava pristup Contex-u trenutnog request-a
        /// </summary>
        private IHttpContextAccessor accessor;

        private IdentityKorisnik trenutni;

        /// <summary>
        /// Konstruktor servisa
        /// </summary>
        public AuthService(ILifetimeScope scope, IHttpContextAccessor accessor)

        {
            this.context = scope.Resolve<Context>();
            this.accessor = accessor;
            trenutni = DajTrenutnogKorisnikaIzBaze();
        }

        public IdentityKorisnik TrenutniKorisnik()
        {
            return trenutni;
        }

        private IdentityKorisnik DajTrenutnogKorisnikaIzBaze()
        {
            if (accessor.HttpContext != null)
            {
                var korisnickoIme = accessor.HttpContext.User.Identity.Name;
                var korisnik = context.Users
                              .FirstOrDefault(x => x.UserName == korisnickoIme);

                var ulogaClaim = accessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == "ulogaId");

                if (ulogaClaim != null)
                {
                    var trenutnaUloga = context.Roles.Where(x => x.Id == Convert.ToInt32(ulogaClaim.Value)).FirstOrDefault();
                    korisnik.TrenutnaUlogaId = trenutnaUloga.Id;

                    korisnik.TrenutnaUloga = trenutnaUloga;

                    // ucitaj samo one korisnik uloge koje trebaju
                    context.Entry(korisnik.TrenutnaUloga).Collection(a => a.Users)
                                                         .Query()
                                                         .Where(a => a.UserId == korisnik.Id)
                                                         .Load();
                }

                return korisnik;
            }
            return null;
        }
    }
}
