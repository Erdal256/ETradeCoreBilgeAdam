using AppCore.DataAccess.Configs;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework.Contexts
{
    public class ETradeContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ConnectionConfig.ConnectionString = "server=DESKTOP-VQ9N1P0\\SEKHARSQL; " +
                "database=ETradeCoreDB;user id=sa;password=Erdal256;multipleactiveresultsets=true;";
            optionsBuilder.UseSqlServer(ConnectionConfig.ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(category => category.Products)
                .WithOne(Product => Product.Category)
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
