using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace BlazorErp.Shared.Models.Request.Sifarnik
{
    public class UpdateSifarnikRequestModel
    {
        public string TipSifarnika { get; set; }
        public Dictionary<string, object> Sifarnik { get; set; }
        public int Id { get; set; }
        public ILifetimeScope Scope { get; set; }
    }
}