using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IStore_WEB_.Controllers
{
    public class InformationController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }
        public async Task SendMessage(string your_name, string your_email, string your_phone, string your_company, string your_message)
        {
            

        }
    }
}