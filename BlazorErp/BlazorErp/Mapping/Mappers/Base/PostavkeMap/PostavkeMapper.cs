using BlazorErp.Entities.Models;
using BlazorErp.Entities.Models.Base;
using BlazorErp.Shared.Models.Response.Postavke;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Mapping.Mappers.PostavkeMap
{
    public static class PostavkeMapper
    {
        public static PostavkeModel ToPostavkeModel(this Postavke postavke)
        {
            return new PostavkeModel()
            {
                NaslovSistema = postavke.NaslovSistema,
                TrajanjeSesije = postavke.TrajanjeSesije,
            };
        }
    }
}