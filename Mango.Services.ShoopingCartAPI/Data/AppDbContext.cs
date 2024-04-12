using Mango.Services.ShoopingCartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoopingCartAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }

        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }

    }
}
