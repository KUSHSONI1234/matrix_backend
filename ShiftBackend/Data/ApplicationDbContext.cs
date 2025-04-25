using Microsoft.EntityFrameworkCore;
using ShiftBackend.Models;

namespace ShiftBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ShiftModel> Shifts { get; set; }
    }
}
