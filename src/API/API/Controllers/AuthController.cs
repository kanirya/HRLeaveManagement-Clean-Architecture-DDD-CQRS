using Application.DTOs.AuthDtos;
using Application.Features.Auth.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Identity.Client;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [OutputCache]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            _mediator=mediator;
            _logger=logger;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginDto,CancellationToken ct)
        {
            try
            {

                _logger.LogInformation("Login endpoint is hit with userName: {username} with pass {pass}",loginDto.Email,loginDto.Password);
                var Request = new LoginUserCommandRequest
                {
                    ipAddress=HttpContext.Connection.RemoteIpAddress.ToString(),
                    loginUserDto=loginDto
                };
                var response = await _mediator.Send(Request);
                return Ok(response);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto, CancellationToken ct)
        {
            try {
                var Request = new RegisterUserCommandRequest
                { 
                    ipAddress=HttpContext.Connection.RemoteIpAddress?.ToString(),RegisterUserDto=registerDto };
                var reponse=await _mediator.Send(Request,ct);
                if (reponse.Success) return Ok(reponse);
                else return BadRequest(reponse);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
