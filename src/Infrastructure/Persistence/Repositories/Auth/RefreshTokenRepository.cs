using Application.Contracts.Persistence.Auth;
using Domain.Auth;
using Persistence.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Auth
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
       private readonly ApplicationDbContext _context;
        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RefreshToken token)
        {
           await _context.RefreshTokens.AddAsync(token);
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens.FindAsync(token);
        }


        public  async Task SaveChangesAsync()
        {
           await  _context.SaveChangesAsync();
        }
    }
}
