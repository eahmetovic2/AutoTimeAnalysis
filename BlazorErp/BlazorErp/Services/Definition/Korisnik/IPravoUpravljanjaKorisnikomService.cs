using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlazorErp.Entities.Models.Korisnik;
using BlazorErp.Shared.Models.Response.Korisnik.PravoUpravljanjaKorisnikom;
using BlazorErp.Services.Result;

namespace BlazorErp.Services.Definition.Korisnik
{
    public interface IPravoUpravljanjaKorisnikomService : IService
    {
        ServiceResult<PravoUpravljanjaKorisnikomListModel> VratiSve(int ulogaId);

        IQueryable<PravoUpravljanjaKorisnikom> VratiPravaUpravljanjaKorisnikom(int ulogaId);
    }
}
