using System.Collections.Generic;

namespace YellowCarrotDbApp.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Description { get; set; } = null!;
        public List<Recipe> Recipies { get; set; } = new();
    }
}
