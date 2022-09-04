using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEcommerce.Models
{
    public class ShoppindCart
    {
        [Key]
        public int CartId { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        [Range(1,int.MaxValue,ErrorMessage ="لا يجب ان تقل الكمية عن 1")]
        public int Quantity { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]

        public virtual ApplicationUser ApplicationUser { get; set; }


    }
}
