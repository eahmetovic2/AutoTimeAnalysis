using Autofac;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BlazorErp.Entities;
using BlazorErp.Shared.Models.Request.Sifarnik;

namespace BlazorErp.Services.Definition.Sifarnik
{
    public interface ISnimanjeIzmjenaPomocnoService : IService
    {
        bool SaveEntity<TEntity>(Context context, KreirajSifarnikRequestModel model, TEntity entity, DbSet<TEntity> entities, ILifetimeScope scope) where TEntity : class;
        bool UpdateEntity<TEntity>(Context context, UpdateSifarnikRequestModel model, TEntity entity, DbSet<TEntity> entities, ILifetimeScope scope) where TEntity : class;
    }
}
