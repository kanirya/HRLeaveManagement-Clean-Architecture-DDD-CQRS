using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Requests.Commands
{
    public class TokenRevokeCommandRequest:IRequest<Unit>
    {
       public string refreshToken { get; set; }
        public string IpAddress { get; set; }
    }
}
