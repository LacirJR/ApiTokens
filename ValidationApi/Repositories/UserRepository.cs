using ValidationApi.Models;
using Microsoft.Extensions.Configuration;
using ValidationApi.Helpers;

namespace ValidationApi.Repositories
{

    public static class UserRepository
    {
        public static User Get()
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Username = Util.ObterConfiguracao("User"), Password = Util.ObterConfiguracao("Password"), Role = "MASTER" });
            
            return users.First();
        }
    }
}

