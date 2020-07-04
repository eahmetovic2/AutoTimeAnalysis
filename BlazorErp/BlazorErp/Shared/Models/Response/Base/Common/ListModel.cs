using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Response.Common
{
    /// <summary>
    /// Model koji predstavlja listu drugih modela
    /// </summary>
    /// <typeparam name="T">Tip modela koji je sadrzan u listi</typeparam>
    public class ListModel<T>
    {
        /// <summary>
        /// Prazan konstruktor
        /// </summary>
        public ListModel()
        {
        }

        /// <summary>
        /// Konstruktor koji prima listu modela
        /// </summary>
        /// <param name="items">Modeli koji su u listi</param>
        public ListModel(IEnumerable<T> items)
        {
            this.Items = items;
        }

        /// <summary>
        /// Lista modela koji su dio ove liste
        /// </summary>
        public IEnumerable<T> Items { get; set; }
    }
}
