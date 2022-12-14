using EntityFrameworkCore.EncryptColumn.Attribute;
using System.ComponentModel.DataAnnotations;

namespace YellowCarrotDbApp.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; } = null!;
        [EncryptColumn]
        public string Password { get; set; } = null!;
    }
}
