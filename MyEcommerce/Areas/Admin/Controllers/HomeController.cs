using Microsoft.AspNetCore.Mvc;
using MyEcommerce.Data;

namespace MyEcommerce.Areas.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context )
        {
           _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowMessages()
        {
            return View(_context.ContactUs.OrderBy(x=>x.Name).ToList());
        }
    }
}
