using YellowCarrotDbApp.Data;

namespace YellowCarrotDbApp.Services
{
    public class UnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly UserDbContext _userContext;

        private IngredientRepository _ingredientRepository;
        private RecipeRepository _recipeRepository;
        private TagRepository _tagRepository;
        private UserRepository _userRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public UnitOfWork(AppDbContext context, UserDbContext userContext)
        {
            _context = context;
            _userContext = userContext;
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

        public UserRepository UserRepository
        {
            get
            {
                // TODO: Possibly change/add conditionals 
                if (_userRepository == null && _userContext != null)
                {
                    _userRepository = new(_userContext);
                }

                return _userRepository;
            }
        }
    }
}
