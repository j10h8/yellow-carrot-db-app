using Microsoft.EntityFrameworkCore;
using YellowCarrotDbApp.Models;

namespace YellowCarrotDbApp.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext()
        {

        }

        public UserDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=YellowCarrotUsersDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: implement delete behaviour if found necessary 
        }
    }
}
