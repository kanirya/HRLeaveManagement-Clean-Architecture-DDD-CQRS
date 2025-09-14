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
    public class UpdateLeaveRequestCommandHandler:IRequestHandler<UpdateLeaveRequestCommand,Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository=leaveRequestRepository;
            _mapper=mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.Get(request.Id);
            if (leaveRequest==null)
            {
                throw new Exception($"Leave request with id {request.Id} not found.");
            }

            if (request.leaveRequestDto!=null)
            {
             
                _mapper.Map(request.leaveRequestDto, leaveRequest);
                await _leaveRequestRepository.Update(leaveRequest);

            }else if(request.changeLeaveRequestDto!=null)
            {
                leaveRequest.Approved = request.changeLeaveRequestDto.Approved;
                leaveRequest.DateActioned = DateTime.Now;
                await _leaveRequestRepository.Update(leaveRequest);
            }
            else
            {
                throw new Exception("No data provided to update the leave request.");
            }

            return Unit.Value;
        }
    }
}
