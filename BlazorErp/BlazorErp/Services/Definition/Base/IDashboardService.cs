using BlazorErp.Shared.Models.Response.Dashboard;
using BlazorErp.Services.Result;

namespace BlazorErp.Services
{
    public interface IDashboardService : IService
    {
        ServiceResult<DashboardModel> OsnovnoDashboard();

        ServiceResult<HeaderModel> Header();

    }
}
