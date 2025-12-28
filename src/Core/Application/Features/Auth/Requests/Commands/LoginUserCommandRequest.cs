using Application.DTOs.AuthDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Requests.Commands
{
    public class LoginUserCommandRequest:IRequest<ReturnDataDto>
    {
        public LoginUserDto loginUserDto { get; set; }
        public string ipAddress { get; set; }
    }
}
