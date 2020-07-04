using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorErp.Entities.Models
{
    /// <summary>
    /// Osnovni model za sve entitete
    /// Ima shadow property IsDeleted koji se koristi za softdelete
    /// </summary>
    public abstract class BazniModel
    {
        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? DatumKreiranja { get; set; }

        public DateTime? DatumIzmjene { get; set; }
    }
}
