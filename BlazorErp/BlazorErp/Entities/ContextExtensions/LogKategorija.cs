using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorErp.Entities.ContextExtensions
{
    public static class LogKategorija
    {
        public static void Execute(Context context)
        {
            var entity = context.LogKategorije.FirstOrDefault(x => x.Sifra == "nijedna" && !x.IsDeleted);
            if (entity == null)
                context.LogKategorije.Add(new Models.Sifarnik.LogKategorija()
                {
                    Naziv = "Nijedna",
                    Sifra = "nijedna",
                    VrijednostUAplikaciji = 0,
                    Poredak = 0,
                    IsDeleted = false
                });

            context.SaveChanges();

            entity = context.LogKategorije.FirstOrDefault(x => x.Sifra == "pristup_sistemu" && !x.IsDeleted);
            if (entity == null)
                context.LogKategorije.Add(new Models.Sifarnik.LogKategorija()
                {
                    Naziv = "Pristup sistemu",
                    Sifra = "pristup_sistemu",
                    VrijednostUAplikaciji = 1,
                    Poredak = 1,
                    IsDeleted = false
                });
            entity = context.LogKategorije.FirstOrDefault(x => x.Sifra == "pristup_sistemu" && !x.IsDeleted);
            if (entity == null)
                context.LogKategorije.Add(new Models.Sifarnik.LogKategorija()
                {
                    Naziv = "Pristup sistemu",
                    Sifra = "pristup_sistemu",
                    VrijednostUAplikaciji = 1,
                    Poredak = 1,
                    IsDeleted = false
                });
            ////dio LogKategorije je preko migracija ubacen ovdje  se nastavilo

            ////spasi promjene 

            context.SaveChanges();




        }
    }
}
