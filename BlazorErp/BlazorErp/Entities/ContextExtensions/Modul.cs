using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazorErp.Entities.ContextExtensions
{
    public static class Modul
    {
        public static void Execute(Context context)
        {
            var entity = context.Moduli.FirstOrDefault(x => x.Sifra == "base");
            if (entity == null)
                context.Moduli.Add(new Models.Korisnik.Modul()
                {
                    Naziv = "Base",
                    Opis = "Base modul",
                    Sifra = "base",
                    DatumKreiranja = DateTime.Now
                });

            context.SaveChanges();


            context.SaveChanges();
        }
    }
}
