using BlogOne.Web.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BlogOne.Web.Identity
{
    public class UserStore : IUserStore<User>, IUserLoginStore<User>
    {
        #region IUserStore
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
        #endregion

        #region IUserLoginStore
        public Task AddLoginAsync(User user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindAsync(UserLoginInfo login)
        {
            return new User("0", "admin");
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveLoginAsync(User user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}