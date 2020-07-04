//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorErp.Server.Common.Attributes
{
    /// <summary>
    /// Atribut koji provjerava da li je model null
    /// </summary>
    public class RequireModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // ako postoji model koji je null, vrati gresku
            if (context.ActionArguments.Values.Contains(null))
                context.Result = new BadRequestObjectResult(
                    "The request is invalid. One of the required parameters is null");
        }
    }
}
