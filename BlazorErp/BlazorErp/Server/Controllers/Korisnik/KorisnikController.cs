using BlazorErp.Server.Common.Attributes;
using BlazorErp.Shared.Models.Request.Korisnik;
using BlazorErp.Services;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
using System;
using BlazorErp.Shared.Models.Request.Korisnik.Korisnik;
using BlazorErp.Server.Auth.Requirements;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BlazorErp.Shared.Prava;
using BlazorErp.Shared.Models.Response.Korisnik.Korisnik;
using BlazorErp.Services.Definition.Korisnik;

namespace BlazorErp.Server.Controllers.Korisnik
{
    /// <summary>
    /// Kontroler za upravljanje korisnicima
    /// </summary>
    [Route("korisnici")]
	public class KorisnikController : BaseController
	{
		/// <summary>
		/// Servis za upravljanje korisnicima
		/// </summary>
		private IKorisnikService korisnikService;
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
		public KorisnikController(IKorisnikService korisnikService, ILogService logService, IAuthService authService)
		{
			this.korisnikService = korisnikService;
			this.logService = logService;
			this.authService = authService;
		}

		[HttpGet("")]
		//[ClaimRequirement(ClaimTypes.UserData, "korisnik_korisnik_lista")]
		[ClaimRequirement("PravoAkcija", (int)PravoAkcije.KorisnikLista)]
		public KorisnikListModel VratiSve([FromQuery]ListaKorisnikaRequestModel model)
		{
			var result = korisnikService.VratiSve(model);
			return result;
			//return Convert(result);
		}

		[HttpGet("{korisnickoIme}")]
        //[ClaimRequirement(ClaimTypes.UserData, "korisnik_korisnik_pregled")]
		[ClaimRequirement("PravoAkcija", (int)PravoAkcije.KorisnikPregled)]	
		//[Authorize("UserIsAdminOrOwner")]
		public IActionResult VratiJedan(String korisnickoIme)
		{
			try
			{
				var result = korisnikService.VratiKorisnikaPoKorisnickomImenu(korisnickoIme);
				return Convert(result);
			}
			catch (Exception)
			{
				throw;
			}
        }

        [HttpPut("{korisnickoIme}")]
        //[ClaimRequirement(ClaimTypes.UserData, "korisnik_korisnik_izmjena")]
        //[Authorize("UserIsAdminOrOwner")]
        [RequireModel]
		public IActionResult Azuriraj(String korisnickoIme, [FromBody] AzurirajKorisnikaRequestModel model)
		{
			var result = korisnikService.AzurirajKorisnika(korisnickoIme, model);
			if (result.IsOk)
			{
				logService.Akcija(Core.Constants.LogLevel.Info,
						Core.Constants.LogKategorija.korisnik,
						Core.Constants.LogAkcija.korisnik_izmijeni,
						"KorisnickoIme: " + result.Value.KorisnickoIme,
						authService.TrenutniKorisnik().NormalizedUserName
						);
			}
			return Convert(result);
		}

        [HttpPut("{korisnickoIme}/licni-detalji")]
        //[ClaimRequirement(ClaimTypes.UserData, "korisnik_korisnik_izmjena_licnih_podataka")]
        [RequireModel]
        public IActionResult AzurirajLicneDetalje(String korisnickoIme, [FromBody] AzurirajLicneDetaljeRequestModel model)
        {
            var result = korisnikService.AzurirajLicneDetalje(korisnickoIme, model);
            if (result.IsOk)
            {
                logService.Akcija(Core.Constants.LogLevel.Info,
                        Core.Constants.LogKategorija.korisnik,
                        Core.Constants.LogAkcija.korisnik_izmijeni,
                        "KorisnickoIme: " + result.Value.KorisnickoIme,
                        authService.TrenutniKorisnik().NormalizedUserName
						);
            }
            return Convert(result);
        }

        [HttpPut("{korisnickoIme}/lozinka")]
		[RequireModel]
        //[ClaimRequirement(ClaimTypes.UserData, "korisnik_korisnik_promjena_lozinke")]
        public async Task<IActionResult> PromijeniLozinku(String korisnickoIme, [FromBody] PromijeniLozinkuRequestModel model)
		{
			var result = await korisnikService.PromijeniLozinku(korisnickoIme, model.Lozinka, model.NovaLozinka);
			return Convert(result);
		}

		[HttpPost("{korisnickoIme}/lozinka")]
		[RequireModel]
        //[ClaimRequirement(ClaimTypes.UserData, "korisnik_korisnik_promjena_lozinke")]
        public async Task<IActionResult> PostaviLozinku(String korisnickoIme, [FromBody] PostaviLozinkuRequestModel model)
		{
			var result = await korisnikService.PostaviLozinku(korisnickoIme, model.NovaLozinka);
			return Convert(result);
		}

		[HttpPut("{korisnickoIme}/onemogucen")]
		[RequireModel]
        //[ClaimRequirement(ClaimTypes.UserData, "korisnik_korisnik_aktivacija")]
        public IActionResult PostaviKorisnikOnemogucen(String korisnickoIme, [FromBody] PostaviKorisnikOnemogucenRequestModel model)
		{
			var result = korisnikService.PostaviKorisnikOnemogucen(korisnickoIme, model.Onemogucen);
			return Convert(result);
		}

		[HttpPost("")]
		[RequireModel]
        //[ClaimRequirement(ClaimTypes.UserData, "korisnik_korisnik_dodavanje")]
        public async Task<IActionResult> Kreiraj([FromBody] KreirajKorisnikaRequestModel model)
		{
			var result = await korisnikService.Kreiraj(model);
			if (result.IsOk)
			{
				logService.Akcija(Core.Constants.LogLevel.Info,
						 Core.Constants.LogKategorija.korisnik,
						 Core.Constants.LogAkcija.korisnik_dodaj,
						 "KorisnickoIme: " + result.Value.KorisnickoIme,
						authService.TrenutniKorisnik().NormalizedUserName
						 );

			}
			return Convert(result);
		}

	

    }
}
