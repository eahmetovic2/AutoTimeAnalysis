using System;
using System.Collections.Generic;
using System.Text;
using BlazorErp.Shared.Models.Request.Korisnik.KorisnikDodatneInformacije;
using BlazorErp.Shared.Models.Response.Korisnik.UlogaTipDodatneInformacije;
using BlazorErp.Services.Result;

namespace BlazorErp.Services.Definition.Korisnik
{
    public interface IUlogaTipoviDodatneInformacijeService
    {
        ServiceResult<Nothing> SnimiZaUlogu(int ulogaId, SnimiDodatneInformacijeUlogeRequestModel model);

        ServiceResult<UlogaTipDodatneInformacijeListModel> VratiZaUlogu(int ulogaId);
    }
}
