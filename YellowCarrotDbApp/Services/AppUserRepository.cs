using YellowCarrotDbApp.Data;
using YellowCarrotDbApp.Models;

namespace YellowCarrotDbApp.Services
{
    public class AppUserRepository
    {
        private readonly AppDbContext _context;

        public AppUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddUser(string username)
        {
            AppUser appUser = new()
            {
                Username = username
            };

            _context.AppUsers.Add(appUser);
        }
    }
}
