//using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorErp.Server.Auth.Requirements;
using BlazorErp.Server.Common.Attributes;
using BlazorErp.Shared.Models.Request.Korisnik.Uloga;
using BlazorErp.Services;
using BlazorErp.Services.Definition.Korisnik;

namespace BlazorErp.Server.Controllers.Korisnik
{
    [Route("korisnici/uloga")]
    public class UlogaController : BaseController
    {
        private IUlogaService ulogaService;
        private ILogService logService;
        private IAuthService authService;

        public UlogaController(IUlogaService ulogaService, ILogService logService, IAuthService authService)
        {
            this.ulogaService = ulogaService;
            this.logService = logService;
            this.authService = authService;
        }

        [HttpPost("")]
        [RequireModel]
       //[ClaimRequirement(ClaimTypes.UserData, "korisnik_uloga_dodavanje")]
        public async Task<IActionResult> Kreiraj([FromBody] KreirajUloguRequestModel model)
        {
            var vrsta = await ulogaService.Kreiraj(model);
            return Convert(vrsta);
        }

        [HttpPut("{ulogaId:int}")]
        [RequireModel]
       //[ClaimRequirement(ClaimTypes.UserData, "korisnik_uloga_izmjena")]
        public IActionResult Azuriraj(int ulogaId, [FromBody] AzurirajUloguRequestModel model)
        {
            var vrsta = ulogaService.Azuriraj(ulogaId, model);
            return Convert(vrsta);
        }

        [HttpGet("")]
       //[ClaimRequirement(ClaimTypes.UserData, "korisnik_uloga_lista")]
        public IActionResult VratiSve()
        {
            var result = ulogaService.VratiSve();
            return Convert(result);
        }

        [HttpGet("{ulogaId:int}")]
       //[ClaimRequirement(ClaimTypes.UserData, "korisnik_uloga_pregled")]
        public IActionResult VratiJednu(int ulogaId)
        {
            var result = ulogaService.VratiPoIdu(ulogaId);
            return Convert(result);
        }


        [HttpGet("{ulogaId:int}/dozvoljeneakcije")]
       //[ClaimRequirement(ClaimTypes.UserData, "korisnik_uloga_lista_dozvoljenih_akcija")]
        public IActionResult VratiSveDozvoljeneAkcije(int ulogaId)
        {
            var result = ulogaService.VratiSveDozvoljeneAkcije(ulogaId);
            return Convert(result);
        }

        [HttpPut("{ulogaId:int}/dozvoljeneakcije")]
       //[ClaimRequirement(ClaimTypes.UserData, "korisnik_uloga_izmjena_dozvoljenih_akcija")]
        public IActionResult SnimiDozvoljeneAkcije(int ulogaId, [FromBody]SnimiDozvoljeneAkcijeRequestModel model)
        {
            var result = ulogaService.SnimiDozvoljeneAkcije(ulogaId, model);
            return Convert(result);
        }

        [HttpGet("{ulogaId:int}/grupeprava")]
       //[ClaimRequirement(ClaimTypes.UserData, "korisnik_uloga_lista_grupa_prava")]
        public IActionResult VratiGrupePravaProsireno(int ulogaId)
        {
            var result = ulogaService.VratiSveGrupePravaProsireno(ulogaId);
            return Convert(result);
        }
    }
}
