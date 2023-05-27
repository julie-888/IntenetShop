using IntenetShop.Domain.Entities;

namespace InternetShop.Web.Models
{
    public class DetailsVM
    {
        public ProductViewModel Product { get; set; }
        public bool IsExistsInCart { get; set; }
    }
}
