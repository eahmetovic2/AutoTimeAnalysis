using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorErp.Server.Core.Constants
{
    /// <summary>
    /// Level za log
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Default level koji ne treba da bude korišten
        /// </summary>
        Nijedan = 0,
        Info = 3,
        Upozorenje = 2,
        Greska = 1,
    }
}
