using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlazorErp.Entities.Models.Korisnik;
using BlazorErp.Shared.Models.Response.Korisnik.PravoUpravljanjaKorisnikom;

namespace BlazorErp.Mapping.Mappers.Korisnik.PravoUpravljanjaKorisnikomMap
{
    public static class PravoUpravljanjaKorisnikomMapper
    {
        public static IQueryable<PravoUpravljanjaKorisnikomListModelItem> ToPravoUpravljanjaKorisnikomListModelItem(this IQueryable<PravoUpravljanjaKorisnikom> query)
        {
            return query.Select(value => new PravoUpravljanjaKorisnikomListModelItem
            {
                Id = value.UlogaUpravljanogId,
                Uloga = value.UlogaUpravljanog.Name,
                PotrebneDodatneInformacije = value.UlogaUpravljanog.TipoviDodatneInformacije.Select(a => a.KorisnikTipDodatneInformacije.Sifra).ToList()
            });
        }
    }
}
