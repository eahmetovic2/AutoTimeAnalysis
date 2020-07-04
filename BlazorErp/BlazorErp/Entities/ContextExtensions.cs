using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Entities
{
    /// <summary>
    /// Ekstenzije za data context klasu
    /// </summary>
    public static class DBContextExtensions
    {
        /// <summary>
        /// Kreiraj pocetne podatke ako je baza prazna
        /// </summary>
        /// <param name="context">Db context</param>
        public static async void EnsureSeedData(this Context context)
        {
            // da li postoje migracije koje nisu izvrene
            //if (context.Database.GetPendingMigrations().Any())
            //    return;

            ContextExtensions.Uloga.Execute(context);


            await ContextExtensions.AdminKorisnik.Execute(context);


            ContextExtensions.LogKategorija.Execute(context);
            ContextExtensions.LogLevel.Execute(context);
        }
    }
}
