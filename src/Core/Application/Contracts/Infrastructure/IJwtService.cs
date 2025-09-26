using Domain.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Infrastructure
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user, IList<string> roles);
        RefreshToken GenerateRefreshToken(string ipAddress);
        int AccessTokenExpirationMinutes { get; }
    }
}
