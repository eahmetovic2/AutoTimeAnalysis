using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Services.Result
{
    /// <summary>
    /// Rezultat koji predstavlja gresku validacije operacije
    /// </summary>
    public class ValidationErrorServiceResult : ServiceResult
    {
        /// <summary>
        /// Ovo stanje nije "ok"
        /// </summary>
        public override bool IsOk { get { return false; } }

        public override int Code { get { return 400; } }

        /// <summary>
        /// Konstruktor koji prima opis greske
        /// </summary>
        /// <param name="error">Opis greske</param>
        public ValidationErrorServiceResult(String error)
        {
            this._message = String.Format("Greška tokom validacije: {0}", error);
        }

        /// <summary>
        /// Visitor pattern za ovaj rezultat, poziva se VisitValidationError metoda
        /// </summary>
        public override TRet Visit<TIn, TRet>(IServiceResultVisitor<TIn, TRet> visitor)
        {
            return visitor.VisitValidationError(this);
        }
    }
}
