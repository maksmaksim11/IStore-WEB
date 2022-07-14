using Domain.EF_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IStore_WEB_.Controllers
{
    public class EmailConfirmController : Controller
    {
        private readonly UserManager<User> _userManager;

        public EmailConfirmController(UserManager<User> userManager)
        {
            _userManager = userManager;

        }
        public async Task<IActionResult> Confirm(string guid,string userEmail)
        {  
            var user = await _userManager.FindByNameAsync(userEmail);
            // add to null
            var res = await _userManager.ConfirmEmailAsync(user, guid);
            if (res.Succeeded)
            {
                return RedirectToAction("Index","Home");
            }
            //todo add error page; 
            return View();
        }
    }
}
