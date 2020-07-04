using BlazorErp.Services.Result;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace BlazorErp.Server.Common.Result
{
    /// <summary>
    /// Visitor koji pretvara ServiceResult koji vracaju servisi u odgovarajuci
    /// IActionResult koji vracaju asp.net mvc controlleri
    /// </summary>
    /// <typeparam name="T">Tip rezultata</typeparam>
    public class ActionResultVisitor<T> : IServiceResultVisitor<T, IActionResult>
    {
        /// <summary>
        /// Konverzija ok rezultata
        /// </summary>
        /// <param name="result">Rezultat tipa ok</param>
        /// <returns>IActionResult koji odgovara rezultatu</returns>
        public IActionResult VisitOk(OkServiceResult<T> result)
        {
            return new OkObjectResult(result.Value);
        }

        /// <summary>
        /// Konverzija not found rezultata
        /// </summary>
        /// <param name="result">Rezultat tipa not found</param>
        /// <returns>IActionResult koji odgovara rezultatu</returns>
        public IActionResult VisitNotFound(NotFoundServiceResult result)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Konverzija error rezultata
        /// </summary>
        /// <param name="result">Rezultat tipa error</param>
        /// <returns>IActionResult koji odgovara rezultatu</returns>
        public IActionResult VisitError(ErrorServiceResult errorResult)
        {
            return new BadRequestObjectResult(errorResult.Message);
        }

        /// <summary>
        /// Konverzija validation error rezultata
        /// </summary>
        /// <param name="result">Rezultat tipa validation error</param>
        /// <returns>IActionResult koji odgovara rezultatu</returns>
        public IActionResult VisitValidationError(ValidationErrorServiceResult validationErrorResult)
        {
            return new BadRequestObjectResult(validationErrorResult.Message);
        }

        /// <summary>
        /// Konverzija missing entity rezultata
        /// </summary>
        /// <param name="result">Rezultat tipa missing entity</param>
        /// <returns>IActionResult koji odgovara rezultatu</returns>
        public IActionResult VisitMissingEntity(MissingEntityServiceResult missingEntityResult)
        {
            return new NotFoundObjectResult(missingEntityResult.Message);
        }

        /// <summary>
        /// Konverzija existing entity rezultata
        /// </summary>
        /// <param name="result">Rezultat tipa existing entity</param>
        /// <returns>IActionResult koji odgovara rezultatu</returns>
        public IActionResult VisitExistingEntity(ExistingEntityServiceResult existingEntityResult)
        {
            var result = new ObjectResult(existingEntityResult.Message);
            result.StatusCode = StatusCodes.Status409Conflict;
            return result;
        }

        public IActionResult VisitForbiddenError(ForbiddenServiceResult forbiddenServiceResult)
        {
            var result = new ObjectResult("Nemate prava na ovu akciju.");
            result.StatusCode = StatusCodes.Status403Forbidden;
            return result;
        }
    }
}
