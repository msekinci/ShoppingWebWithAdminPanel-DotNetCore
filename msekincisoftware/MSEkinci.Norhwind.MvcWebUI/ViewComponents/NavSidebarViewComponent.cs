using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using MSEkinci.Northwind.MvcWebUI.Entities;
using MSEkinci.Northwind.MvcWebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MSEkinci.Northwind.MvcWebUI.ViewComponents
{
    public class NavSidebarViewComponent : ViewComponent
    {
        private UserManager<CustomIdentityUser> _userManager;

        public NavSidebarViewComponent(UserManager<CustomIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            var currentUser = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            NavSidebarViewModel model = new NavSidebarViewModel
            {
                Username = currentUser.UserName
            };
            return View(model);
        }
    }
}
