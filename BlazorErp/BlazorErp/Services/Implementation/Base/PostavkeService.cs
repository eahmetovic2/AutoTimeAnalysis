using Autofac;
using BlazorErp.Entities;
using BlazorErp.Entities.Models.Base;
using BlazorErp.Shared.Models.Request.Postavke;
using BlazorErp.Shared.Models.Response.Postavke;
using BlazorErp.Services.Result;
using System.Linq;
using BlazorErp.Mapping.Mappers.PostavkeMap;

namespace BlazorErp.Services.Implementation
{
    /// <summary>
    /// Implementacija servisa koji radi sa postavkama
    /// </summary>
    public class PostavkeService : Service, IPostavkeService
    {
        /// <summary>
        /// Kesirana vrijednost postavki
        /// </summary>
        private static ServiceResult<PostavkeModel> cachedResult;

        /// <summary>
        /// Entity framework db kontekst 
        /// </summary>
        private Context context;

 

        /// <summary>
        /// Konstruktor servisa
        /// </summary>
        public PostavkeService(ILifetimeScope scope, Context context)
            : base(scope)
        {
            this.context = context;

        }

        public ServiceResult<PostavkeModel> VratiPostavke()
        {
            // ako imamo kesiran rezultat, vrati to 
            if (cachedResult != null)
                return cachedResult;

            // dobavi postavke ako postoje, ako ne, kreiraj default vrijednost
            var postavke = context.Postavke
                .OrderBy(p => p.Id)
                .FirstOrDefault();

            if (postavke == null)
                postavke = new Postavke();

            // uradi mapiranje, spasi rezultat ako je ok
            var result = postavke.ToPostavkeModel();

            return Ok(result);
        }

        public ServiceResult<PostavkeModel> AzurirajPostavke(AzurirajPostavkeRequestModel model)
        {
            // dobavi postavke
            var postavke = context.Postavke
                .OrderBy(p => p.Id)
                .FirstOrDefault();

            // ako ne postoje, napravi nove
            if (postavke == null)
            {
                postavke = new Postavke();
                context.Add(postavke);
            }

            // postavi vrijednosti
            postavke.NaslovSistema = model.NaslovSistema;
            postavke.TrajanjeSesije = model.TrajanjeSesije;
            
            SaveChanges(context);

            // ocisti kes, vrati kreirane/azuirane postavke
            cachedResult = null;
            return VratiPostavke();
        }

        public Postavke DajSistemskePostavke() {
            var postavke = context.Postavke
                .OrderBy(p => p.Id)
                .FirstOrDefault();

            // ako ne postoje, napravi nove
            if (postavke == null)
            {
                postavke = new Postavke();
                context.Add(postavke);
            }

            return postavke;
        }
    }
}
