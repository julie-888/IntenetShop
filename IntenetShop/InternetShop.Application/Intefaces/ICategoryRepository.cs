using IntenetShop.Domain.Entities;


namespace InternetShop.Application.Intefaces
{
    public interface ICategoryRepository
    {
        public void Add(Category category);
        public IEnumerable<Category> GetAll();
        public Category GetById(int id);
        public void Update(Category category);
    }
}
