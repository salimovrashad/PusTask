using PustokMVC.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace PustokMVC.ViewModels.ProductVM
{
    public class ProductListItemVM
    {
        public int Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }
        [Column(TypeName = "smallmoney")]
        public decimal SellPrice { get; set; }
        [Column(TypeName = "smallmoney")]
        public decimal CostPrice { get; set; }
        [Range(0, 100)]
        public float Discount { get; set; }
        public ushort Quantity { get; set; }
        public string ImageUrl { get; set; }
        public Category? Category { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<Color> Colors { get; set; }
    }
}
