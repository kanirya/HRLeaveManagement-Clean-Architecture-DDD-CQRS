using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LeaveTypes.Requests.Commands
{
    public class UpdateLeaveTypeCommand:IRequest<int>
    {
        public int Id { get; set; }
        public LeaveTypeDto LeaveTypeDto { get; set; }
    }
}
