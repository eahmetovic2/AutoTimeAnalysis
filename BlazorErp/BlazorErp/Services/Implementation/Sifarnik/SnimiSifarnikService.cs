using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using BlazorErp.Core.Constants;
using BlazorErp.Entities;
using BlazorErp.Entities.Models.Sifarnik;
using BlazorErp.Shared.Models.Request.Sifarnik;
using BlazorErp.Services.Definition.Sifarnik;
using BlazorErp.Services.Implementation;

namespace BlazorErp.Services.Implementation.Sifarnik
{
    /// <summary>
    /// Servis koji vraca dictionary za snimanje sifarnika
    /// </summary>
    public class SnimiSifarnikService : Service, ISnimiSifarnikService
    {
        private ISnimanjeIzmjenaPomocnoService snimanjeIzmjenaPomocnoService;

        public SnimiSifarnikService(ILifetimeScope scope, ISnimanjeIzmjenaPomocnoService snimanjeIzmjenaPomocnoService) : base(scope)
        {
            this.snimanjeIzmjenaPomocnoService = snimanjeIzmjenaPomocnoService;
        }

        public Dictionary<ESifarnik, Func<Context, KreirajSifarnikRequestModel, ILifetimeScope, bool>> GetSnimiSifarnik()
        {
            return new
            Dictionary<ESifarnik, Func<Context, KreirajSifarnikRequestModel, ILifetimeScope, bool>>() {
                //{
                //    ESifarnik.Pol,
                //    new Func<Context, KreirajSifarnikRequestModel, ILifetimeScope, bool>
                //    (
                //        (context, model, scope) => {
                //            var entity = new Pol ();
                //            return snimanjeIzmjenaPomocnoService.SaveEntity<Pol> (context, model, entity, context.Polovi, scope);
                //        }
                //    )
                //}
            };
        }
    }
}