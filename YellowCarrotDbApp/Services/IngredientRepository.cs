using YellowCarrotDbApp.Data;

namespace YellowCarrotDbApp.Services
{
    public class IngredientRepository
    {
        private readonly AppDbContext _context;

        // Class constructor. Takes AppDbContext argument and sets field variable.
        public IngredientRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
