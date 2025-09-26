using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence.Auth;
using Application.DTOs.AuthDtos;
using Application.DTOs.AuthDtos.Validator;
using Application.Features.Auth.Requests.Commands;
using Application.Responses;
using Domain.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Handlers.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, RegisterUserResponses>
    {
        public readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepo;
        private readonly IRefreshTokenRepository _refreshRepo;
        public RegisterUserCommandHandler(IJwtService jwtService, IUserRepository userRepo, IRefreshTokenRepository refreshRepo)
        {
            _jwtService=jwtService;
            _userRepo=userRepo;
            _refreshRepo=refreshRepo;
        }

        public async Task<RegisterUserResponses> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            var validator = new RegisterUserDtoValidator();
            var response = new RegisterUserResponses();
            var validationResult = await validator.ValidateAsync(request.RegisterUserDto);
            if (!validationResult.IsValid)
            {
                response.Success=false;
                response.Message="Creation Failed";
                response.Errors=validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                return response;
            }

            var role =string.IsNullOrWhiteSpace(request.RegisterUserDto.Role)?"User":request.RegisterUserDto.Role;
            await _userRepo.EnsureRoleExistsAsync(role);
            var domainUser = new User(
               Guid.NewGuid(),
               request.RegisterUserDto.Name,
               request.RegisterUserDto.Email,
               role
                );
            var (succeeded, errors)=await _userRepo.CreateAsync(domainUser, request.RegisterUserDto.Password);
            if (!succeeded)
            {
                throw new Exception(string.Join(Environment.NewLine, errors));
            }
            var roles=await _userRepo.GetRolesAsync(domainUser);
            var accessToken= _jwtService.GenerateAccessToken(domainUser, roles);
            var refreshToken= _jwtService.GenerateRefreshToken(request.ipAddress);
            refreshToken.UserId=domainUser.Id;
            await _refreshRepo.AddAsync(refreshToken);

            await _refreshRepo.SaveChangesAsync();
             var userData = new UserDto(
                domainUser.Name,
                domainUser.Id,
                domainUser.Email,
                domainUser.Role,
                DateTime.UtcNow
                );
            var returnData = new ReturnDataDto(
                accessToken,
                refreshToken.Token, 
                DateTime.UtcNow.AddMinutes(_jwtService.AccessTokenExpirationMinutes),
                userData
                );
            response.Message="User Created Successfully";
            response.Success=true;
            response.Data=returnData;

            return response;
        }
    }
}
