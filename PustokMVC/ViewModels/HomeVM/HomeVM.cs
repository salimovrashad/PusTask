using PustokMVC.ViewModels.ProductVM;
using PustokMVC.ViewModels.SliderVM;

namespace PustokMVC.ViewModels.HomeVM
{
    public class HomeVM
    {
        public IEnumerable<SliderListItemVM> Sliders { get; set; }
        public IEnumerable<ProductListItemVM> Products { get; set; }
    }
}
