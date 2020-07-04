using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Response.Postavke
{
    public class PostavkeModel
    {
        public String NaslovSistema { get; set; }

        public int TrajanjeSesije { get; set; }

        public String UrlKarte { get; set; }
        public String AutorskaPravaKarte { get; set; }
        public bool OmogucenoKreiranjeRazredaUProsleGodine { get; set; }
        public bool RetroaktivniUnosOcjenaSkola { get; set; }
    }
}
