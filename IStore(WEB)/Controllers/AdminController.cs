using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Service;
using Business.Service.FileService;
using Business.Service.Interfaces;
using Domain.EF_Models;
using IStore_WEB_.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IStore_WEB_.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly ProductCharacteristicService _productCharacteristicService;
        private readonly BrandService _brandService;
        private readonly CategoryService _categoryService;
        private readonly ProductService _productService;
        private readonly ImageFileService _fileService;
        private readonly ImportExportService _importExportService;

        public AdminController(ProductCharacteristicService productCharacteristicService, ImageFileService fileService,
            BrandService brandService, CategoryService categoryService, ProductService productService, ImportExportService importExportService)
        {

            _fileService = fileService;
            _brandService = brandService;
            _categoryService = categoryService;
            _productService = productService;
            _productCharacteristicService = productCharacteristicService;
            _importExportService = importExportService;
        }

        //[AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddCharacteristic()
        {
            return PartialView("ProductCharacteristicPartialView");
        }

        //[HttpPost]
        //public async Task GetCharacteristicAsync(string parameters)
        //{
        //    await _productCharacteristicService.SaveGroupAsync((IEnumerable<ProductCharacteristic>)JsonConvert
        //        .DeserializeObject<List<ProductCharacteristic>>(parameters));
        //}

        public async Task<IActionResult> AddImage()
        {
            return PartialView("ImagePartialView");
        }


        //[HttpPost]
        //public async Task AddFile(IFormFileCollection uploads)
        //{
        //    await _fileService.Save(uploads);
        //}

        public async Task<IActionResult> AddProduct()
        {
            var categories = await _categoryService.GetAllAsync();
            var brands = await _brandService.GetAllAsync();
            ViewBag.Categories = categories.Select(x => x.Title);
            ViewBag.Brands = brands.Select(x => x.Name);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(IFormCollection formCode)
        {
            try
            {
                var res = await _productService.CreateAsync(formCode);
                return Json(res.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Json("Add photo preview");
            }
            catch (JsonSerializationException ex)
            {
                return Json("Fill all fields");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public IActionResult AddCategory()
        {
            return PartialView("AddCategoryPartialView");
        }

        [HttpPost]
        public async Task<IActionResult> SaveCategory(IFormCollection formCode)
        {
            await _categoryService.CreateAsync(formCode);

            var result = (await _categoryService.GetAllAsync()).Select(x => x.Title).ToList();

            return Json(result);
        }

        public IActionResult AddBrand()
        {
            return PartialView("AddBrandPartialView");
        }

        [HttpPost]
        public async Task<IActionResult> SaveBrand(Brand brand)
        {
            await _brandService.CreateAsync(brand);

            var result = (await _brandService.GetAllAsync()).Select(x => x.Name).ToList();

            return Json(result);
        }

        public IActionResult ProductEdit()
        {
            return View();
        }

        public async Task<IActionResult> Categories()
        {
            return View(await _categoryService.GetAllAsync());
        }

        public async Task<IActionResult> AddSubcategory()
        {
            ViewBag.Categories = (await _categoryService.GetAllAsync()).Select(x => x.Title);
            return View();
        }

        public async Task<IActionResult> SaveSubcategory(IFormCollection formCode)
        {
            var res = await _categoryService.AddSubCategoryAsync(formCode);
            return View(res.Message);
        }


        [HttpPost]
        public async Task<IActionResult> ProductEdit(IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null || file.Length <= 0)
            {
                return View();
            }

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return View();
            }
            var list = new List<Product>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream, cancellationToken);
                list = _importExportService.ExcelToObject(stream);
            }

            foreach (var item in list)
            {
                var res2 = await _productService.CreateAsync(item);         //add range and checking on valid
            }

            var res = await _productService.FindByConditionAsync(pr => pr.RetailPrice == 6);// типа идентити))

            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> UploadProducts(string products)
        {
            if (products != null)
            {
                List<Product> listProducts = JsonConvert.DeserializeObject<List<Product>>(products);
                await _productService.UpdateProductsAsync(listProducts);
            }
            return View();
        }


    }
}