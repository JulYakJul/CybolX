using Microsoft.EntityFrameworkCore;
using CybontrolX.DBModels;

namespace CybontrolX.DataBase
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Product>().ToTable("Products");
        //}
    }
}
