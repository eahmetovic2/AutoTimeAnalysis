using BlazorErp.Shared.Request.Korisnik;
using BlazorErp.Shared.Result.Korisnik;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorErp.Client.Auth
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
        Task<RegisterResult> Register(RegisterModel registerModel);
        Task<AuthenticationState> CurrentUser();
    }
}
