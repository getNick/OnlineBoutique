using MarketCore.Classes;
using Microsoft.EntityFrameworkCore;

namespace OnlineBoutique.Data
{
    public class BoutiqueContex : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariation> ProductVariations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Storehouse> Storehouses { get; set; }

        public BoutiqueContex(DbContextOptions<BoutiqueContex> options)
            : base(options)
        {
        }
    }
}
