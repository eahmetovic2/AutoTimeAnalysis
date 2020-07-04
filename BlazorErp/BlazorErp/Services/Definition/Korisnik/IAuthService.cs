using BlazorErp.Entities.Models;
using BlazorErp.Entities.Models.Korisnik;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Services
{
    /// <summary>
    /// Daje mogucnost pristupanja podacima trento logovanog korisnika
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Dobavlja trenutno logovanog korisnika iz baze
        /// </summary>
        /// <returns></returns>
        IdentityKorisnik TrenutniKorisnik();
    }
}
