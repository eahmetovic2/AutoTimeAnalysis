using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Services.Result
{
    /// <summary>
    /// Rezultat koji predstavlja genericnu gresku
    /// </summary>
    public class ErrorServiceResult : ServiceResult
    {
        /// <summary>
        /// Ovo stanje nije "ok"
        /// </summary>
        public override bool IsOk { get { return false; } }

        public override int Code { get { return 400; } }

        /// <summary>
        /// Konstruktor koji prihvaca opis greske
        /// </summary>
        /// <param name="error">Opis greske</param>
        public ErrorServiceResult(String error, bool prefix = true)
        {
            if (prefix)
            {
                this._message = String.Format("Greška: {0}", error);
            }
            else
            {
                this._message = String.Format("{0}", error);
            }
        }

        /// <summary>
        /// Visitor pattern za ovaj rezultat, poziva se VisitError metoda
        /// </summary>
        public override TRet Visit<TIn, TRet>(IServiceResultVisitor<TIn, TRet> visitor)
        {
            return visitor.VisitError(this);
        }
    }
}
