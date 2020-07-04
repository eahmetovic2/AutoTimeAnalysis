using Autofac;
using BlazorErp.Entities.Models;
using BlazorErp.Entities.Models.Korisnik;
using BlazorErp.Shared.Models.Response.Korisnik;
using BlazorErp.Shared.Models.Response.Token;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BlazorErp.Shared.Models.Response.Korisnik.Korisnik;
using BlazorErp.Shared.Models.Response.Korisnik.PravoAkcija;
using BlazorErp.Shared.Models.Response.Korisnik.KorisnikDodatneInformacije;
using BlazorErp.Shared.Models.Response.Korisnik.KorisnikTipDodatneInformacije;

namespace BlazorErp.Mapping.Mappers.TokenMap
{
    public static class TokenMapper
    {
        public static IQueryable<TokenListModelItem> ToTokenListModelItem(this IQueryable<Token> query)
        {
            return query.Select(token => new TokenListModelItem()
            {
                Id = token.Id,
                DatumKreiranja = token.DatumKreiranja,
                DatumIsteka = token.DatumIsteka,
                PosljednjiIp = token.PosljednjiIp,
                PosljednjiKlijent = token.PosljednjiKlijent,
                DatumPosljednjeAkcije = token.DatumPosljednjeAkcije
            });
        }

        public static IQueryable<TokenModel> ToTokenModel(this IQueryable<Token> query)
        {
            return query.Select(token => new TokenModel()
            {
                Id = token.Id,
                DatumKreiranja = token.DatumKreiranja,
                DatumIsteka = token.DatumIsteka,
                PosljednjiIp = token.PosljednjiIp,
                PosljedniKlijent = token.PosljednjiKlijent,
                DatumZadnjeAkcije = token.DatumPosljednjeAkcije,
                Tip = token.Tip,
                Vlasnik = token.Vlasnik != null ? new KorisnikTokenModel()
                {
                    KorisnickoIme = token.Vlasnik.NormalizedUserName,
                    Email = token.Vlasnik.Email,
                    PunoIme = token.Vlasnik.PunoIme,
                    //DatumKreiranja = token.Vlasnik.DatumKreiranja,
                } : null
            });
        }

        public static TokenModel ToTokenModel(this Token token)
        {
            return new TokenModel
            {
                Id = token.Id,
                DatumKreiranja = token.DatumKreiranja,
                DatumIsteka = token.DatumIsteka,
                PosljednjiIp = token.PosljednjiIp,
                PosljedniKlijent = token.PosljednjiKlijent,
                DatumZadnjeAkcije = token.DatumPosljednjeAkcije,
                Tip = token.Tip,
                Uloga = token.Uloga == null ? "" : token.Uloga.Name,
                UlogaId = token.UlogaId,
                Vlasnik = token.Vlasnik != null ? new KorisnikTokenModel()
                {
                    KorisnickoIme = token.Vlasnik.NormalizedUserName,
                    Email = token.Vlasnik.Email,
                    PunoIme = token.Vlasnik.PunoIme,
                    //DatumKreiranja = token.Vlasnik.DatumKreiranja,
                    TrenutnaUlogaId = token.Vlasnik.TrenutnaUloga.Id,
                    DodatneInformacije = token.Vlasnik.TrenutnaUloga
                                                .Users
                                                .Where(a => a.UserId == token.Vlasnik.Id)
                                                .Select(a => new KorisnikDodatneInformacijeListModel
                                                {
                                                    Items = a.KorisnikUlogaDodatnaInformacija.Select(dodatno => new KorisnikDodatneInformacijeListModelItem
                                                    {
                                                        KorisnikId = dodatno.KorisnikId,
                                                        Vrijednost = dodatno.Vrijednost,
                                                        TipDodatneInformacije = new KorisnikTipDodatneInformacijeModel
                                                        {
                                                            Id = dodatno.KorisnikTipDodatneInformacije.Id,
                                                            Naziv = dodatno.KorisnikTipDodatneInformacije.Naziv,
                                                            Onemogucen = dodatno.KorisnikTipDodatneInformacije.Onemogucen,
                                                            Opis = dodatno.KorisnikTipDodatneInformacije.Opis,
                                                            Poredak = dodatno.KorisnikTipDodatneInformacije.Poredak,
                                                            Sifra = dodatno.KorisnikTipDodatneInformacije.Sifra
                                                        }
                                                    }).ToList()
                                                }).First(),
                    DozvoljeneAkcije = new PravoAkcijaListModel
                    {
                        Items = token.Vlasnik.TrenutnaUloga
                                    .PravoAkcijaUloge
                                    .Select(akcija => new PravoAkcijaListModelItem
                                    {
                                        Id = akcija.PravoAkcija.Id,
                                        Naziv = akcija.PravoAkcija.Naziv,
                                        Opis = akcija.PravoAkcija.Opis,
                                        Sifra = akcija.PravoAkcija.Sifra
                                    }).ToList()
                    },
                } : null
            };
        }
    }
}
