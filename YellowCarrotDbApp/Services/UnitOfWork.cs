using YellowCarrotDbApp.Data;

namespace YellowCarrotDbApp.Services
{
    public class UnitOfWork
    {
        private readonly AppDbContext _appContext;

        private IngredientRepository _ingredientRepository;
        private RecipeRepository _recipeRepository;
        private TagRepository _tagRepository;
        private AppUserRepository _appUserRepository;

        public UnitOfWork(AppDbContext context)
        {
            _appContext = context;
        }

        public IngredientRepository IngredientRepository
        {
            get
            {
                if (_ingredientRepository == null)
                {
                    _ingredientRepository = new(_appContext);
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
                    _recipeRepository = new(_appContext);
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
                    _tagRepository = new(_appContext);
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
                    _appUserRepository = new(_appContext);
                }

                return _appUserRepository;
            }
        }

        // Saves all changes made to YellowCarrotDb
        public void SaveChanges()
        {
            _appContext.SaveChanges();
        }
    }
}
