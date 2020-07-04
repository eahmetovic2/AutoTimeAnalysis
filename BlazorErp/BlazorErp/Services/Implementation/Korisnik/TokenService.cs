using Autofac;
using BlazorErp.Core.Constants;
using BlazorErp.Entities;
using BlazorErp.Entities.Models.Korisnik;
using BlazorErp.Shared.Models.Response.Token;
using BlazorErp.Services.Result;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using BlazorErp.Mapping.Mappers.TokenMap;
using BlazorErp.Services.Definition.Korisnik;
using BlazorErp.Shared.Models.Request.Korisnik.Token;

namespace BlazorErp.Services.Implementation
{
    /// <summary>
    /// Implementacija servisa koji radi sa osobama
    /// </summary>
    public class TokenService : Service, ITokenService
    {
        /// <summary>
        /// Entity framework db kontekst 
        /// </summary>
        private Context context;

        /// <summary>
        /// Servis za rad sa postavkama
        /// </summary>
        private IPostavkeService postavkeService;

   

        IKorisnikService korisnikService;

        IUlogaService ulogaService;

        /// <summary>
        /// Konstruktor servisa
        /// </summary>
        public TokenService(ILifetimeScope scope, Context context, IPostavkeService postavkeService, IKorisnikService korisnikService, IUlogaService ulogaService)
            : base(scope)
        {
            this.context = context;
            this.postavkeService = postavkeService;         
            this.korisnikService = korisnikService;
            this.ulogaService = ulogaService;
        }

        public ServiceResult<TokenListModel> VratiTokenePoKorisnickomImenu(String korisnickoIme, ListaTokenaRequestModel model)
        {
            var korisnikId = context.Users.Where(x => x.NormalizedUserName == korisnickoIme).FirstOrDefault()?.Id;
            // vrati tokene za datog korisnika koji nisu istekli
            var datumIsteka = DateTime.Now;
            var query = context.Tokeni
                .Where(t =>
                    t.VlasnikId == korisnikId &&
                    t.DatumIsteka > datumIsteka);

            // uradi stranicenje
            var total = query.Count();
            var tokeni = query
                .OrderByDescending(t => t.DatumPosljednjeAkcije)
                .Skip(model.Page * model.Count - model.Count)
                .Take(model.Count)
                .ToTokenListModelItem()
                .ToList();

            var result = new TokenListModel
            {
                Items = tokeni,
                Page = model.Page,
                Total = total
            };

            return Ok(result);
        }

        public ServiceResult<TokenModel> VratiTokenPoIdu(String korisnickoIme, Guid tokenId)
        {
            var korisnikId = context.Users.Where(x => x.NormalizedUserName == korisnickoIme).FirstOrDefault()?.Id;
            // dobavi token za korisnika ako postoji
            var token = context.Tokeni
                .Include(t => t.Vlasnik)
                    .ThenInclude(a => a.Roles)
                        .ThenInclude(t => t.KorisnikUlogaDodatnaInformacija)
                            .ThenInclude(t => t.KorisnikTipDodatneInformacije)
                .SingleOrDefault(t =>
                    t.VlasnikId == korisnikId &&
                    t.Id == tokenId);

            // uradi validaciu tokena
            return ValidirajToken(token, null, null);
        }

        public int BrojOnlineKorisnika()
        {
            var tokeni = context.Tokeni.Where(a => a.DatumIsteka > DateTime.Now && a.Tip == TokenTip.Sesija).GroupBy(v => v.VlasnikId).ToList();
            return tokeni.Count();
        }

        public ServiceResult<TokenModel> ValidirajToken(Guid tokenId, String ip, String klijent)
        {
            // dobavi token po id-u ako postoji
            var token = context.Tokeni
                .Include(t => t.Vlasnik)
                    .ThenInclude(a => a.Roles)
                        .ThenInclude(a => a.KorisnikUlogaDodatnaInformacija)
                            .ThenInclude(a => a.KorisnikTipDodatneInformacije)
                             .Where(x => x.Id == tokenId)
                             .FirstOrDefault();
                            //.SingleOrDefault(t => t.Id == tokenId);

            // uradi pravu validaciju tokena
            return ValidirajToken(token, ip, klijent);
        }

        private ServiceResult<TokenModel> ValidirajToken(Token token, String ip, String klijent)
        {
            // provjeri token
            if (token == null)
                return NotFound();

            var trenutnaUloga = context.Roles.Include(a => a.PravoAkcijaUloge)
                                                .ThenInclude(a => a.PravoAkcija)
                                             .Where(a => a.Id == token.UlogaId)
                                             .FirstOrDefault();

            // ucitaj samo one korisnik uloge koje trebaju
            context.Entry(trenutnaUloga).Collection(a => a.Users)
                                                 .Query()
                                                 .Where(a => a.User.Id == token.VlasnikId)
                                                 .Include(a => a.KorisnikUlogaDodatnaInformacija)
                                                 .ThenInclude(a => a.KorisnikTipDodatneInformacije)
                                                 .Load();

            //token.Vlasnik.TrenutnaUloga = trenutnaUloga;

            if (token.DatumIsteka < DateTime.Now)
                return ValidationError("Token je istekao.");

            // ako su postavljeni ip ili adresa, uradi azuriranje tokena
            if (!String.IsNullOrWhiteSpace(ip) ||
                !String.IsNullOrWhiteSpace(klijent))
            {
                // koristimo sistemske postavke za trajanje sesije
                var postavke = postavkeService.VratiPostavke();
                if (postavke.IsOk)
                {
                    token.DatumIsteka = DateTime.Now.AddDays(
                        postavke.Value.TrajanjeSesije);
                    token.DatumPosljednjeAkcije = DateTime.Now;

                    token.PosljednjiIp = ip;
                    token.PosljednjiKlijent = klijent;

                    SaveChanges(context);
                }
            }

            // uradi mapiranje
            var result = token.ToTokenModel();

            return Ok(result);
        }

		public ServiceResult<TokenModel> KreirajToken(String korisnickoIme, int ulogaId, String ip, String klijent, Core.Constants.TokenTip Tip = Core.Constants.TokenTip.Sesija)        
        {
            var vlasnik = korisnikService.VratiKorisnikaPoKorisnickomImenu(korisnickoIme);
            if (!vlasnik.IsOk)
                return MissingEntity("Vlasnik");

            if (vlasnik.Value.Onemogucen)
                return ValidationError("Vlasnik tokena je onemogucen.");

            // koristimo sistemske postavke za trajanje sesije
            var postavke = postavkeService.VratiPostavke();
            if (!postavke.IsOk)
                return MissingEntity("Postavke");

            var uloge = ulogaService.VratiSveZaKorisnickoIme(korisnickoIme);
            if (!uloge.IsOk)
            {
                return Error("Nije moguće dobaviti uloge korisnika");
            }

            //todo ovo provjeriti da li je ok
            if (Tip == TokenTip.Temp)
            {
                ulogaId = uloge.Value.Items.First().Id;
            }

            if (uloge.Value.Items.All(a => a.Id != ulogaId))
            {
                return Error("Korisnik nije u datoj ulozi.");
            }

            var datumIsteka = DateTime.Now.AddDays(postavke.Value.TrajanjeSesije);
           
            // kreiraj entitet
            var token = new Token()
            {
                Id = Guid.NewGuid(),
                VlasnikId = vlasnik.Value.Id,
                DatumKreiranja = DateTime.Now,
                DatumIsteka = datumIsteka,
                DatumPosljednjeAkcije = DateTime.Now,
                UlogaId = ulogaId,
                Tip = Tip
            };

            // spasi token
            context.Tokeni.Add(token);
            SaveChanges(context);

            // uradi validaciju i vrati rezultat
            var result = ValidirajToken(token.Id, ip, klijent);

            if (!result.IsOk)
            {
                return result;
            }

            return result;
        }

        public ServiceResult<Nothing> ObrisiToken(String korisnickoIme, Guid tokenId)
        {
            var korisnikId = context.Users.Where(x => x.NormalizedUserName == korisnickoIme).FirstOrDefault()?.Id;
            // dobavi token ako postoji
            var token = context.Tokeni
                .SingleOrDefault(t =>
                    t.VlasnikId == korisnikId &&
                    t.Id == tokenId);
            if (token == null)
                return NotFound();

            context.Tokeni.Remove(token);
            SaveChanges(context);

            // sve je ok
            return Ok();
        }
    }
}
