using InternetShop.Application.Intefaces;
using InternetShop.Presistance.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InternetShop.Web.Services
{
    public class ProductService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IApplicationTypeRepository _applicationTypeRepository;

        public ProductService(ICategoryRepository categoryRepository, IApplicationTypeRepository applicationTypeRepository)
        {
            _categoryRepository = categoryRepository;
            _applicationTypeRepository = applicationTypeRepository;
       
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
        public List<SelectListItem> GetApplicationTypeList()
        {
            var appTypes = _applicationTypeRepository.GetAll();
            var dropdown = appTypes.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            })
                .ToList();
            return dropdown;
        }
    }
}
