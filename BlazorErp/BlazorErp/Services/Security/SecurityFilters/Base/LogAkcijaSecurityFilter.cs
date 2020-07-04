using BlazorErp.Core.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazorErp.Services.Security.SecurityFilters.Base
{
    internal class LogAkcijaSecurityFilter : SecurityFilter<BlazorErp.Entities.Models.Base.LogAkcija>
    {
        private IAuthService authService;

        public LogAkcijaSecurityFilter(IAuthService authService)
        {
            this.authService = authService;
        }

        public override IQueryable<Entities.Models.Base.LogAkcija> Secure(IQueryable<Entities.Models.Base.LogAkcija> query, SecurityLevel securityLevel)
        {
            return query;
        }
    }
}
