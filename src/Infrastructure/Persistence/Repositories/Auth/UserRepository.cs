
using Application.Contracts.Persistence.Auth;
using AutoMapper;
using Domain.Auth;
using Microsoft.AspNetCore.Identity;
using Persistence.data;
using Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Auth
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public UserRepository(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            ApplicationDbContext db,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _mapper=mapper;
        }
        public async Task<(bool Succeeded, string[] Errors)> CreateAsync(User user, string password)
        {
            var appUser = new ApplicationUser
            {
                UserName = user.Email,
                Email = user.Email,
                Name = user.Name
            };
            var CreatedUser=await _userManager.CreateAsync(appUser, password)
                .ContinueWith(t =>
                {
                    if (t.Result.Succeeded)
                    {
                        return (true, Array.Empty<string>());
                    }
                    else
                    {
                        return (false, t.Result.Errors.Select(e => e.Description).ToArray());
                    }
                });
                   await   _db.SaveChangesAsync();
            var role=string.IsNullOrWhiteSpace(user.Role)?"User":user.Role;

            if (!await _roleManager.RoleExistsAsync(role)) await _roleManager.CreateAsync(new ApplicationRole { Name = role });

            await _userManager.AddToRoleAsync(appUser, role);

            return (true, Array.Empty<string>());

        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
           var appUser= await _userManager.FindByIdAsync(user.Id.ToString());
            if(appUser==null) return false;
            return await _userManager.CheckPasswordAsync(appUser, password);
        }

        public async Task AddToRoleAsync(User user, string role)
        {
            
            var appUser=await _userManager.FindByIdAsync(user.Id.ToString());
            if(appUser==null) throw new Exception($"User with id {user.Id} not found");
            if(!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new ApplicationRole { Name = role });
            }
            await _userManager.AddToRoleAsync(appUser, role);
        }

       

      
        public async Task EnsureRoleExistsAsync(string role)
        {
            if(! await _roleManager.RoleExistsAsync(role)) 
                await _roleManager.CreateAsync(new ApplicationRole { Name=role});
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            var user= await _userManager.FindByEmailAsync(email);
            if(user==null) return null;
            return MapToDomain(user);

        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            var user=await _userManager.FindByIdAsync(id.ToString());
            if(user==null) return null;
            return MapToDomain(user);
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            var app = await _userManager.FindByIdAsync(user.Id.ToString());
            if (app == null) return new List<string>();
            return await _userManager.GetRolesAsync(app);
        }
        private User MapToDomain(ApplicationUser app) => new User(app.Id, app.Name ?? string.Empty, app.Email ?? string.Empty, GetPrimaryRole(app).Result ?? "User");
        private async Task<string?> GetPrimaryRole(ApplicationUser app)
        {
            var roles = await _userManager.GetRolesAsync(app);
            return roles.FirstOrDefault();
        }
    }


}
