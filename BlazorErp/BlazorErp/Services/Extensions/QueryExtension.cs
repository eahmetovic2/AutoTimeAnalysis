using BlazorErp.Shared.Models.Request.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlazorErp.Shared.Models.Response.Sifarnik;
using BlazorErp.Entities.Models;
using BlazorErp.Entities.Models.Base;

namespace BlazorErp.Services.Extensions
{
    public static class QueryExtension
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, PagedRequestModel pagedModel)
        {
            return pagedModel.Sve ? source
                                  : source.Skip(pagedModel.Page * pagedModel.Count - pagedModel.Count)
                                          .Take(pagedModel.Count);
        }

        public static SifarnikList ToSifarnikList(this IEnumerable<SifarnikModel> source)
        {
            var lista = new SifarnikList();

            foreach (SifarnikModel item in source.ToList())
            {
                lista.Add(item);
            }

            var najnoviji = source.Max(a => a.DatumIzmjene);

            lista.DatumResponsa = najnoviji.HasValue ? najnoviji.Value : new DateTime();

            return lista;
        }
        
    }
}
