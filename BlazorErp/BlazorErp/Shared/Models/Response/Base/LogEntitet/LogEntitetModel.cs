using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Response.LogEntitet
{
    public class LogEntitetModel
    {
        public string Id { get; set; }
        public string KorisnickoIme { get; set; }
        public string Entitet { get; set; }
        public int EntitetId { get; set; }
        public DateTime DatumAkcije { get; set; }
        public string Vrijednost { get; set; }
    }
}
