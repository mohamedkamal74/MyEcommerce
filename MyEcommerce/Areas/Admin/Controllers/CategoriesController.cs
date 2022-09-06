using Microsoft.AspNetCore.Mvc;
using MyEcommerce.Data;
using MyEcommerce.Models;

namespace MyEcommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Categories.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(Category model,IFormFile File)
        {
            if (ModelState.IsValid)
            {
                if (File != null)
                {
                    string imagename = Guid.NewGuid().ToString() + ".jpg";
                    string filepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/category", imagename);

                    using (var filestream = System.IO.File.Create(filepath))
                    {
                        await File.CopyToAsync(filestream);

                    }
                    model.CategoryPhoto = imagename;
                }
                _context.Categories.Add(model);
                 await  _context.SaveChangesAsync();
             return   RedirectToAction(nameof(Index));
            }
              
            return View(model);

        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
    }
}
