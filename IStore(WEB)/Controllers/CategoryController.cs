using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Service;
using Microsoft.AspNetCore.Mvc;

namespace IStore_WEB_.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

      
        public async Task<IActionResult> CategoryPartialAsync()
        {
            var res = await _categoryService.GetAllAsync();
            return PartialView(res.ToList());
        }

        public async Task<IActionResult> GetCategoryJsonAsync()
        {
            var categoryCollection = await _categoryService.GetAllAsync();
            var res = Json(categoryCollection);
            return res;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetCategoryTitle()
        {
            return Json((await _categoryService.GetAllAsync()).Select(x=>x.Title));
        }
    }
}
