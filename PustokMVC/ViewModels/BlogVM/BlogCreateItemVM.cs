namespace PustokMVC.ViewModels.BlogVM
{
    public class BlogCreateItemVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<int> TagId { get; set; }
    }
}
