using BlazorErp.Entities.Models.Korisnik;
using BlazorErp.Shared.Models.Response.Korisnik;
using System.Collections.Generic;
using System.Linq;
using BlazorErp.Shared.Models.Response.Korisnik.PravoAkcija;
using BlazorErp.Shared.Models.Response.Korisnik.Korisnik;
using BlazorErp.Shared.Models.Response.Korisnik.PravoUpravljanjaKorisnikom;
using BlazorErp.Shared.Models.Response.Korisnik.KorisnikDodatneInformacije;
using BlazorErp.Shared.Models.Response.Korisnik.KorisnikTipDodatneInformacije;

namespace BlazorErp.Mapping.Mappers.KorisnikMap
{
    public static class KorisnikMapper
    {
        public static IQueryable<KorisnikModel> ToKorisnikModel(this IQueryable<IdentityKorisnik> query)
        {
            return query.Select(korisnik => new KorisnikModel()
            {
                Id = korisnik.Id,
                KorisnickoIme = korisnik.UserName,
                Email = korisnik.Email,
                PunoIme = korisnik.PunoIme,
                //DatumKreiranja = korisnik.DatumKreiranja,
                Uloge = korisnik.Roles.Select(a => new KorisnikUlogaModel
                {
                    VrstaUloge = new PravoUpravljanjaKorisnikomListModelItem
                    {
                        Id = a.RoleId,
                        Uloga = a.Role.Name,
                        PotrebneDodatneInformacije = a.Role
                                                      .TipoviDodatneInformacije
                                                      .Select(u => u.KorisnikTipDodatneInformacije.Sifra)
                                                      .ToList()
                    }                    
                }).ToList()
            });
        }

        public static IQueryable<KorisnikTokenModel> ToKorisnikTokenModel(this IQueryable<IdentityKorisnik> query)
        {
            return query.Select(korisnik => new KorisnikTokenModel()
            {
                Id = korisnik.Id,
                KorisnickoIme = korisnik.UserName,
                TrenutnaUlogaId = korisnik.TrenutnaUlogaId,
                Email = korisnik.Email,
                PunoIme = korisnik.PunoIme,
                //DatumKreiranja = korisnik.DatumKreiranja,
                DodatneInformacije = korisnik.TrenutnaUloga
                                                .Users
                                                .Where(a => a.UserId == korisnik.Id)
                                                .Select(a => new KorisnikDodatneInformacijeListModel
                                                {
                                                    Items = a.KorisnikUlogaDodatnaInformacija.Select(dodatno => new KorisnikDodatneInformacijeListModelItem
                                                    {
                                                        KorisnikId = dodatno.KorisnikId,
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
                    Items = korisnik.TrenutnaUloga
                                    .PravoAkcijaUloge
                                    .Select(akcija => new PravoAkcijaListModelItem
                                    {
                                        Id = akcija.PravoAkcija.Id,
                                        Naziv = akcija.PravoAkcija.Naziv,
                                        Opis = akcija.PravoAkcija.Opis,
                                        Sifra = akcija.PravoAkcija.Sifra
                                    }).ToList()
                }
            });
        }

        public static IQueryable<KorisnikListModelItem> ToKorisnikListModelItem(this IQueryable<IdentityKorisnik> query)
        {
            return query.Select(korisnik => new KorisnikListModelItem()
            {
                Id = korisnik.Id,
                KorisnickoIme = korisnik.UserName,
                Uloga = string.Join(", ", korisnik.Roles
                                                  .Select(a => a.Role.Name)
                                                  .ToList()),
                Email = korisnik.Email,
                PunoIme = korisnik.PunoIme,
                //DatumKreiranja = korisnik.DatumKreiranja,
            });
        }
    }
}
