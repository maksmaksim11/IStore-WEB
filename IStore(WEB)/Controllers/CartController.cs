using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Business.Service;
using DataAccess.UnitOfWork;
using Domain.EF_Models;
using IStore_WEB_.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IStore_WEB_.Controllers
{
    public class CartController : Controller
    {
        private readonly OrderService _orderService;
        private readonly UserManager<User> _userManager;
        public CartController(OrderService orderService, UserManager<User> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }
        public async Task<string> TestOrder(string parameters)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var list = JsonConvert.DeserializeObject<List<ProductViewModel>>(parameters);
                var order = new Order();
                order.Date = DateTime.Now;
                order.User = user;
                foreach (var item in list)
                {
                    var det = new OrderDetails();
                    det.Count = item.Count;
                    det.ProductId = item.Id;
                    order.Products.Add(det);
                }
                await _orderService.CreateAsync(order);
                return "ok";
            }
            return "not";
        }
        public async Task<string> GetOrder()
        {
            if (User.Identity.IsAuthenticated)
            {
                var list = new List<ProductViewModel>();
                var res = (await _orderService.FindByConditionAsync(x => x.User.Email == User.Identity.Name)).LastOrDefault().Products;
                foreach (var item in res)
                {
                    var prod = new ProductViewModel();
                    prod.Count = item.Count;
                    prod.Id = item.Product.Id;
                    prod.Model = item.Product.Model;
                    prod.PreviewImage = item.Product.PreviewImage;
                    prod.RetailPrice = (double)item.Product.RetailPrice;
                    prod.Title = item.Product.Title;
                    list.Add(prod);
                }
                return JsonConvert.SerializeObject(list);
            }
            return null;
        }
        public async Task<IActionResult> ShoppingCart()
        {
            return View();
        }
        public async Task<IActionResult> Checkout()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else return RedirectToAction("LoginRegister", "Account");
        }
        public async Task<IActionResult> CheckoutProductPartial(string parameters)
        {
            if (parameters != "[]")
                return PartialView(JsonConvert.DeserializeObject<List<ProductViewModel>>(parameters));
            return null;
        }
        public async Task<IActionResult> ShoppingCartPartial(string parameters)
        {
            if (parameters != "[]")
                return PartialView(JsonConvert.DeserializeObject<List<ProductViewModel>>(parameters));
            return null;
        }
        public async Task<IActionResult> ShoppingCartProductsPartial(string parameters)
        {
            if (parameters != "[]")
                return PartialView(JsonConvert.DeserializeObject<List<ProductViewModel>>(parameters));
            return null;
        }
    }
}