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
    }
}
