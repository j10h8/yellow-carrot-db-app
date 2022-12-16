using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using YellowCarrotDbApp.Data;
using YellowCarrotDbApp.Models;

namespace YellowCarrotDbApp.Services
{
    public class RecipeRepository
    {
        private readonly AppDbContext _appContext;

        // Class constructor. Takes AppDbContext argument and sets field variable.
        public RecipeRepository(AppDbContext context)
        {
            _appContext = context;
        }

        // Gets and returns a recipe from YellowCarrotDb 
        public Recipe GetRecipe(string recipeName)
        {
            return _appContext.Recipes.Include(u => u.User).Include(i => i.Ingredients).Include(t => t.Tags).First(r => r.Name == recipeName);
        }

        // Gets and returns a list of all Recipes in YellowCarrotDb
        public List<Recipe> GetRecipes()
        {
            return _appContext.Recipes.Include(u => u.User).ToList();
        }

        // UpdateRecipe updates recipe in YellowCarrotDb
        public void UpdateRecipe(string oldRecipeName, string newRecipeName, string username, List<Ingredient> ingredients, List<Tag> tags)
        {
            // GetRecipe gets and returns a recipe from YellowCarrotDb 
            Recipe recipe = GetRecipe(oldRecipeName);
            recipe.Name = newRecipeName;
            recipe.Username = username;
            recipe.Ingredients = ingredients;
            recipe.Tags = tags;

            _appContext.Recipes.Update(recipe);
        }

        // Deletes recipe from YellowCarrotDb
        public void DeleteRecipe(string recipeName)
        {
            DeleteTags(recipeName);

            _appContext.Recipes.Remove(_appContext.Recipes.First(r => r.Name == recipeName));
        }

        // Returns true if recipe name exists in YellowCarrotDb
        public bool IsUsed(string recipeName)
        {
            if (_appContext.Recipes.Any(r => r.Name == recipeName))
            {
                return true;
            }

            return false;
        }

        // Adds recipe to YellowCarrotDb 
        public void AddRecipe(string recipeName, string username, List<Ingredient> ingredients, List<Tag> tags)
        {
            Recipe recipe = new()
            {
                Name = recipeName,
                Username = username,
                Ingredients = ingredients,
                Tags = tags
            };

            _appContext.Recipes.Add(recipe);
        }
        // Deletes recipes tags in YellowCarrotDb
        public void DeleteTags(string recipeName)
        {
            foreach (Tag tag in _appContext.Recipes.Include(t => t.Tags).First(r => r.Name == recipeName).Tags)
            {
                _appContext.Tags.Remove(tag);
            }
        }
    }
}
