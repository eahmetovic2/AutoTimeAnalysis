using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Services.Result
{
    /// <summary>
    /// Rezultat koji predstavlja ok stanje
    /// </summary>
    /// <typeparam name="T">Tip rezultata</typeparam>
    public class OkServiceResult<T> : ServiceResult<T>
    {
        /// <summary>
        /// Vrijednost rezultata
        /// </summary>
        private T _value;

        /// <summary>
        /// Vrijednost rezultata
        /// </summary>
        public override T Value { get { return _value; } }

        /// <summary>
        /// Ovo stanje jeste "ok"
        /// </summary>
        public override bool IsOk { get { return true; } }

        /// <summary>
        /// Poruka rezultata je samo "OK"
        /// </summary>
        public override String Message { get { return "OK"; } }

        public override int Code { get { return 200; } }

        /// <summary>
        /// Konstruktor koji postavlja vrijednost rezultata
        /// </summary>
        /// <param name="value">Vrijednost rezultata</param>
        public OkServiceResult(T value)
        {
            this._value = value;
        }

        /// <summary>
        /// Visitor pattern za ovaj rezultat, poziva se VisitOk metoda
        /// </summary>
        public override TRet Visit<TIn, TRet>(IServiceResultVisitor<TIn, TRet> visitor)
        {
            return visitor.VisitOk(this as OkServiceResult<TIn>);
        }
    }

    /// <summary>
    /// Rezultat koji predstavlja ok stanje, ali je prazan
    /// </summary>
    public class OkServiceResult : OkServiceResult<Nothing>
    {
        /// <summary>
        /// Prazan konstruktor
        /// </summary>
        public OkServiceResult()
            : base(null)
        {
        }
    }
}
