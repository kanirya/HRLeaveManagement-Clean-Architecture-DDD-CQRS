using Application.Features.LeaveRequests.Requests.Commands;
using Application.Persistence.Contracts;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LeaveRequests.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,IMapper mapper)
        {
            _leaveRequestRepository=leaveRequestRepository;
            _mapper=mapper;
        }
        public  async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
          var leaveRequest=_mapper.Map<LeaveRequest>(request.leaveRequest);
            leaveRequest=await _leaveRequestRepository.Add(leaveRequest);
            if (leaveRequest==null)
            {
                throw new Exception("Error creating leave request");
            }
                return leaveRequest.Id;
        }
    }
}
