using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.EntityFrameworkCore;
using YellowCarrotDbApp.Models;

namespace YellowCarrotDbApp.Data
{
    public class UserDbContext : DbContext
    {
        private IEncryptionProvider _encryptionProvider;

        public UserDbContext()
        {
            _encryptionProvider = new GenerateEncryptionProvider("jdYw6172H6F5drsu267543gT");
        }

        public UserDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=YellowCarrotUserDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEncryption(_encryptionProvider);

            modelBuilder.Entity<User>().HasData(new User()
            {
                Username = "user",
                Password = "password"
            });
            // TODO: implement delete behaviour if found necessary 
        }
    }
}
