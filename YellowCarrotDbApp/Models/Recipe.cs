using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YellowCarrotDbApp.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; } = null!;
        [ForeignKey(nameof(User))]
        public string? Username { get; set; }
        public AppUser? User { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();
    }
}
