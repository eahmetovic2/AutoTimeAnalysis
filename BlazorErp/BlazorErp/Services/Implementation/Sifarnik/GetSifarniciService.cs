using Autofac;
using BlazorErp.Core.Constants;
using BlazorErp.Core.Database;
using BlazorErp.Core.Extensions;
using BlazorErp.Services.Definition.Sifarnik;
using BlazorErp.Services.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static BlazorErp.Services.Implementation.SifarnikService;
using BlazorErp.Shared.Models.Response.Sifarnik;

namespace BlazorErp.Services.Implementation.Sifarnik
{
    /// <summary>
    /// Servis koji vraca sifarnike za dropdown liste
    /// </summary>
    public class GetSifarniciService : Service, IGetSifarniciService
    {
        public GetSifarniciService(ILifetimeScope scope) : base(scope) { }

        public static string GetMemberName<T, TValue>(Expression<Func<T, TValue>> memberAccess)
        {
            return ((MemberExpression)memberAccess.Body).Member.Name;
        }

        public Dictionary<ESifarnik, Func<InputParameters, SifarnikList>> GetSifarnici()
        {
            return new
            Dictionary<ESifarnik, Func<InputParameters, SifarnikList>>() { 
                {
                    ESifarnik.KorisnikTipDodatneInformacije,
                    new Func<InputParameters, SifarnikList>
                    (
                        (inputParameters) => {
                            if (inputParameters.SamoDatum) {
                                var max = inputParameters.Context.KorisnikTipoviDodatneInformacije.Max (a => a.DatumIzmjene);
                                return new SifarnikList () {
                                    DatumResponsa = max.HasValue ? max.Value : new DateTime ()
                                };
                            }

                            var list = inputParameters.Context.KorisnikTipoviDodatneInformacije.AsQueryable();

                            if (inputParameters.DatumIzmjene.HasValue) {
                                list = list.Where (a => a.DatumIzmjene.HasValue && a.DatumIzmjene >= inputParameters.DatumIzmjene);
                            }

                            return list.OrderBy (a => a.Poredak).Select (a => new SifarnikKorisnikDodatnoModel () {
                                Id = a.Id,
                                    Naziv = a.Naziv,
                                    Opis = a.Opis,
                                    Poredak = a.Poredak

                            }).ToSifarnikList ();
                        }
                    )
                },
            };
        }
    }
}