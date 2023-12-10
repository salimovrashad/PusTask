using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Context;
using PustokMVC.Models;
using PustokMVC.ViewModels.SliderVM;

namespace PustokMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        PustokDBContext _db { get; }

        public SliderController(PustokDBContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var item = await _db.Sliders.Select(s => new SliderListItemVM
            {
                Description = s.Description,
                Id = s.Id,
                ImageUrl = s.ImageUrl,
                IsLeft = s.IsLeft,
                Title = s.Title,
            }).ToListAsync();
            return View(item);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(SliderCreateItemVM item)
        {
            Slider slider = new Slider
            {
                Description = item.Description,
                ImageUrl = item.ImageUrl,
                IsLeft = item.IsLeft,
                Title = item.Title,
            };
            await _db.Sliders.AddAsync(slider);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var item = await _db.Sliders.FindAsync(id);
            return View(new SliderUpdateItemVM
            {
                Description = item.Description,
                ImageUrl = item.ImageUrl,
                IsLeft = item.IsLeft,
                Title = item.Title,
            });
        }

        [HttpPost]

        public async Task<IActionResult> Update(SliderUpdateItemVM vm, int id)
        {
            var data = await _db.Sliders.FindAsync(id);
            data.Title = vm.Title;
            data.IsLeft = vm.IsLeft;
            data.ImageUrl = vm.ImageUrl;
            data.Description = vm.Description;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _db.Sliders.FindAsync(id);
            _db.Sliders.Remove(data);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
