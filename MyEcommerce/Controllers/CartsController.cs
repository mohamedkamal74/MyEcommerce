using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEcommerce.Data;
using MyEcommerce.Models;

namespace MyEcommerce.Controllers
{
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartsController(ApplicationDbContext context,UserManager<ApplicationUser>userManager)
        {
           _context = context;
           _userManager = userManager;
        }

       

        public async Task< IActionResult> Cart()
        {
            var user=await _userManager.GetUserAsync(User);
            var result = _context.ShoppindCarts.Include(p => p.Product).Where(x => x.UserId.Equals(user.Id)).ToList();
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> AddToCart(ShoppindCart model,int Qty)
        {
            var product = _context.Products.FirstOrDefault(x => x.ProductId.Equals(model.ProductId));
            var user = await _userManager.GetUserAsync(User);


            if (Qty <= 0)
                Qty = 1;
            var cart = new ShoppindCart
            {
                UserId= user.Id,
                ProductId = product.ProductId,
                Quantity =Qty
            };

            var shoppingcart=_context.ShoppindCarts.FirstOrDefault(x=>x.UserId == user.Id&&x.ProductId.Equals(model.ProductId));

            if (shoppingcart == null)
                _context.ShoppindCarts.Add(cart);
            else
                shoppingcart.Quantity += model.Quantity;

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");

           
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var user=await _userManager.GetUserAsync(User);
            var shoppingcart=_context.ShoppindCarts.FirstOrDefault(x=>x.UserId==user.Id&&x.CartId.Equals(id));

            if (shoppingcart != null)
                _context.ShoppindCarts.Remove(shoppingcart);
            _context.SaveChanges();
            return RedirectToAction(nameof(Cart));

        }
    }
}
