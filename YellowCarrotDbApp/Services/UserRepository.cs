using System.Linq;
using YellowCarrotDbApp.Data;
using YellowCarrotDbApp.Models;

namespace YellowCarrotDbApp.Services
{
    public class UserRepository
    {
        private readonly UserDbContext _userContext;

        public UserRepository(UserDbContext userContext)
        {
            _userContext = userContext;
        }

        // Checks if provided username and password exist in YellowCarrotUserDb 
        public bool IsRegistered(string username, string password)
        {
            if (_userContext.Users.Any(u => u.Username == username && u.Password == password))
            {
                return true;
            }

            return false;
        }

        // Creates and saves a User in YellowCarrotUserDb if provided username does not exist in YellowCarrotUserDb
        public bool RegisterNewUser(string username, string password)
        {
            if (_userContext.Users.Any(u => u.Username == username))
            {
                return false;
            }
            else
            {
                User user = new()
                {
                    Username = username,
                    Password = password
                };

                _userContext.Users.Add(user);

                return true;
            }
        }

        // Saves all changes made to YellowCarrotUserDb
        public void SaveChanges()
        {
            _userContext.SaveChanges();
        }
    }
}
