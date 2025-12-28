using Application.Contracts.Persistence.Auth;
using Domain.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Auth
{
    public class UserRepository : IUserRepository
    {
        public Task AddToRoleAsync(User user, string role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckPasswordAsync(User user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<(bool Succeeded, string[] Errors)> CreateAsync(User user, string password)
        {
            throw new NotImplementedException();
        }

        public Task EnsureRoleExistsAsync(string role)
        {
            throw new NotImplementedException();
        }

        public Task<User?> FindByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
