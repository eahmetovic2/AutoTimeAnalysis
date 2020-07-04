using BlazorErp.Core.Constants;
using BlazorErp.Entities.Models.Korisnik;
using BlazorErp.Shared.Models.Response.Dashboard;
using BlazorErp.Services.Result;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BlazorErp.Entities;

namespace BlazorErp.Services.Implementation
{
    public class DashboardService : IDashboardService
    {
        private readonly Context _context;
        private readonly ITokenService _tokenService;
        private readonly IAuthService authService;

        public DashboardService(Context context, ITokenService tokenService, IAuthService authService)
        {
            _context = context;
            _tokenService = tokenService;
            this.authService = authService;
  
        }

        public ServiceResult<HeaderModel> Header()
        {
            var korisnik = authService.TrenutniKorisnik();        
            
            HeaderModel model = null;
       

            return new OkServiceResult<HeaderModel>(model);
        }

        public ServiceResult<DashboardModel> OsnovnoDashboard()
        {
            DashboardModel model = null;
            var trutniKonrisnik = authService.TrenutniKorisnik();
          
            return new OkServiceResult<DashboardModel>(model);
        }

    }
}