//using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorErp.Server.Auth.Requirements;
using BlazorErp.Shared.Models.Request.Korisnik.KorisnikDodatneInformacije;
using BlazorErp.Services.Definition.Korisnik;

namespace BlazorErp.Server.Controllers.Korisnik
{
    [Route("korisnici/ulogatipovidodatneinformacije")]
    public class UlogaTipoviDodatneInformacijeController : BaseController
    {
        public IUlogaTipoviDodatneInformacijeService ulogaTipoviDodatneInformacijeService { get; set; }
        public UlogaTipoviDodatneInformacijeController(IUlogaTipoviDodatneInformacijeService ulogaTipoviDodatneInformacijeService)
        {
            this.ulogaTipoviDodatneInformacijeService = ulogaTipoviDodatneInformacijeService;
        }

        [HttpGet("zaulogu/{ulogaId:int}")]
       //[ClaimRequirement(ClaimTypes.UserData, "korisnik_uloga_lista_tipova_dodatne_informacije")]
        public IActionResult VratiSve(int ulogaId)
        {
            var result = ulogaTipoviDodatneInformacijeService.VratiZaUlogu(ulogaId);
            return Convert(result);
        }

        [HttpPut("zaulogu/{ulogaId:int}")]
       //[ClaimRequirement(ClaimTypes.UserData, "korisnik_uloga_izmjena_tipova_dodatne_informacije")]
        public IActionResult SnimiZaUlogu(int ulogaId, [FromBody]SnimiDodatneInformacijeUlogeRequestModel model)
        {
            var result = ulogaTipoviDodatneInformacijeService.SnimiZaUlogu(ulogaId, model);
            return Convert(result);
        }
    }
}
