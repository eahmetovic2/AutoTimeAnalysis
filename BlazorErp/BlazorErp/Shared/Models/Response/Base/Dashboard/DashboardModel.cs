using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Response.Dashboard
{
    public class DashboardModel
    {
        public int UkupnoKorisnika { get; set; }

        public int OnlineKorisnika { get; set; }

        public string SkolskaGodina { get; set; }

        public int BrojSkola { get; set; }

        public int BrojNastavnika { get; set; }

        public int BrojPredmeta { get; set; }

        public int BrojUcenika { get; set; }

        public int BrojRazreda { get; set; }
    }
}
