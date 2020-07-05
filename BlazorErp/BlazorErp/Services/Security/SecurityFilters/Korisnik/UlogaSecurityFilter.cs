using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlazorErp.Core.Database;
using BlazorErp.Entities;
using BlazorErp.Entities.Models.Korisnik;
using BlazorErp.Entities.Models.Sifarnik;
using BlazorErp.Services.Definition.Korisnik;

namespace BlazorErp.Services.Security.SecurityFilters.Korisnik
{
    internal class UlogaSecurityFilter : SecurityFilter<Uloga>
    {
        private IAuthService authService;
        private Context context;

        public UlogaSecurityFilter(IAuthService authService, Context context)
        {
            this.authService = authService;
            this.context = context;
        }

        public override IQueryable<Uloga> Secure(IQueryable<Uloga> query, SecurityLevel securityLevel)
        {
            return query;
        }
    }
}
