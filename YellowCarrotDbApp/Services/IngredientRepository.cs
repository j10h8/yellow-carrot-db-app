using YellowCarrotDbApp.Data;

namespace YellowCarrotDbApp.Services
{
    public class IngredientRepository
    {
        private readonly AppDbContext _context;

        public IngredientRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
