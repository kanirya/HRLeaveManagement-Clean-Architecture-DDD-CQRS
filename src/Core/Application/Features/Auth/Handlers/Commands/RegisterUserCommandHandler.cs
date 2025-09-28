using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence.Auth;
using Application.DTOs.AuthDtos;
using Application.DTOs.AuthDtos.Validator;
using Application.Features.Auth.Requests.Commands;
using Application.Responses;
using Domain.Auth;
using MediatR;
using Polly;
using Polly.Retry;
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
        private readonly AsyncRetryPolicy _retryPolicy;
        public RegisterUserCommandHandler(IJwtService jwtService, IUserRepository userRepo, IRefreshTokenRepository refreshRepo)
        {
            _jwtService=jwtService;
            _userRepo=userRepo;
            _refreshRepo=refreshRepo;
            _retryPolicy = Policy
            .Handle<Exception>() // You can narrow this down (e.g., DbUpdateException, SqlException)
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: attempt => TimeSpan.FromMilliseconds(200 * attempt), // 200ms, 400ms, 600ms
                onRetry: (exception, timespan, retryCount, context) =>
                {
                    // Add logging here (e.g., Serilog, ILogger)
                    Console.WriteLine($"Retry {retryCount} after {timespan}. Exception: {exception.Message}");
                });
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

          await  _retryPolicy.ExecuteAsync(async () =>
            {

                var roles = await _userRepo.GetRolesAsync(domainUser);
                var accessToken = _jwtService.GenerateAccessToken(domainUser, roles);
                var refreshToken = _jwtService.GenerateRefreshToken(request.ipAddress);
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
                    refreshToken.Expires,
                    userData
                    );
                response.Message="User Created Successfully";
                response.Success=true;
                response.Data=returnData;

            });
          
            return response;
        }
    }
}
