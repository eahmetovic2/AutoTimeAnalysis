//using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorErp.Server.Auth.Requirements;
using BlazorErp.Services.Definition.Korisnik;

namespace BlazorErp.Server.Controllers.Korisnik
{
    [Route("korisnici/pravoupravljanjakorisnikom")]
    public class PravoUpravljanjaKorisnikomController : BaseController
    {
        private IPravoUpravljanjaKorisnikomService pravoUpravljanjaKorisnikomService;

        public PravoUpravljanjaKorisnikomController(IPravoUpravljanjaKorisnikomService pravoUpravljanjaKorisnikomService)
        {
            this.pravoUpravljanjaKorisnikomService = pravoUpravljanjaKorisnikomService;
        }

        [HttpGet("{ulogaId:int}")]
        [ClaimRequirement(ClaimTypes.UserData, "korisnik_pravo_upravljanja_korisnikom_lista")]
        public IActionResult VratiSve(int ulogaId)
        {
            var result = pravoUpravljanjaKorisnikomService.VratiSve(ulogaId);
            return Convert(result);
        }
    }
}
