using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence.Auth;
using Application.DTOs.AuthDtos;
using Application.DTOs.AuthDtos.Validator;
using Application.Features.Auth.Requests.Commands;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Handlers.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, ReturnDataDto>
    {
        public readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepo;
        private readonly IRefreshTokenRepository _refreshRepo;
        private readonly IMapper _mapper;
        public LoginUserCommandHandler(IJwtService jwtService, IUserRepository userRepo, IRefreshTokenRepository refreshRepo, IMapper mapper)
        {
            _jwtService=jwtService;
            _userRepo=userRepo;
            _refreshRepo=refreshRepo;
            _mapper=mapper;
        }
        public async Task<ReturnDataDto> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var validator = new LoginUserDtoValidator();
            var validationResult = await validator.ValidateAsync(request.loginUserDto);
            if (!validationResult.IsValid)
            {
                throw new Exception(string.Join(Environment.NewLine, validationResult.Errors.Select(q => q.ErrorMessage)));
            }
            var user = await _userRepo.FindByEmailAsync(request.loginUserDto.Email)??throw new Exception("Invalid email or password");
            if(!await _userRepo.CheckPasswordAsync(user, request.loginUserDto.Password))
            {
                throw new Exception("Invalid password");
            }
            var roles=await _userRepo.GetRolesAsync(user);
            var accessToken= _jwtService.GenerateAccessToken(user, roles);
            var refreshToken= _jwtService.GenerateRefreshToken(request.ipAddress);
            refreshToken.UserId=user.Id;
            await _refreshRepo.AddAsync(refreshToken);
            await _refreshRepo.SaveChangesAsync();
            
             var userData =_mapper.Map<UserDto>(user);
           
            return new ReturnDataDto(
                accessToken,
                refreshToken.Token,
                DateTime.UtcNow.AddMinutes(_jwtService.AccessTokenExpirationMinutes),
                userData
                );  
        }
    }
}
