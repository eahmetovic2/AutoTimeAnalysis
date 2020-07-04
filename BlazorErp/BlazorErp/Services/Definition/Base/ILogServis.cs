using BlazorErp.Core.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using BlazorErp.Shared.Models.Request.LogAkcija;
using BlazorErp.Services.Result;
using BlazorErp.Shared.Models.Response.LogAkcija;
using BlazorErp.Shared.Models.Response.Sifarnik;
using BlazorErp.Shared.Models.Response.LogEntitet;

namespace BlazorErp.Services
{
    public interface ILogService
    {
        /// <summary>
        /// Dodaje info zapis u log
        /// </summary>
        /// <param name="kategorija">Kategorija</param>
        /// <param name="akcija">Akcija</param>
        /// <param name="opis">Opis koji će biti prikazan korisniku</param>
        void InfoAkcija(LogKategorija kategorija, LogAkcija akcija, string opis);

        /// <summary>
        /// Dodaje upozorenje u log
        /// </summary>
        /// <param name="kategorija">Kategorija</param>
        /// <param name="akcija">Akcija</param>
        /// <param name="opis">Opis koji će biti prikazan korisniku</param>
        void UpozorenjeAkcija(LogKategorija kategorija, LogAkcija akcija, string opis);
      
        /// <summary>
        /// Dodaje grešku u log
        /// </summary>
        /// <param name="kategorija">Kategorija</param>
        /// <param name="akcija">Akcija</param>
        /// <param name="opis">Opis koji će biti prikazan korisniku</param>
        void GreskaAkcija(LogKategorija kategorija, LogAkcija akcija, string opis);
       
        /// <summary>
        /// Dodaje zapis u log
        /// </summary>
        /// <param name="level"></param>
        /// <param name="kategorija"></param>
        /// <param name="akcija"></param>
        /// <param name="opis"></param>
        void Akcija(LogLevel level, LogKategorija kategorija, LogAkcija akcija, string opis);

        /// <summary>
        ///  Dodaje zapis u log
        /// </summary>
        /// <param name="level"></param>
        /// <param name="kategorija"></param>
        /// <param name="akcija"></param>
        /// <param name="opis"></param>
        /// <param name="korisnickoIme"></param>
        void Akcija(LogLevel level, LogKategorija kategorija, LogAkcija akcija, string opis, string korisnickoIme);
              
        /// <summary>
        /// Vaća filtrirane akcije po kriteriju
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ServiceResult<LogAkcijaListModel> VratiSveAkcije(ListLogAkcijaRequestModel model);

        /// <summary>
        /// Vraća šifrarnik log akcija
        /// </summary>
        /// <returns></returns>
        List<SifarnikModel> VratiLogAkcije();

        /// <summary>
        /// Vraća šifrarnik log levela
        /// </summary>
        /// <returns></returns>
        List<SifarnikModel> VratiLogLevele();

        /// <summary>
        /// Vraća šifrarnik log kategorija
        /// </summary>
        /// <returns></returns>
        List<SifarnikModel> VratiLogKategorije();            
    }
}
