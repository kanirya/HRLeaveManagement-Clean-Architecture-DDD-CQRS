using Application.DTOs.AuthDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Requests.Commands
{
    public class RegisterUserCommandRequest:IRequest<ReturnDataDto>
    {
        public RegisterUserDto RegisterUserDto { get; set; }
        public string ipAddress { get; set; }
    }
}
