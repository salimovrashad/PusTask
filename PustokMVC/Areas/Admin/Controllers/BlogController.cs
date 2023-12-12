using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Context;
using PustokMVC.Models;
using PustokMVC.ViewModels.BlogVM;
using PustokMVC.ViewModels.TagVM;

namespace PustokMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        PustokDBContext _db { get; }

        public BlogController(PustokDBContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var item = await _db.Blogs.Select(t => new BlogListItemVM 
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                Tags = t.BlogTags.Select(bt => bt.Tag).ToList(),
            }).ToListAsync();
            return View(item);
        }

        public IActionResult Create()
        {
            ViewBag.Tags = _db.Tags;
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(BlogCreateItemVM item)
        {
            Blog blog = new Blog
            {
                Name = item.Name,
                Description = item.Description,
                BlogTags = item.TagId.Select(id => new BlogTag
                {
                    TagId = id,
                }).ToList(),
            };
            await _db.Blogs.AddAsync(blog);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
