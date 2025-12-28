using Application.Exceptions;
using Application.Features.LeaveRequests.Requests.Commands;
using Application.Persistence.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LeaveRequests.Handlers.Commands
{
    public class DeleteLeaveRequestCommandHandler:IRequestHandler<DeleteLeaveRequestCommand>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository=leaveRequestRepository;
            _mapper=mapper;
        }

        public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
           var leaveRequest=await _leaveRequestRepository.Get(request.Id);
            if (leaveRequest==null)
            {
                throw new NotFoundException(nameof(leaveRequest),request.Id);
            }
            await _leaveRequestRepository.Delete(leaveRequest);
            return Unit.Value;
        }
    }
}
