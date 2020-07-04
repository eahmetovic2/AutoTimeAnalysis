using BlazorErp.Entities.Models;
using BlazorErp.Entities.Models.Base;
using BlazorErp.Shared.Models.Request.Postavke;
using BlazorErp.Shared.Models.Response.Postavke;
using BlazorErp.Services.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Services
{
    /// <summary>
    /// Servis koji radi sa postavkama sistema
    /// </summary>
    public interface IPostavkeService : IService
    {
        /// <summary>
        /// Vrati trenutne postavke sistema
        /// Ako postavke ne postoje, vrati default postavke
        /// </summary>
        /// <returns>Model postavki sistema</returns>
        ServiceResult<PostavkeModel> VratiPostavke();

        /// <summary>
        /// Azuriraj postavke sistema
        /// </summary>
        /// <param name="model">Nove postavke sistema</param>
        /// <returns>Model azuriranih postavki</returns>
        ServiceResult<PostavkeModel> AzurirajPostavke(AzurirajPostavkeRequestModel model);

        Postavke DajSistemskePostavke();
    }
}
