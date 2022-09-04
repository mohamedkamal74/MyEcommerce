using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEcommerce.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "اسم المنتج مطلوب")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "وصف المنتج مطلوب")]

        public string? Description { get; set; }
        [Required(ErrorMessage = "سعر المنتج مطلوب")]

        public decimal Price { get; set; }
        public decimal ProductImage { get; set; }
        public IFormFile File { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
    }
}
