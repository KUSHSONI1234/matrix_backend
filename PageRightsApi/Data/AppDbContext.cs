using Microsoft.EntityFrameworkCore;
using PageRightsApi.Models;

namespace PageRightsApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<PageRight> PageRights { get; set; }
    }
}
