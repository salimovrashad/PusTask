using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Context;
using PustokMVC.Models;
using PustokMVC.ViewModels.CategoryVM;
using PustokMVC.ViewModels.TagVM;

namespace PustokMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagController : Controller
    {
        PustokDBContext _db { get; }

        public TagController(PustokDBContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var item = await _db.Tags.Select(t => new TagListItemVM 
            { 
                Id = t.Id,
                Name = t.Name,
            }).ToListAsync();
            return View(item);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(TagCreateItemVM item)
        {
            Tag tag = new Tag
            {
                Name = item.Name,
            };
            await _db.Tags.AddAsync(tag);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
