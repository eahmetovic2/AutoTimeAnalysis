using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlazorErp.Entities;
using BlazorErp.Entities.Models.Korisnik;
using BlazorErp.Mapping.Mappers.Korisnik.UlogaTipDodatneInformacijeMap;
using BlazorErp.Shared.Models.Request.Korisnik.KorisnikDodatneInformacije;
using BlazorErp.Shared.Models.Response.Korisnik.UlogaTipDodatneInformacije;
using BlazorErp.Services.Definition.Korisnik;
using BlazorErp.Services.Result;

namespace BlazorErp.Services.Implementation.Korisnik
{
    public class UlogaTipoviDodatneInformacijeService : Service, IUlogaTipoviDodatneInformacijeService
    {
        private Context context;

        public UlogaTipoviDodatneInformacijeService(ILifetimeScope scope, Context context) : base(scope)
        {
            this.context = context;
        }

        public ServiceResult<Nothing> SnimiZaUlogu(int ulogaId, SnimiDodatneInformacijeUlogeRequestModel model)
        {
            var stareDodatneInformacije = context.UlogaTipoviDodatneInformacije.Where(a => a.UlogaId == ulogaId)
                                                                .ToList();

            var zaBrisanje = stareDodatneInformacije.Where(st => model.DodatneInformacije
                                                                    .All(a => a != st.KorisnikTipDodatneInformacijeId))
                                                  .ToList();

            var nove = model.DodatneInformacije.Where(a => stareDodatneInformacije
                                                    .All(st => st.KorisnikTipDodatneInformacijeId != a))
                                   .ToList();

            context.UlogaTipoviDodatneInformacije.RemoveRange(zaBrisanje);

            context.UlogaTipoviDodatneInformacije.AddRange(
                nove.Select(n => new UlogaTipDodatneInformacije
                {
                    KorisnikTipDodatneInformacijeId = n,
                    UlogaId = ulogaId
                })
            );

            context.SaveChanges();

            return Ok();
        }

        public ServiceResult<UlogaTipDodatneInformacijeListModel> VratiZaUlogu(int ulogaId)
        {
            var tipovi = context.UlogaTipoviDodatneInformacije
                                .Where(a => a.UlogaId == ulogaId)
                                .ToUlogaTipDodatneInformacijeListModelItem()
                                .ToList();

            var result = new UlogaTipDodatneInformacijeListModel
            {
                Items = tipovi
            };

            return Ok(result);
        }
    }
}
