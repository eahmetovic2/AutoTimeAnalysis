using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorErp.Core.Constants
{
    /// <summary>
    /// Uloga korisnika u sistemu
    /// </summary>
    public enum Uloga
    {
        /// <summary>
        /// Uloga nije odredena, ne bi trebalo da se desi
        /// </summary>
        Neodredeno = 0,

        /// <summary>
        /// Korisnik je administrator sistema
        /// </summary>
        Administrator = 1
    }
}

