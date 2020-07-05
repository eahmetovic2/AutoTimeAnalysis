using BlazorErp.Server.Common.Attributes;
using BlazorErp.Server.Common.Result;
using BlazorErp.Services.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc;

namespace BlazorErp.Server.Controllers
{
    /// <summary>
    /// Bazni kontroler za aplikaciju
    /// </summary>
    [Authorize]
    [ApiController]
    [ValidateModel]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Konvertuj ServiceResult u IActionResult
        /// </summary>
        /// <typeparam name="T">Tip rezultata</typeparam>
        /// <param name="result">Rezultat koji se konvertuje</param>
        /// <returns>Odgovarajuci IActionResult za resultat koji je dat</returns>
        public IActionResult Convert<T>(ServiceResult<T> result)
        {
            var visitor = new ActionResultVisitor<T>();
            return result.Visit(visitor);
        }
    }
}
