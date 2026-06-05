using bilet3.Models;
using Microsoft.EntityFrameworkCore;

namespace bilet3.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
    }
}
