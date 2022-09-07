using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEcommerce.Data;
using MyEcommerce.Models;

namespace MyEcommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(_context.Products.Include(c=>c.Category).OrderBy(x=>x.ProductName).ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.category = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product model, IFormFile File)
        {
            if (ModelState.IsValid)
            {
                if (File != null)
                {
                    string imagename = Guid.NewGuid().ToString() + ".jpg";
                    string filepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/product", imagename);

                    using (var filestream = System.IO.File.Create(filepath))
                    {
                        await File.CopyToAsync(filestream);

                    }
                    model.ProductImage = imagename;
                }
                _context.Products.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.category = _context.Categories.ToList();
            return View(model);

        }
    }
}
