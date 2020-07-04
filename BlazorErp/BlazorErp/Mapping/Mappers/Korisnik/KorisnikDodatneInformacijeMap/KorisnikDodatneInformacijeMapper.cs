using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlazorErp.Entities.Models.Korisnik;
using BlazorErp.Shared.Models.Response.Korisnik.KorisnikDodatneInformacije;
using BlazorErp.Shared.Models.Response.Korisnik.KorisnikTipDodatneInformacije;

namespace BlazorErp.Mapping.Mappers.Korisnik.KorisnikDodatneInformacijeMap
{
    public static class KorisnikDodatneInformacijeMapper
    {
        public static IQueryable<KorisnikDodatneInformacijeListModelItem> ToKorisnikDodatneInformacijeListModelItem(this IQueryable<KorisnikUlogaDodatnaInformacija> query)
        {
            return query.Select(value => new KorisnikDodatneInformacijeListModelItem
            {
                KorisnikId = value.KorisnikId,
                TipDodatneInformacije = new KorisnikTipDodatneInformacijeModel
                {
                    Id = value.KorisnikTipDodatneInformacije.Id,
                    Naziv = value.KorisnikTipDodatneInformacije.Naziv,
                    Onemogucen = value.KorisnikTipDodatneInformacije.Onemogucen,
                    Opis = value.KorisnikTipDodatneInformacije.Opis,
                    Poredak = value.KorisnikTipDodatneInformacije.Poredak,
                    Sifra = value.KorisnikTipDodatneInformacije.Sifra
                },
                Vrijednost = value.Vrijednost
            });
        }
    }
}
