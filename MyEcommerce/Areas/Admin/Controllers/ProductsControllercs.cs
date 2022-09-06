using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEcommerce.Data;

namespace MyEcommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsControllercs : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsControllercs(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Products.Include(c=>c.Category).OrderBy(x=>x.ProductName).ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
