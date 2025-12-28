using Application.DTOs.AuthDtos;
using Application.Features.Auth.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator=mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginDto,CancellationToken ct)
        {
            try
            {

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
