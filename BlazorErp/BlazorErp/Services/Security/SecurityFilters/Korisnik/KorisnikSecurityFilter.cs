using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlazorErp.Core.Database;
using BlazorErp.Entities;
using BlazorErp.Entities.Models.Korisnik;

namespace BlazorErp.Services.Security.SecurityFilters.Korisnik
{
    internal class KorisnikSecurityFilter : SecurityFilter<IdentityKorisnik>
    {
        private IAuthService authService;
        private IKorisnikService korisnikService;
        private Context context;

        public KorisnikSecurityFilter(IAuthService authService, IKorisnikService korisnikService, Context context)
        {
            this.authService = authService;
            this.korisnikService = korisnikService;
            this.context = context;
        }

        public override IQueryable<IdentityKorisnik> Secure(IQueryable<IdentityKorisnik> query, SecurityLevel securityLevel)
        {
            var korisnik = authService.TrenutniKorisnik();

            if (korisnik == null)
                return query;
            
            return query;
        }
    }
}
