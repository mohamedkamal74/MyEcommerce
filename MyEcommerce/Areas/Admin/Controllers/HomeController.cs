using Microsoft.AspNetCore.Mvc;

namespace MyEcommerce.Areas.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
