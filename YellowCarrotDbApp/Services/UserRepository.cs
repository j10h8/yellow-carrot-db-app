using System.Linq;
using YellowCarrotDbApp.Data;

namespace YellowCarrotDbApp.Services
{
    public class UserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public bool IsRegistered(string userName, string password)
        {
            if (_context.Users.Any(u => u.Username == userName && u.Password == password))
            {
                return true;
            }

            return false;
        }
    }
}
