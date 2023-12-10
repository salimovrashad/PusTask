using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Context;
using PustokMVC.Models;
using PustokMVC.ViewModels.CategoryVM;

namespace PustokMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        PustokDBContext _db { get; }
        public CategoryController(PustokDBContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var item = await _db.Categories.Select(c => new CategoryListItemVM
            {
                Id = c.Id,
                Name = c.Name,
            }).ToListAsync();
            return View(item);
        }

        public IActionResult Create() 
        { 
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(CategoryCreateItemVM item)
        {
            Category category = new Category
            {
                Name = item.Name,
            };
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _db.Categories.FindAsync(id);
            _db.Categories.Remove(data);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
