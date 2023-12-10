using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Context;
using PustokMVC.Helpers;
using PustokMVC.Models;
using PustokMVC.ViewModels.ProductVM;

namespace PustokMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        PustokDBContext _db { get; }

        public ProductController(PustokDBContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var item = await _db.Products.Select(p => new ProductListItemVM
            {
                Id = p.Id,
                Name = p.Name,
                Discount = p.Discount,
                IsDeleted = p.IsDeleted,
                CostPrice = p.CostPrice,
                ImageUrl = p.ImageUrl,
                Quantity = p.Quantity,
                SellPrice = p.SellPrice,
                Category = p.Category

            }).ToListAsync();
            return View(item);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _db.Categories;
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(ProductCreateItemVM vm)
        {
            Product product = new Product
            {
                Description = vm.Description,
                Discount = vm.Discount,
                SellPrice = vm.SellPrice,
                About = vm.About,
                CategoryId = vm.CategoryId,
                CostPrice = vm.CostPrice,
                ImageUrl = await vm.ImageFile.SaveAsync(PathConstants.Product),
                Name = vm.Name,
                Quantity = vm.Quantity,
            };
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var data = await _db.Products.FindAsync(id);
            _db.Products.Remove(data);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
