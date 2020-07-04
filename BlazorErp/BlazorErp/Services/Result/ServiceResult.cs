using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Services.Result
{
    /// <summary>
    /// Klasa koja predstavlja povratnu vrijednost servisa
    /// Povratna vrijednost moze da ima nekoliko tipova (slicno IActionResult, ali genericno)
    /// </summary>
    /// <typeparam name="T">Tip rezultata</typeparam>
    public abstract class ServiceResult<T>
    {
        /// <summary>
        /// Vrijednost rezultata
        /// </summary>
        public abstract T Value { get; }

        /// <summary>
        /// Rezultat predstavlja "ok" stanje
        /// </summary>
        public abstract bool IsOk { get; }

        /// <summary>
        /// Oznacava status kod (trenutno se koristi samo za forbidden)
        /// </summary>
        public abstract int Code { get; }

        /// <summary>
        /// Poruka rezultata
        /// </summary>
        public abstract String Message { get; }

        /// <summary>
        /// Visitor patern za konverziju i inspekciju rezultata
        /// </summary>
        /// <typeparam name="TIn">Tip koji je okruzen ServiceResult klasom koja se visit-ira</typeparam>
        /// <typeparam name="TRet">Tip koji visitor treba da vrati</typeparam>
        /// <param name="visitor">Visitor koji ce da visit-ira ovu klasu</param>
        /// <returns>Rezultat koji vrati visitor nakon sto visit-ira ovu klasu</returns>
        public abstract TRet Visit<TIn, TRet>(IServiceResultVisitor<TIn, TRet> visitor);

        /// <summary>
        /// Implicitni konvertor koji pretvara prazne (Nothing) rezultate u rezultate sa pravim tipom
        /// </summary>
        /// <param name="result">Rezultat sa tipom koji obuhvata stvarni rezultat koji je prazan</param>
        public static implicit operator ServiceResult<T>(ServiceResult<Nothing> result)
        {
            // koristi se wrapper visitor koji kreira instancu ServiceResultWrapper
            // mogao bi se koristiti i konstruktor ServiceResultWrapper klase
            // ali ovako imamo vise kontrole
            var wrapperVisitor = new ServiceResultWrapperVisitor<T>();
            return result.Visit(wrapperVisitor);
        }
    }

    /// <summary>
    /// Implamentacija praznog rezultata
    /// </summary>
    public abstract class ServiceResult : ServiceResult<Nothing>
    {
        /// <summary>
        /// Poruka rezultata
        /// </summary>
        protected String _message;

        /// <summary>
        /// Prazan rezultat nema vrijednost, vrati null
        /// </summary>
        public override Nothing Value { get { return null; } }

        /// <summary>
        /// Poruka rezultata
        /// </summary>
        public override String Message { get { return _message; } }

        /// <summary>
        /// Prazan konstruktor
        /// </summary>
        public ServiceResult()
        {
        }

        /// <summary>
        /// Konstruktor koji postavlja poruku
        /// </summary>
        /// <param name="message">Poruka rezultata</param>
        public ServiceResult(String message)
        {
            this._message = message;
        }
    }
}
