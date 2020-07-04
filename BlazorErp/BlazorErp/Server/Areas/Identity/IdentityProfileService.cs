using BlazorErp.Entities.Models.Korisnik;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorErp.Server.Areas.Identity
{
    public class IdentityProfileService : IProfileService
    {

        private readonly IUserClaimsPrincipalFactory<IdentityKorisnik> _claimsFactory;
        private readonly UserManager<IdentityKorisnik> _userManager;

        public IdentityProfileService(IUserClaimsPrincipalFactory<IdentityKorisnik> claimsFactory, UserManager<IdentityKorisnik> userManager)
        {
            _claimsFactory = claimsFactory ?? throw new ArgumentNullException(nameof(claimsFactory));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
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
