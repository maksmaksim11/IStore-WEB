using AutoMapper;
using Business.Service;
using Domain.EF_Models;
using IStore_WEB_.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IStore_WEB_.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductService _productservice;
        private readonly ProductCharacteristicService _productCharacteristicService;
        private readonly CommentService _commentService;
        private readonly LikeService _likeService;
        private readonly UserManager<User> _userManager;
        private readonly CategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductController(ILogger<ProductController> logger, ProductService productservice, ProductCharacteristicService productCharacteristicService, CommentService commentService, LikeService likeService,UserManager<User> userManager,IMapper mapper, CategoryService categoryService)
        {
            _logger = logger;
            _productservice = productservice;
            _commentService = commentService;
            _categoryService = categoryService;
            this._productCharacteristicService = productCharacteristicService;
            _likeService = likeService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _productservice.GetSortByRatingAsync(24);
            return View(result);
        }

        public async Task<IActionResult> ProductCharacteristicPart(int id)
        {
            var res = await _productCharacteristicService.FindByConditionAsync(x=>x.ProductId == id);
            return View(res.FirstOrDefault());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ProductPartial(Product product)
        {
            return View(product);
        }

        public async Task<IActionResult> GetProductDetails(Product product)
        {
            var res = await _productservice.GetByIdsync(1);


            return PartialView("ProductDetails", res);
        }
        public async Task<IActionResult> Product(int id)
        {
            var res = await _productservice.GetByIdsync(id);

            return View("Product", res);
        }
       

        [Route("Products/{categoryTitle?}")]
        public IActionResult GetProductFromCategory(string categoryTitle)
        {
            var res = _productservice.GetAllAsync().Result;
            if (categoryTitle != null)
            {
                res = _productservice.FindByConditionAsync(x => x.Category.Title == categoryTitle).Result;
            }
            ViewBag.Category = _categoryService.GetAllAsync();
            return View(res); //todo refactor this
        }
        public async Task<IActionResult> GetCommentsPartial(int id)
        {

            var comments = await _commentService.GetCommentsByProductAsync(id);
            // if (User.Identity.IsAuthenticated)
            //  {
            //  var authorizedUser = await _userManager.FindByNameAsync(User.Identity.Name);
          //  var res = await _commentService.GetCommentAllIncludedAsync(1);
            var userId = "6cab4e64-cd04-4ba5-94d4-3c9b5ad2e78f";
                ViewBag.Likes = await _likeService.GetLikedCommentsIdAsync(userId, id);
                ViewBag.Dislikes = await _likeService.GetDislikedCommentsIdAsync(userId, id);
         //   }
            return PartialView(_mapper.Map<List<CommentViewModel>>(comments.Take(5)));
        }
        public async Task<IActionResult> GetSubComments(int parentId,int productId)
        {
            var userId = "6cab4e64-cd04-4ba5-94d4-3c9b5ad2e78f";
            ViewBag.Likes = await _likeService.GetLikedCommentsIdAsync(userId, productId);
            ViewBag.Dislikes = await _likeService.GetDislikedCommentsIdAsync(userId, productId);
            var subcoms = await _commentService.GetCommentAllIncludedAsync(parentId);
            return PartialView("GetCommentsPartial", _mapper.Map<List<CommentViewModel>>(subcoms.Answers.Take(5)));
        }

        public ActionResult GetInputCommentPartial(string commentId)
        {
            ViewBag.CommentId = commentId;
            return PartialView();
        }
        //[HttpPost]
        //public async Task UpdateLikes(string localStorageResult)
        //{
        //    await _likeService.ManageLikesAsync(localStorageResult);
        //}
        [HttpPost]
        public async Task UpdateLikesTotal(string localStorageResult)
        {
            var res = JsonConvert.DeserializeObject<List<Comment>>(localStorageResult);
            await _commentService.UpdateCommentLikesAsync(res);
        }

        [HttpPost]
        public async Task AddComment(string comment)
        {
            var res = JsonConvert.DeserializeObject<Comment>(comment);
            res.Date = DateTime.Now;
            await _commentService.CreateCommentAsync(res);
        }

        [HttpPost]
        public async Task<IActionResult> GetProducts (IList<string> filter)
        {          
            return Json((await _productservice.FindByConditionAsync(x => x.Title.Contains(filter[1]))).Where(c=>c.Category.Title== filter[0]).Select(obj => new
            {
                id = obj.Id,
                title = obj.Title,
                price = obj.RetailPrice,
                image = obj.PreviewImage
            }));            
              
    
            //await _commentService.UpdateCommentLikesAsync(localStorageResult);
        }

        //public async Task<IActionResult> AddComment(string comment)
        //{
        //    var commentReturned =  await _commentService.CreateCommentAsync(comment);
        //    var res = _mapper.Map<CommentViewModel>(commentReturned);
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var user = await _userManager.FindByEmailAsync(User.Identity.Name);
        //        res.CurrentUserId = user.Id;
        //    }
        //    var userId = "6cab4e64-cd04-4ba5-94d4-3c9b5ad2e78f";
        //    ViewBag.Likes = await _likeService.GetLikedCommentsIdAsync(userId, commentReturned.ProductId);
        //    ViewBag.Dislikes = await _likeService.GetDislikedCommentsIdAsync(userId, commentReturned.ProductId);
        //    return PartialView("GetCommentsPartial", new List<CommentViewModel> { res });
        //}
        //[HttpPost]     
        //public async Task<IActionResult> UpdateComment(string comment)
        //{
        //   // var commentReturned = await _commentService.UpdateCommentAsync(comment);
        //   // if (commentReturned.IsRemoved == true) return new EmptyResult();
        //    //var res = _mapper.Map<CommentViewModel>(commentReturned);
        //    //if (User.Identity.IsAuthenticated)
        //    //{
        //    //    var user = await _userManager.FindByEmailAsync(User.Identity.Name);
        //    //    res.CurrentUserId = user.Id;
        //    //}
        //    //var userId = "6cab4e64-cd04-4ba5-94d4-3c9b5ad2e78f";
        //    //ViewBag.Likes = await _likeService.GetLikedCommentsIdAsync(userId, commentReturned.ProductId);
        //    //ViewBag.Dislikes = await _likeService.GetDislikedCommentsIdAsync(userId, commentReturned.ProductId);
        //    //return PartialView("GetCommentsPartial", new List<CommentViewModel> { res });
        //}



    }
  
}
