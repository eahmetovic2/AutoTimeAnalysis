using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using BlazorErp.Entities.Models.Korisnik;

namespace BlazorErp.Entities.ContextExtensions
{
    public static class Uloga
    {
        public static void Execute(Context context)
        {
            var entity = context.Roles.FirstOrDefault(x => x.NormalizedName == "neodredeno" && !x.IsDeleted);

            if (entity == null)
                context.Roles.Add(new Models.Korisnik.Uloga()
                {
                    Name = "Neodređeno",
                    NormalizedName = "neodredeno",
                    VrijednostUAplikaciji = 0,
                    Poredak = 0,
                    IsDeleted = false,
                });

            context.SaveChanges();

            entity = context.Roles.FirstOrDefault(x => x.NormalizedName == "administrator" && !x.IsDeleted);
            if (entity == null)
            {
                var admin = new Models.Korisnik.Uloga()
                {
                    Name = "Administrator",
                    NormalizedName = "administrator",
                    VrijednostUAplikaciji = 1,
                    Poredak = 1,
                    IsDeleted = false,
                };

                context.Roles.Add(admin);

                context.SaveChanges();

            }
        }
    }
}
