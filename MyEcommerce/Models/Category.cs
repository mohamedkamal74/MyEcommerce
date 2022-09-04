using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEcommerce.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="اسم الصنف مطلوب")]
        public string CategoryName { get; set; }
        public string? CategoryPhoto { get; set; }
        public IFormFile File { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual ICollection <Product>? Product { get; set; }
    }
}
