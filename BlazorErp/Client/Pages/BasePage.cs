using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorErp.Client.Auth;
using Microsoft.AspNetCore.Components;

namespace BlazorErp.Client.Pages
{
    public class BasePage : ComponentBase
    {
        [Inject]
        protected IAuthService authService { get; set; }
        [Inject]
        protected NavigationManager navigationManager { get; set; }

        //protected override void OnInitialized()
        //{
        //    var result = authService.CurrentUser();
        //    Console.WriteLine("Normal: " + result);
        //}
        protected override async Task OnInitializedAsync()
        {
            var result = await authService.CurrentUser();
            //Console.WriteLine("Async: " + result.User.Identity.IsAuthenticated);
            if(!result.User.Identity.IsAuthenticated)
            {
                navigationManager.NavigateTo("login");
            }
        }
    }
}