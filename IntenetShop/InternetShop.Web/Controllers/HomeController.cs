using InternetShop.Application.Intefaces;
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

       
    }
}