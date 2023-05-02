using IntenetShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.Application.Intefaces
{
   public interface IGenericRepository<T> where T : class
    {
        public void Add(T item);
        public IEnumerable<T> GetAll();
        public T GetById(int id);
        public void Update(T item);
        public void Remove(T item);
    }
}
