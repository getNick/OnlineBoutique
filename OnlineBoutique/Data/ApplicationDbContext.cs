using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketCore.Classes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineBoutique.Models;
using OnlineBoutique.Models.Classes;

namespace OnlineBoutique.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariation> ProductVariations { get; set; }
        public DbSet<ColorVariation> ColorVariation { get; set; }
        public DbSet<SizeVariation> SizeVariation { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<FilePath> Images { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().HasOne(x => x.UserSizes);
            builder.Entity<ApplicationUser>().HasMany(x => x.Orders);
            builder.Entity<Product>().HasOne(x => x.Category);
            builder.Entity<Product>().HasMany(x => x.ProductVariations).WithOne(x => x.BaseProduct);
            builder.Entity<ColorVariation>()
                .HasMany(c => c.ImageURLs)
                .WithOne(e => e.ColorVariation);
            builder.Entity<ProductVariation>().HasOne(x => x.ColorVariation);
            builder.Entity<SizeVariation>()
                .HasMany(c => c.ListParams)
                .WithOne(e => e.SizeVariation);
            builder.Entity<ProductVariation>().HasMany(x => x.SizeVariation).WithOne(x => x.ProductVariation);
            builder.Entity<Order>()
                .HasMany(s => s.OrderItems).WithOne(x=>x.Order);
            builder.Entity<OrderItem>().HasOne(x => x.ProductVariation);
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
