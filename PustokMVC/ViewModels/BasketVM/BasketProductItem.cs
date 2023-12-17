namespace PustokMVC.ViewModels.BasketVM
{
    public class BasketProductItem
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
    }
}
