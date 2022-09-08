using Microsoft.AspNetCore.Mvc;
using MyEcommerce.Data;
using MyEcommerce.Models;
using MyEcommerce.ViewModels;
using System.Diagnostics;

namespace MyEcommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                Categories=_context.Categories.ToList(),
                Products=_context.Products.ToList(),
            };
            return View(model);
        }

        public IActionResult Product()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactUs model)
        {
            if (ModelState.IsValid)
            {
                _context.ContactUs.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult ProductDetails()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Products(int Id)
        {
            var products=_context.Products.Where(x=>x.CategoryId==Id).ToList();
            return View(products);
        }
    }
}