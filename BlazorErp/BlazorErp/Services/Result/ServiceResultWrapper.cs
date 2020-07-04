using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Services.Result
{
    /// <summary>
    /// Rezultat koji se koristi za obuhvatanje praznog rezultata
    /// </summary>
    /// <typeparam name="T">Tip rezultata koji se trazi</typeparam>
    public class ServiceResultWrapper<T> : ServiceResult<T>
    {
        /// <summary>
        /// Obuhvaceni prazni rezultat
        /// </summary>
        public ServiceResult<Nothing> InnerResult { get; private set; }

        /// <summary>
        /// Vrijednost obuhvacenog rezultata nije korektnog tipa i ignorise se
        /// Vrati default vrijednost
        /// </summary>
        public override T Value { get { return default(T); } }

        /// <summary>
        /// Da li rezultat predstavlja "ok" stanje
        /// Proslijedi vrijednost koja je obuhvacena
        /// </summary>
        public override bool IsOk { get { return InnerResult.IsOk; } }

        /// <summary>
        /// Poruka rezultata
        /// Proslijedi vrijednost koja je obuhvacena
        /// </summary>
        public override String Message { get { return InnerResult.Message; } }

        public override int Code { get { return InnerResult.Code; } }

        /// <summary>
        /// Konstruktor koji prihvaca obuhvaceni rezultat
        /// </summary>
        /// <param name="innerResult">Rezultat koji se obuhvaca</param>
        public ServiceResultWrapper(ServiceResult<Nothing> innerResult)
        {
            this.InnerResult = innerResult;
        }

        /// <summary>
        /// Visitor pattern za ovu klasu
        /// Proslijeduje se obuhvacenom rezultatu
        /// </summary>
        /// <typeparam name="TIn">Tip rezultata koji se visit-ira</typeparam>
        /// <typeparam name="TRet">Tip koji visitor treba da vrati</typeparam>
        /// <param name="visitor">Visitor koji ce da visit-ira ovu klasu</param>
        /// <returns>Rezultat koji vrati visitor nakon sto visit-ira obuhvaceni rezultat</returns>
        public override TRet Visit<TIn, TRet>(IServiceResultVisitor<TIn, TRet> visitor)
        {
            return InnerResult.Visit(visitor);
        }
    }
}
