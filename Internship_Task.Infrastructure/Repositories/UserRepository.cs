using Internship_Task.Domain.Entities;
using Internship_Task.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Task.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<User> GetUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                var checkPassword = await _userManager.CheckPasswordAsync(user, password);
                if (!checkPassword)
                    return null;
                else
                    return user;
            }
            else
                return null;
        }

        public async Task<IdentityResult> RegisterAsync(User user , string password)
        {
            return await _userManager.CreateAsync(user,password);
        }
    }
}
