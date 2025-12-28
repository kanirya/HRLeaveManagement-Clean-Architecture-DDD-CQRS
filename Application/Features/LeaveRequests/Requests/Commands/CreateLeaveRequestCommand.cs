using Application.DTOs.LeaveRequest;
using Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LeaveRequests.Requests.Commands
{
    public class CreateLeaveRequestCommand:IRequest<BaseCommandResponse>

    {
        public CreateLeaveRequestDto CreateLeaveRequestDto { get; set; }
    }
}
