using BlazorErp.Shared.Models.Response.Token;
using BlazorErp.Shared.Models.Request.Korisnik.Token;
using BlazorErp.Services.Result;
using System;
using System.Collections.Generic;
using System.Text;
using BlazorErp.Shared.Models.Request.Korisnik;
using BlazorErp.Entities.Models;

namespace BlazorErp.Services.Definition.Korisnik
{
    /// <summary>
    /// Servis koji radi sa tokenima
    /// </summary>
    public interface ITokenService : IService
    {
        /// <summary>
        /// Vrati listu svih tokena za korisnika sa datim korisnickim imenom
        /// </summary>
        /// <param name="korisnickoIme">Korisnicko ime korisnika ciji se tokeni traze</param>
        /// <returns>Model liste aktivnih tokena</returns>
        ServiceResult<TokenListModel> VratiTokenePoKorisnickomImenu(String korisnickoIme, ListaTokenaRequestModel model);

        /// <summary>
        /// Validiraj i vrati model tokena osobe sa datim korisnickim imenom i datim id-om
        /// </summary>
        /// <param name="korisnickoIme">Korisnicko ime korisnika</param>
        /// <param name="tokenId">Id tokena koji se trazi</param>
        /// <returns>Model trazenog tokena</returns>
        ServiceResult<TokenModel> VratiTokenPoIdu(String korisnickoIme, Guid tokenId);

        /// <summary>
        /// Provjeri da li je token validan
        /// Ujedno postavi polja ip i klijent za taj token
        /// </summary>
        /// <param name="tokenId">Id tokena koji se validira</param>
        /// <param name="ip">Ip adresa korisnika koji pokusava da koristi token</param>
        /// <param name="klijent">Klijent korisnika koji pokusava da koristi token</param>
        /// <returns>Model tokena koji se validirao, ako je validan</returns>
        ServiceResult<TokenModel> ValidirajToken(Guid tokenId, String ip, String klijent);

        /// <summary>
        /// Kreira token sa datim podacima
        /// </summary>
        /// <param name="korisnickoIme">Korisnicko ime korisnika za kojeg se kreira token</param>
        /// <param name="ip">Ip adresa korisnika koji kreira token</param>
        /// <param name="klijent">Klijent korisnika koji kreira token</param>
        /// <returns>Model kreiranog tokena</returns>
        ServiceResult<TokenModel> KreirajToken(String korisnickoIme, int ulogaId, String ip, String klijent, Core.Constants.TokenTip Tip = Core.Constants.TokenTip.Sesija);

        /// <summary>
        /// Obrisi token sa datim korisnickim imenom i id-om
        /// </summary>
        /// <param name="korisnickoIme">Korisnicko ime korisnika kojem pripada token</param>
        /// <param name="tokenId">Id tokena koji se brise</param>
        /// <returns>Info o uspjehu operacije</returns>
        ServiceResult<Nothing> ObrisiToken(String korisnickoIme, Guid tokenId);

		int BrojOnlineKorisnika();
    }
}
