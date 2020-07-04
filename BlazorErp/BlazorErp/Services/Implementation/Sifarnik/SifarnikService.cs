using Autofac;
using BlazorErp.Core.Constants;
using BlazorErp.Entities;
using BlazorErp.Entities.Models.Sifarnik;
using BlazorErp.Shared.Models.Request.Sifarnik;
using BlazorErp.Shared.Models.Response.Sifarnik;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BlazorErp.Services.Extensions;
using BlazorErp.Services.Definition.Sifarnik;

namespace BlazorErp.Services.Implementation
{
    public class SifarnikService : Service, ISifarnikService
    {
        public class InputParameters
        {
            public IGetSifarniciService GetSifarniciService;
            public Context Context { get; set; }
            public ILifetimeScope Scope { get; set; }
            public bool SamoDatum { get; set; }
            public IAuthService AuthService { get; set; }
            public DateTime? DatumIzmjene { get; internal set; }
            public IGetPoljaSifarnikaService GetPoljaSifarnikaService { get; set; }
            public ISnimiSifarnikService SnimiSifarnikService { get; set; }
            public IUpdateSifarnikService UpdateSifarnikService { get; set; }
        }

        private Context context;
        private readonly InputParameters inputParameters;

        public SifarnikService(ILifetimeScope scope, Context context, IAuthService authService, IGetSifarniciService getSifarniciService, IGetPoljaSifarnikaService getPoljaSifarnikaService, IUpdateSifarnikService updateSifarnikService, ISnimiSifarnikService snimiSifarnikService) : base(scope)
        {
            this.context = context;
            inputParameters = new InputParameters()
            {
                Context = context,
                Scope = scope,
                SamoDatum = false,
                AuthService = authService,
                GetSifarniciService = getSifarniciService,
                GetPoljaSifarnikaService = getPoljaSifarnikaService,
                UpdateSifarnikService = updateSifarnikService,
                SnimiSifarnikService = snimiSifarnikService
            };
        }

        public SifarnikList VratiSve(ESifarnik sifarnik, bool samoDatum, DateTime? datumIzmjene)
        {
            var inputs = this.inputParameters;
            inputs.SamoDatum = samoDatum;
            inputs.DatumIzmjene = datumIzmjene;
            var sifarnici = inputParameters.GetSifarniciService.GetSifarnici();
            return sifarnici[sifarnik].Invoke(this.inputParameters).OrderByDescending(x => x.Prvi).ThenBy(x => x.Poredak).ThenBy(x => x.Naziv).AsQueryable().ToSifarnikList();
        }

        public SifarnikListModel VratiSveSaPoljima(ESifarnik sifarnik, ListaSifarnikRequestModel model)
        {
            var polja = inputParameters.GetPoljaSifarnikaService.GetPoljaSifarnika();

            var result = polja[sifarnik].Invoke(inputParameters);

            if (!string.IsNullOrEmpty(model.Filter))
            {
                result.Items = result.Items.Where(a => a.Naziv.ToLower().Contains(model.Filter.ToLower()));
            }

            var total = result.Items.Count();

            result.Items = result.Items.Skip(model.Page * model.Count - model.Count)
                .Take(model.Count).ToList();

            return new SifarnikListModel(result.Items, result.FieldsList, total);
        }

        public List<PoljeSifarnika> VratiPolja(ESifarnik sifarnik)
        {
            var polja = inputParameters.GetPoljaSifarnikaService.GetPoljaSifarnika();
            return polja[sifarnik].Invoke(inputParameters).FieldsList;
        }

        public bool SnimiSifarnik(ESifarnik sifarnik, KreirajSifarnikRequestModel model)
        {
            var snimanje = inputParameters.SnimiSifarnikService.GetSnimiSifarnik();
            return snimanje[sifarnik].Invoke(context, model, Scope);
        }

        public bool UpdateSifarnik(ESifarnik tipSifarnika, UpdateSifarnikRequestModel model)
        {
            model.Scope = this.inputParameters.Scope;
            var update = inputParameters.UpdateSifarnikService.GetUpdateSifarnici();
            return update[tipSifarnika].Invoke(context, model, Scope);
        }

        public SifarnikModel DajSifarnik(ESifarnik tipSifarnika, int id)
        {
            var polja = inputParameters.GetPoljaSifarnikaService.GetPoljaSifarnika();
            var items = polja[tipSifarnika].Invoke(inputParameters).Items.ToList();
            return items.First(a => a.Id == id);
        }
    }
}
