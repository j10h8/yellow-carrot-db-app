using System.Collections.Generic;

namespace YellowCarrotDbApp.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; } = null!;
        public User? User { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();
    }
}
