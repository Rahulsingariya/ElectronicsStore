using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ElectronicsStore.Models;

namespace ElectronicsStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for our models
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Review> Reviews { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Laptops", Description = "Laptops and Notebooks", CreatedDate = DateTime.Now },
                new Category { CategoryId = 2, CategoryName = "Smartphones", Description = "Mobile Phones and Accessories", CreatedDate = DateTime.Now },
                new Category { CategoryId = 3, CategoryName = "Headphones", Description = "Audio Devices", CreatedDate = DateTime.Now },
                new Category { CategoryId = 4, CategoryName = "Cameras", Description = "Digital Cameras and Accessories", CreatedDate = DateTime.Now },
                new Category { CategoryId = 5, CategoryName = "Smartwatches", Description = "Wearable Technology", CreatedDate = DateTime.Now }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                // Laptops
                new Product { ProductId = 1, ProductName = "Dell XPS 15", Description = "15-inch premium laptop with Intel i7 processor", Price = 85000, StockQuantity = 10, CategoryId = 1, CreatedDate = DateTime.Now },
                new Product { ProductId = 2, ProductName = "HP Pavilion 14", Description = "14-inch budget-friendly laptop", Price = 45000, StockQuantity = 15, CategoryId = 1, CreatedDate = DateTime.Now },

                // Smartphones
                new Product { ProductId = 3, ProductName = "Samsung Galaxy S24", Description = "Latest flagship smartphone", Price = 79999, StockQuantity = 20, CategoryId = 2, CreatedDate = DateTime.Now },
                new Product { ProductId = 4, ProductName = "iPhone 15 Pro", Description = "Apple's premium smartphone", Price = 134900, StockQuantity = 8, CategoryId = 2, CreatedDate = DateTime.Now },

                // Headphones
                new Product { ProductId = 5, ProductName = "Sony WH-1000XM5", Description = "Noise-canceling wireless headphones", Price = 29990, StockQuantity = 25, CategoryId = 3, CreatedDate = DateTime.Now },
                new Product { ProductId = 6, ProductName = "JBL Tune 760NC", Description = "Wireless over-ear headphones", Price = 4999, StockQuantity = 30, CategoryId = 3, CreatedDate = DateTime.Now },

                // Cameras
                new Product { ProductId = 7, ProductName = "Canon EOS R6", Description = "Full-frame mirrorless camera", Price = 215000, StockQuantity = 5, CategoryId = 4, CreatedDate = DateTime.Now },

                // Smartwatches
                new Product { ProductId = 8, ProductName = "Apple Watch Series 9", Description = "Advanced health and fitness tracker", Price = 41900, StockQuantity = 12, CategoryId = 5, CreatedDate = DateTime.Now }
            );
        }
    }
}
