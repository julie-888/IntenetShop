
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace IntenetShop.Domain.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; } = string.Empty;
        public int CategoryId { get; set; } //внешний ключ
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }// навигационное свойство
        public int ApplicationTypeId { get; set; }
        [ForeignKey("ApplicationTypeId")]
        public virtual ApplicationType? ApplicationType { get; set; }
    }
}
