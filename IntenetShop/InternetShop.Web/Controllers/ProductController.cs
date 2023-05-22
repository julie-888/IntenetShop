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
                Category = p.Category,
                ApplicationType = p.ApplicationType
            

            }).ToList();
            return View(product);
        }
        [HttpGet]
        public IActionResult Create()
        {
           
            var products = new ProductViewModel
            {
                CategoryDropDown = _productService.GetCategoryList(),
                ApplicationTypeDropDown = _productService.GetApplicationTypeList(),

            };
            return View(products);
            
        }
        [HttpPost]
        public IActionResult Create(ProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                product.CategoryDropDown= _productService.GetCategoryList();
                product.ApplicationTypeDropDown= _productService.GetApplicationTypeList();
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
                ApplicationTypeId= product.ApplicationTypeId,
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
                ApplicationTypeId= entity.ApplicationTypeId,
                ApplicationTypeDropDown = _productService.GetApplicationTypeList(),
            };

            return View(product);

        }
        [HttpPost]
        public IActionResult Edit(ProductViewModel product)
        {
            if(!ModelState.IsValid) {
                return View(product);
            }

            var currentProduct = _productRepository.GetProductById(product.Id);
            _imageService.DeleteImage(currentProduct.Image);

            var files = HttpContext.Request.Form.Files;

            var entity = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Image = _imageService.ImageLoad(files),
                CategoryId = product.CategoryId,
                
            };

            _productRepository.Update(entity);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var currentProduct = _productRepository.GetProductById(id);
            _imageService.DeleteImage(currentProduct.Image);
           
            _productRepository.Remove(currentProduct);

            return RedirectToAction("Index");

        }

    }
}
