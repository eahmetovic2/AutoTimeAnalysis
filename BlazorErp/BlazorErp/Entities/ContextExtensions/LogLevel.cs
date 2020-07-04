using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorErp.Entities.ContextExtensions
{
    public static class LogLevel
    {
        public static void Execute(Context context)
        {
            var entity = context.LogLeveli.FirstOrDefault(x => x.Sifra == "nijedan" && !x.IsDeleted);
            if (entity == null)
                context.LogLeveli.Add(new Models.Sifarnik.LogLevel()
                {
                    Naziv = "Nijedan",
                    Sifra = "nijedan",
                    VrijednostUAplikaciji = 0,
                    Poredak = 0,
                    IsDeleted = false
                });

            context.SaveChanges();

            entity = context.LogLeveli.FirstOrDefault(x => x.Sifra == "greska" && !x.IsDeleted);
            if (entity == null)
                context.LogLeveli.Add(new Models.Sifarnik.LogLevel()
                {
                    Naziv = "Greška",
                    Sifra = "greska",
                    VrijednostUAplikaciji = 1,
                    Poredak = 1,
                    IsDeleted = false
                });

            context.SaveChanges();

            entity = context.LogLeveli.FirstOrDefault(x => x.Sifra == "upozorenje" && !x.IsDeleted);
            if (entity == null)
                context.LogLeveli.Add(new Models.Sifarnik.LogLevel()
                {
                    Naziv = "Upozorenje",
                    Sifra = "upozorenje",
                    VrijednostUAplikaciji = 2,
                    Poredak = 2,
                    IsDeleted = false
                });

            context.SaveChanges();

            entity = context.LogLeveli.FirstOrDefault(x => x.Sifra == "info" && !x.IsDeleted);
            if (entity == null)
                context.LogLeveli.Add(new Models.Sifarnik.LogLevel()
                {
                    Naziv = "Info",
                    Sifra = "info",
                    VrijednostUAplikaciji = 3,
                    Poredak = 3,
                    IsDeleted = false
                });

            context.SaveChanges();
        }
    }
}
