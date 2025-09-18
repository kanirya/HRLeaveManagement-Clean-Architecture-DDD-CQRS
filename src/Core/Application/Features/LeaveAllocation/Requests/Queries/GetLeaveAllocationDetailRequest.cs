using Application.DTOs.LeaveAllocation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LeaveAllocation.Requests.Queries
{
    public class GetLeaveAllocationDetailRequest:IRequest<LeaveAllocationDto>
    {
        public int Id { get; set; }
    }
    
}
