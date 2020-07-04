using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Services.Result
{
    /// <summary>
    /// Rezultat koji predstavlja gresku kada entitet koji se kreira vec postoji
    /// </summary>
    public class ExistingEntityServiceResult : ServiceResult
    {
        /// <summary>
        /// Ovo stanje nije "ok"
        /// </summary>
        public override bool IsOk { get { return false; } }

        public override int Code { get { return 400; } }

        /// <summary>
        /// Konstruktor koji prihvaca ime kolone u kojoj postoji konflikt
        /// </summary>
        /// <param name="propetyName">Ime kolone u kojoj postoji konflikt</param>
        public ExistingEntityServiceResult(String propetyName)
        {
            this._message = String.Format("Entity already exists, conflict in property '{0}'.", propetyName);
        }

        /// <summary>
        /// Visitor pattern za ovaj rezultat, poziva se VisitExistingEntity metoda
        /// </summary>
        public override TRet Visit<TIn, TRet>(IServiceResultVisitor<TIn, TRet> visitor)
        {
            return visitor.VisitExistingEntity(this);
        }
    }
}
