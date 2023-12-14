using PustokMVC.ViewModels.CommonVM;
using PustokMVC.ViewModels.ProductVM;
using PustokMVC.ViewModels.SliderVM;

namespace PustokMVC.ViewModels.HomeVM
{
    public class HomeVM
    {
        public PaginationVM<IEnumerable<ProductListItemVM>> PaginatedProducts { get; set; }
    }
}
