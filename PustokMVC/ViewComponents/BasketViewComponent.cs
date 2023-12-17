using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PustokMVC.Context;
using PustokMVC.ViewModels.BasketVM;

namespace PustokMVC.ViewComponents
{
    public class BasketViewComponent : ViewComponent
    {
        PustokDBContext _db { get; }

        public BasketViewComponent(PustokDBContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = JsonConvert.DeserializeObject<List<BasketProductAndCountVM>>(HttpContext.Request.Cookies["basket"] ?? "[]");
            var products = _db.Products.Where(p => items.Select(i => i.Id).Contains(p.Id));
            List<BasketProductItem> basketItems = new();
            foreach (var item in products)
            {
                basketItems.Add(new BasketProductItem
                {
                    Id = item.Id,
                    Name = item.Name,
                    Count = items.FirstOrDefault(x => x.Id == item.Id).Count,
                    Discount = item.Discount,
                    ImageUrl = item.ImageUrl,
                    Price = (float)item.SellPrice
                });
            }
            return View(basketItems);
        }
    }
}
