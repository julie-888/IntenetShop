using InternetShop.Application.Intefaces;
using InternetShop.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Web.Controllers
{
    public class ProductController : Controller
       
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var entity = _productRepository.GetAll();
            var product = entity.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,


            }).ToList();
            return View(product);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
    }
}
