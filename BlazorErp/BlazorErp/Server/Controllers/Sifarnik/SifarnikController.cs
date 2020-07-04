using BlazorErp.Server.Common.Attributes;
using BlazorErp.Server.Common.Result;
using BlazorErp.Core.Constants;
using BlazorErp.Shared.Models.Request.Sifarnik;
using BlazorErp.Shared.Models.Response.Sifarnik;
using BlazorErp.Services;
using BlazorErp.Services.Result;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorErp.Server.Auth.Requirements;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace BlazorErp.Server.Controllers.Sifarnik
{
    /// <summary>
    /// Kontroler za dobijanje sifarnika
    /// </summary>
    
    [Route("sifarnik/")]
    public class SifarnikController : BaseController
    {
        private const string modelNamespace = "Web.Entities.Models";
        /// <summary>
        /// Servis za upravljanje sifarnicima
        /// </summary>
        private ISifarnikService sifarnikService;
        /// <summary>
        /// Servis za logiranje
        /// </summary>
        private ILogService logService;
        /// <summary>
        /// Servis za autentikaciju
        /// </summary>
        private IAuthService authService;

        /// <summary>
        /// Konstruktor kontrolera
        /// </summary>
        public SifarnikController(ISifarnikService sifarnikService, ILogService logService, IAuthService authService)
        {
            this.sifarnikService = sifarnikService;
            this.logService = logService;
            this.authService = authService;
        }

        [HttpGet("")]
        [ClaimRequirement(ClaimTypes.UserData, "sifarnik_sifarnik_lista")]
        public IActionResult VratiSve([FromQuery]ESifarnik sifarnik, [FromQuery] DateTime? datumIzmjene)
        {
            Request.Headers.TryGetValue("If-None-Match", out StringValues header);
            if (header.Count > 0)
            {
                // ovo je datum koji dodje u headeru
                var oldDate = long.Parse(header[0]);

                // ovo je datum iz baze
                var newDate = sifarnikService.VratiSve(sifarnik, true, datumIzmjene).DatumResponsa.Ticks;
                
                if (oldDate == newDate)
                {                        
                    return new StatusCodeResult(304);
                }
            }
            var result = sifarnikService.VratiSve(sifarnik, false, datumIzmjene);
            HttpContext.Response.Headers.Add("ETag", result.DatumResponsa.Ticks.ToString());
            return Ok(result);
        }

        [HttpGet("{sifarnik}/nekesirano")]
        [ClaimRequirement(ClaimTypes.UserData, "sifarnik_sifarnik_lista")]
        public IActionResult VratiSveNekesirano(ESifarnik sifarnik, [FromQuery] ListaSifarnikRequestModel model)
        {
            return Ok(sifarnikService.VratiSveSaPoljima(sifarnik, model));
        }

        [HttpGet("{sifarnik}/polja")]
        [ClaimRequirement(ClaimTypes.UserData, "sifarnik_sifarnik_pregled")]
        public IActionResult VratiPolja(ESifarnik sifarnik)
        {
            return Ok(sifarnikService.VratiPolja(sifarnik));
        }

        [HttpPost("{tipSifarnika}")]
        [ClaimRequirement(ClaimTypes.UserData, "sifarnik_sifarnik_dodavanje")]
        public IActionResult SnimiSifarnik(ESifarnik tipSifarnika, [FromBody]KreirajSifarnikRequestModel model)
        {
            if (sifarnikService.SnimiSifarnik(tipSifarnika, model))
            {

                logService.Akcija(Core.Constants.LogLevel.Info,
                         Core.Constants.LogKategorija.sifarnici,
                         Core.Constants.LogAkcija.sifarnik_dodaj_red,
                         "ESifarnik: " + tipSifarnika ,
                        authService.TrenutniKorisnik().NormalizedUserName
                         );
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("{tipSifarnika}/{id}")]
        [ClaimRequirement(ClaimTypes.UserData, "sifarnik_sifarnik_izmjena")]
        public IActionResult UpdateSifarnik(ESifarnik tipSifarnika, int id, [FromBody]UpdateSifarnikRequestModel model)
        {
            model.Id = id;
            if (sifarnikService.UpdateSifarnik(tipSifarnika, model))
            {

                logService.Akcija(Core.Constants.LogLevel.Info,
                        Core.Constants.LogKategorija.sifarnici,
                        Core.Constants.LogAkcija.sifarnik_izmijeni_red,
                        "ESifarnik: " + tipSifarnika,
                       authService.TrenutniKorisnik().NormalizedUserName
                        );
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{tipSifarnika}/{id}")]
        [ClaimRequirement(ClaimTypes.UserData, "sifarnik_sifarnik_pregled")]
        public IActionResult GetSifarnikById(ESifarnik tipSifarnika, int id)
        {
            var sifarnik = sifarnikService.DajSifarnik(tipSifarnika, id);
            return Ok(sifarnik);
        }
    }
}
