using Microsoft.AspNetCore.Mvc;
using BlazorErp.Services;
using BlazorErp.Services.Definition.Korisnik;
//using Microsoft.AspNetCore.Mvc;
namespace BlazorErp.Server.Controllers.Base
{
    [Route("dashboard")]
    public class DashboardController : BaseController
    {
        private IDashboardService dashboardService;

        public DashboardController(IDashboardService dashboardService, ITokenService tokenService)
        {
            this.dashboardService = dashboardService;
        }

        [HttpGet("osnovno")]
        [ResponseCache(Duration = 10080, Location = ResponseCacheLocation.Any, VaryByHeader = "X-AUTH-TOKEN")]
        public IActionResult OsnovnoDashboard()
        {
            var result = dashboardService.OsnovnoDashboard();

            return Convert(result);
        }

        [HttpGet("header")]
        [ResponseCache(Duration = 10080, Location = ResponseCacheLocation.Any, VaryByHeader = "X-AUTH-TOKEN")]
        public IActionResult Header()
        {
            var result = dashboardService.Header();

            return Convert(result);
        }
       

      
    }
}
