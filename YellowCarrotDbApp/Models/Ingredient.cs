namespace YellowCarrotDbApp.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string Name { get; set; } = null!;
        public string Quantity { get; set; } = null!;
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; } = new();
    }
}
