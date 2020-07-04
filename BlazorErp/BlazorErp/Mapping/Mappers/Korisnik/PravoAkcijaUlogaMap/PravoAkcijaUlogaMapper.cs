using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlazorErp.Entities.Models.Korisnik;
using BlazorErp.Shared.Models.Response.Korisnik.PravoAkcijaUloga;

namespace BlazorErp.Mapping.Mappers.Korisnik.PravoAkcijaUlogaMap
{
    public static class PravoAkcijaUlogaMapper
    {
        public static IQueryable<PravoAkcijaUlogaListModelItem> ToPravoAkcijaUlogaListModelItem(this IQueryable<PravoAkcijaUloga> query)
        {
            return query.Select(value => new PravoAkcijaUlogaListModelItem
            {
                PravoAkcijaId = value.PravoAkcijaId,
                UlogaId = value.UlogaId
            });
        }
    }
}
