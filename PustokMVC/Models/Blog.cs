namespace PustokMVC.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<BlogTag> BlogTags { get; set; }
    }
}