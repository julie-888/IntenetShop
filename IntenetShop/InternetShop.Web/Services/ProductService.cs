using InternetShop.Application.Intefaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InternetShop.Web.Services
{
    public class ProductService
    {
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public List<SelectListItem> GetCategoryList()
        {
            var categories = _categoryRepository.GetAll();
            var dropdown = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            })
                .ToList();
            return dropdown;
        }
    }
}
