using Autofac;
using BlazorErp.Core.Constants;
using BlazorErp.Shared.Models.Response.Sifarnik;
using BlazorErp.Services.Definition.Sifarnik;
using BlazorErp.Services.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static BlazorErp.Services.Implementation.SifarnikService;
using BlazorErp.Services.Implementation;

namespace BlazorErp.Services.Implementation.Sifarnik
{
    /// <summary>
    /// Servis koji vraca opis polja sifarnika, koja se koriste na frontendu prilikom izmjene i snimanja
    /// </summary>
    public class GetPoljaSifarnikaService : Service, IGetPoljaSifarnikaService
    {
        public GetPoljaSifarnikaService(ILifetimeScope scope) : base(scope)
        {

        }

        private static List<PoljeSifarnika> CommonFields()
        {
            return new List<PoljeSifarnika>() {
                new PoljeSifarnika ("naziv", "Naziv", TipPolja.Tekst, null, SveVidljivosti (), true),
                    new PoljeSifarnika ("sifra", "Šifra", TipPolja.Tekst, null, SveVidljivosti ())
            };
        }

        public Dictionary<ESifarnik, Func<SifarnikService.InputParameters, SifarnikListModel>> GetPoljaSifarnika()
        {

            return new
            Dictionary<ESifarnik, Func<InputParameters, SifarnikListModel>>() {
                //{
                //    ESifarnik.Pol,
                //    new Func<InputParameters, SifarnikListModel>
                //    (
                //        (inputParameters) => {
                //            List<PoljeSifarnika> listaPolja = new List<PoljeSifarnika> () {
                //            new PoljeSifarnika ("naziv", "Naziv", TipPolja.Tekst, null, SveVidljivosti (), true),
                //            new PoljeSifarnika ("sifra", "Šifra", TipPolja.Tekst, null, SveVidljivosti (), true),
                //            new PoljeSifarnika ("poredak", "Poredak", TipPolja.Broj, null, SveVidljivosti (), true)
                //            };

                //            return new SifarnikListModel () {
                //                FieldsList = listaPolja,
                //                    Items = inputParameters.Context.Polovi
                //                    .Select (s => new SifarnikModel () {
                //                        Id = s.Id,
                //                        Naziv = s.Naziv,
                //                        Poredak = s.Poredak,
                //                        Sifra = s.Sifra
                //                        })
                //            };
                //        }
                //    )
                //}, 
            };


        }
        public static List<VidljivostPolja> SveVidljivosti()
        {
            return new List<VidljivostPolja>() {
                VidljivostPolja.Dodavanje,
                    VidljivostPolja.Edit,
                    VidljivostPolja.Lista
            };
        }

        public static List<VidljivostPolja> SamoEdit()
        {
            return new List<VidljivostPolja>() {
                VidljivostPolja.Edit
            };
        }

        public static List<VidljivostPolja> SamoLista()
        {
            return new List<VidljivostPolja>() {
                VidljivostPolja.Lista
            };
        }

        public static List<VidljivostPolja> EditIDodavanje()
        {
            return new List<VidljivostPolja>() {
                VidljivostPolja.Edit,
                    VidljivostPolja.Dodavanje
            };
        }
    }
}