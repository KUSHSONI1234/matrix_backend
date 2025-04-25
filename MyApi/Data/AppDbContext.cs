using Microsoft.EntityFrameworkCore;

namespace MyApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<FormData> FormData { get; set; }
    }
}
