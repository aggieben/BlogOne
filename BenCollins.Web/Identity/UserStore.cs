using BenCollins.Web.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BenCollins.Web.Identity
{
    public class UserStore : IUserStore<User>
    {
        public System.Threading.Tasks.Task CreateAsync(User user)
        {
            return Task.FromResult<object>(null);
        }

        public System.Threading.Tasks.Task DeleteAsync(User user)
        {
            return Task.FromResult<object>(null);
        }

        public System.Threading.Tasks.Task<User> FindByIdAsync(string userId)
        {
            if (userId == "admin")
                return Task.FromResult<User>(new User("admin", "admin"));

            return Task.FromResult<User>(null);
        }

        public System.Threading.Tasks.Task<User> FindByNameAsync(string userName)
        {
            if (userName == "admin")
                return Task.FromResult<User>(new User("admin", "admin"));

            return Task.FromResult<User>(null);
        }

        public System.Threading.Tasks.Task UpdateAsync(User user)
        {
            return Task.FromResult<object>(null);
        }

        public void Dispose()
        {
            
        }
    }
}