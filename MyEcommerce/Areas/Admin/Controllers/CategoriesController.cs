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
        public async Task<IActionResult> Create(Category model, IFormFile File)
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
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);

        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            var category = _context.Categories.Find(Id);
            if (category == null)
                return NotFound();
            else
                return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category model, IFormFile File)
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
                else
                {
                    model.CategoryPhoto = model.CategoryPhoto;
                }
                _context.Categories.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
         
        }

        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            var category = _context.Categories.Find(Id);
            if (category == null)
                return NotFound();
            else
                return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? Id,Category model)
        {
            if(Id != null)
            {
                _context.Categories.Remove(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}