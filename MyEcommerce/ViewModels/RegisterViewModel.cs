using System.ComponentModel.DataAnnotations;

namespace MyEcommerce.ViewModels
{
    public class RegisterViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password",ErrorMessage ="Invalid")]
        public string ConfirmPassword { get; set; }
    }
}
