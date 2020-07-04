using BlazorErp.Core.Constants;
using System.Collections.Generic;

namespace BlazorErp.Shared.Models.Response.Sifarnik
{
    public class PoljeSifarnika
    {
        public string PoljeUTabeli { get; set; }
        public string Naziv { get; set; }
        public bool Required { get; set; }
        public TipPolja Tip { get; set; }
        public List<VidljivostPolja> Vidljivosti { get; set; }
        /// <summary>
        /// Potrebno samo ako je select polje
        /// </summary>
        public List<SifarnikModel> Opcije { get; set; }

        public PoljeSifarnika(string poljeUTabeli, string naziv, TipPolja tip, List<SifarnikModel> opcije, List<VidljivostPolja> vidljivosti, bool required = false)
        {
            PoljeUTabeli = poljeUTabeli;
            Naziv = naziv;
            Tip = tip;
            Opcije = opcije;
            Vidljivosti = vidljivosti;
            Required = required;
        }

        public Dictionary<string, object> DajKonfiguracije()
        {
            var konfiguracija = new Dictionary<string, object>()
            {
                { "text", Naziv },
                { "align", "left" },
                { "sortable", false },
                { "value", PoljeUTabeli }
            };

            if (!Vidljivosti.Contains(VidljivostPolja.Lista))
            {
                konfiguracija.Add("class", "hidden");
            }
            return konfiguracija;
        }
    }
}