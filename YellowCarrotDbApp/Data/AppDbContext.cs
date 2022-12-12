using Microsoft.EntityFrameworkCore;

namespace YellowCarrotDbApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
