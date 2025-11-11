using Internship_Task.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Task.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string id);
        Task<IdentityResult> RegisterAsync(User user , string password);
        Task<User> LoginAsync(string username, string password);
    }
}
