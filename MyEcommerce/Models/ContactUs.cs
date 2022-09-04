using System.ComponentModel.DataAnnotations;

namespace MyEcommerce.Models
{
    public class ContactUs
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
    }
}
