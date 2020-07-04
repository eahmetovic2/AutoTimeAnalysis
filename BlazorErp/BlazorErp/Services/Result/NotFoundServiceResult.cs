using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Services.Result
{
    /// <summary>
    /// Rezultat koji predstavlja gresku kada entitet nije pronaden
    /// </summary>
    public class NotFoundServiceResult : ServiceResult
    {
        /// <summary>
        /// Ovo stanje nije "ok"
        /// </summary>
        public override bool IsOk { get { return false; } }

        public override int Code { get { return 404; } }

        /// <summary>
        /// Prazan konstruktor
        /// </summary>
        public NotFoundServiceResult()
            : base("Entity was not found.")
        {
        }

        /// <summary>
        /// Visitor pattern za ovaj rezultat, poziva se VisitNotFound metoda
        /// </summary>
        public override TRet Visit<TIn, TRet>(IServiceResultVisitor<TIn, TRet> visitor)
        {
            return visitor.VisitNotFound(this);
        }
    }
}
