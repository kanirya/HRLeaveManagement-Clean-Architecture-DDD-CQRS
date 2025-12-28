using Application.DTOs.AuthDtos;
using Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Requests.Commands
{
    public class RegisterUserCommandRequest:IRequest<RegisterUserResponses>
    {
        public RegisterUserDto RegisterUserDto { get; set; }
        public string ipAddress { get; set; }
    }
}
