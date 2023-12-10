using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Context;
using PustokMVC.ViewModels.HomeVM;
using PustokMVC.ViewModels.ProductVM;
using PustokMVC.ViewModels.SliderVM;

namespace PustokMVC.Controllers
{
    public class HomeController : Controller
    {
        PustokDBContext _db;

        public HomeController(PustokDBContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM vm = new HomeVM
            {
                Sliders = await _db.Sliders.Select(s => new SliderListItemVM
                {
                    Id = s.Id,
                    ImageUrl = s.ImageUrl,
                    IsLeft = s.IsLeft,
                    Title = s.Title,
                    Description = s.Description,
                }).ToListAsync(),
                Products = await _db.Products.Where(p => !p.IsDeleted).Select(p => new ProductListItemVM
                {
                    Id = p.Id,
                    Category = p.Category,
                    Discount = p.Discount,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Quantity = p.Quantity,
                    SellPrice = p.SellPrice,
                    CostPrice = p.CostPrice,
                }).ToListAsync()
            };
            return View(vm);
        }
    }
}
