using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorErp.Entities.Models.Base
{
    public class Dokument : BazniModel
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Naziv dokumenta, koji se moze koristiti za prikaz
        /// </summary>
        public string Naziv { get; set; }

        /// <summary>
        /// Putanja u upload folderu
        /// </summary>
        public string Putanja { get; set; }


    }
}
