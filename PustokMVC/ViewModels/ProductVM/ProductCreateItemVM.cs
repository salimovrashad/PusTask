﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PustokMVC.ViewModels.ProductVM
{
    public class ProductCreateItemVM
    {
        [MaxLength(64)]
        public string Name { get; set; }
        [MaxLength(128)]
        public string? About { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "smallmoney")]
        public decimal SellPrice { get; set; }
        [Column(TypeName = "smallmoney")]
        public decimal CostPrice { get; set; }
        [Range(0, 100)]
        public float Discount { get; set; }
        public ushort Quantity { get; set; }
        public IFormFile ImageFile { get; set; } 
        public int CategoryId { get; set; }
    }
}
