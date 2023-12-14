using Microsoft.EntityFrameworkCore;
using PustokMVC.Models;

namespace PustokMVC.Context
{
    public class PustokDBContext : DbContext
    {
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public PustokDBContext(DbContextOptions opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Setting>()
                .HasData(new Setting
                {
                    Id = 1,
                    Address = "Baku,sdaafs",
                    Email = "mail.ru",
                    Number1 = "122132",
                    Number2 = "adssadsa",
                    AccountIcon = "sasdssa",
                    Logo = "saadsas"
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
