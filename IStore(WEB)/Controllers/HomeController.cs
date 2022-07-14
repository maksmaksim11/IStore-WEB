using Business.Service;
using Business.Service.NewsService;
using Domain.EF_Models;
using IStore_WEB_.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Diagnostics;
using System.Net.Mail;
using System.Threading.Tasks;

namespace IStore_WEB_.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductService _productservice;
        private readonly NewsSenderService _newsSenserService;

        public HomeController(ILogger<HomeController> logger, ProductService productservice, NewsSenderService newsSenserService)
        {
            _logger = logger;
            _productservice = productservice;
            _newsSenserService = newsSenserService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetProducts()
        {
            return Json(await _productservice.GetSortByRatingAsync(8));
        }

        [HttpPost]
        public async Task<IActionResult> GetProductsAfterId(int id)
        {
            return Json(await _productservice.GetAfterIdAsync(id));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> ProductPartial(Product product)
        {
            return View(product);
        }
        public IActionResult GetProductDetails(string parameters)
        {
            if (parameters != "[]")
                return PartialView("ProductDetails", JsonConvert.DeserializeObject<Product>(parameters));
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> SubscribeNews(string email)
        {
            try
            {
                var mailAdress = new MailAddress(email);
                var res = await _newsSenserService.AddObserver(email);
                return Json(res.Message);
            }
            catch(Exception ex)
            {
                return Json(ex.Message);
            }
            
           
        }
    }
}