using Application.Contracts.Persistence.Auth;
using Application.DTOs.AuthDtos;
using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Auth
{
    public class UserDataRepository : IUserDataRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserDataRepository(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager=userManager;
            _httpContextAccessor=httpContextAccessor;
        }

        public async Task<UserDataDtos?> GetByIdAsync(Guid id)
        {
            var sub = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if(sub==null || sub!=id.ToString())
                throw new UnauthorizedAccessException("Please login first");
            if (sub == id.ToString())
            {
                // ✅ Same user – return user with extra "Admin" property
                var user = await _userManager.FindByIdAsync(sub);

                if (user == null) throw new NotFoundException(nameof(user),id);

                return new UserDataDtos(
                   user.Id.ToString(),
                    user.Email,
                    user.Name,
                    true
                );
            }
            else
            {
                // ✅ Different user – fetch by id and return without admin
                var user = await _userManager.FindByIdAsync(id.ToString());

                if (user == null) return null;

                return new UserDataDtos(
                   user.Id.ToString(),
                    user.Email,
                    user.Name,
                    false
                );
            }
        }
    }
}
