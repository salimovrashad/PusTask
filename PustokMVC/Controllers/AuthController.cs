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

        [HttpPost]

        public async Task<IActionResult> Login(LoginVM vm)
        {
            AppUser user;
            if (vm.UsernameOrEmail.Contains("@")) 
            { 
                user = await _userManager.FindByEmailAsync(vm.UsernameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(vm.UsernameOrEmail);
            }
            if (user == null)
            {
                ModelState.AddModelError("", "Username or password wrong!");
                return View(vm);
            }
            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.IsRemember, true);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Too many attempts wait until" + user.LockoutEnd);
                }
                else
                {
                    ModelState.AddModelError("", "Username or password wrong!");
                }
                return View(vm);
            }
            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

		public async Task<IActionResult> Profile()
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);

            UpdateVM update = new UpdateVM
            {
                Email = user.Email,
                Fullname = user.Fullname,
                Username = user.UserName
            };
			return View(update);
		}

        [HttpPost]

        public async Task<IActionResult> Profile(UpdateVM vm)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            user.Email = vm.Email;
            user.Fullname = vm.Fullname;
            user.UserName = vm.Username;

			await _userManager.UpdateAsync(user);
			return RedirectToAction("Profile","Auth");
        }
    }
}
