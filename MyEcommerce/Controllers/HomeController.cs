using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public  async Task< IActionResult> Product(int page=1)
        {
            var products = _context.Products.OrderByDescending(x => x.Price);
            var model=await GetPage(products,page);
            return View(model);
        }
        [HttpPost]
        public IActionResult Search(string proName)
        {
            return View(_context.Products.Where(x=>x.ProductName.Contains(proName.Trim())).ToList());
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

        public IActionResult ProductDetails(int? Id)
        {
            return View(_context.Products.Include(x=>x.Category).FirstOrDefault(x=>x.ProductId==Id));
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

        public async Task<List<Product>>GetPage(IQueryable<Product> result,int pagenumber)
        {
            const int pagesize = 2;
            decimal rowCount = await _context.Products.CountAsync();
            var pageCount=Math.Ceiling(rowCount/pagesize);
            if (pagenumber > pageCount)
                pagenumber = 1;
            pagenumber = pagenumber <= 0 ? 1 : pagenumber;
            int skipCount = (pagenumber - 1) * pagesize;
            var paeData = await result.Skip(skipCount).Take(pagesize).ToListAsync();

            ViewBag.CurrentPage = pagenumber;
            ViewBag.PageCount = pageCount;

            return paeData;
        }
    }
}