using BlazorErp.Shared.Models.Request.LogAkcija;
using BlazorErp.Services;
//using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlazorErp.Server.Controllers.Base
{
    /// <summary>
    /// Kontroler za prikaz logova
    /// </summary>
    [Route("log")]
    public class LogController : BaseController
    {
        private ILogService logService;

        /// <summary>
        /// Konstruktor kontrolera
        /// </summary>
        public LogController(ILogService logService)
        {
            this.logService = logService;
       
        }

        [HttpGet("akcija")]
        public IActionResult VratiSveAkcije([FromQuery] ListLogAkcijaRequestModel model)
        {
            var rezultat = logService.VratiSveAkcije(model);
            return Convert(rezultat);
        }

        [ResponseCache(Duration = 10080, Location = ResponseCacheLocation.Client)]
        [HttpGet("log_level")]
        public IActionResult VratiLogLevele()
        {
            var rezultat = logService.VratiLogLevele();
            return Ok(rezultat);
        }

        [ResponseCache(Duration = 10080, Location = ResponseCacheLocation.Client)]
        [HttpGet("log_kategorija")]
        public IActionResult VratiLogKategorije()
        {
            var rezultat = logService.VratiLogKategorije();
            return Ok(rezultat);
        }

        [ResponseCache(Duration = 10080, Location = ResponseCacheLocation.Client)]
        [HttpGet("log_akcija")]
        public IActionResult VratiLogAkcije()
        {
            var rezultat = logService.VratiLogAkcije();
            return Ok(rezultat);
        }

    }
}
