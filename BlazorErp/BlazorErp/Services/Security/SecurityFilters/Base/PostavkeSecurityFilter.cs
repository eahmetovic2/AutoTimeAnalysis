using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlazorErp.Core.Database;
using BlazorErp.Entities.Models.Base;
using BlazorErp.Services.Definition.Korisnik;

namespace BlazorErp.Services.Security.SecurityFilters.Base
{
    internal class PostavkeSecurityFilter : SecurityFilter<Postavke>
    {
        private IAuthService authService;

        public PostavkeSecurityFilter(IAuthService authService)
        {
            this.authService = authService;
        }

        public override IQueryable<Postavke> Secure(IQueryable<Postavke> query, SecurityLevel securityLevel)
        {          

            return query;
        }
    }
}
