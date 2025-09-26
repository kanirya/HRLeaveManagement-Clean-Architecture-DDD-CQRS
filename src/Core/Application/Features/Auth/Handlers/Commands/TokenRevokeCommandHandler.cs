using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence.Auth;
using Application.Features.Auth.Requests.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Handlers.Commands
{
    public class TokenRevokeCommandHandler : IRequestHandler<TokenRevokeCommandRequest, Unit>
    {
        public readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepo;
        private readonly IRefreshTokenRepository _refreshRepo;
        public TokenRevokeCommandHandler(IJwtService jwtService, IUserRepository userRepo, IRefreshTokenRepository refreshRepo)
        {
            _jwtService=jwtService;
            _userRepo=userRepo;
            _refreshRepo=refreshRepo;
        }
        public async Task<Unit> Handle(TokenRevokeCommandRequest request, CancellationToken cancellationToken)
        {
            var rt=await _refreshRepo.GetByTokenAsync(request.refreshToken)??throw new Exception("Invalid refresh token");
            if(!rt.IsActive) throw new Exception("Invalid refresh token");
            rt.Revoked=DateTime.UtcNow;
            rt.RevokedByIp=request.IpAddress;
            rt.IsRevoked=true;
            await _refreshRepo.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
