using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Services.Result
{
    /// <summary>
    /// Visitor koji prazne rezultate pretvara u rezultate sa tipom
    /// </summary>
    /// <typeparam name="TIn">Tip rezultata koji se ocekuje</typeparam>
    public class ServiceResultWrapperVisitor<TIn> : IServiceResultVisitor<TIn, ServiceResultWrapper<TIn>>
    {
        /// <summary>
        /// Konverzija ok rezultata
        /// Ova konverzija nije dozvoljena
        /// </summary>
        /// <param name="result">Rezultat ok tipa</param>
        /// <returns>Baca izuzetak jer ova konverzija nije dozvoljena</returns>
        public ServiceResultWrapper<TIn> VisitOk(OkServiceResult<TIn> result)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Konverzija not found rezultata
        /// </summary>
        /// <param name="result">Rezultat tipa not found</param>
        /// <returns>Dati rezultat obuhvacen u ServiceResultWrapper klasi</returns>
        public ServiceResultWrapper<TIn> VisitNotFound(NotFoundServiceResult result)
        {
            return new ServiceResultWrapper<TIn>(result);
        }

        /// <summary>
        /// Konverzija error rezultata
        /// </summary>
        /// <param name="result">Rezultat tipa error</param>
        /// <returns>Dati rezultat obuhvacen u ServiceResultWrapper klasi</returns>
        public ServiceResultWrapper<TIn> VisitError(ErrorServiceResult result)
        {
            return new ServiceResultWrapper<TIn>(result);
        }

        /// <summary>
        /// Konverzija validation error rezultata
        /// </summary>
        /// <param name="result">Rezultat tipa validation error</param>
        /// <returns>Dati rezultat obuhvacen u ServiceResultWrapper klasi</returns>
        public ServiceResultWrapper<TIn> VisitValidationError(ValidationErrorServiceResult result)
        {
            return new ServiceResultWrapper<TIn>(result);
        }

        /// <summary>
        /// Konverzija missing entity rezultata
        /// </summary>
        /// <param name="result">Rezultat tipa missing entity</param>
        /// <returns>Dati rezultat obuhvacen u ServiceResultWrapper klasi</returns>
        public ServiceResultWrapper<TIn> VisitMissingEntity(MissingEntityServiceResult result)
        {
            return new ServiceResultWrapper<TIn>(result);
        }

        /// <summary>
        /// Konverzija existing entity rezultata
        /// </summary>
        /// <param name="result">Rezultat tipa existing entity</param>
        /// <returns>Dati rezultat obuhvacen u ServiceResultWrapper klasi</returns>
        public ServiceResultWrapper<TIn> VisitExistingEntity(ExistingEntityServiceResult result)
        {
            return new ServiceResultWrapper<TIn>(result);
        }

        public ServiceResultWrapper<TIn> VisitForbiddenError(ForbiddenServiceResult result)
        {
            return new ServiceResultWrapper<TIn>(result);
        }
    }
}
