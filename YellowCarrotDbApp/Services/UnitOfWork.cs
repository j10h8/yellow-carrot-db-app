using YellowCarrotDbApp.Data;

namespace YellowCarrotDbApp.Services
{
    public class UnitOfWork
    {
        private readonly AppDbContext _context;

        private IngredientRepository _ingredientRepository;
        private RecipeRepository _recipeRepository;
        private TagRepository _tagRepository;
        private AppUserRepository _appUserRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IngredientRepository IngredientRepository
        {
            get
            {
                if (_ingredientRepository == null)
                {
                    _ingredientRepository = new(_context);
                }

                return _ingredientRepository;
            }
        }

        public RecipeRepository RecipeRepository
        {
            get
            {
                if (_recipeRepository == null)
                {
                    _recipeRepository = new(_context);
                }

                return _recipeRepository;
            }
        }

        public TagRepository TagRepository
        {
            get
            {
                if (_tagRepository == null)
                {
                    _tagRepository = new(_context);
                }

                return _tagRepository;
            }
        }

        public AppUserRepository AppUserRepository
        {
            get
            {
                if (_appUserRepository == null)
                {
                    _appUserRepository = new(_context);
                }

                return _appUserRepository;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
