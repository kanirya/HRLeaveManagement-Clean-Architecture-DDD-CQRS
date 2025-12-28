using Application.DTOs.LeaveRequest;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LeaveRequests.Requests.Queries
{
    public class GetLeaveRequestDetailRequest:IRequest<LeaveRequestDto>
    {
        public int id { get; set; }
       
    }
}
