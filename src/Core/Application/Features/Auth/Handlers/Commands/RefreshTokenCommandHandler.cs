using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence.Auth;
using Application.DTOs.AuthDtos;
using Application.Features.Auth.Requests.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Handlers.Commands
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommandRequest, TokenDto>
    {
        public readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepo;
        private readonly IRefreshTokenRepository _refreshRepo;
        public RefreshTokenCommandHandler(IJwtService jwtService, IUserRepository userRepo, IRefreshTokenRepository refreshRepo)
        {
            _jwtService=jwtService;
            _userRepo=userRepo;
            _refreshRepo=refreshRepo;
        }
        public async Task<TokenDto> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshRepo.GetByTokenAsync(request.RefreshToken) ?? throw new Exception("Invalid refresh token");
            if (!refreshToken.IsActive) throw new Exception("Invalid refresh token");
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = request.IpAddress;
            refreshToken.IsRevoked=true;
            var newRefreshToken = _jwtService.GenerateRefreshToken(request.IpAddress);
            newRefreshToken.UserId = refreshToken.UserId;
            await _refreshRepo.AddAsync(newRefreshToken);
            await _refreshRepo.SaveChangesAsync();
            var user = await _userRepo.GetByIdAsync(refreshToken.UserId) ?? throw new Exception("User not found");
            var roles = await _userRepo.GetRolesAsync(user);
            var accessToken = _jwtService.GenerateAccessToken(user, roles);
            return new TokenDto(
                accessToken,
                newRefreshToken.Token,
                DateTime.UtcNow.AddMinutes(_jwtService.AccessTokenExpirationMinutes)
                );
        }
    }
}
