using ValidationApi.Models;
using Microsoft.Extensions.Configuration;
using ValidationApi.Helpers;

namespace ValidationApi.Repositories
{

    public static class UserRepository
    {
        public static User Get(string id, string secret)
        {
            var users = new List<User>();

            if (id != Util.ObterConfiguracao("User"))
                throw new InvalidOperationException("Usuario Invalido");

            if (secret != Util.ObterConfiguracao("Password"))
                throw new InvalidOperationException("Usuario Invalido");

            users.Add(new User { Id = 1, Username = Util.ObterConfiguracao("User"), Password = Util.ObterConfiguracao("Password"), Role = "MASTER" });
            
            return users.First();
        }
    }
}

