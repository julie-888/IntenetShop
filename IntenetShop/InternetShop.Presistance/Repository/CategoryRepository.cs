using IntenetShop.Domain.Entities;
using InternetShop.Application.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.Presistance.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(Category category)
        {
            _appDbContext.Categories.Add(category);
            _appDbContext.SaveChanges();
           
        }
        public IEnumerable<Category> GetAll()
        {
            var categories = _appDbContext.Categories.ToList();
            return categories;
        }

        public Category GetById(int id)
        {
           var category = _appDbContext
                .Categories
                .First(c => c.Id == id);

            return category;
        }
        public void Update(Category category)
        {
            _appDbContext
                .Categories
                .Update(category);
            _appDbContext.SaveChanges();
        }
    }
}
