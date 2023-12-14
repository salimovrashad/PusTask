using Microsoft.AspNetCore.Mvc;

namespace PustokMVC.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
