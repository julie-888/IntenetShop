using IntenetShop.Domain.Entities;
using InternetShop.Application.Intefaces;
using InternetShop.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Web.Controllers
{
    public class ApplicationTypeController : Controller
    {
        private readonly IApplicationTypeRepository _repository;
        public ApplicationTypeController(IApplicationTypeRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            var applicationTypes = _repository.GetAll();

            var model = applicationTypes.Select(c => new ApplicationTypeViewModel
            {
                Id = c.Id,
                Name = c.Name,

            })
            .ToList();

            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ApplicationTypeViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var applicationType = new ApplicationType
            {
                Name = model.Name,
            };

            _repository.Add(applicationType);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var applicationType = _repository.GetById(id);
            var model = new ApplicationTypeViewModel
            {
                Name = applicationType.Name,
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(ApplicationTypeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var applicationType = new ApplicationType
            {
                Id = model.Id,
                Name = model.Name
            };
            _repository.Update(applicationType);

            return RedirectToAction("Index");

        }
        public IActionResult Delete(int id)
        {
            var applicationType = _repository.GetById(id);
            _repository.Remove(applicationType);

            return RedirectToAction("Index");
        }
    }
}
