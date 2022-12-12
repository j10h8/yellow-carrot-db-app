using System.Collections.Generic;

namespace YellowCarrotDbApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public List<Recipe> Recipies { get; set; } = new();
    }
}
