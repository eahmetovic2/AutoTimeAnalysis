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
    /// Atribut koji provjerava da li je model validan
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // ako model nije validan, vrati gresku
            if (context.ModelState.IsValid == false)
                context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}
