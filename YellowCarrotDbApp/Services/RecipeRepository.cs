using YellowCarrotDbApp.Data;

namespace YellowCarrotDbApp.Services
{
    public class RecipeRepository
    {
        private readonly AppDbContext _context;

        public RecipeRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
