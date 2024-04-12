using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ProductAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(0, 1000)]
        public double Price { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [MaxLength(100)]
        public string CategoryName { get; set; }
        [MaxLength(1000)]
        public string ImageUrl { get; set; }
    }
}
