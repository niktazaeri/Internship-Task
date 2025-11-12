using Internship_Task.Domain.Entities;
using Internship_Task.Domain.Interfaces;
using Internship_Task.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Task.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _db;

        public RefreshTokenRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<RefreshToken> CreateAsync(string userId)
        {
            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = Guid.NewGuid().ToString(),
                Expiration = DateTime.Now.AddDays(6)
            };
            await _db.RefreshTokens.AddAsync(refreshToken);
            await _db.SaveChangesAsync();
            return refreshToken;
        }

        public async Task DeleteAsync(string refreshToken)
        {
            var token = await _db.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken);
            _db.RefreshTokens.Remove(token);
            await _db.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetAsync(string refreshToken)
        {
            var token = await _db.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken);
            return token;
        }
    }
}
