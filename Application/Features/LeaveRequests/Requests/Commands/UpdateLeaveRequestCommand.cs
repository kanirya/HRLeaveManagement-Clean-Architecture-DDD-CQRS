using Application.DTOs.LeaveRequest;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LeaveRequests.Requests.Commands
{
    public class UpdateLeaveRequestCommand:IRequest<Unit>
    {
        public int Id { get; set; }
        public LeaveRequestDto leaveRequestDto { get; set; }
        public ChangeLeaveRequestApprovalDto changeLeaveRequestDto { get; set; }
    }
}
