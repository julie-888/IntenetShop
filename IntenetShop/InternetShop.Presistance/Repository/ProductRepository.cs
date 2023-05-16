using IntenetShop.Domain.Entities;
using InternetShop.Application.Intefaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.Presistance.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProductRepository(AppDbContext context) : base(context)
        {
           _appDbContext= context;
        }
        public IEnumerable<Product> GetProducts()
        {
            var products = _appDbContext.Products
            .Include(p => p.Category);
            return products;
        }
    }
}
