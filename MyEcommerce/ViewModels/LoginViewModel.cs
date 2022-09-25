using System.ComponentModel.DataAnnotations;

namespace MyEcommerce.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress]
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
