using MyEcommerce.Models;

namespace MyEcommerce.ViewModels
{
    public class HomeViewModel
    {
        public List<Category> Categories { get; set; }=new List<Category>();
        public List<Product> Products { get; set; }=new List<Product>();
    }
}
