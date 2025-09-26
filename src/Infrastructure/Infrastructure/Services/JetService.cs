using Application.Contracts.Infrastructure;
using Application.Models;
using Domain.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class JetService : IJwtService
    {
        private readonly JwtSettings _settings;

        public JetService(IOptions<JwtSettings> settings)
        {
            _settings=settings.Value;
        }

      

        public int AccessTokenExpirationMinutes => _settings.AccessTokenExpirationMinutes;


        public string GenerateAccessToken(User user, IList<string> roles)
        {
            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
            var creds=new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        }

        public RefreshToken GenerateRefreshToken(string ipAddress)
        {
            throw new NotImplementedException();
        }
    }
}
