using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlazorErp.Entities;
using BlazorErp.Entities.Models.Korisnik;
using BlazorErp.Mapping.Mappers.Korisnik.PravoUpravljanjaKorisnikomMap;
using BlazorErp.Shared.Models.Response.Korisnik.PravoUpravljanjaKorisnikom;
using BlazorErp.Services.Definition.Korisnik;
using BlazorErp.Services.Result;

namespace BlazorErp.Services.Implementation.Korisnik
{
    public class PravoUpravljanjaKorisnikomService : Service, IPravoUpravljanjaKorisnikomService
    {
        Context context;

        public PravoUpravljanjaKorisnikomService(ILifetimeScope scope, Context context) : base(scope)
        {
            this.context = context;
        }

        public ServiceResult<PravoUpravljanjaKorisnikomListModel> VratiSve(int ulogaId)
        {
            var dozvoljeneUloge = VratiPravaUpravljanjaKorisnikom(ulogaId)
                                    .ToPravoUpravljanjaKorisnikomListModelItem()
                                    .ToList();

            var result = new PravoUpravljanjaKorisnikomListModel
            {
                Items = dozvoljeneUloge
            };

            return Ok(result);
        }

        public IQueryable<PravoUpravljanjaKorisnikom> VratiPravaUpravljanjaKorisnikom(int ulogaId)
        {
            return context.PravaUpravljanjaKorisnicima
                            .Where(a => a.UlogaUpraviteljaId == ulogaId);
        }
    }
}
