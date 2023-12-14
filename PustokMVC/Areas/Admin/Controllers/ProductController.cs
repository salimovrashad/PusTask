using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Context;
using PustokMVC.Helpers;
using PustokMVC.Models;
using PustokMVC.ViewModels.CategoryVM;
using PustokMVC.ViewModels.CommonVM;
using PustokMVC.ViewModels.HomeVM;
using PustokMVC.ViewModels.ProductVM;
using PustokMVC.ViewModels.SliderVM;

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
            var items = _db.Products.Where(p => !p.IsDeleted).Take(2).Select(p => new ProductListItemVM
            {
                Id = p.Id,
                Category = p.Category,
                Discount = p.Discount,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Quantity = p.Quantity,
                SellPrice = p.SellPrice,
                CostPrice = p.CostPrice,
            });
            int count = await _db.Products.CountAsync(x=>!x.IsDeleted);
            PaginationVM<IEnumerable<ProductListItemVM>> pag = new(count, 1, (int)Math.Ceiling((decimal)count/2), items);
            HomeVM vm = new HomeVM
            {
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
                }).ToListAsync(),
                PaginatedProducts = pag
            };
            return View(vm);
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

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", data.ImageUrl);
            System.IO.File.Delete(fullPath);
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Categories = _db.Categories;
            var item = await _db.Products.FindAsync(id);

            return View(new ProductUpdateItemVM
            {
                Name = item.Name,
                Description = item.Description,
                Discount = item.Discount,
                SellPrice = item.SellPrice,
                About = item.About,
                CategoryId = item.CategoryId,
                CostPrice = item.CostPrice,
                Quantity= item.Quantity,
                ImageUrl = item.ImageUrl
            });
        }
        
        [HttpPost]

        public async Task<IActionResult> Update(ProductUpdateItemVM vm, int id) 
        {
            var item = await _db.Products.FindAsync(id);
            item.Name = vm.Name;
            item.Description = vm.Description;
            item.Discount = vm.Discount;
            item.SellPrice = vm.SellPrice;
            item.About = vm.About;
            item.CategoryId = vm.CategoryId;
            item.CostPrice = vm.CostPrice;
            item.Quantity = vm.Quantity;
            item.ImageUrl = await vm.ImageFile.SaveAsync(PathConstants.Product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ProductPagination(int page = 1, int count = 2)
        {
            var datas = await _db.Products.Select(p=>new ProductListItemVM 
            {
                Id = p.Id,
                Category = p.Category,
                Discount = p.Discount,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Quantity = p.Quantity,
                SellPrice = p.SellPrice,
                CostPrice = p.CostPrice,
            }).Skip((page-1)*count).Take(count).ToListAsync();
            return Json(datas);
        }
    }
}
