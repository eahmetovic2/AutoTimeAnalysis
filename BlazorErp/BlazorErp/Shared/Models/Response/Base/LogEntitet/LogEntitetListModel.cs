using BlazorErp.Shared.Models.Response.Common;
using System;
using System.Collections.Generic;
using System.Text;
using static BlazorErp.Shared.Models.Response.LogEntitet.LogEntitetListModel;

namespace BlazorErp.Shared.Models.Response.LogEntitet
{
    public class LogEntitetListModel : PagedListModel<LogEntitetListModelItem>
    {
        //public IList<LogEntitetListModelItem> Items { get; set; }

        public class LogEntitetListModelItem
        {
            public string Id { get; set; }
            public string KorisnickoIme { get; set; }
            public string Entitet { get; set; }
            public int EntitetId { get; set; }
            public DateTime DatumAkcije { get; set; }
            public string Vrijednost { get; set; }

        }
    }
}
