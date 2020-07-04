using System;
using System.Collections.Generic;
using System.Text;
using BlazorErp.Core.Constants;
using BlazorErp.Shared.Models.Response.Sifarnik;
using static BlazorErp.Services.Implementation.SifarnikService;

namespace BlazorErp.Services.Definition.Sifarnik
{
    public interface IGetPoljaSifarnikaService : IService
    {
        Dictionary<ESifarnik, Func<InputParameters, SifarnikListModel>> GetPoljaSifarnika();
    }
}
