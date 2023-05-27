using InternetShop.Application.Intefaces;
using InternetShop.Web.Common;
using InternetShop.Web.Models;
using InternetShop.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InternetShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        public readonly IConfiguration _config;
        public readonly ICategoryRepository _categoryRepository;
        public HomeController(IProductRepository productRepository, IConfiguration config, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _config = config;
        }
        public IActionResult Index()
        {
            var entity = _productRepository.GetProducts();
            var categories = _categoryRepository.GetAll();
            var model = new HomeViewModel
            {
                Products = entity.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price= p.Price,
                    Category = p.Category,
                    ApplicationType= p.ApplicationType,
                    Image = _config["ImagePath"] + p.Image

                }).ToList(),
                Categories = categories.Select(c=> new CategoryViewModel
                {
                    Id = c.Id,
                    Name= c.Name,   
                
                }).ToList(),
            };
            return View(model);
        }
        public IActionResult Details(int id)
        {
            var cart = new List<ShoppingCart>();
            var sessionGet = HttpContext.Session.Get<List<ShoppingCart>>("ShoppingCart");
            if (sessionGet !=null && sessionGet.Count >0)
            {
                cart = sessionGet;
            }
            var entity = _productRepository.GetProductById(id);
            var productDetails = new DetailsVM
            {
                Product = new ProductViewModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,
                    Price = entity.Price,
                    Category = entity.Category,
                    ApplicationType= entity.ApplicationType,
                    Image = _config["ImagePath"] + entity.Image
                },
                IsExistsInCart = false
            };
            foreach(var item in cart)
            {
                if(item.ProductId==productDetails.Product.Id)
                {
                    productDetails.IsExistsInCart = true;

                }
                
            }
            return View(productDetails);
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var shopCart = new List<ShoppingCart>();
            var sessionGet = HttpContext.Session
                .Get<List<ShoppingCart>>("SessionCart");
            if (sessionGet != null && sessionGet.Count > 0)
            {
                shopCart = sessionGet;
            }
            shopCart.Add(new ShoppingCart { ProductId= id });
            HttpContext.Session.Set("SessionCart", shopCart);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var shopCart = new List<ShoppingCart>();
            var sessionGet = HttpContext.Session
                .Get<List<ShoppingCart>>("SessionCart");
            if (sessionGet != null)
            {
                shopCart = sessionGet;
            }
           var itemToRemove = shopCart.FirstOrDefault(x=>x.ProductId==id);
            if(itemToRemove != null)
            {
                shopCart.Remove(itemToRemove);
            }

            HttpContext.Session.Set("SessionCart", shopCart);

            return RedirectToAction("Index");
        }

    }
}