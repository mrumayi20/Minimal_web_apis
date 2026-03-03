using Microsoft.EntityFrameworkCore;
using MinimalApi.Models.Entities;

namespace MinimalApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        // This creates the "Products" table in SQL Server
        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }
    }
}

