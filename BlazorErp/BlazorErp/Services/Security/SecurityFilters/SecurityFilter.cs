using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlazorErp.Core.Database;

namespace BlazorErp.Services.Security.SecurityFilters
{
    public abstract class SecurityFilter<T> : ISecurityFilter<T>
    {
        public abstract IQueryable<T> Secure(IQueryable<T> query, SecurityLevel securityLevel);

        public IQueryable<T> VratiPrazan(IQueryable<T> query)
        {
            return Enumerable.Empty<T>().AsQueryable();
        }
    }
}
