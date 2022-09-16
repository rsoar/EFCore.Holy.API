using EFCore.Holy.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Holy.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Church> Churchs { get; set; }
        public DbSet<Manager> Managers { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base (options) { }
    }
}
