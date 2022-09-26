using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyEcommerce.Models;
using MyEcommerce.ViewModels;

namespace MyEcommerce.Controllers
{
    public class AccountsController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
      
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, true);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                    return View(model);
                
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Id=model.Id.ToString(),
                    UserName = model.Email,
                    Name = model.Name,
                    Email = model.Email

                };
                user.Id = Guid.NewGuid().ToString();
                var result = await _userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, true);
                    return RedirectToAction("Index", "Home");
                }    
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
