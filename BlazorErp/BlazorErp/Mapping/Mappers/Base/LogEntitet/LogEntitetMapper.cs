using BlazorErp.Shared.Models.Response.LogEntitet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static BlazorErp.Shared.Models.Response.LogEntitet.LogEntitetListModel;

namespace BlazorErp.Mapping.Mappers.LogEntitet
{
    public static class LogEntitetMapper
    {
        public static IQueryable<LogEntitetModel> ToLogEntitetModel(this IQueryable<Entities.Models.Base.LogEntitet> query)
        {
            return query.Select(entitet => new LogEntitetModel
            {
                DatumAkcije = entitet.DatumAkcije,
                Entitet = entitet.Entitet.ToString(),
                EntitetId = entitet.EntitetId,
                KorisnickoIme = entitet.Korisnik.UserName,
                Vrijednost = entitet.Vrijednost
            });
        }

        public static IQueryable<LogEntitetListModelItem> ToLogEntitetListModelItem(this IQueryable<Entities.Models.Base.LogEntitet> query)
        {
            return query.Select(entitet => new LogEntitetListModelItem
            {
                DatumAkcije = entitet.DatumAkcije,
                Entitet = entitet.Entitet.ToString(),
                EntitetId = entitet.EntitetId,
                KorisnickoIme = entitet.Korisnik.UserName,
                Vrijednost = entitet.Vrijednost
            });
        }
    }
}
