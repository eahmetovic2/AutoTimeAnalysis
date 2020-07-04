using BlazorErp.Entities.Models;
using BlazorErp.Entities.Models.Korisnik;
using System;
using System.Linq;
using System.Text;
using BlazorErp.Core.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlazorErp.Entities.ContextExtensions
{
    public static class AdminKorisnik
    {
        public static async Task Execute(Context context)
        {
            //todo ovo je potrebno ponovo implementirati sa dinamickim ulogama
            

            if (!context.Users.Any(x => x.NormalizedUserName == "admin"))
            {
                var korisnik = new IdentityKorisnik()
                {
                    UserName = "admin",
                    NormalizedUserName = "admin",
                    Email = "admin@example.ba",
                    PunoIme = "Administrator",
                    SecurityStamp = new Guid().ToString()
                };
                var password = new PasswordHasher<IdentityKorisnik>();
                var hashed = password.HashPassword(korisnik, "adminpass");
                korisnik.PasswordHash = hashed;

                context.Users.Add(korisnik);

                context.SaveChanges();

                var ulogaId = context.Roles.Where(x => x.NormalizedName == "administrator").FirstOrDefault().Id;

                context.UserRoles.Add(new KorisnikUloga
                {
                    RoleId = ulogaId,
                    UserId = korisnik.Id
                });
                context.SaveChanges();                
            }
        }
    }
}
