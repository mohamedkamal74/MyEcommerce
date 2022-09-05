using Microsoft.AspNetCore.Mvc;

namespace MyEcommerce.Controllers
{
    public class CartsController : Controller
    {
        public IActionResult Cart()
        {
            return View();
        }
    }
}
