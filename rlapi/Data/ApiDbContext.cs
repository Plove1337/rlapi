using Microsoft.EntityFrameworkCore;
using rlapi.Models;

namespace rlapi.Data
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=SQL6030.site4now.net;Initial Catalog=db_ab1786_realestatedb;User Id=db_ab1786_realestatedb_admin;Password=1Kolposter3");
        }
    }
}
