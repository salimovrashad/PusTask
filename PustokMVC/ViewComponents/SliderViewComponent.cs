using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Context;
using PustokMVC.ViewModels.SliderVM;

namespace PustokMVC.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        PustokDBContext _db { get; }

        public SliderViewComponent(PustokDBContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _db.Sliders.Select(s => new SliderListItemVM
            {
                Id = s.Id,
                ImageUrl = s.ImageUrl,
                Description = s.Description,
                IsLeft = s.IsLeft,
                Title = s.Title,
            }).ToListAsync());
        }
    }
}
