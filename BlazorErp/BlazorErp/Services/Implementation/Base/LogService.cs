using System;
using System.Collections.Generic;
using System.Linq;
using BlazorErp.Core.Constants;
using BlazorErp.Core.Extensions;
using BlazorErp.Entities;
using Autofac;
using BlazorErp.Shared.Models.Request.LogAkcija;
using BlazorErp.Shared.Models.Response.LogAkcija;
using BlazorErp.Services.Result;
using BlazorErp.Shared.Models.Response.Sifarnik;
using BlazorErp.Shared.Models.Response.LogEntitet;
using BlazorErp.Mapping.Mappers.Base.LogAkcijaMap;
using BlazorErp.Mapping.Mappers.LogEntitet;
using Microsoft.EntityFrameworkCore;

namespace BlazorErp.Services.Implementation
{
    public class LogService : Service, ILogService
    {
        /// <summary>
        /// Entity framework db kontekst 
        /// </summary>
        private Context context;
        private IAuthService authService;

        /// <summary>
        /// Konstruktor servisa
        /// </summary>
        public LogService(ILifetimeScope scope, Context context, IAuthService authService)
            : base(scope)
        {
            this.context = context;
            this.authService = authService;
        }      

        public void GreskaAkcija(LogKategorija kategorija, LogAkcija akcija, string opis)
        {
            Akcija(LogLevel.Greska, kategorija, akcija, opis);
        }

        public void InfoAkcija(LogKategorija kategorija, LogAkcija akcija, string opis)
        {
            Akcija(LogLevel.Info, kategorija, akcija, opis);
        }

        public void UpozorenjeAkcija(LogKategorija kategorija, LogAkcija akcija, string opis)
        {
            Akcija(LogLevel.Upozorenje, kategorija, akcija, opis);
        }

        public void Akcija(LogLevel level, LogKategorija kategorija, LogAkcija akcija, string opis)
        {
            context.LogAkcije.Add(new Entities.Models.Base.LogAkcija()
            {
                Level = level,
                Kategorija = kategorija,
                Akcija = akcija,
                Opis = opis,
                KorisnikId = authService.TrenutniKorisnik().Id,
                DatumAkcije = DateTime.Now
            });

            SaveChanges(context);
        }

        public void Akcija(LogLevel level, LogKategorija kategorija, LogAkcija akcija, string opis, string korisnickoIme)
        {
            var userId = context.Users.Where(x => x.NormalizedUserName == korisnickoIme).FirstOrDefault().Id;
            context.LogAkcije.Add(new Entities.Models.Base.LogAkcija()
            {
                Level = level,
                Kategorija = kategorija,
                Akcija = akcija,
                Opis = opis,
                KorisnikId = userId,
                DatumAkcije = DateTime.Now
            });

            SaveChanges(context);
        }

        public ServiceResult<LogAkcijaListModel> VratiSveAkcije(ListLogAkcijaRequestModel model)
        {
            var query = context.LogAkcije.Include(x => x.Korisnik).AsQueryable();

            model.Od = model.Od.BeginingOfDay();
            model.Do = model.Do.EndOfDay();

            if (!String.IsNullOrWhiteSpace(model.KorisnickoIme))
            {
                query = query.Where(s => s.Korisnik.NormalizedUserName.Contains(model.KorisnickoIme));
            }

            LogLevel level = (LogLevel)model.Level;
            if (model.Level != 0)
            {
                query = query.Where(s => s.Level == level);
            }

            LogAkcija akcija = (LogAkcija)model.Akcija;
            if (model.Akcija != 0)
            {
                query = query.Where(s => s.Akcija == akcija);
            }

            LogKategorija kategorija = (LogKategorija)model.Kategorija;
            if (model.Kategorija != 0)
            {
                query = query.Where(s => s.Kategorija == kategorija);
            }

            if (model.Od.HasValue)
            {
                query = query.Where(s => s.DatumAkcije >= model.Od);
            }

            if (model.Do.HasValue)
            {
                query = query.Where(s => s.DatumAkcije <= model.Do);
            }

            // uradi stranicenje
            var total = query.Count();
            var logovi = query
                .OrderByDescending(s => s.DatumAkcije)
                .Skip(model.Page * model.Count - model.Count)
                .Take(model.Count)
                .ToLogAkcijaListModelItem()
                .ToList();

            var result = new LogAkcijaListModel
            {
                Items = logovi,
                Page = model.Page,
                Total = total
            };

            return Ok(result);
        }

        public ServiceResult<LogEntitetModel> VratiJedanEntitet(int id)
        {
            var model = context.LogEntiteti
                                .Include(x => x.Korisnik)
                                .Where(a => a.Id == id)
                                .ToLogEntitetModel()
                                .FirstOrDefault();

            return Ok(model);
        }

        public List<SifarnikModel> VratiLogAkcije()
        {
            return context.VrsteLogAkcija.Where(a => !a.IsDeleted && a.VrijednostUAplikaciji.HasValue)
                .OrderBy(a => a.Naziv)
                .Select((x => new SifarnikModel() { Id = x.VrijednostUAplikaciji.Value, Naziv = x.Naziv })).ToList();
        }

        public List<SifarnikModel> VratiLogLevele()
        {
            return context.LogLeveli.Where(a => !a.IsDeleted && a.VrijednostUAplikaciji.HasValue)
                .OrderBy(a => a.Poredak)
                .Select((x => new SifarnikModel() { Id = x.VrijednostUAplikaciji.Value, Naziv = x.Naziv })).ToList();
        }

        public List<SifarnikModel> VratiLogKategorije()
        {
            return context.LogKategorije.Where(a => !a.IsDeleted && a.VrijednostUAplikaciji.HasValue)
                .OrderBy(a => a.Naziv)
                .Select((x => new SifarnikModel() { Id = x.VrijednostUAplikaciji.Value, Naziv = x.Naziv })).ToList();
        }
    }
}
