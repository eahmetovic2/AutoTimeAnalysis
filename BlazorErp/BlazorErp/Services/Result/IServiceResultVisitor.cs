using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Services.Result
{
    /// <summary>
    /// Interfejs za visitor rezultata
    /// </summary>
    /// <typeparam name="Tin">Tip rezultata koji se visit-ira</typeparam>
    /// <typeparam name="TRet">Povratni tip visitora</typeparam>
    public interface IServiceResultVisitor<Tin, TRet>
    {
        /// <summary>
        /// Provjeri rezultat koji je tipa ok
        /// </summary>
        /// <param name="result">Rezultat tipa ok</param>
        /// <returns>Rezultat visit-iranja</returns>
        TRet VisitOk(OkServiceResult<Tin> result);

        /// <summary>
        /// Provjeri rezultat koji je tipa not found
        /// </summary>
        /// <param name="result">Rezultat tipa not found</param>
        /// <returns>Rezultat visit-iranja</returns>
        TRet VisitNotFound(NotFoundServiceResult result);


        TRet VisitForbiddenError(ForbiddenServiceResult forbiddenServiceResult);

        /// <summary>
        /// Provjeri rezultat koji je tipa error
        /// </summary>
        /// <param name="result">Rezultat tipa error</param>
        /// <returns>Rezultat visit-iranja</returns>
        TRet VisitError(ErrorServiceResult errorResult);

        /// <summary>
        /// Provjeri rezultat koji je tipa validation error
        /// </summary>
        /// <param name="result">Rezultat tipa validation error</param>
        /// <returns>Rezultat visit-iranja</returns>
        TRet VisitValidationError(ValidationErrorServiceResult validationErrorResult);

        /// <summary>
        /// Provjeri rezultat koji je tipa missing entity
        /// </summary>
        /// <param name="result">Rezultat tipa missing entity</param>
        /// <returns>Rezultat visit-iranja</returns>
        TRet VisitMissingEntity(MissingEntityServiceResult missingEntityResult);

        /// <summary>
        /// Provjeri rezultat koji je tipa existing entity
        /// </summary>
        /// <param name="result">Rezultat tipa existing entity</param>
        /// <returns>Rezultat visit-iranja</returns>
        TRet VisitExistingEntity(ExistingEntityServiceResult existingEntityResult);
    }
}
