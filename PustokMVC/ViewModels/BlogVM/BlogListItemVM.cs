using System.Drawing;
using PustokMVC.Models;

namespace PustokMVC.ViewModels.BlogVM
{
    public class BlogListItemVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
