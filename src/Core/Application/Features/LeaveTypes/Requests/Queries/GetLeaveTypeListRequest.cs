using Application.DTOs.LeaveType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LeaveTypes.Requests.Queries
{
    public class GetLeaveTypeListRequest:IRequest<List<LeaveTypeDto>>
    {

    }
}
