using Microsoft.EntityFrameworkCore;
using MInimalApi.Core.Entities;

namespace MInimalApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product> Products => Set<Product>();
    }
}
