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
        [NotMapped]

        public IFormFile File { get; set; }
       
    }
}
