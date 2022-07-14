using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IStore_WEB_.Controllers
{
    [Authorize(Roles = "user")]
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}