using BlazorErp.Core.Constants;
using BlazorErp.Entities;
using BlazorErp.Entities.Models.Korisnik;
using BlazorErp.Server.Auth;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorErp.Server.Areas.Identity
{
    public class IdentityProfileService : IProfileService
    {

        private readonly IUserClaimsPrincipalFactory<IdentityKorisnik> _claimsFactory;
        private readonly UserManager<IdentityKorisnik> _userManager;
        private readonly Context dbContext;

        public IdentityProfileService(IUserClaimsPrincipalFactory<IdentityKorisnik> claimsFactory, UserManager<IdentityKorisnik> userManager, Context dbContext)
        {
            _claimsFactory = claimsFactory ?? throw new ArgumentNullException(nameof(claimsFactory));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.dbContext = dbContext;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            if (user == null)
            {
                throw new InvalidOperationException();
            }
            var principal = await _claimsFactory.CreateAsync(user);
            var claims = principal.Claims.ToList();

            claims.Add(new Claim("punoIme", user.PunoIme, ClaimValueTypes.String));

            var trenutnaUloga = dbContext.UserRoles.Include(x => x.Role).Where(x => x.UserId == user.Id).FirstOrDefault();

            user.TrenutnaUlogaId = trenutnaUloga.RoleId;

            claims.Add(new Claim("ulogaId", user.TrenutnaUlogaId.ToString(), ClaimValueTypes.String));

            var dozvoljeneAkcije = dbContext.PravoAkcijaUloge
                                            .Include(a => a.PravoAkcija)
                                            .Include(x => x.Uloga)
                                            .Where(a => a.UlogaId == user.TrenutnaUlogaId)
                                            .Select(x => x.PravoAkcija.Sifra).ToList();

            foreach (var dozvoljenaAkcija in dozvoljeneAkcije)
            {
                claims.Add(new Claim(ClaimTypes.UserData, dozvoljenaAkcija));
            }

            if(trenutnaUloga.Role.VrijednostUAplikaciji == (int)Core.Constants.Uloga.Administrator)
            {
                foreach (var dozvoljenaAkcija in UlogePrava.AdministratorPrava)
                {
                    int akcija = (int)dozvoljenaAkcija;
                    claims.Add(new Claim("PravoAkcija", akcija.ToString()));
                }
            }

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }

}
