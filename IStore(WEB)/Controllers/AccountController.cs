using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Business.Service;
using Domain.EF_Models;
using IStore_WEB_.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace IStore_WEB_.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly HashService _hashService;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, IEmailSender emailSender, HashService hashService, RoleManager<IdentityRole> roleManager)
        {
            _emailSender = emailSender;
            _signInManager = signInManager;
            _userManager = userManager;
            _hashService = hashService;
            _roleManager = roleManager;
        }

        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> LoginRegister()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        
        public async Task<IActionResult> Register(RegisterViewModel model, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
                
                var user = new User() { Email = model.Login, UserName = model.Login };
                var result = await _userManager.CreateAsync(user, model.Password);
               
                if (result.Succeeded)
                {
                    if (await _roleManager.FindByNameAsync("user") == null)
                    {
                        var role = await _roleManager.CreateAsync(new IdentityRole("user"));
                        if (role.Succeeded)
                            await _userManager.AddToRoleAsync(user, "user");
                    }
                    else
                        await _userManager.AddToRoleAsync(user, "admin");

                    await _signInManager.SignInAsync(user, false);

                    if (!String.IsNullOrEmpty(ReturnUrl))
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationLink = Url.Action("", "confirmation", new { guid=token, userEmail = user.Email }, Request.Scheme,Request.Host.Value);

                        await _emailSender.SendEmailAsync(user.Email, "LINK->", confirmationLink);
                        if (ReturnUrl == "/")
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            if (Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            }

                        }
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }

            return new EmptyResult();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
      
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.IsRememberMe, false);

                if (result.Succeeded)
                {
                    if (!String.IsNullOrEmpty(ReturnUrl))
                    {
                        var hash = await _hashService.GetProductsHashByUserLogin(model.Login);
                        if (ReturnUrl == "/")
                        {
                            return RedirectToAction("Index", "Home", hash.ToString());
                        }
                        else
                        {
                            if (Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl + "#" + hash.ToString());
                            }

                        }
                    }
                }
                else
                {
                    return Json("USER NOT FOUND");
                }
            }

            return Json("MODEL NO VALID");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Logout(string ReturnUrl = "")
        {
            await _signInManager.SignOutAsync();

            if (!String.IsNullOrEmpty(ReturnUrl))
            {
                if (ReturnUrl == "/")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }

                }
            }

            //todo redirect error page 
            return new EmptyResult();
        }

        
    }
}