using BlazorErp.Entities.Models;
using BlazorErp.Entities.Models.Korisnik;
using BlazorErp.Shared.Models.Request.Korisnik;
using BlazorErp.Shared.Models.Response.Korisnik;
using BlazorErp.Shared.Models.Response.Token;
using BlazorErp.Services.Result;
using System;
using System.Collections.Generic;
using System.Text;
using BlazorErp.Shared.Models.Response.Korisnik.Korisnik;
using BlazorErp.Shared.Models.Request.Korisnik.Korisnik;
using System.Threading.Tasks;

namespace BlazorErp.Services
{
    /// <summary>
    /// Servis koji radi sa korisnicima
    /// </summary>
    public interface IKorisnikService : IService
    {
        /// <summary>
        /// Vrati model korisnika prema njegovom korisnickom imenu
        /// </summary>
        /// <param name="korisnickoIme">Korisnicko ime korisnika koji se trazi</param>
        /// <returns>Model trazenog korisnika</returns>
        ServiceResult<KorisnikModel> VratiKorisnikaPoKorisnickomImenu(String korisnickoIme);

        /// <summary>
        /// Azurira korisnika sa datim korisnickim imenom
        /// </summary>
        /// <param name="korisnickoIme">Korisnicko ime korisnika koji se azurira</param>
        /// <param name="model">Novi podaci korisnika</param>
        /// <returns>Model azuriranog korisnika</returns>
        ServiceResult<KorisnikModel> AzurirajKorisnika(String korisnickoIme, AzurirajKorisnikaRequestModel model);

        /// <summary>
        /// Vrati model korisnika po kriterijima
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ServiceResult<KorisnikListModel> VratiSve(ListaKorisnikaRequestModel model);

        /// <summary>
        /// Postavlja korisnika kao omogucenog ili onemogucenog
        /// </summary>
        /// <param name="korisnickoIme">Korisnicko ime korisnika koji se omogucuje/onemogucuje</param>
        /// <param name="onemogucen">Da li korisnik treba da bude omogucen ili onemogucen</param>
        /// <returns>Info o uspjehu operacije</returns>
        ServiceResult<Nothing> PostaviKorisnikOnemogucen(String korisnickoIme, bool onemogucen);

        /// <summary>
        /// Mijenja lozinku korisnika sa provjerom trenutne lozinke
        /// </summary>
        /// <param name="korisnickoIme">Korisnicko ime korisnika za kojeg se mijenja lozinka</param>
        /// <param name="staraLozinka">Stara lozinka korisnika</param>
        /// <param name="novaLozinka">Nova lozinka korisnika</param>
        /// <returns>Info o usjpjehu operacije</returns>
        Task<ServiceResult<Nothing>> PromijeniLozinku(String korisnickoIme, String staraLozinka, String novaLozinka);

        /// <summary>
        /// Postavlja lozinku korisnika bez provjere trenutne lozinke
        /// </summary>
        /// <param name="korisnickoIme">Korisnicko ime korisnika za kojeg se postavlja lozinka</param>
        /// <param name="novaLozinka">Nova lozinka korisnika</param>
        /// <returns>Info o uspjehu operacije</returns>
        Task<ServiceResult<Nothing>> PostaviLozinku(String korisnickoIme, String novaLozinka);

        /// <summary>
        /// Dodaje novog korisnika
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ServiceResult<KorisnikModel>> Kreiraj(KreirajKorisnikaRequestModel model);

        /// <summary>
        /// Provjerava da li je korektna lozinka korisnika
        /// </summary>
        /// <param name="korisnickoIme">Korisnicko ime korisnika za kojeg se provjerava lozinka</param>
        /// <param name="lozinka">Lozinka koja se provjerava</param>
        /// <returns>Info o uspjehu operacije</returns>
        ServiceResult<Nothing> ProvjeriLozinku(String korisnickoIme, String lozinka);

		
		//ServiceResult<KorisnikModel> Aktiviraj(string aktivacijskiToken);


     

        //ServiceResult<Nothing> ResetSifre(ResetSifreRequestModel model);

        ServiceResult<KorisnikModel> AzurirajLicneDetalje(String korisnickoIme, AzurirajLicneDetaljeRequestModel model);

    }
}

