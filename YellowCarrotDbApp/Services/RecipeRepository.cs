﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using YellowCarrotDbApp.Data;
using YellowCarrotDbApp.Models;

namespace YellowCarrotDbApp.Services
{
    public class RecipeRepository
    {
        private readonly AppDbContext _appContext;

        public RecipeRepository(AppDbContext context)
        {
            _appContext = context;
        }

        public List<Recipe> GetRecipies()
        {
            return _appContext.Recipes.Include(u => u.User).OrderBy(n => n.Name).ToList();
        }

        public void DeleteRecipe(string name)
        {
            _appContext.Recipes.Remove(_appContext.Recipes.First(r => r.Name == name));
        }

        public bool IsUsed(string recipeName)
        {
            if (_appContext.Recipes.Any(r => r.Name == recipeName))
            {
                return true;
            }

            return false;
        }

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
    }
}
