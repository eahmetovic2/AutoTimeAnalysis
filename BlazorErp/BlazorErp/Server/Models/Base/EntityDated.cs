using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorErp.Server.Models.Base
{
    public class EntityDated
    {
        public DateTime? DatumKreiranja { get; set; }
        public DateTime? DatumIzmjene { get; set; }
    }
}
