

using IntenetShop.Domain.Entities;
using InternetShop.Application.Intefaces;

namespace InternetShop.Presistance.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(T item)
        {
            _context.Set<T>().Add(item);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> list = _context.Set<T>();
            return list;
        }

        public T GetById(int id)
        {
            var entity = _context.Set<T>().Find(id);
            return entity;
        }

        public void Remove(T item)
        {
            _context.Set<T>().Remove(item);
        }

        public void Update(T item)
        {
            _context.Set<T>().Update(item);
        }

       
    }
}
