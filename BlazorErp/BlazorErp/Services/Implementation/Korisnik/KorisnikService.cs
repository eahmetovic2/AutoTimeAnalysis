using Autofac;
using BlazorErp.Entities;
using BlazorErp.Entities.Models.Korisnik;
using BlazorErp.Shared.Models.Request.Korisnik;
using BlazorErp.Shared.Models.Response.Korisnik;
using BlazorErp.Shared.Models.Response.Token;
using BlazorErp.Services.Result;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using BlazorErp.Core.Database;
using BlazorErp.Mapping.Mappers.KorisnikMap;
using BlazorErp.Shared.Models.Response.Korisnik.Korisnik;
using BlazorErp.Services.Definition.Korisnik;
using BlazorErp.Core.Constants;
using BlazorErp.Shared.Models.Request.Korisnik.Korisnik;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BlazorErp.Services.Implementation
{
    /// <summary>
    /// Implementacija servisa koji radi sa korisnicima
    /// </summary>
    public class KorisnikService : Service, IKorisnikService
	{
		/// <summary>
		/// Entity framework db kontekst 
		/// </summary>
		private Context context;

		/// <summary>
		/// 
		/// </summary>
		private IAuthService authService;

		/// <summary>
		/// 
		/// </summary>


        private IPravoUpravljanjaKorisnikomService pravoUpravljanjaKorisnikomService;


        /// <summary>
        /// Konstruktor servisa
        /// </summary>
        public KorisnikService(ILifetimeScope scope, Context context, IAuthService authService, IPravoUpravljanjaKorisnikomService pravoUpravljanjaKorisnikomService)
			: base(scope)
		{
			this.context = context;
			this.authService = authService;		
            this.pravoUpravljanjaKorisnikomService = pravoUpravljanjaKorisnikomService;

        }

		public ServiceResult<KorisnikModel> VratiKorisnikaPoKorisnickomImenu(String korisnickoIme)
        {
            var securityLevel = new SecurityLevel();

            // dobavi razred ako postoji
            var korisnik = Secure(context.Users.AsQueryable(), securityLevel)
                            .ToKorisnikModel()
                            .SingleOrDefault(k => k.KorisnickoIme == korisnickoIme);
			// dobavi korisnika ako postoji
			if (korisnik == null)
				return NotFound();

			return Ok(korisnik);
		}

		public ServiceResult<KorisnikModel> AzurirajKorisnika(String korisnickoIme, AzurirajKorisnikaRequestModel model)
        {
            var securityLevel = new SecurityLevel();
            // dobavi razred ako postoji
            var korisnik = Secure(context.Users.AsQueryable(), securityLevel)
                            .SingleOrDefault(k => k.NormalizedUserName == korisnickoIme);

			if (korisnik == null)
				return NotFound();

			var trenutni = authService.TrenutniKorisnik();

            // Provjeriti pravo dodavanja uloge
            var dozvoljeneUloge = pravoUpravljanjaKorisnikomService.VratiPravaUpravljanjaKorisnikom(trenutni.TrenutnaUlogaId);
            if (model.Uloge.Any(a => dozvoljeneUloge.All(doz => doz.UlogaUpravljanogId != a.VrstaUlogeId)))
            {
                return Error("Nemate prava izmjene korisnika sa tim ulogama");
            }

            var stareUloge = context.UserRoles
                            .Include(x => x.KorisnikUlogaDodatnaInformacija)
                                    .Where(a => a.UserId == korisnik.Id).ToList();

            // Koristi se transakcija zato sto se brisu uloge pa se onda ponovo dodaju
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Obirisi uloge i dodatne informacije
                    foreach (var uloga in stareUloge)
                    {
                        context.KorisnikUlogaDodatneInformacije.RemoveRange(uloga.KorisnikUlogaDodatnaInformacija);
                    }
                    context.UserRoles.RemoveRange(stareUloge);

                    context.SaveChanges();

                    foreach (var novaUloga in model.Uloge)
                    {
                        var nova = new KorisnikUloga
                        {
                            RoleId = novaUloga.VrstaUlogeId,
                            UserId = korisnik.Id,
                            KorisnikUlogaDodatnaInformacija = new List<KorisnikUlogaDodatnaInformacija>()
                        };

                        context.UserRoles.Add(nova);
                    }

                    // postavi vrijednosti
                    korisnik.Email = model.Email;
                    korisnik.PunoIme = model.PunoIme;

                    SaveChanges(context);

                    if (!string.IsNullOrEmpty(model.Lozinka) && model.Lozinka.Length >= 6)
                        PostaviLozinku(korisnickoIme, model.Lozinka);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    return Error("Greška", false);
                }
            }

            // vrati azuriranog korisnika
            return VratiKorisnikaPoKorisnickomImenu(korisnickoIme);
            
		}

		public ServiceResult<Nothing> PostaviKorisnikOnemogucen(String korisnickoIme, bool onemogucen)
		{
			// dobavi korisnika ako postoji
			var korisnik = FilterPoUlogama()
				.SingleOrDefault(k => k.NormalizedUserName == korisnickoIme);
			if (korisnik == null)
				return NotFound();

			// postavi vrijednost onemogucen
			//korisnik.Onemogucen = onemogucen;
			SaveChanges(context);

			// sve je ok
			return Ok();
		}

        List<IdentityKorisnik> FilterPoUlogama()
        {
            var korisnici = new List<IdentityKorisnik>();

            return korisnici.ToList();
        }

        public async Task<ServiceResult<Nothing>> PromijeniLozinku(String korisnickoIme, String staraLozinka, String novaLozinka)
		{
			// provjeri da li je lozinka ok
			var lozinkaOk = ProvjeriLozinku(korisnickoIme, staraLozinka);
			if (!lozinkaOk.IsOk)
				return lozinkaOk;

          

			// uradi promjenu lozinke
			return await PostaviLozinku(korisnickoIme, novaLozinka);
		}

		public async Task<ServiceResult<Nothing>> PostaviLozinku(String korisnickoIme, String novaLozinka)
		{
			// dobavi korisnika ako postoji
			var korisnik = context.Users
				.SingleOrDefault(k => k.NormalizedUserName == korisnickoIme);
			if (korisnik == null)
				return NotFound();

            var userStore = new UserStore<IdentityKorisnik, Uloga, Context, int, IdentityUserClaim<int>, KorisnikUloga, IdentityUserLogin<int>, IdentityUserToken<int>, IdentityRoleClaim<int>>(context);

            var passwordHasher = new PasswordHasher<IdentityKorisnik>();
            var hashed = passwordHasher.HashPassword(korisnik, novaLozinka);

            await userStore.SetPasswordHashAsync(korisnik, hashed);

            SaveChanges(context);

			// sve je ok
			return Ok();
		}

		public ServiceResult<Nothing> ProvjeriLozinku(String korisnickoIme, String lozinka)
		{
			// dobavi korisnika ako postoji
			var korisnik = context.Users
				.SingleOrDefault(k => k.NormalizedUserName == korisnickoIme);
			if (korisnik == null)
				return NotFound();


            var passwordHasher = new PasswordHasher<IdentityKorisnik>();

			// uradi validaciju lozinke
			var valid = passwordHasher.VerifyHashedPassword(korisnik, korisnik.PasswordHash, lozinka);
			if (valid == PasswordVerificationResult.Failed)
				return ValidationError("Pogresna lozinka");

			// sve je ok
			return Ok();
		}

		public ServiceResult<KorisnikListModel> VratiSve(ListaKorisnikaRequestModel model)
        {
            var securityLevel = new SecurityLevel();

            var query = context.Users
                               .Include(a => a.Roles)
                                    .ThenInclude(a => a.Role)
                               .AsQueryable();

            query = Secure(query, securityLevel);

            // uradi filtriranje po nazivu
            if (!String.IsNullOrWhiteSpace(model.Filter))
			{
				var lowerFilter = model.Filter.ToLower();
				query = query.Where(s => s.PunoIme.ToLower().Contains(lowerFilter));
            }
            // uradi filtriranje po korisnickom imenu
            if (!String.IsNullOrWhiteSpace(model.Username))
            {
                var lowerFilter = model.Username.ToLower();
                query = query.Where(s => s.NormalizedUserName.ToLower().Contains(lowerFilter));
            }
            // uradi filtriranje po ulozi
            if (model.UlogaId.HasValue)
            {
                query = query.Where(s => s.Roles.Select(a => a.RoleId).Where(x => x.Equals(model.UlogaId)).Count() > 0);
            }

            // uradi stranicenje
            var total = query.Count();
			var korisnici = query
				.OrderBy(s => s.PunoIme)
				.Skip(model.Page * model.Count - model.Count)
				.Take(model.Count)
                .ToKorisnikListModelItem()
                .ToList();

            var result = new KorisnikListModel
            {
                Items = korisnici,
                Page = model.Page,
                Total = total
            };

            return Ok(result);
        }


        List<IdentityKorisnik> FilterPoUlogama(IQueryable<IdentityKorisnik> korisnici)
		{
			var korisnik = authService.TrenutniKorisnik();

			if (korisnik == null)
				return korisnici.ToList();

		

			return korisnici.ToList();
		}

		public async Task<ServiceResult<KorisnikModel>> Kreiraj(KreirajKorisnikaRequestModel model)
		{
			model.KorisnickoIme = model.KorisnickoIme.Trim().ToLower();

			//Provjeri da li je korisničko ime zauzeto
			if (context.Users.FirstOrDefault(x => x.NormalizedUserName == model.KorisnickoIme) != null)
				return Error("Korisničko ime zauzeto.");

			var trenutni = authService.TrenutniKorisnik();

            // Provjeriti pravo dodavanja uloge
            var dozvoljeneUloge = pravoUpravljanjaKorisnikomService.VratiPravaUpravljanjaKorisnikom(trenutni.TrenutnaUlogaId);
            if (model.Uloge.Any(a => dozvoljeneUloge.All(doz => doz.UlogaUpravljanogId != a.VrstaUlogeId)))
            {
                return Error("Nemate prava da dodate korisnika sa tim ulogama");
            }

            var korisnik = new IdentityKorisnik
            {
                UserName = model.KorisnickoIme,
                NormalizedUserName = model.KorisnickoIme,
                Email = model.Email,
                EmailConfirmed = false,
                NormalizedEmail = model.Email,                
                PunoIme = model.PunoIme,
                Roles = new List<KorisnikUloga>()
            };

            var passwordHasher = new PasswordHasher<IdentityKorisnik>();
            var hashed = passwordHasher.HashPassword(korisnik, model.Lozinka);
            korisnik.PasswordHash = hashed;
            var userStore = new UserStore<IdentityKorisnik, Uloga, Context, int, IdentityUserClaim<int>, KorisnikUloga, IdentityUserLogin<int>, IdentityUserToken<int>, IdentityRoleClaim<int>>(context);
            await userStore.CreateAsync(korisnik);

            foreach (var uloga in model.Uloge)
            {

                var ulogaNaziv = context.Roles.FirstOrDefault(x => x.Id == uloga.VrstaUlogeId)?.NormalizedName;

                await userStore.AddToRoleAsync(korisnik, ulogaNaziv);
            }

            context.Add(korisnik);
			SaveChanges(context);

			return VratiKorisnikaPoKorisnickomImenu(korisnik.NormalizedUserName);
		}

		
        public ServiceResult<KorisnikModel> AzurirajLicneDetalje(string korisnickoIme, AzurirajLicneDetaljeRequestModel model)
        {
            var securityLevel = new SecurityLevel();

            // dobavi razred ako postoji
            var korisnik = Secure(context.Users.AsQueryable(), securityLevel)
                            .SingleOrDefault(k => k.NormalizedUserName == korisnickoIme);
            if (korisnik == null)
                return NotFound();

            if (!String.IsNullOrEmpty(model.StaraLozinka))
            {
                if (!ImaPravo("korisnik_korisnik_promjena_lozinke"))
                {
                    return Error("Korisniku je zabranjena promjena lozinke");
                }

                var passwordHasher = new PasswordHasher<IdentityKorisnik>();

                // uradi validaciju lozinke
                var valid = passwordHasher.VerifyHashedPassword(korisnik, korisnik.PasswordHash, model.StaraLozinka);
                if (valid == PasswordVerificationResult.Failed)
                {
                    return Error("Neuspješna promjena lozinke");
                }

                var novaTajna = passwordHasher.HashPassword(korisnik, model.NovaLozinka);
                korisnik.PasswordHash = novaTajna;

                if (model.Odjavi)
                {
                    // Onemoguci sve tokene
                    var tokeni = context.Tokeni.Where(a => a.VlasnikId == korisnik.Id
                                                            && a.DatumIsteka > DateTime.Now).ToList();
                    foreach (var token in tokeni)
                    {
                        token.DatumIsteka = DateTime.Now.AddMinutes(-1);
                    }
                }
            }


            // postavi vrijednosti
            korisnik.Email = model.Email;
            korisnik.NormalizedEmail = model.Email;
            korisnik.PunoIme = model.PunoIme;
            korisnik.PhoneNumber = model.Telefon;

            SaveChanges(context);

            // vrati azuriranog korisnika
            return VratiKorisnikaPoKorisnickomImenu(korisnickoIme);
        }

    }
}