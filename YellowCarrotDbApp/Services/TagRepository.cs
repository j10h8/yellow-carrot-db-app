using YellowCarrotDbApp.Data;

namespace YellowCarrotDbApp.Services
{
    public class TagRepository
    {
        private readonly AppDbContext _context;

        public TagRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
