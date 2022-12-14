using System.Collections.Generic;
using System.Linq;
using YellowCarrotDbApp.Data;
using YellowCarrotDbApp.Models;

namespace YellowCarrotDbApp.Services
{
    public class RecipeRepository
    {
        private readonly AppDbContext _context;

        public RecipeRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Recipe> GetRecipies()
        {
            return _context.Recipes.OrderBy(n => n.Name).ToList();
        }

        public void DeleteRecipe(string name)
        {
            _context.Recipes.Remove(_context.Recipes.First(r => r.Name == name));
        }
    }
}
