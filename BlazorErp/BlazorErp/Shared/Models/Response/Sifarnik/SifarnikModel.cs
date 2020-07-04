using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Response.Sifarnik
{
    public class SifarnikModel
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Sifra { get; set; }
        public bool IsDeleted { get; set; }
        public int Poredak { get; set; }
        // Pomocno polje koje omogucava da se jedna opcija stavi na prvo mjesto u SifarnikService
        // npr. Bosna i Hercegovina u drzavljanstvima
        public bool Prvi { get; set; }

        //Kada je potrebno filtriranje po nekom nadpojmu
        public int? RoditeljReferenca { get; set; }
        /// <summary>
        /// Kada je potrebno proslijediti neku dodatnu vrijednost 
        /// </summary>
        public string Data { get; set; }

        public DateTime? DatumIzmjene { get; set; }

        // Ovo je potrebno za bosnu i hercegovinu kada se listaju drzave
        // Kod upisa ucenika iz drugih kantona
        public bool Default { get; set; }
    }
}
