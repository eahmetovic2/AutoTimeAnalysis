using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorErp.Shared.Models.Request.Korisnik.Uloga;
using BlazorErp.Shared.Models.Response.Korisnik.PravoAkcijaUloga;
using BlazorErp.Shared.Models.Response.Korisnik.Uloga;
using BlazorErp.Services.Result;

namespace BlazorErp.Services.Definition.Korisnik
{
    public interface IUlogaService
    {
        ServiceResult<UlogaListModel> VratiSveZaKorisnickoIme(string korisnickoIme);

        ServiceResult<Nothing> SnimiDozvoljeneAkcije(int ulogaId, SnimiDozvoljeneAkcijeRequestModel model);

        ServiceResult<PravoAkcijaUlogaListModel> VratiSveDozvoljeneAkcije(int ulogaId);

        ServiceResult<GrupaPravaUlogeListModel> VratiSveGrupePravaProsireno(int ulogaId);

        Task<ServiceResult<UlogaModel>> Kreiraj(KreirajUloguRequestModel model);

        ServiceResult<UlogaModel> Azuriraj(int ulogaId, AzurirajUloguRequestModel model);

        ServiceResult<UlogaListModel> VratiSve();

        ServiceResult<UlogaModel> VratiPoIdu(int ulogaId);
    }
}
