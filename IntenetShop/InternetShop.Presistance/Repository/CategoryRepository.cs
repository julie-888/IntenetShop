using IntenetShop.Domain.Entities;
using InternetShop.Application.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.Presistance.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }

}
