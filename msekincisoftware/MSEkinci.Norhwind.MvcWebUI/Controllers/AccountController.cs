using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MSEkinci.Northwind.MvcWebUI.Entities;
using MSEkinci.Northwind.MvcWebUI.ViewModels;

namespace MSEkinci.Northwind.MvcWebUI.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<CustomIdentityUser> _userManager;
        private RoleManager<CustomIdentityRole> _roleManager;
        private SignInManager<CustomIdentityUser> _signInManager;

        public AccountController(
            UserManager<CustomIdentityUser> userManager, 
            RoleManager<CustomIdentityRole> roleManager, 
            SignInManager<CustomIdentityUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false).Result;
                //false: yanlış girilmesi durumunda hesap kilitlensin mi
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            
            ModelState.AddModelError("", "Invalid login!");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            CustomIdentityUser user = new CustomIdentityUser
            {
                UserName = model.UserName,
                Email = model.Email
            };
            IdentityResult result = _userManager.CreateAsync(user, model.Password).Result;

            if (result.Succeeded)
            {
                if (!_roleManager.RoleExistsAsync("Admin").Result)
                {
                    CustomIdentityRole role = new CustomIdentityRole
                    {
                        Name = "Admin"
                    };

                    IdentityResult roleResult = _roleManager.CreateAsync(role).Result;

                    if (!roleResult.Succeeded)
                    {
                        ModelState.AddModelError("", "We can't add the role!");
                        return View(model);
                    }
                }
                _userManager.AddToRoleAsync(user, "Admin").Wait();
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }

        public IActionResult LogOff()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction("Login");
        }
    }
}