using Application.Contracts.Infrastructure;
using Application.Models;
using Domain.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _settings;

        public JwtService(IOptions<JwtSettings> settings)
        {
            _settings=settings.Value;
        }

      

        public int AccessTokenExpirationMinutes => _settings.AccessTokenExpirationMinutes;


        public string GenerateAccessToken(User user, IList<string> roles)
        {
            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
            var creds=new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name)
                };
            foreach(var r in roles) claims.Add(new Claim(ClaimTypes.Role, r));
            var now = DateTime.UtcNow;
            var token=new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_settings.AccessTokenExpirationMinutes),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(string ipAddress)
        {
            var randomBytes=new byte[64];
            using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            Console.WriteLine($"RefreshTokenExpirationDays: {_settings.RefreshTokenExpirationDays}");
            Console.WriteLine($"Expires: {DateTime.UtcNow.AddDays(_settings.RefreshTokenExpirationDays)}");

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddDays(_settings.RefreshTokenExpirationDays),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }
    }
}
