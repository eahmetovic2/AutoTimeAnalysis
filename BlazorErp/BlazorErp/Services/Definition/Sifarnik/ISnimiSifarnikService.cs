using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using BlazorErp.Core.Constants;
using BlazorErp.Entities;
using BlazorErp.Shared.Models.Request.Sifarnik;

namespace BlazorErp.Services.Definition.Sifarnik
{
    public interface ISnimiSifarnikService : IService
    {
        Dictionary<ESifarnik, Func<Context, KreirajSifarnikRequestModel, ILifetimeScope, bool>> GetSnimiSifarnik();
    }
}
