using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Services.Result
{
    /// <summary>
    /// Rezultat koji predstavlja gresku validacije operacije
    /// </summary>
    public class ForbiddenServiceResult : ServiceResult
    {
        /// <summary>
        /// Ovo stanje nije "ok"
        /// </summary>
        public override bool IsOk { get { return false; } }

        public override int Code { get { return 403; } }

        /// <summary>
        /// Konstruktor koji prima opis greske
        /// </summary>
        /// <param name="error">Opis greske</param>
        public ForbiddenServiceResult()
        {
            this._message = "Akcija je zabranjena.";
        }

        /// <summary>
        /// Visitor pattern za ovaj rezultat, poziva se VisitValidationError metoda
        /// </summary>
        public override TRet Visit<TIn, TRet>(IServiceResultVisitor<TIn, TRet> visitor)
        {
            return visitor.VisitForbiddenError(this);
        }
    }
}
