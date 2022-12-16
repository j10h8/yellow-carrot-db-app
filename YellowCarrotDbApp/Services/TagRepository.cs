using YellowCarrotDbApp.Data;

namespace YellowCarrotDbApp.Services
{
    public class TagRepository
    {
        private readonly AppDbContext _context;

        // Class constructor. Takes AppDbContext argument and sets field variable.
        public TagRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
