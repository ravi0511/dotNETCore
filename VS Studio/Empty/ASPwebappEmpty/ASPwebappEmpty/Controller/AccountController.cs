using ASPwebappEmpty.Model;
using ASPwebappEmpty.ViewData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace ASPwebappEmpty
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public SignInManager<ApplicationUser> SignInManager { get; }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {

            return View();
        }

        [AcceptVerbs("Get","Post")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckEmail(string UserName)
        {
            var user = await userManager.FindByEmailAsync(UserName);
            if (user != null)
            {
                return Json($"Email {UserName} already exists.");
            }
            else
            {
                return Json(true);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var userinfo = new ApplicationUser { Email = registerViewModel.UserName, UserName = registerViewModel.UserName, City = registerViewModel.City };
                var result = await userManager.CreateAsync(userinfo, registerViewModel.Password);
                if (result.Succeeded)
                {
                    if(signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("DisplayUsers", "Administrator");
                    }
                    else
                    {
                        await signInManager.SignInAsync(userinfo, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                }

                foreach (var error in result.Errors){
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login([FromQuery(Name = "ReturnUrl")] string returnUrl)
        {
            ViewBag.returnurl = returnUrl;
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                //var userinfo = new ApplicationUser { Email = loginViewModel.UserName, UserName = loginViewModel.UserName };
                var result = await signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, loginViewModel.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Error in login.");
            }
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
