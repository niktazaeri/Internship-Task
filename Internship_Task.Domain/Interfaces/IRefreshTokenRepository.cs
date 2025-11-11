using Internship_Task.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Task.Domain.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetAsync(string refreshToken);
        Task DeleteAsync(string refreshToken);
        Task<RefreshToken> CreateAsync(string userId);
    }
}
