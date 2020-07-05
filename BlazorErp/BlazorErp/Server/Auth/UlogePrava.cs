using BlazorErp.Shared.Prava;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorErp.Server.Auth
{
    public static class UlogePrava
    {
        public static List<PravoAkcije> AdministratorPrava = new List<PravoAkcije>
        {
            PravoAkcije.KorisnikPregled,
            PravoAkcije.KorisnikLista,
        };
    }

    public abstract class PravoAkcijeMeta
    {
        public string EnumTip { get; set; }
    }

    public class PravoAkcijeVrijednost<T> : PravoAkcijeMeta
    {
        public T Vrijednost { get; set; }

        public PravoAkcijeVrijednost<T> PostaviVrijednost(T vrijednost)
        {
            Vrijednost = vrijednost;
            EnumTip = vrijednost.GetType().Name;
            return this;
        }
    }
}
