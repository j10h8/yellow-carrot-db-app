using YellowCarrotDbApp.Data;
using YellowCarrotDbApp.Models;

namespace YellowCarrotDbApp.Services
{
    public class AppUserRepository
    {
        private readonly AppDbContext _appContext;

        public AppUserRepository(AppDbContext context)
        {
            _appContext = context;
        }

        // Creates and saves an AppUser to YellowCarrotDb
        public void AddUser(string username)
        {
            AppUser appUser = new()
            {
                Username = username
            };

            _appContext.AppUsers.Add(appUser);
        }
    }
}
