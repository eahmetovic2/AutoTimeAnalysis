using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BlazorErp.Shared.Models.Request.Common
{
    /// <summary>
    /// Predstavlja request model koji sadrzi podatke za stranicenje
    /// </summary>
    public class PagedRequestModel
    {
        /// <summary>
        /// Prazan konstruktor
        /// </summary>
        public PagedRequestModel()
        {
            // postavljamo pocetne vrijednosti
            Page = 1;
            Count = 10;
            Sve = false;
        }

        /// <summary>
        /// Broj stranice koja se trazi
        /// </summary>
        [Range(1, 1000)]
        public int Page { get; set; }

        /// <summary>
        /// Broj rezultata u jednoj stranici
        /// </summary>
        [Range(1, 100)]
        public int Count { get; set; }

        /// <summary>
        /// Koristi se u slučajevima kada je potrebno sve
        /// </summary>
        public bool Sve { get; set; }
	}
}
