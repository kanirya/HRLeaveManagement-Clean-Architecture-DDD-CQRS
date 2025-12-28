using Application.DTOs.AuthDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Requests.Commands
{
    public class RefreshTokenCommandRequest:IRequest<TokenDto>
    {
        public string RefreshToken { get; set; }
        public string IpAddress { get; set; }
    }
}
