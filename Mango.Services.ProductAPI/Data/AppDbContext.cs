using Mango.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Name = "Samosa",
                Price = 15.0,
                Description = "Punjabi Samosa",
                CategoryName = "Appetizer",
                ImageUrl = ""
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 2,
                Name = "Paneer Tikka",
                Price = 13.0,
                Description = "Paneer Tikka with Marination",
                CategoryName = "Appetizer",
                ImageUrl = ""
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 3,
                Name = "Paneer Butter Masala",
                Price = 22.0,
                Description = "Paneer Butter Masala with Creamy Gravy",
                CategoryName = "Entree",
                ImageUrl = ""
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 4,
                Name = "Mix Veg Curry",
                Price = 18.0,
                Description = "Mix Veg Curry with Spicy Gravy",
                CategoryName = "Entree",
                ImageUrl = ""
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 5,
                Name = "Samosa",
                Price = 15.0,
                Description = "Punjabi Samosa",
                CategoryName = "Appetizer",
                ImageUrl = ""
            });
        }
    }
}
