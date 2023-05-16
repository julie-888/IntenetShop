using IntenetShop.Domain.Entities;
using InternetShop.Application.Intefaces;
using InternetShop.Web.Models;
using InternetShop.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InternetShop.Web.Controllers
{
    public class ProductController : Controller
       
    {
        private readonly IProductRepository _productRepository;
        private readonly ImageService _imageService;
        private readonly ProductService _productService; 

        public ProductController(IProductRepository productRepository, ImageService imageService, ProductService productService)
        {
            _productRepository = productRepository;
           
            _imageService = imageService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var entity = _productRepository.GetProducts();
            var product = entity.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Category = p.Category


            }).ToList();
            return View(product);
        }
        [HttpGet]
        public IActionResult Create()
        {
           
            var products = new ProductViewModel
            {
                CategoryDropDown = _productService.GetCategoryList()
            };
            return View(products);
            
        }
        [HttpPost]
        public IActionResult Create(ProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                product.CategoryDropDown= _productService.GetCategoryList();
                return View(product);
            }
            var files = HttpContext.Request.Form.Files;
            product.Image = _imageService.ImageLoad(files);

            var entity = new Product
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Image = product.Image,
                CategoryId = product.CategoryId,
            };
            _productRepository.Add(entity);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var entity = _productRepository.GetById(id);
            var product = new ProductViewModel
            {
                Name = entity.Name,
                Price = entity.Price,
                Description = entity.Description,
                Image = _imageService.imagePath + entity.Image,
                CategoryId= entity.CategoryId,
                CategoryDropDown = _productService.GetCategoryList(),
            };

            return View(product);

        }
        [HttpPost]
        public IActionResult Edit(ProductViewModel product)
        {
            return RedirectToAction("Index");
        }
        
    }
}
