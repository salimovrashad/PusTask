using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Context;
using PustokMVC.ViewModels.ProductVM;
using PustokMVC.ViewModels.SliderVM;

namespace PustokMVC.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        PustokDBContext _db { get; }

        public ProductViewComponent(PustokDBContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _db.Products.Where(p => !p.IsDeleted).Select(p => new ProductListItemVM
            {
                Id = p.Id,
                Category = p.Category,
                Discount = p.Discount,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Quantity = p.Quantity,
                SellPrice = p.SellPrice,
                CostPrice = p.CostPrice,
            }).ToListAsync());
        }
    }
}
