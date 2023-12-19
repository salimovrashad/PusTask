using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PustokMVC.Models;
using PustokMVC.ViewModels.AuthVM;

namespace PustokMVC.Controllers
{
    public class AuthController : Controller
    {
        SignInManager<AppUser> _signInManager;
        UserManager<AppUser> _userManager;
        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }





        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var result = await _userManager.CreateAsync(new AppUser
            {
                Fullname = vm.Fullname,
                Email = vm.Email,
                UserName = vm.Username
            }, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(vm);
            }
            return View();
        }









        public IActionResult Login()
        {
            return View();
        }
    }
}
