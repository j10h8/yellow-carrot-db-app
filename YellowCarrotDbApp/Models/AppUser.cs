using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YellowCarrotDbApp.Models
{
    public class AppUser
    {
        [Key]
        public string Username { get; set; } = null!;
        public List<Recipe> Recipies { get; set; } = new();
    }
}
