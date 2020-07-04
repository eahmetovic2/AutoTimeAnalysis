using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Services.Result
{
    /// <summary>
    /// Rezultat koji predstavlja gresku kada neki povezani entitet nije pronaden
    /// </summary>
    public class MissingEntityServiceResult : ServiceResult
    {
        /// <summary>
        /// Ovo stanje nije "ok"
        /// </summary>
        public override bool IsOk { get { return false; } }

        public override int Code { get { return 404; } }

        /// <summary>
        /// Konstruktor koji prihvaca ime povezanog entiteta
        /// </summary>
        /// <param name="name">Ime povezanog entiteta</param>
        public MissingEntityServiceResult(String name)
        {
            this._message = String.Format("Dependent entity '{0}' is missing and was not found.", name);
        }

        /// <summary>
        /// Visitor pattern za ovaj rezultat, poziva se VisitMissingEntity metoda
        /// </summary>
        public override TRet Visit<TIn, TRet>(IServiceResultVisitor<TIn, TRet> visitor)
        {
            return visitor.VisitMissingEntity(this);
        }
    }
}